﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teams
{
    public interface ITeamEditor<T, X, S>
    {
        void AddTeamMember(X team, S teamMember);
        void RemoveTeamMember(T group, int teamId, int teamMemberId);
    }
}