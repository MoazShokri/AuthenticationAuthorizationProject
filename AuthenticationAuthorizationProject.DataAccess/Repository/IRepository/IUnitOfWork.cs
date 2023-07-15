using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IPermissionRepository Permission { get; }
        public IGroupRepository Group { get; }
        public IGroupPermissionRepository GroupPermission { get; }


        void Save();    
    }
}
