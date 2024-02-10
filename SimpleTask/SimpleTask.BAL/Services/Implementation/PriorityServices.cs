using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.BAL.Services.Implementation
{
    public class PriorityServices : IPriorityServices
    {
        private readonly IPriorityRepository _PriorityRepository;

        public PriorityServices(IPriorityRepository priorityRepository)
        {
            _PriorityRepository = priorityRepository;
        }

        public async Task<List<Priority>> GetPrioritiesAsync()
        {
            var Priorities = await _PriorityRepository.GetAllAsNoTracking();

            return Priorities.ToList();
        }
    }
}