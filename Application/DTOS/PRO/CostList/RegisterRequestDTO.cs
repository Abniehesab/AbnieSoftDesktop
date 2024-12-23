using Application.DTOS.ACC.AccDocmentDetails;

using Application.DTOS.PRO.CostListDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostList
{
    public class RegisterRequestCostListDTO
    {
        public RegisterCostListDTO Register { get; set; }
        public RegisterCostListDetailsDTO[] RegisterCostListDetails { get; set; }
    }
}
