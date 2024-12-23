using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Material
{
    public class MaterialDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? MaterialType { get; set; }
        public int? MaterialCode { get; set; }
        public string? MaterialTitle { get; set; }      
        public long? MaterialUnitId { get; set; }
        public string? MaterialUnitTitle { get; set; }
        public long? MaterialGroupId { get; set; }
        public string? MaterialGroupTitle { get; set; }
        public string? MaterialDescription { get; set; }

    }
}
