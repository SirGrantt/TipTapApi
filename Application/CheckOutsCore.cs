using AutoMapper;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.JobDtos;
using Common.DTOs.StaffMemberDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Domain.Checkouts;
using Domain.Jobs;
using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CheckoutsCore
    {
        private ICheckoutRepository _repository;

        public CheckoutsCore(ICheckoutRepository repository)
        {
            _repository = repository;
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

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);
            return Mapper.Map<CheckoutDto>(checkOutEntity);
        }

        public CheckoutDto GetCheckoutByDate(GetCheckoutByDateDto data, StaffMemberDto staffMemberDto)
        {
            DateTime shiftDate = Convert.ToDateTime(data.UnformattedDate);
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
    }
}
