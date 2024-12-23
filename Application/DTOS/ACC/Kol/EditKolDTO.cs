using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Kol
{
    public class EditKolDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int KolCode { get; set; }
        public int? AccKolCode { get; set; }
        public string KolName { get; set; }
        public long AccGroupId { get; set; }
    }
    public enum EditKolResult
    {
        Success,
        IncorrectDataCode,
        IncorrectDataName,
        UnSuccess,

    }
}
