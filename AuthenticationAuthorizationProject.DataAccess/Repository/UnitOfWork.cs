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
      



        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
           
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
