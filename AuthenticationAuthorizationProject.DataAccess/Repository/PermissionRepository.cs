using AuthenticationAuthorizationProject.DataAccess.Data;
using AuthenticationAuthorizationProject.DataAccess.Repository.IRepository;
using AuthenticationAuthorizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
