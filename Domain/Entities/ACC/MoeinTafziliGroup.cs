using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ACC
{
    public class MoeinTafziliGroups : BaseEntity
    {
        public long MoeinId { get; set; }
        public Moein Moein { get; set; }

        public long TafziliGroupId { get; set; }
        //public TafziliGroup TafziliGroup { get; set; }

        public long BusinessId { get; set; }
    }
}
