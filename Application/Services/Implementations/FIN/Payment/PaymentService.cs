
using Application.DTOS.ACC.AccDocmentDetails;

using Application.DTOS.Fin.PaymentCheque;

using Application.Services.Interfaces.ACC.IMoein;
using Application.Services.Interfaces.FIN.IPayment;
using Application.Services.Interfaces.FIN.IReceive;
using Common.Utilities.Paging;
using Domain.Entities.ACC;

using Domain.Entities.FIN;
using Domain.Entities.PRO;

using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace Application.Services.Implementations.FIN.Payment
{
    public class PaymentService : IPaymentService
    {
        #region constructor

   
     

        private IGenericRepository<Domain.Entities.FIN.PaymentCheque> _paymentChequeRepository;
        private IGenericRepository<Domain.Entities.PRO.Contract> _contractRepository; 
        private IGenericRepository<Domain.Entities.ACC.Tafzili> _tafziliRepository;


        public PaymentService(
          
            IGenericRepository<Domain.Entities.FIN.PaymentCheque> paymentChequeRepository,
            IGenericRepository<Domain.Entities.PRO.Contract> contractRepository,           
            IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository
            )
        {
        
            this._paymentChequeRepository = paymentChequeRepository;
            this._contractRepository = contractRepository;        
            this._tafziliRepository = tafziliRepository;
        }

        #endregion

        public async Task<PaymentCheque> GetPaymentCheque(long PaymentChequeId)
        {
            return _paymentChequeRepository.GetEntitiesQuery().AsQueryable().
           SingleOrDefault(f => !f.IsDelete && f.Id == PaymentChequeId && !f.IsUpdate);
        }

        public async Task<FilterPaymentChequeDTO> FilterPaymentCheques(FilterPaymentChequeDTO filter)
        {
            try
            {
                var PaymentChequeQuery =
                    (from PaymentCheque in _paymentChequeRepository.GetEntitiesQuery()
                     where !PaymentCheque.IsUpdate

                     join contract in _contractRepository.GetEntitiesQuery() on PaymentCheque.ContractId equals contract.Id into contracts
                     from contract in contracts.DefaultIfEmpty()

                     join bank in _tafziliRepository.GetEntitiesQuery() on PaymentCheque.BankId equals bank.Id into banks
                     from bank in banks.DefaultIfEmpty()

                     join reciver in _tafziliRepository.GetEntitiesQuery() on PaymentCheque.ReciverId equals reciver.Id into recivers
                     from reciver in recivers.DefaultIfEmpty()



                     select new PaymentChequeDTO
                     {
                         Id = PaymentCheque.Id,
                         BusinessId = PaymentCheque.BusinessId,

                         PaymentChequeNumber = PaymentCheque.PaymentChequeNumber,
                         PaymentChequeSayyadiNumber = PaymentCheque.PaymentChequeSayyadiNumber,

                         BankId = PaymentCheque.BankId,
                         BankTitle = bank.TafziliName,

                         ReciverId = PaymentCheque.ReciverId,
                         ReciverTitle = reciver.TafziliName,

                         PaymentChequeDate = PaymentCheque.PaymentChequeDate,
                         PaymentChequeLastState = PaymentCheque.PaymentChequeLastState,
                         IsFirstPeriod = PaymentCheque.IsFirstPeriod,
                         IsGuarantee = PaymentCheque.IsGuarantee,

                         PaymentChequeDescription = PaymentCheque.PaymentChequeDescription,
                         PaymentChequeValue = PaymentCheque.PaymentChequeValue,

                         ContractId = PaymentCheque != null && PaymentCheque.ContractId != null ? PaymentCheque.ContractId : 0,
                         ContractTitle = contract != null && contract.ContractTitle != null ? contract.ContractTitle : "",

                         AccDocumentId = PaymentCheque.AccDocumentId,


                     }).AsQueryable();

                switch (filter.OrderBy)
                {
                    case PaymentChequesOrderBy.CodeAsc:
                        PaymentChequeQuery = PaymentChequeQuery.OrderByDescending(s => s.PaymentChequeDate);
                        break;
                    case PaymentChequesOrderBy.CodeDec:
                        PaymentChequeQuery = PaymentChequeQuery.OrderBy(s => s.PaymentChequeDate);
                        break;
                }

                if (!string.IsNullOrEmpty(filter.PaymentChequeDateStart.ToString()))
                {
                    PaymentChequeQuery = PaymentChequeQuery.Where(A => A.PaymentChequeDate.Value.Date >= filter.PaymentChequeDateStart.Value.Date);
                }

                if (!string.IsNullOrEmpty(filter.PaymentChequeDateEnd.ToString()))
                {
                    PaymentChequeQuery = PaymentChequeQuery.Where(A => filter.PaymentChequeDateEnd.Value.Date >= A.PaymentChequeDate.Value.Date);
                }

                if (filter.PaymentChequeNumber != null)
                {
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.PaymentChequeNumber.Contains(filter.PaymentChequeNumber));
                }



                if (filter.PaymentChequeSayyadiNumber != null)
                {
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.PaymentChequeSayyadiNumber.Contains(filter.PaymentChequeSayyadiNumber));
                }



                if (filter.PaymentChequeLastState.HasValue && filter.PaymentChequeLastState != 0)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.PaymentChequeLastState == filter.PaymentChequeLastState);

                if (filter.BankId.HasValue && filter.BankId != 0)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.BankId == filter.BankId);

                if (filter.ReciverId.HasValue && filter.ReciverId != 0)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.ReciverId == filter.ReciverId);

                if (filter.IsGuarantee == true)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.IsGuarantee == filter.IsGuarantee);

                if (filter.IsFirstPeriod == true)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.IsFirstPeriod == filter.IsFirstPeriod);

                if (filter.ContractId.HasValue && filter.ContractId != 0)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.ContractId == filter.ContractId);


                if (!string.IsNullOrEmpty(filter.PaymentChequeDescription))
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.PaymentChequeDescription.Contains(filter.PaymentChequeDescription));

                if (filter.PaymentChequeValue.HasValue && filter.PaymentChequeValue != 0)
                    PaymentChequeQuery = PaymentChequeQuery.Where(s => s.PaymentChequeValue == filter.PaymentChequeValue);



                var count = (int)Math.Ceiling(PaymentChequeQuery.Count() / (double)filter.TakeEntity);
                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var PaymentCheques = PaymentChequeQuery.Paging(pager).ToList();



                return filter.SetPaymentCheques(PaymentCheques).SetPaging(pager);



            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
