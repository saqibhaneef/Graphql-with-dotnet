using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Instructor
{
    public class InstructorRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public InstructorRepository(IDbContextFactory<SchoolDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<InstructorDTO>> GetAll()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors                    
                    .ToListAsync();
            }

        }

        public async Task<InstructorDTO> GetById(Guid courseId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors
                    .FirstOrDefaultAsync(x => x.Id == courseId);
            }

        }

        public async Task<InstructorDTO> Create(InstructorDTO instructor)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Instructors.Add(instructor);
                await context.SaveChangesAsync();
            }

            return instructor;
        }


        public async Task<InstructorDTO> Update(InstructorDTO instructor)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Instructors.Update(instructor);
                await context.SaveChangesAsync();
            }

            return instructor;
        }

        public async Task<bool> Delete(Guid id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                InstructorDTO instructor = new InstructorDTO()
                {
                    Id = id
                };

                context.Instructors.Remove(instructor);
                return await context.SaveChangesAsync() > 0;
            }

        }
    }
}
