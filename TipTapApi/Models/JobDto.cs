using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Models
{
    public class JobDto
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public decimal TipOutPercent { get; set; }
    }
}
