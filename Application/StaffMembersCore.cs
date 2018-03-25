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
            var staffEntity = _repository.GetStaffMember(staffId);
            StaffMemberDto staffDto = Mapper.Map<StaffMemberDto>(staffEntity);
            return staffDto;
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
            
            if (!_repository.Save())
            {
                return null;
            }
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

            if (!_repository.Save())
            {
                return false;
            }
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

            if (!_repository.Save())
            {
                return false;
            }

            return true;
        }
    }
}
