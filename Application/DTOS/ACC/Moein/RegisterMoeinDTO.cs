using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Moein
{
    public class RegisterMoeinDTO
    {
        public long BusinessId { get; set; }

        public int MoeinCode { get; set; }
        public int AccMoeinCode { get; set; }
        public string MoeinName { get; set; }
        public long AccNatureId { set; get; }
       
        public long KolId { set; get; }
       
    }

    public enum RegisterMoeinResult
    {
        Success,
        CodeExists,
        NameExists
    }
}
