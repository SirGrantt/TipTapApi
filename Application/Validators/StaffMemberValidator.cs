using Application.DTOs.StaffMemberDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class StaffMemberValidator : AbstractValidator<StaffMemberDto>
    {
        public StaffMemberValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.FirstName).Length(1, 25);
            RuleFor(x => x.LastName).Length(1, 25);
            RuleFor(x => x.LastName).NotNull();
        }
    }
}
