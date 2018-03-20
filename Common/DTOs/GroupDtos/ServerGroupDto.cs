using Common.DTOs.TeamDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.GroupDtos
{
    public class ServerGroupDto
    {
        public int Id { get; set; }
        public List<ServerTeamDto> ServerTeams { get; set; }
        public decimal ServersTipOutToBar { get; set; }
        public decimal ServersTipOutToSAs { get; set; }
    }
}
