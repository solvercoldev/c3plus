using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.MainModule.Contratos.DTO
{
    public class Dto_FaseContrato
    {
        public Dto_FaseContrato()
        {
            UID = Guid.NewGuid();
        }

        public Guid UID { get; set; }

        public int FaseId { get; set; }
        public string Periodo { get; set; }
        public string Fase { get; set; }
        public int DuracionFase { get; set; }
        public int Grupo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int MinDuracionFase { get; set; }
        public int MaxDuracionFase { get; set; }
    }
}
