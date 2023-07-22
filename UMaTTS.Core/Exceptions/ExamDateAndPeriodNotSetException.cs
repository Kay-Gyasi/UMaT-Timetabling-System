namespace UMaTLMS.Core.Exceptions;

public class ExamDateAndPeriodNotSetException : Exception
{
    public ExamDateAndPeriodNotSetException(string message = "No date and period has been set for exam") : base(message)
    {

    }
}

public class EmptyRoomsDataException : Exception
{
    public EmptyRoomsDataException(string message = "Rooms data is empty") : base(message)
    {

    }
}

public class EmptyLecturersDataException : Exception
{
    public EmptyLecturersDataException(string message = "Lecturers data is empty. Sync Data to get them.") : base(message)
    {

    }
}

public class EmptyGroupsDataException : Exception
{
    public EmptyGroupsDataException(string message = "Class groups data is empty. Generate Lectures to get them.") : base(message)
    {

    }
}

public class EmptyCoursesDataException : Exception
{
    public EmptyCoursesDataException(string message = "Courses data is empty. Sync Data to get them.") : base(message)
    {

    }
}