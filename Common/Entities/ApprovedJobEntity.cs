using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ApprovedJobEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int StaffMemberId { get; set; }
        [Required]
        public int JobId { get; set; }

        [ForeignKey("StaffMemberId")]
        public StaffMemberEntity StaffMember { get; set; }

        [ForeignKey("JobId")]
        public JobEntity Job { get; set; }
    }
}
