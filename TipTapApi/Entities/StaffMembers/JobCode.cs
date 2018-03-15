using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Entities
{
    public class JobCode
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal TipOutPercent { get; set; }

        [ForeignKey("StaffMemberId")]
        public StaffMemberOLD StaffMember { get; set; }

        public int StaffMemberId { get; set; }
    }
}
