using Bogus;
using GraphQLDemo.API.Models;
using GraphQLDemo.API.Services.Course;
using System.Threading.Tasks;

namespace PizzaOrder.API.Schema.Queries
{    
    public class Query
    {
        private readonly CourseRepository _courseRepository;
        
        public Query(CourseRepository courseRepository)
        {           
            _courseRepository = courseRepository;
        }


        [GraphQLDeprecated("This query us depricated.")]
        public string Instructions => "This is the query instruction";


        public async Task<IEnumerable<CourseType>> GetCourses()
        {            
            var courses = await _courseRepository.GetAll();

            return courses.Select(x=> new CourseType()
            {
                Id = x.Id,
                Name = x.Name,
                Subject = x.Subject,
                InstructorId = x.InstructorId,               
            });           
        }

        public async Task<CourseType> GetCourseById(Guid id)
        {
            var course = await _courseRepository.GetById(id);

            return new CourseType()
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                InstructorId = course.InstructorId,
            };

        }
    }
}
