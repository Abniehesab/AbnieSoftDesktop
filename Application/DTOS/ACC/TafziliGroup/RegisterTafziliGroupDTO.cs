using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.TafziliGroup
{
    public class RegisterTafziliGroupDTO
    {
       
        public long BusinessId { get; set; }
        public int? TafziliGroupCode { get; set; }
        public string TafziliGroupName { get; set; }
        public List<long> MoeinId { get; set; }
        public long TafziliTypeId { get; set; }
    }
    public enum RegisterTafziliGroupResult
    {
        Success,
        CodeExists,
        NameExists
    }
}
