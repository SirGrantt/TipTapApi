using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services.RoleServices
{
    public class RoleRepository : IRoleRepository
    {
        private IRoleRepository _context;
        public RoleRepository(IRoleRepository context)
        {
            _context = context;
        }
        public Role CreateRole(RoleDto role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public bool RoleExists(int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(int roleId, RoleDto role)
        {
            throw new NotImplementedException();
        }
    }
}
