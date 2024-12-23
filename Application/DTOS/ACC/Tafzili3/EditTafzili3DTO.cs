using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tafzili3
{
    public class EditTafzili3DTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }

        public int? Tafzili3Code { get; set; }


        public string? Tafzili3Name { get; set; }
    }
    public enum EditTafzili3Result
    {
        Success,
        IncorrectDataCode,
        IncorrectDataName,
        UnSuccess,

    }
}
