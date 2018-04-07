using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RepositoryInterfaces
{
    public interface ICheckoutRepository : IRepository
    {
        void AddCheckOut(CheckoutEntity c);
        void DeleteCheckOut(CheckoutEntity c);
        bool CheckoutExists(DateTime date, int staffId, string lunchOrDinner);
        bool CheckoutExistsById(int checkoutId);
        CheckoutEntity GetCheckOutById(int checkOutId);
        CheckoutEntity GetCheckOutForStaffMemberForSpecificDate(DateTime date, int staffMemberId, string lunchOrDinner);
        IEnumerable<CheckoutEntity> GetCheckOutsForAShift(DateTime date, string lunchOrDinner);
        IEnumerable<CheckoutEntity> GetCheckOutsForAStaffMember(StaffMemberEntity staffmember);
        IEnumerable<CheckoutEntity> GetCheckOutsForAStaffMemberForAJob(StaffMemberEntity staffMember, JobEntity j);
        IEnumerable<CheckoutEntity> GetCheckoutsForAServerTeam(int serverTeamId);
    }
}
