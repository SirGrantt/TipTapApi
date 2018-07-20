using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class TipOutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal TeamGrossSales { get; set; }
        public decimal FinalTeamBarSales { get; set; }
        public decimal SaTipOut { get; set; }
        public decimal BarTipOut { get; set; }
        public DateTime ShiftDate { get; set; }
        public int ServerTeamId { get; set; }

        [ForeignKey("ServerTeamId")]
        public TeamEntity ServerTeam { get; set; }
    }
}
