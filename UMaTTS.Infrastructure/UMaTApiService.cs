using System.Net.Http.Json;
using System.Text.Json;
using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Processors;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Infrastructure;

public class UMaTApiService : IUMaTApiService
{
    private readonly ILogger<UMaTApiService> _logger;
    private readonly HttpClient _client;

    public UMaTApiService(IHttpClientFactory factory, ILogger<UMaTApiService> logger)
    {
        _logger = logger;
        _client = factory.CreateClient("UMaT");
    }

    /// <summary>
    /// Gets courses being offered at UMaT Main campus
    /// </summary>
    /// <returns>List of all courses being offered at the UMaT main campus</returns>
    public async Task<List<CourseResponse>?> GetCourses()
    {
        var coursesExists = File.Exists("_content/courses.json");
        if (coursesExists)
        {
            var data = await File.ReadAllTextAsync("_content/courses.json");
            return JsonSerializer.Deserialize<List<CourseResponse>>(data);
        }

        var request = await _client.PostAsJsonAsync("course/getPage", new
        {
            PageSize = 2000,
            PageNumber = 1
        });
        var courses = (await request.Content.ReadFromJsonAsync<PaginatedList<CourseResponse>>())?.Data;
        var semester = await GetCurrentSemester();
        var filteredCourses = courses?.Where(x => x.AcademicPeriod.UpperYear == DateTime.UtcNow.Year
            && x.AcademicPeriod.Semester == semester && x.Programme!.Department.Faculty.SchoolCentre.Campus.Id == 2
            && !x.Programme!.Code!.Contains("Cert")).ToList();
        await File.WriteAllTextAsync("_content/courses.json", JsonSerializer.Serialize(filteredCourses, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
        return filteredCourses;
    }

    /// <summary>
    /// Retrieves list of undergraduate courses
    /// </summary>
    /// <returns></returns>
    public async Task<List<Group>?> GetClasses()
    {
        var groupsExist = File.Exists("_content/classGroups.json");
        if (groupsExist)
        {
            var data = await File.ReadAllTextAsync("_content/classGroups.json");
            return JsonSerializer.Deserialize<List<Group>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        var groups = new List<Group>();
        var courses = (await GetCourses())?.ToList();
        if (courses is null) return null;
        
        for (var i = 1; i <= courses.Count; i++)
        {
            var course = courses[i - 1];
            if (course.Programme!.Code!.Contains("Cert")) continue;
            var groupName = $"{course.Programme?.Code?.Trim() ?? ""} {DateTime.Now.Year - course.YearGroup}";
            if (groups.Any(x => x.Name == groupName)) continue;
            var classSize = await GetClassSize(course.YearGroup ?? 0, course.Programme?.Id ?? 0);
            groups.Add(new Group(i, groupName, classSize));
        }

        await File.WriteAllTextAsync("_content/classGroups.json", JsonSerializer.Serialize(groups, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
        return groups;
    }

    public async Task<List<DepartmentResponse>?> GetDepartments()
    {
        var result = new HashSet<DepartmentResponse>();
        var courses = await GetCourses();
        var departments = courses?.Select(x => new DepartmentResponse()
        {
            Id = x.Programme!.Department.Id,
            Code = x.Programme.Code ?? "",
            Name = x.Programme.Department.Name
        }).ToList();
        if (departments is null) return null;
        
        foreach (var department in departments)
        {
            result.Add(department);
        }

        return result.ToList();
    }

    public async Task<List<Staff>?> GetLecturers()
    {
        var lecturersExists = File.Exists("_content/lecturers.json");
        if (lecturersExists)
        {
            var data = await File.ReadAllTextAsync("_content/lecturers.json");
            return JsonSerializer.Deserialize<List<Staff>>(data);
        }

        var request = await _client.PostAsJsonAsync("staff/getPage", new
        {
            PageSize = 500,
            PageNumber = 1
        });
        var lecturers = (await request.Content.ReadFromJsonAsync<PaginatedList<Staff>>())?.Data;
        await File.WriteAllTextAsync("_content/lecturers.json", JsonSerializer.Serialize(lecturers, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
        return lecturers;    
    }

    private async Task<int> GetClassSize(int yearGroup, int programmeId)
    {
        var request = await _client.PostAsync(
            $"Student/ClassSize?yearGroup={yearGroup}&programmeId={programmeId}",
            null, new CancellationToken());
        return (await request.Content.ReadFromJsonAsync<ApiDomainResponse>())?.Result ?? 0;
    }

    private async Task<int?> GetCurrentSemester()
    {
        var request = await _client.GetAsync("academicPeriod/get");
        return (await request.Content.ReadFromJsonAsync<AcademicPeriod>())?.Result.Semester;
    }
}

internal record ApiDomainResponse(string Version, int StatusCode, string Message, int Result);