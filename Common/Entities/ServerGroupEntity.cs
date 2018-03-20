using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ServerGroupEntity
    {
        public int Id { get; set; }
        public List<ServerTeamEntity> ServerTeams { get; set; }
        public decimal ServersTipOutToBar { get; set; }
        public decimal ServersTipOutToSAs { get; set; }

        public int ShiftId { get; set; }

        [Required]
        [ForeignKey("ShiftId")]
        public ShiftEntity Shift { get; set; }
    }
}
