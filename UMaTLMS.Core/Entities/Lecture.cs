namespace UMaTLMS.Core.Entities
{
    public class Lecture : Entity
    {
        private Lecture(int lecturerId, int courseId, int duration, bool isPractical)
        {
            LecturerId = lecturerId;
            CourseId = courseId;
            Duration = duration;
            IsPractical = isPractical;
        }

        public int LecturerId { get; private set; }
        public int CourseId { get; private set; }
        public string? PreferredRoom { get; private set; }
        public int? OnlineLectureScheduleId { get; private set; }
        public OnlineLectureSchedule? OnlineLectureSchedule { get; private set; }
        public int Duration { get; private set; }
        public bool IsPractical { get; private set; }
        public bool IsVLE { get; private set; }
        public Lecturer? Lecturer { get; private set; }
        public IncomingCourse? Course { get; private set; }
        private List<SubClassGroup> _subClassGroups = new();
        public IReadOnlyList<SubClassGroup> SubClassGroups => _subClassGroups.AsReadOnly();

        public static Lecture Create(int lecturerId, int courseId, int duration, bool isPractical = false)
            => new (lecturerId, courseId, duration, isPractical);

        public Lecture AddGroup(SubClassGroup? group)
        {
            if (group is null) return this;
            _subClassGroups.Add(group);
            return this;
        }
        
        public Lecture AddGroups(IEnumerable<SubClassGroup>? groups)
        {
            if (groups is null) return this;
            _subClassGroups.AddRange(groups);
            return this;
        }

        public Lecture ForCourse(IncomingCourse? course)
        {
            Course = course;
            return this;
        }

        public Lecture Delete()
        {
            Audit?.Delete();
            return this;
        }

        public Lecture IsHeldOnline()
        {
            IsVLE = true;
            return this;
        }

        public Lecture HasPreferredRoom(string? room)
        {
            if (string.IsNullOrWhiteSpace(room)) return this;
            PreferredRoom = room;
            return this;
        }
    }
}