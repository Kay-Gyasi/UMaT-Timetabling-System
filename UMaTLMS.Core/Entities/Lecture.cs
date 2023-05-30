namespace UMaTLMS.Core.Entities
{
    public class Lecture : Entity
    {
        private Lecture(int lecturerId, int courseId, int duration, string courseNo, bool isLab)
        {
            LecturerId = lecturerId;
            CourseId = courseId;
            Duration = duration;
            CourseNo = courseNo;
            IsLab = isLab;
        }

        public int LecturerId { get; private set; }
        public int CourseId { get; private set; }
        public int Duration { get; private set; }
        public string CourseNo { get; private set; }
        public bool IsLab { get; private set; }
        private List<SubClassGroup> _subClassGroups = new();
        public IReadOnlyList<SubClassGroup> SubClassGroups => _subClassGroups.AsReadOnly();

        public static Lecture Create(int lecturerId, int courseId, int duration, string courseNo, bool isLab)
            => new (lecturerId, courseId, duration, courseNo, isLab);

        public Lecture AddGroup(SubClassGroup? group)
        {
            if (group is null) return this;
            _subClassGroups.Add(group);
            return this;
        }
    }
}