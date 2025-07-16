using HotChocolate.Data.Filters;
using PizzaOrder.API.Schema.Queries;

namespace GraphQLDemo.API.Schema.Filters
{
    public class CourseFilterType : FilterInputType<CourseType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor
                .Ignore(c => c.Id)
                .Ignore(c => c.InstructorId);

            base.Configure(descriptor);
        }
    }
}
