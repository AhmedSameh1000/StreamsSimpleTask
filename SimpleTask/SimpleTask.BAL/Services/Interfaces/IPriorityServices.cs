using SimpleTask.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IPriorityServices
    {
        Task<List<Priority>> GetPrioritiesAsync();
    }
}