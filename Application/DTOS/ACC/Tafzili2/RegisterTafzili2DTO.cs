using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tafzili2
{
    public class RegisterTafzili2DTO
    {
        public long BusinessId { get; set; } 
        public int? Tafzili2Code { get; set; } 
        public string? Tafzili2Name { get; set; }
    }
    public enum RegisterTafzili2Result
    {
        Success,
        CodeExists,
        NameExists
    }
}
