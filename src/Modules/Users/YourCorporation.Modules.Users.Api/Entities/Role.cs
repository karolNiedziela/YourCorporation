﻿using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Users.Api.Entities
{
    internal class Role : Entity<int>
    {
        public string Name { get; set; }

        public List<SystemUser> SystemUsers { get; private set; }
    }
}
