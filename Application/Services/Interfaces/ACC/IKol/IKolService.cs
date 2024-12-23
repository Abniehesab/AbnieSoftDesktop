using Application.DTOS.ACC.Kol;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.IKol
{
    public interface IKolService
    {
        #region Kol
       
       
        Task<FilterKolDTO> FilterKol(FilterKolDTO filter);
      
        #endregion
    }
}
