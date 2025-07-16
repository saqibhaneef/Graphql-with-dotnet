using Bogus;
using GraphQLDemo.API.Models;
using GraphQLDemo.API.Schema.Filters;
using GraphQLDemo.API.Schema.Sorters;
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

        [UseSorting(typeof(CourseSortType))]// Sorting is applied after db results
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

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 2)]
        [UseProjection] 
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting]// Sorting is applying on db directly with query
        public IQueryable<CourseType> GetPaginatedCourses()
        {
            var courses = _courseRepository.GetAllQuerable();

            return courses.Select(x=> new CourseType()
            {
                Id = x.Id,
                InstructorId= x.InstructorId,
                Name= x.Name,  
                Subject = x.Subject,
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
