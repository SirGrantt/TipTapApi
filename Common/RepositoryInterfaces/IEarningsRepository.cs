using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RepositoryInterfaces
{
    public interface IEarningsRepository
    {
        void AddEarning(EarningsEntity earning);
        void DeleteEarning(int earningsId);
        EarningsEntity GetEarning(int staffMemberId, DateTime shiftDate);
        EarningsEntity GetEarningById(int earningId);
        IEnumerable<EarningsEntity> GetEarningsForAStaffMemberBetweenDates(int staffId, DateTime startDate, DateTime endDate);
        bool EarningExists(int staffMemberId, DateTime shiftDate, string lunchOrDinner);
        bool EarningExistsById(int earningId);
        bool Save();
    }
}
