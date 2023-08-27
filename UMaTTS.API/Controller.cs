using Microsoft.AspNetCore.Mvc;
using UMaTLMS.Core.Exceptions;

namespace UMaTLMS.API;

[ApiController]
[Route("api/[controller]/[action]")]
public class Controller : ControllerBase
{
    protected static ApiSuccessResponse<TResponse> SuccessResponse<TResponse>(
        TResponse? result,
        int? statusCode = 200,
        string message = "")
    {
        if (result is null)
        {
            return new ApiSuccessResponse<TResponse>(result, StatusCodes.Status204NoContent, message);
        }
        return new ApiSuccessResponse<TResponse>(result, statusCode, message);
    }

    protected static ApiErrorResponse ErrorResponse(Exception ex)
    {
        return new ApiErrorResponse(ex.GetStatusCode(), ex.Message, ex.Message);
    }

    protected static ApiErrorResponse ErrorResponse(int? statusCode = 404,
        object? id = null,
        string message = "Bad request")
    {
        if (id is null) return new ApiErrorResponse(statusCode, message, message);
        return new ApiErrorResponse(StatusCodes.Status404NotFound,
            $"Entity with id: {id} does not exist", "");
    }

    protected IActionResult BuildProblemDetails(Exception ex)
        => Problem(title: ex.Message, statusCode: ex.GetStatusCode());
}

public static class Exceptions
{
    public static int GetStatusCode(this Exception ex)
    {
        return ex switch
        {
            EntityExistsException => StatusCodes.Status400BadRequest,
            SystemNotInitializedException => StatusCodes.Status204NoContent,
            DataNotSyncedWithUmatException => StatusCodes.Status204NoContent,
            LecturesNotGeneratedException => StatusCodes.Status204NoContent,
            TimetableGeneratedException => StatusCodes.Status400BadRequest,
            ExamNotAssignedRoomException => StatusCodes.Status412PreconditionFailed,
            ExamDateAndPeriodNotSetException => StatusCodes.Status412PreconditionFailed,
            InvalidIdException => StatusCodes.Status400BadRequest,
            InvalidLoginException => StatusCodes.Status400BadRequest,
            LecturesNotScheduledException => StatusCodes.Status206PartialContent,
            ExamNotScheduledException => StatusCodes.Status206PartialContent,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}

public record ApiSuccessResponse<TResponse>(TResponse? Data, int? StatusCode, string? Message);
public record ApiErrorResponse(int? StatusCode, string? Message, string? DetailedMessage);