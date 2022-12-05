using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.Core.DTOs.ML
{
    public class emergencia_detalleDto
    {
        public int id { get; set; }
        public long idFechaHora { get; set; }
        public string organinismo { get; set; }
        public decimal valor { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
    }
}
