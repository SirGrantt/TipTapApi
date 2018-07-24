using AutoMapper;
using Common.Entities;
using Common.RepositoryInterfaces;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class BarCore
    {
        private ITeamRepository _teamRepository;
 
        public BarCore(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public BarTeam GetBarTeamForShift(DateTime shiftDate, string lunchOrDinner)
        {
            TeamEntity barTeamEntity = _teamRepository.GetBarTeamForShift(shiftDate, lunchOrDinner);
            BarTeam barTeam = Mapper.Map<BarTeam>(barTeamEntity);
            return barTeam;
        }
    }
}
