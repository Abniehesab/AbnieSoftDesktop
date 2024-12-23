using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialGroup
{
    public class RegisterMaterialGroupDTO
    {
        public long BusinessId { get; set; }
        public int? MaterialGroupCode { get; set; }
        public string? MaterialGroupTitle { get; set; }
    }
    public enum RegisterMaterialGroupResult
    {
        Success,
        CodeExists,
        NameExists
    }
}
