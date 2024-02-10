using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.DAL.Seeding
{
    public class SeedPriorities
    {
        private readonly IPriorityRepository _PriorityRepository;

        public SeedPriorities(IPriorityRepository priorityRepository)
        {
            _PriorityRepository = priorityRepository;
        }

        public async Task SeedPrioritiesAsync()
        {
            if (await _PriorityRepository.GetCount() > 0)
            {
                return;
            }
            await _PriorityRepository.AddRange(GetPriorities());
            await _PriorityRepository.SaveChanges();
        }

        public List<Priority> GetPriorities()
        {
            return new List<Priority>()
            {
                new Priority () {Name="Low"},
                new Priority () {Name="High"},
                new Priority () {Name="Critical"},
            };
        }
    }
}