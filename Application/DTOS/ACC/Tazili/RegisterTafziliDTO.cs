using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tazili
{
    public class RegisterTafziliDTO
    {
        public long BusinessId { get; set; }
        public long TafziliGroupId { get; set; }
        public int TafziliCode { get; set; }
        public int AccTafziliCode { get; set; }
        public string? TafziliName { get; set; }
        public int TafziliType { get; set; }
        public int TafziliRef { get; set; }
        public bool Tafzili2 { get; set; }
        public bool Tafzili3 { get; set; }

    }
    public enum RegisterTaziliResult
    {
        Success,
        CodeExists,
        NameExists
    }
}
