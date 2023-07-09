using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class RoomProcessor
{
	private readonly IRoomRepository _roomRepository;
	private readonly ILogger<RoomProcessor> _logger;

	public RoomProcessor(IRoomRepository roomRepository, ILogger<RoomProcessor> logger)
	{
		_roomRepository = roomRepository;
		_logger = logger;
	}

	//TODO:: Create schedules for room when inserted (use outbox)
	public async Task<OneOf<int, Exception>> UpsertAsync(RoomCommand command)
	{
		var isNew = command.Id == 0;
		ClassRoom? room;

		if (isNew)
		{
			var roomExists = await _roomRepository.Exists(command.Name);
			if (roomExists) return new EntityExistsException();

			room = ClassRoom.Create(command.Name, command.Capacity);
			room.IsLabRoom(command.IsLab);
			if (!command.IncludeInGeneralAssignment) room.IsExcludedFromGeneralAssignment();
			if (!command.IsExaminationCenter) room.IsNotExaminationCenter();

			try
			{
				await _roomRepository.AddAsync(room);
				return room.Id;
			}
			catch (Exception e)
			{
				_logger.LogError("{Message}", e.Message);
				return e;
			}
		}

		room = await _roomRepository.FindByIdAsync(command.Id);
		if (room is null) return new NullReferenceException();

		room.WithName(command.Name)
			.HasCapacity(command.Capacity)
			.IsLabRoom(command.IsLab)
			.IsExcludedFromGeneralAssignment(command.IncludeInGeneralAssignment)
			.IsNotExaminationCenter(command.IsExaminationCenter);

		try
		{
			await _roomRepository.UpdateAsync(room);
			return room.Id;
		}
		catch (Exception e)
		{
			_logger.LogError("{Message}", e.Message);
			return e;
		}
	}

	public async Task<OneOf<RoomDto, Exception>> GetAsync(int id)
	{
		var room = await _roomRepository.FindByIdAsync(id);
		if (room is null) return new NullReferenceException();

		return room.Adapt<RoomDto>();
	}   

	public async Task<bool> Exists(string name)
	{
		return await _roomRepository.Exists(name);
	}

	public async Task<PaginatedList<RoomPageDto>> GetPageAsync(PaginatedCommand command)
	{
		var page = await _roomRepository.GetPageAsync(command);
		return page.Adapt<PaginatedList<RoomPageDto>>(Mapping.GetTypeAdapterConfig());
	}

	// TODO:: Delete all schedules for particular room
	public async Task DeleteAsync(int id)
	{
		var room = await _roomRepository.FindByIdAsync(id);
		if (room is null) return;

		await _roomRepository.SoftDeleteAsync(room);
	}

	public async Task HardDeleteAsync(int id)
	{
		var room = await _roomRepository.FindByIdAsync(id);
		if (room is null) return;

		await _roomRepository.DeleteAsync(room);
	}
}

public record RoomCommand(int Id, string Name, int Capacity, bool IsLab,
    bool IsExaminationCenter, bool IncludeInGeneralAssignment);
public record RoomDto(int Id, string Name, int Capacity, bool IsLab, 
	bool IsExaminationCenter, bool IncludeInGeneralAssignment);
public record RoomPageDto(int Id, string Name, int Capacity, bool IsLab, 
	bool IsExaminationCenter, bool IncludeInGeneralAssignment);