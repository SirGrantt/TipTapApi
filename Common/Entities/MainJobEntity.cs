using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class MainJobEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StaffMemberId { get; set; }
        public int JobId { get; set; }

        [Required]
        [ForeignKey("StaffMemberId")]
        public StaffMemberEntity StaffMember { get; set; }

        [Required]
        [ForeignKey("JobId")]
        public JobEntity Job { get; set; }
    }
}
