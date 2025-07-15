using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Course;
using HotChocolate.Subscriptions;
using PizzaOrder.API.Schema.Queries;
using System.Threading.Tasks;

namespace PizzaOrder.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly CourseRepository _courseRepository;
        public Mutation(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseResult> CreateCourse(CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstructorId
            };
            
            courseDTO = await _courseRepository.Create(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstructorId
            };

            topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInputType, [Service] ITopicEventSender topicEventSender)
        {
            //CourseResult course = _course.Where(x=>x.Id == id).FirstOrDefault();

            //if(course is null)
            //{
            //    throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
            //}

            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstructorId
            };

            courseDTO = await _courseRepository.Update(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseInputType.Name,
                Subject = courseInputType.Subject,
                InstructorId = courseInputType.InstructorId
            };

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            return await _courseRepository.Delete(id);
        }
    }
}
