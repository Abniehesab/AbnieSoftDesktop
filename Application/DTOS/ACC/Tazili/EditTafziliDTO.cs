using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tazili
{
    public class EditTafziliDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public long? TafziliGroupId { get; set; }
        public int? TafziliCode { get; set; }
        public int AccTafziliCode { get; set; }
        public string? TafziliName { get; set; }
        public int TafziliType { get; set; }
        public long TafziliRef { get; set; }
        public string SharedKey { get; set; }
        public bool Tafzili2{ get; set; }
        public bool Tafzili3{ get; set; }

    }
    public enum EditTaziliResult
    {
        Success,
        IncorrectDataCode,
        IncorrectDataName,
        UnSuccess,

    }
}
