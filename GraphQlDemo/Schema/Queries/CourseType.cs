using GraphQLDemo.API.Models;
using GraphQLDemo.API.Services.Instructor;

namespace PizzaOrder.API.Schema.Queries
{    
    public class CourseType
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public Subject Subject { get; set; }

        public Guid InstructorId { get; set; }
        
        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorRepository instructorRepository)
        {
            var instructor = await instructorRepository.GetById(InstructorId);
            return new InstructorType(){
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Salary = instructor.Salary  
            };
        }

        public IEnumerable<StudentType>? Students { get; set; }

    }
}
