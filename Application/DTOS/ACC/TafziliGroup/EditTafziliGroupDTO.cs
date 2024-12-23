using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.TafziliGroup
{
    public class EditTafziliGroupDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? TafziliGroupCode { get; set; }
        public string TafziliGroupName { get; set; }
        public List<long> MoeinId { get; set; }
        public long TafziliTypeId { get; set; }
    }
    public enum EditTafziliGroupResult
    {
        Success,
        IncorrectDataCode,
        IncorrectDataName,
        UnSuccess,

    }
}

