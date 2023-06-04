namespace UMaTLMS.Core.Entities
{
    public class Lecture : Entity
    {
        private Lecture(int lecturerId, int courseId, int duration, string courseNo, bool isPractical)
        {
            LecturerId = lecturerId;
            CourseId = courseId;
            Duration = duration;
            CourseNo = courseNo;
            IsPractical = isPractical;
        }

        public int LecturerId { get; private set; }
        public int CourseId { get; private set; }
        public int Duration { get; private set; }
        public string CourseNo { get; private set; }
        public bool IsPractical { get; private set; }
        public bool IsConfirmed { get; private set; }
        private List<SubClassGroup> _subClassGroups = new();
        public IReadOnlyList<SubClassGroup> SubClassGroups => _subClassGroups.AsReadOnly();

        public static Lecture Create(int lecturerId, int courseId, int duration, string courseNo, bool isPractical)
            => new (lecturerId, courseId, duration, courseNo, isPractical);

        public Lecture AddGroup(SubClassGroup? group)
        {
            if (group is null) return this;
            _subClassGroups.Add(group);
            return this;
        }

        public Lecture Confirm()
        {
            IsConfirmed = true;
            return this;
        }
    }
}