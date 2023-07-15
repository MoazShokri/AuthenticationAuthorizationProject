using AuthenticationAuthorizationProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository.IRepository
{
    public interface IGroupRepository : IRepository<Group>
    {
         Task<Group> GetGroupWithPermissions(int groupId);
    }
}
