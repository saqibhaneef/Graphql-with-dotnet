using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services.Instructor;

namespace GraphQLDemo.API.Models.DataLoader
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly InstructorRepository _instructorRepository;
        public InstructorDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions options
            , InstructorRepository instructorRepository
            ) : base(batchScheduler, options)
        {
            _instructorRepository = instructorRepository;
        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<InstructorDTO> instructors = await _instructorRepository.GetManyByIds(keys);

            return instructors.ToDictionary(i => i.Id);

        }
    }
}
