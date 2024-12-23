using Application.DTOS.ACC.AccDocmentDetails;

using Application.DTOS.Fin.PaymentCheque;

using Application.DTOS.Fin.ReceiveCheque;


using Application.Services.Interfaces.ACC.IMoein;
using Application.Services.Interfaces.FIN.IReceive;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using Domain.Entities.FIN;
using Domain.Entities.PRO;
using Infrastructure.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;


namespace Application.Services.Implementations.FIN.Receive
{
    public class ReceiveService:IReceiveService
    {

        #region constructor

        private IGenericRepository<Domain.Entities.FIN.ReceiveCheque> _receiveChequeRepository;
        private IGenericRepository<Domain.Entities.PRO.Contract> _contractRepository;

 
        private IGenericRepository<Domain.Entities.ACC.Tafzili> _tafziliRepository;

        public ReceiveService(
            
            IGenericRepository<Domain.Entities.FIN.ReceiveCheque> receiveChequeRepository,
            IGenericRepository<Domain.Entities.PRO.Contract> contractRepository,           
            IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository
           
            )
        {        
            this._receiveChequeRepository = receiveChequeRepository;
            this._contractRepository = contractRepository;         
            this._tafziliRepository = tafziliRepository;
          
        }

        #endregion

        //چک ها



        public async Task<FilterReceiveChequeDTO> FilterReceiveCheques(FilterReceiveChequeDTO filter)
        {
            try
            {
                var ReceiveChequeQuery =
                    (from ReceiveCheque in _receiveChequeRepository.GetEntitiesQuery()
                     where !ReceiveCheque.IsUpdate
                     join contract in _contractRepository.GetEntitiesQuery() on ReceiveCheque.ContractId equals contract.Id into contracts
                     from contract in contracts.DefaultIfEmpty()

                     join bank in _tafziliRepository.GetEntitiesQuery() on ReceiveCheque.BankId equals bank.Id into banks
                     from bank in banks.DefaultIfEmpty()

                     join reciver in _tafziliRepository.GetEntitiesQuery() on ReceiveCheque.PayerId equals reciver.Id into recivers
                     from reciver in recivers.DefaultIfEmpty()



                     select new ReceiveChequeDTO
                     {
                         Id = ReceiveCheque.Id,
                         BusinessId = ReceiveCheque.BusinessId,

                         ReceiveChequeNumber = ReceiveCheque.ReceiveChequeNumber,
                         ReceiveChequeSayyadiNumber = ReceiveCheque.ReceiveChequeSayyadiNumber,

                         BankId = ReceiveCheque != null ? ReceiveCheque.BankId ?? 0 : 0,
                         BankTitle = bank != null ? bank.TafziliName ?? "" : "",

                         PayerId = ReceiveCheque.PayerId,
                         PayerTitle = reciver.TafziliName,

                         ReceiveChequeDate = ReceiveCheque.ReceiveChequeDate,
                         ReceiveChequeLastState = ReceiveCheque.ReceiveChequeLastState,
                         IsFirstPeriod = ReceiveCheque.IsFirstPeriod,
                         IsGuarantee = ReceiveCheque.IsGuarantee,

                         ReceiveChequeDescription = ReceiveCheque.ReceiveChequeDescription,
                         ReceiveChequeValue = ReceiveCheque.ReceiveChequeValue,

                         ContractId = ReceiveCheque != null ? ReceiveCheque.ContractId ?? 0 : 0,
                         ContractTitle = contract != null ? contract.ContractTitle ?? "" : "",

                         AccDocumentId = ReceiveCheque.AccDocumentId,


                     }).AsQueryable();

                switch (filter.OrderBy)
                {
                    case ReceiveChequesOrderBy.CodeAsc:
                        ReceiveChequeQuery = ReceiveChequeQuery.OrderByDescending(s => s.ReceiveChequeDate);
                        break;
                    case ReceiveChequesOrderBy.CodeDec:
                        ReceiveChequeQuery = ReceiveChequeQuery.OrderBy(s => s.ReceiveChequeDate);
                        break;
                }

                if (!string.IsNullOrEmpty(filter.ReceiveChequeDateStart.ToString()))
                {
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(A => A.ReceiveChequeDate.Value.Date >= filter.ReceiveChequeDateStart.Value.Date);
                }

                if (!string.IsNullOrEmpty(filter.ReceiveChequeDateEnd.ToString()))
                {
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(A => filter.ReceiveChequeDateEnd.Value.Date >= A.ReceiveChequeDate.Value.Date);
                }

                if (filter.ReceiveChequeNumber != null)
                {
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.ReceiveChequeNumber.Contains(filter.ReceiveChequeNumber));
                }



                if (filter.ReceiveChequeSayyadiNumber != null)
                {
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.ReceiveChequeSayyadiNumber.Contains(filter.ReceiveChequeSayyadiNumber));
                }



                if (filter.ReceiveChequeLastState.HasValue && filter.ReceiveChequeLastState != 0)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.ReceiveChequeLastState == filter.ReceiveChequeLastState);

                if (filter.BankId.HasValue && filter.BankId != 0)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.BankId == filter.BankId);

                if (filter.PayerId.HasValue && filter.PayerId != 0)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.PayerId == filter.PayerId);

                if (filter.IsGuarantee == true)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.IsGuarantee == filter.IsGuarantee);

                if (filter.IsFirstPeriod == true)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.IsFirstPeriod == filter.IsFirstPeriod);

                if (filter.ContractId.HasValue && filter.ContractId != 0)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.ContractId == filter.ContractId);


                if (!string.IsNullOrEmpty(filter.ReceiveChequeDescription))
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.ReceiveChequeDescription.Contains(filter.ReceiveChequeDescription));

                if (filter.ReceiveChequeValue.HasValue && filter.ReceiveChequeValue != 0)
                    ReceiveChequeQuery = ReceiveChequeQuery.Where(s => s.ReceiveChequeValue == filter.ReceiveChequeValue);



                var count = (int)Math.Ceiling(ReceiveChequeQuery.Count() / (double)filter.TakeEntity);
                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var ReceiveCheques = ReceiveChequeQuery.Paging(pager).ToList();



                return filter.SetReceiveCheques(ReceiveCheques).SetPaging(pager);



            }
            catch (Exception ex)
            {
                return null;
            }
        }
     


        public async Task<ReceiveCheque> GetReceiveCheque(long ReceiveChequeId)
        {
            return _receiveChequeRepository.GetEntitiesQuery().AsQueryable().
           SingleOrDefault(f => !f.IsDelete && f.Id == ReceiveChequeId && !f.IsUpdate);
        }
     

      
    }
}
