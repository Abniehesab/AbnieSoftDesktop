using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialUnit
{
    public class RegisterMaterialUnitDTO
    {
        public long BusinessId { get; set; }
        public int? MaterialUnitCode { get; set; }
        public string? MaterialUnitTitle { get; set; }
    }
    public enum RegisterMaterialUnitResult
    {
        Success,
        CodeExists,
        NameExists
    }
}
