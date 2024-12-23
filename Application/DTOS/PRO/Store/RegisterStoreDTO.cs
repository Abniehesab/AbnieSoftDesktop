using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Store
{
    public class RegisterStoreDTO
    {
        public long BusinessId { get; set; }
        public int? StoreCode { get; set; }
        public string? StoreTitle { get; set; }
        public string? StoreAdmin { get; set; }
        public string? StoreAddress { get; set; }
        public long? MoeinId { set; get; }
    }

    public enum RegisterStoreResult
    {
        Success,
        CodeExists,
        NameExists,
        UnSuccess
    }
}
