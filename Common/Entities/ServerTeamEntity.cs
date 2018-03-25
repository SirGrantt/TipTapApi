﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ServerTeamEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public TipOutEntity TipOut { get; set; }
        public List<CheckOutEntity> CheckOuts { get; set; }
        public bool CheckoutHasBeenRun { get; set; }

    }
}
