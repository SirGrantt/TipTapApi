﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class TeamCheckoutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public int TeamId { get; set; }
        public int CheckoutId { get; set; }

        [Required]
        [ForeignKey("TeamId")]
        public TeamEntity Team { get; set; }

        [Required]
        [ForeignKey("CheckoutId")]
        public CheckoutEntity Checkout { get; set; }
    }
}
