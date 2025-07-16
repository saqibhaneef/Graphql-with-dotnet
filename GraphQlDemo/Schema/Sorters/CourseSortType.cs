using HotChocolate.Data.Sorting;
using PizzaOrder.API.Schema.Queries;

namespace GraphQLDemo.API.Schema.Sorters
{
    public class CourseSortType : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor
                .Ignore(c => c.Id)
                .Ignore(c => c.InstructorId);

            base.Configure(descriptor);
        }
    }
}
