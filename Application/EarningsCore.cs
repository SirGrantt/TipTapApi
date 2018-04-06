using AutoMapper;
using Common.DTOs.EarningsDtos;
using Common.DTOs.StaffMemberDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Domain.StaffEarnings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class EarningsCore
    {
        private IEarningsRepository earningsRepository;

        public EarningsCore(IEarningsRepository repository)
        {
            earningsRepository = repository;
        }

        public List<EarningDto> AddEarning(List<StaffMemberDto> staffMembers, EarningDto earning)
        {
            List<EarningDto> earningDtos = new List<EarningDto>();
            foreach (StaffMemberDto staffMember in staffMembers)
            {
                if (earningsRepository.EarningExists(staffMember.Id, earning.ShiftDate, earning.LunchOrDinner))
                {
                    throw new InvalidOperationException("A earning could not be created because one for that staff member already exists for that shift.");
                }

                EarningsEntity earningsEntity = Mapper.Map<EarningsEntity>(earning);
                StaffMemberEntity staffMemberEntity = Mapper.Map<StaffMemberEntity>(staffMember);
                earningsEntity.StaffMember = staffMemberEntity;
                earningsRepository.AddEarning(earningsEntity);

                if (!earningsRepository.Save())
                {
                    throw new Exception($"An unexpected error occured while trying to save the earnings for staff members for the date {earning.ShiftDate}");
                }

                EarningDto earningToReturn = Mapper.Map<EarningDto>(earningsEntity);
                earningDtos.Add(earningToReturn);
            }
            return earningDtos;

        }
    }
}
