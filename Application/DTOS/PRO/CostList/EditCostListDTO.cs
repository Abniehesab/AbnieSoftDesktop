using Application.DTOS.PRO.CostListDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostList
{
    public class EditCostListDTO
    {
        public long? CostListIdForUpdate { get; set; }
        public long[]? CostListDetailesIdForUpdate { get; set; }

        public long? AccDocumentIdForUpdate { get; set; }
        public long[]? AccDocumentDetailesIdForUpdate { get; set; }

        public RegisterCostListDTO Register { get; set; }
        public RegisterCostListDetailsDTO[] RegisterCostListDetails { get; set; }

        public enum EditCostListResult
        {
            Success,
            UnSuccess,
            CodeExists,
        }
    }
}
