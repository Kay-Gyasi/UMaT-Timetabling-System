using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Helpers;

internal class Mapping
{
	public static TypeAdapterConfig GetTypeAdapterConfig()
	{
		var config = new TypeAdapterConfig();
		config.NewConfig<PaginatedList<ClassRoom>, PaginatedList<RoomPageDto>>()
			.MapWith(x => new PaginatedList<RoomPageDto>(x.Data.Adapt<List<RoomPageDto>>(), 
				x.TotalCount, x.CurrentPage, x.PageSize));

		config.NewConfig<ClassGroup, ClassGroupDto>()
			.MapWith(x => new ClassGroupDto(x.Id, x.UmatId, x.Size, x.Name, 
				x.SubClassGroups.Adapt<List<SubClassGroupDto>>()));

		config.NewConfig<SubClassGroup, SubClassGroupDto>()
			.MapWith(x => new SubClassGroupDto(x.Id, x.GroupId, x.Size, x.Name,
				x.Lectures.Adapt<List<LectureDto>>()));

		config.NewConfig<PaginatedList<ClassGroup>, PaginatedList<ClassGroupPageDto>>()
			.MapWith(x => new PaginatedList<ClassGroupPageDto>(x.Data.Adapt<List<ClassGroupPageDto>>(), 
				x.TotalCount, x.CurrentPage, x.PageSize));

		config.NewConfig<PaginatedList<SubClassGroup>, PaginatedList<SubClassGroupPageDto>>()
			.MapWith(x => new PaginatedList<SubClassGroupPageDto>(x.Data.Adapt<List<SubClassGroupPageDto>>(), 
				x.TotalCount, x.CurrentPage, x.PageSize));
		return config;
	}
}
