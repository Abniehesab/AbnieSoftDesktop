using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Store
{
    public class StoreDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }    
        public int? StoreCode { get; set; }
        public string? StoreTitle { get; set; }
        public string? StoreAdmin { get; set; }
        public string? StoreAddress { get; set; }
        public long? MoeinId { set; get; }
        public string? MoeinTitle { get; set; }
    }
}
