using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.MainModules.Entities
{
    public partial class Fases
    {
        public int MinMesesDuracion { get; set; }
        public int MaxMesesDuracion { get; set; }
        public bool FaseActiva { get; set; }
    }
}