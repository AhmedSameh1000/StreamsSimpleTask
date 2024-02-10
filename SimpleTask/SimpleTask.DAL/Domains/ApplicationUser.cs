using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SimpleTask.DAL.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public List<Document> documents { get; set; }
    }
}