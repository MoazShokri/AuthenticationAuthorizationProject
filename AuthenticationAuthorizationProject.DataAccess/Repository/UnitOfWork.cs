using AuthenticationAuthorizationProject.DataAccess.Data;
using AuthenticationAuthorizationProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IPermissionRepository Permission { get; private set; }
        public IGroupRepository Group { get; private set; }
        public IGroupPermissionRepository GroupPermission { get; private set; }



        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            Permission = new PermissionRepository(_db);
            Group = new GroupRepository(_db);
            GroupPermission = new GroupPermissionRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
