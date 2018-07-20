using Application.PageFormatting;
using AutoMapper;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.EarningsDtos;
using Common.DTOs.JobDtos;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.TeamDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Domain.Checkouts;
using Domain.Jobs;
using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class CheckoutsCore
    {
        private ICheckoutRepository _repository;
        private GroupedCheckoutsFormatter groupFormatter;

        public CheckoutsCore(ICheckoutRepository repository)
        {
            _repository = repository;
            groupFormatter = new GroupedCheckoutsFormatter();
        }
        public CheckoutDto CreateCheckout(CreateCheckoutDto data, StaffMemberDto staffMemberDto, JobDto jobDto)
        {
            Checkout checkout = new Checkout(Mapper.Map<StaffMember>(staffMemberDto), data.ShiftDate, Mapper.Map<Job>(jobDto));
            if (_repository.CheckoutExists(data.ShiftDate, staffMemberDto.Id, data.LunchOrDinner))
            {
                throw new InvalidOperationException("A checkout for this employee already exists for this day and shift.");
            }
            Mapper.Map(data, checkout);
            CheckoutEntity checkOutEntity = Mapper.Map<CheckoutEntity>(checkout);
            _repository.AddCheckOut(checkOutEntity);

            _repository.VerifyDatabaseSaveSuccess();
            return Mapper.Map<CheckoutDto>(checkOutEntity);
        }

        public CheckoutDto GetCheckoutByDate(GetCheckoutByDateDto data, StaffMemberDto staffMemberDto)
        {
            DateTime shiftDate = Convert.ToDateTime(data.StringDate).Date;
            CheckoutEntity checkoutEntity = _repository.GetCheckOutForStaffMemberForSpecificDate(shiftDate, staffMemberDto.Id, data.LunchOrDinner);

            if (checkoutEntity == null)
            {
                throw new KeyNotFoundException("No checkout for the provided staffmember was found for the given parameters.");
            }

            CheckoutDto checkoutDto = Mapper.Map<CheckoutDto>(checkoutEntity);
            return checkoutDto;
        }

        public bool CheckoutExistsById(int checkoutId)
        {
            return _repository.CheckoutExistsById(checkoutId);
        }

        public void DeleteCheckout(int checkoutId)
        {
            CheckoutEntity checkout = _repository.GetCheckOutById(checkoutId);
            _repository.DeleteCheckOut(checkout);

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);
        }

        public CheckoutDto GetCheckoutById(int checkoutId)
        {
            return Mapper.Map<CheckoutDto>(_repository.GetCheckOutById(checkoutId));
        }

        public CheckoutEntity GetCheckoutEntityById(int checkoutId)
        {
            return _repository.GetCheckOutById(checkoutId);
        }

        public IEnumerable<CheckoutEntity> GetCheckoutEntitiesForAServerTeam(int serverTeamId)
        {
            return _repository.GetCheckoutsForAServerTeam(serverTeamId);
        }

        public IEnumerable<CheckoutOverviewDto> GetCheckoutsForShift(DateTime shiftDate, string lunchOrDinner)
        {
            IEnumerable<CheckoutEntity> checkoutEntities = _repository.GetCheckOutsForAShift(shiftDate, lunchOrDinner);
            List<CheckoutOverviewDto> checkouts = new List<CheckoutOverviewDto>();
            
            foreach (CheckoutEntity c in checkoutEntities)
            {
                var checkoutDto = Mapper.Map<CheckoutOverviewDto>(c);
                checkouts.Add(checkoutDto);
            }

            return checkouts;
        }

        public CheckoutPagePresentationDto FormatPageData(List<CheckoutOverviewDto> checkouts, List<ServerTeamDto> serverTeams, List<EarningDto> shiftEarnings)
        {
            CheckoutPagePresentationDto pageData = new CheckoutPagePresentationDto();

            //Here the teams that have had their checkouts run are assembled into groups for presentation
            foreach (ServerTeamDto s in serverTeams)
            {
                IEnumerable<CheckoutEntity> entities = _repository.GetCheckoutsForAServerTeam(s.Id);
                if (entities.Count() > 0) {
                    List<TeamGroupedCheckoutsDto> groupedCheckouts = groupFormatter.FormatServerTeamGroupCheckouts(s, entities, checkouts, serverTeams, shiftEarnings);

                    foreach (TeamGroupedCheckoutsDto t in groupedCheckouts)
                    {
                        pageData.TeamCheckouts.Add(t);
                    }
                }

            }

            //Here is where the distinctions need to be made between bar checkouts, patio,
            //and the server teamed/unrun checkouts
            pageData.BarCheckouts = groupFormatter.FormatBarCheckouts(checkouts, shiftEarnings);

            //Now to grab all the checkouts that havent been run and then return the whole dataset
            pageData.NotRunCheckouts = groupFormatter.FormatUnrunServerCheckouts(checkouts, pageData.TeamCheckouts);

            return pageData;

        }

        public bool UpdateCheckout(UpdateCheckoutDto data)
        {
            CheckoutEntity checkoutToUpdate = _repository.GetCheckOutById(data.Id);
            List<CheckoutEntity> checkouts = _repository.GetCheckOutsForAShift(checkoutToUpdate.ShiftDate.Date, checkoutToUpdate.LunchOrDinner).ToList();
            
            if (checkouts.Any(c => c.StaffMemberId == data.StaffMemberId && c.Id != data.Id))
            {
                throw new InvalidOperationException("The staff member is already assigned to a checkout for that shift");
            }

            Mapper.Map(data, checkoutToUpdate);

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);

            return true;
        }
    }
}
