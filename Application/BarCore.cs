using AutoMapper;
using Common.Entities;
using Common.RepositoryInterfaces;
using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class BarCore
    {
        private ITeamRepository _teamRepository;
        private ICheckoutRepository _checkoutRepository;
 
        public BarCore(ITeamRepository teamRepository, ICheckoutRepository checkoutRepo)
        {
            _teamRepository = teamRepository;
            _checkoutRepository = checkoutRepo;
        }

        public BarTeam GetBarTeamForShift(DateTime shiftDate, string lunchOrDinner)
        {
            TeamEntity barTeamEntity = _teamRepository.GetBarTeamForShift(shiftDate, lunchOrDinner);
            BarTeam barTeam = Mapper.Map<BarTeam>(barTeamEntity);
            return barTeam;
        }

        public bool BarTeamExists(DateTime shiftDate, string lunchOrDinner)
        {
            return _teamRepository.BarTeamExistsForShift(shiftDate, lunchOrDinner);
        }

        
        public List<Earnings> RunBarTeamCheckout(DateTime shiftDate, string lunchOrDinner, int barBackCount)
        {
            TeamEntity barTeam = _teamRepository.GetBarTeamForShift(shiftDate, lunchOrDinner);
            BarTeam team = Mapper.Map<BarTeam>(barTeam);
            List<CheckoutEntity> checkouts = _checkoutRepository.GetCheckoutsForATeam(team.Id).ToList();
            foreach (CheckoutEntity c in checkouts)
            {
                Checkout chkout = Mapper.Map<Checkout>(c);
                team.Checkouts.Add(chkout);

            }
            List<TipOutEntity> serverTipOuts = _teamRepository.GetTipOuts(shiftDate, lunchOrDinner, "server");
            decimal serverTips = 0;
            foreach (TipOutEntity t in serverTipOuts)
            {
                serverTips += t.BarTipOut;
            }

            if (team.CheckoutHasBeenRun == true)
            {
                _teamRepository.DeleteTipOut(team.Id);
            }

            List<Earnings> teamEarnings = team.RunBarCheckout(serverTips, barBackCount);
            TipOutEntity tipoutEntity = Mapper.Map<TipOutEntity>(team.TipOut);
            tipoutEntity.Team = barTeam;
            _teamRepository.AddTipOut(tipoutEntity);
            return teamEarnings;
        }
    }
}
