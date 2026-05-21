using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IAdminSeederService
    {
        Task SeedAdminAsync();
    }
}
