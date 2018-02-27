using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services.RoleServices
{
    public interface IRoleRepository
    {
        Role CreateRole(RoleDto role);
        IEnumerable<Role> GetRoles();
        Role GetRole(int roleId);
        void UpdateRole(int roleId, RoleDto role);
        bool RoleExists(int roleId);
        void DeleteRole(int roleId);
    }
}
