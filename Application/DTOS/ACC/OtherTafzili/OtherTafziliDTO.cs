using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.OtherTafzili
{
    public class OtherTafziliDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? OtherTafziliCode { get; set; }
        public string OtherTafziliName { get; set; }
        public long TafziliGroupId { get; set; }
        public long TafziliId { get; set; }
    }
}
