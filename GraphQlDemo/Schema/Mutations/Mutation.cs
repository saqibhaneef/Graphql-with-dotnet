using PizzaOrder.API.Schema.Queries;

namespace PizzaOrder.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _course;

        public Mutation()
        {
            _course = new List<CourseResult>();
        }

        public CourseResult CreateCourse(CourseInputType courseInputType)
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstructorId
            };

            _course.Add(courseType);

            return courseType;
        }

        public CourseResult UpdateCourse(Guid id, CourseInputType courseInputType)
        {
            CourseResult course = _course.Where(x=>x.Id == id).FirstOrDefault();

            if(course is null)
            {
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
            }

            course.Name = courseInputType.Name;
            course.Subject = courseInputType.Subject;
            course.InstructorId = courseInputType.InstructorId;

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _course.RemoveAll(x => x.Id == id) >= 1;
        }
    }
}
