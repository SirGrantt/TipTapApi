using Common.DTOs.StaffMemberDtos;
using Application.Validators;
using AutoMapper;
using Common;
using Common.Entities;
using Domain.StaffMembers;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Jobs;
using Common.Utilities;

namespace Application
{
    public class StaffMembersCore
    {
        StaffMemberValidator Validator = new StaffMemberValidator();
        

        private IStaffMemberRepository _repository;
        public StaffMembersCore(IStaffMemberRepository repo)
        {
            _repository = repo;
        }


        public IEnumerable<StaffMemberDto> GetStaffMembers()
        {
            var staffEntities = _repository.GetStaffMembers();
            IEnumerable<StaffMemberDto> staffDtos = Mapper.Map<IEnumerable<StaffMemberDto>>(staffEntities);
            return staffDtos;
        }

        public bool StaffMemberExists(int staffId)
        {
            return _repository.StaffMemberExists(staffId);
        }

        public StaffMemberDto GetStaffMember(int staffId)
        {
            if (!StaffMemberExists(staffId))
            {
                throw new KeyNotFoundException("no staff member with that ID was found.");
            }
            var staffEntity = _repository.GetStaffMember(staffId);
            StaffMemberDto staffDto = Mapper.Map<StaffMemberDto>(staffEntity);
            return staffDto;
        }

        public StaffMemberEntity GetStaffMemberEntity(int staffMemberId)
        {
            var staffEntity = _repository.GetStaffMember(staffMemberId);
            return staffEntity;
        }

        public bool ValidateStaffMember(StaffMemberDto sm)
        {
            ValidationResult results = Validator.Validate(sm);
            bool validationSucceded = results.IsValid;

            if (!validationSucceded)
            {
                return false;
            }

            return true;
        }

        public StaffMemberDto AddStaffMember(StaffMemberDto sm)
        {
            if (!ValidateStaffMember(sm))
            {
                return null;
            }

            StaffMemberEntity staffEntity = Mapper.Map<StaffMemberEntity>(sm);
            _repository.AddStaffMember(staffEntity);

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);

            StaffMemberDto savedStaffMember = Mapper.Map<StaffMemberDto>(staffEntity);
            return savedStaffMember;

        }

        public bool UpdateStaffMemberName(UpdateStaffMemberName sm, int staffId)
        {

            if (!_repository.StaffMemberExists(staffId))
            {
                return false;
            }

            var staffToUpdate = _repository.GetStaffMember(staffId);
            var updatedStaffMember = Mapper.Map(sm, staffToUpdate);

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);

            return true;
        }

        public bool DeleteStaffMember(int staffId)
        {
            if (!StaffMemberExists(staffId))
            {
                return false;
            }
            StaffMemberEntity smToDelete = _repository.GetStaffMember(staffId);
            _repository.DeleteStaffMember(smToDelete);

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);

            return true;
        }

        public void SetInactiveStatus(int staffId)
        {
            StaffMemberEntity sm = _repository.GetStaffMember(staffId);
            sm.Status = "inactive";

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);
        }

        public StaffMember SetMainJob(int staffId, Job job)
        {
            StaffMemberEntity smEntity = _repository.GetStaffMember(staffId);
            JobEntity jobEntity = Mapper.Map<JobEntity>(job);
            _repository.SetStaffMemberMainJob(smEntity, jobEntity);

            UtilityMethods.VerifyDatabaseSaveSuccess(_repository);

            return Mapper.Map<StaffMember>(smEntity);
            
        }

        public List<StaffMemberDto> GetApprovedStaffForJob(int jobId)
        {
            List<StaffMemberDto> approvedStaff = Mapper.Map<List<StaffMemberDto>>(_repository.GetApprovedStaffForJob(jobId));
            return (approvedStaff);
            
        }
    }
}
