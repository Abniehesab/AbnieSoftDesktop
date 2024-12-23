using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.OtherTafzili
{
    public class RegisterOtherTafziliDTO
    {
        public long BusinessId { get; set; }
        public int? OtherTafziliCode { get; set; }
        public string OtherTafziliName { get; set; }
        public long TafziliGroupId { get; set; }
        public long TafziliId { get; set; }
        public int TafziliAccCode { get; set; }
        public int OtherTafziliViewCode { get; set; }
        public bool Tafzili2{ get; set; }
        public bool Tafzili3 { get; set; }


    }
    public enum RegisterOtherTafziliResult
    {
        Success,
        CodeExists,
        DataAccCodeExists,
        NameExists,
        UnSuccess
    }
}
