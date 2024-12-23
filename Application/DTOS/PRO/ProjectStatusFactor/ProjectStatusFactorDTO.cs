using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.ProjectStatusFactor
{
    public class ProjectStatusFactorDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? ProjectStatusFactorCode { get; set; }
        public string? ProjectStatusFactorTitle { get; set; }
        public int? ProjectStatusFactorType { get; set; }
        public int? ProjectStatusFactorUser { get; set; }
        public float? ProjectStatusFactorPercent { get; set; }
        public long? MoeinId { set; get; }
        public string? MoeinTitle { get; set; }
        public int? AccMoeinCode { get; set; }
        public long? KolId { set; get; }
        public string? KolTitle { get; set; }
        public int? AccKolCode { get; set; }
    }
}
