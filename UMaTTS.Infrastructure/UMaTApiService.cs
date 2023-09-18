using System.Net.Http.Json;
using System.Text.Json;
using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Processors;
using UMaTLMS.Core.Services;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace UMaTLMS.Infrastructure;

public class UMaTApiService : IUMaTApiService
{
    private readonly ILogger<UMaTApiService> _logger;
    private readonly IConfiguration _configuration;
    private readonly LoginFormOptions _loginFormOptions;
    private readonly HttpClient _client;

    public UMaTApiService(IHttpClientFactory factory, ILogger<UMaTApiService> logger,
        IOptions<LoginFormOptions> loginFormOptions, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _loginFormOptions = loginFormOptions.Value;
        _client = factory.CreateClient("UMaT");
        var authToken = GetAuthTokenAsync().GetAwaiter().GetResult();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
    }

    public async Task<List<CourseResponse>?> GetCourses()
    {
        var request = await _client.PostAsJsonAsync("course/getPage", new
        {
            PageSize = 2000,
            PageNumber = 1
        });
        var courses = (await request.Content.ReadFromJsonAsync<PaginatedList<CourseResponse>>())?.Data;
        var semester = await GetCurrentSemester();
        var filteredCourses = courses?.Where(x => x.AcademicPeriod.UpperYear == DateTime.UtcNow.Year
                                                    && x.AcademicPeriod.Semester == semester 
                                                    && x.Programme!.Department.Faculty.SchoolCentre.Campus.Id == 2
                                                    && !x.Programme!.Code!.Contains("Cert")).ToList();
        await File.WriteAllTextAsync("_content/courses.json", JsonSerializer.Serialize(filteredCourses, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
        return filteredCourses;
    }

    public async Task<List<Group>?> GetClasses()
    {
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

    private async Task<int> GetClassSize(int yearGroup, int programmeId)
    {
        var request = await _client.PostAsync($"Student/ClassSize?yearGroup={yearGroup}&programmeId={programmeId}",
                                                    null, new CancellationToken());
        return (await request.Content.ReadFromJsonAsync<ApiDomainResponse>())?.Result ?? 0;
    }

    private async Task<int?> GetCurrentSemester()
    {
        var request = await _client.GetAsync("academicPeriod/get");
        return (await request.Content.ReadFromJsonAsync<AcademicPeriod>())?.Result.Semester;
    }

    private async Task<string?> GetAuthTokenAsync()
    {
        var formData = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", _loginFormOptions.GrantType),
            new KeyValuePair<string, string>("client_id", _loginFormOptions.ClientId),
            new KeyValuePair<string, string>("client_secret", _loginFormOptions.ClientSecret),
            new KeyValuePair<string, string>("username", _loginFormOptions.Username),
            new KeyValuePair<string, string>("password", _loginFormOptions.Password)
        };
        var formContent = new FormUrlEncodedContent(formData);

        try
        {
            var request = await _client.PostAsync(_configuration["UMaTAuthTokenUrl"] ?? string.Empty, formContent, new CancellationToken());
            var response = await request.Content.ReadFromJsonAsync<UMaTIdentityLoginResponse>();
            return response?.AccessToken ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while trying to get auth token. Error: {Error}", ex);
            return null;
        }
    }
}

public class LoginFormOptions
{
    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

internal class UMaTIdentityLoginResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}

internal record ApiDomainResponse(string Version, int StatusCode, string Message, int Result);