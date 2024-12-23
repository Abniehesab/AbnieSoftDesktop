using Application.DTOS.PRO.CostListDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostList
{
    public class CostListAndDetailsDTO
    {
        public CostListDTO CostList { get; set; }
        public List<CostListDetailsDTO> CostListDetails { get; set; }
    }
}
