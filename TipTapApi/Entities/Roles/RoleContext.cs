﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Entities
{
    public class RoleContext : DbContext
    {
        public RoleContext(DbContextOptions options)
            :base(options)
        {

        }

        DbSet<Role> Roles { get; set; }
    }
}
