using AuthenticationAuthorizationProject.DataAccess.Data;
using AuthenticationAuthorizationProject.DataAccess.Repository.IRepository;
using AuthenticationAuthorizationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Repository
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public GroupRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        //public async Task<Group> GetGroupWithPermissions(int groupId)
        //{
        //    return await _db.Groups
        //        .Include(g => g.GroupPermissions)
        //            .ThenInclude(gp => gp.Permission)
        //        .FirstOrDefaultAsync(g => g.Id == groupId);
        //}
        public async Task<Group> GetGroupWithPermissions(int groupId)
        {
            var group = await _db.Groups
                .Include(g => g.GroupPermissions)
                    .ThenInclude(gp => gp.Permission)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedGroup = JsonSerializer.Serialize(group, jsonOptions);
            var deserializedGroup = JsonSerializer.Deserialize<Group>(serializedGroup, jsonOptions);

            return deserializedGroup;
        }
    }
}
