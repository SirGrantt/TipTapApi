using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TeamDtos
{
    public class AddCheckoutToTeamDto
    {
        public int CheckoutId { get; set; }
        public int ServerTeamId { get; set; }
    }
}
