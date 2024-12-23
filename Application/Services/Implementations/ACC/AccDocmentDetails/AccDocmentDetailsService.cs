using Application.DTOS.ACC.AccDocmentDetails;

using Application.Services.Interfaces.ACC.IAccDocmentDetails;
using Common.Utilities.Paging;
using Infrastructure.Repository;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;
using Application.DTOS.ACC.Kol;

namespace Application.Services.Implementations.ACC.AccDocmentDetails
{
    public class AccDocmentDetailsService : IAccDocmentDetailsService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> AccDocmentDetailsRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili> TafziliRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili2> Tafzili2Repository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili3> Tafzili3Repository;

        public AccDocmentDetailsService(IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> accDocmentDetailsRepository, IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository, IGenericRepository<Domain.Entities.ACC.Tafzili2> tafzili2Repository, IGenericRepository<Domain.Entities.ACC.Tafzili3> tafzili3Repository)
        {
            this.AccDocmentDetailsRepository = accDocmentDetailsRepository;
            this.TafziliRepository = tafziliRepository;
            this.Tafzili2Repository = tafzili2Repository;
            this.Tafzili3Repository = tafzili3Repository;
        }


        #endregion

        #region FilterAccDocmentDetails

        public async Task<FilterAccDocmentDetailsDTO> FilterAccDocmentDetails(FilterAccDocmentDetailsDTO filter)
        {
            var AccDocmentDetailsQuery = AccDocmentDetailsRepository.GetEntitiesQuery()           
            .Select((AccDocmentDetailsQuery) => new AccDocmentDetailsDTO
            {
                AccDocumentNumber = (int)AccDocmentDetailsQuery.AccDocumentNumber,
                AccDocumentRowDate = (DateTime)AccDocmentDetailsQuery.AccDocumentRowDate,
                AccDocumentRowNumber = (int)AccDocmentDetailsQuery.AccDocumentRowNumber,
                Inflection = (int)AccDocmentDetailsQuery.Inflection,
                AccDocumentRowDescription = AccDocmentDetailsQuery.AccDocumentRowDescription,

                KolId = AccDocmentDetailsQuery.KolId.ToString(),
               
             

                MoeinId = AccDocmentDetailsQuery.MoeinId,
               

                TafziliId = AccDocmentDetailsQuery.TafziliId,
                

                Tafzili2Id = AccDocmentDetailsQuery.Tafzili2Id,
            

                Tafzili3Id = AccDocmentDetailsQuery.Tafzili3Id,
               

                DebtorValue = (decimal)AccDocmentDetailsQuery.DebtorValue,
                CreditorValue = (decimal)AccDocmentDetailsQuery.CreditorValue,

                BusinessId = AccDocmentDetailsQuery.BusinessId,
                FK_AccDocumentRowLastModifierId = AccDocmentDetailsQuery.FK_AccDocumentRowLastModifierId,
                Id = AccDocmentDetailsQuery.Id,
                TotalDebtorValue = 0,
                TotalCreditorValue = 0,
                Finalbalance = 0,
                NatureFinalBalance = 0

            }).OrderBy(acc => acc.AccDocumentRowDate) // مرتب‌سازی بر اساس تاریخ
             .ThenBy(acc => acc.AccDocumentNumber).AsQueryable(); // مرتب‌سازی بر اساس شماره سند


            


            if (!string.IsNullOrEmpty(filter.AccDocumentRowDescription))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.AccDocumentRowDescription == filter.AccDocumentRowDescription);
            }

            if ((filter.AccDocumentNumber.HasValue && filter.AccDocumentNumber.Value != 0))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.AccDocumentNumber == filter.AccDocumentNumber);
            }

            if ((filter.DebtorValue.HasValue && filter.DebtorValue.Value != 0))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.DebtorValue == filter.DebtorValue);
            }

            if ((filter.CreditorValue.HasValue && filter.CreditorValue.Value != 0))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.CreditorValue == filter.CreditorValue);
            }

            if (!string.IsNullOrEmpty(filter.AccDocumentRowDate.ToString()))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.AccDocumentRowDate == filter.AccDocumentRowDate);
            }

            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date);
            }

            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date);
            }

            if (filter.KolId.HasValue && filter.KolId.Value != 0)
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.KolId == filter.KolId.ToString());
            }

            if (filter.MoeinId.HasValue && filter.MoeinId.Value != 0)
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.MoeinId == filter.MoeinId);
            }

            if (filter.TafziliId.HasValue && filter.TafziliId.Value != 0)
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.TafziliId == filter.TafziliId);
            }

            if (filter.Tafzili2Id.HasValue && filter.Tafzili2Id.Value != 0)
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.Tafzili2Id == filter.Tafzili2Id);
            }

            if (filter.Tafzili3Id.HasValue && filter.Tafzili3Id.Value != 0)
            {
                AccDocmentDetailsQuery = AccDocmentDetailsQuery.Where(A => A.Tafzili3Id == filter.Tafzili3Id);
            }

            if (filter.KolAccountingBook == true)
            {
                var acc1 = AccDocmentDetailsQuery.ToList();
                var ReturnAccDocmentDetailsList = new List<AccDocmentDetailsDTO>();


                var groupedDetails2 = acc1
                 .GroupBy(details => new { details.AccDocumentNumber, details.AccKolCode })
                 .Select(group => new AccDocmentDetailsDTO
                 {

                     KolName = group.FirstOrDefault()?.KolName,
                     AccDocumentNumber = group.Key.AccDocumentNumber,
                     AccKolCode = group.Key.AccKolCode,
                     DebtorValue = group.Sum(details => details.DebtorValue),
                     CreditorValue = 0,
                     AccDocumentRowDate = group.FirstOrDefault()?.AccDocumentRowDate,
                     AccDocumentId=0,
                     Inflection=0,
                     AccDocumentRowDescription="",
                     AccDocumentRowNumber = 0,


                 })
                 .OrderBy(details => details.AccDocumentNumber)
                 .ThenBy(details => details.AccKolCode)
                 .ToList();

                ReturnAccDocmentDetailsList.AddRange(groupedDetails2);

                var groupedDetails3 = acc1
                       .GroupBy(details => new { details.AccDocumentNumber, details.AccKolCode })
                       .Select(group => new AccDocmentDetailsDTO
                       {
                           KolName = group.FirstOrDefault()?.KolName,
                           AccDocumentNumber = group.Key.AccDocumentNumber,
                           AccKolCode = group.Key.AccKolCode,
                           DebtorValue = 0,
                           CreditorValue = group.Sum(details => details.CreditorValue),
                           AccDocumentRowDate = group.FirstOrDefault()?.AccDocumentRowDate,
                           AccDocumentId = 0,
                           Inflection = 0,
                           AccDocumentRowDescription = "",
                           AccDocumentRowNumber = 0,
                       })
                       .OrderBy(details => details.AccDocumentNumber)
                       .ThenBy(details => details.AccKolCode)
                       .ToList();

                ReturnAccDocmentDetailsList.AddRange(groupedDetails3);
                ReturnAccDocmentDetailsList= ReturnAccDocmentDetailsList.Where(details => details.DebtorValue > 0 || details.CreditorValue > 0)
                 .OrderBy(details => details.AccDocumentRowDate)
                 .ThenBy(details => details.AccDocumentNumber)
                 .ToList();

                // مرتب سازی بر اساس نوع چیدمان قبل از محاسبه
                switch (filter.OrderBy)
                {
                    case AccDocumentDetailsOrderBy.CodeAsc:
                        ReturnAccDocmentDetailsList = ReturnAccDocmentDetailsList.OrderBy(s => s.AccDocumentRowDate).ToList();
                        break;
                    case AccDocumentDetailsOrderBy.CodeDec:
                        ReturnAccDocmentDetailsList = ReturnAccDocmentDetailsList.OrderByDescending(s => s.AccDocumentRowDate).ToList();
                        break;
                }

                // محاسبه داده‌های مرتب شده
                var calculatedData1 = await CalculateData(ReturnAccDocmentDetailsList);
                var count1 = (int)Math.Ceiling(calculatedData1.Count / (double)filter.TakeEntity);
                var pager1 = Pager.Build(count1, filter.PageId, filter.TakeEntity);

                var calculatedDataEnumerable1 = calculatedData1.AsEnumerable();
                var AccDocmentDetails1 = calculatedDataEnumerable1.Paging(pager1).ToList();

                return filter.SetAccDocumentDetails(AccDocmentDetails1).SetPaging(pager1);
            }
           
                var acc = AccDocmentDetailsQuery.ToList();

            // مرتب‌سازی داده‌ها برای محاسبات اولیه
            var orderedDataForCalculation = AccDocmentDetailsQuery
                .OrderBy(s => s.AccDocumentRowDate)       // مرتب‌سازی بر اساس تاریخ
                .ThenBy(s => s.AccDocumentNumber)            // مرتب‌سازی بر اساس شماره سند
                .ThenBy(s => s.AccDocumentRowNumber)         // مرتب‌سازی بر اساس شماره سطر سند
                .ToList();

            // محاسبه داده‌های مرتب شده
            var calculatedData = await CalculateData(AccDocmentDetailsQuery.ToList());

            // مرتب سازی بر اساس نوع چیدمان
            switch (filter.OrderBy)
            {
                case AccDocumentDetailsOrderBy.CodeAsc:
                    calculatedData = calculatedData
                     .OrderBy(s => s.AccDocumentRowDate)       // مرتب‌سازی بر اساس تاریخ
                    .ThenBy(s => s.AccDocumentNumber)            // مرتب‌سازی بر اساس شماره سند
                    .ThenBy(s => s.AccDocumentRowNumber)         // مرتب‌سازی بر اساس شماره سطر سند
                    .ToList();
                    break;
                case AccDocumentDetailsOrderBy.CodeDec:
                    calculatedData = calculatedData
                    .OrderByDescending(s => s.AccDocumentRowDate)
                    .ThenByDescending(s => s.AccDocumentNumber)            // مرتب‌سازی بر اساس شماره سند
                    .ThenByDescending(s => s.AccDocumentRowNumber)         // مرتب‌سازی بر اساس شماره سطر سند
                    .ToList();
                    break;
            }

           

            var count = (int)Math.Ceiling(calculatedData.Count / (double)filter.TakeEntity);
                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var calculatedDataEnumerable = calculatedData.AsEnumerable();
                var AccDocmentDetails = calculatedDataEnumerable.Paging(pager).ToList();

                return filter.SetAccDocumentDetails(AccDocmentDetails).SetPaging(pager);
          
         
         
            
          
        }

        public async Task<List<AccDocmentDetailsDTO>> CalculateData(List<AccDocmentDetailsDTO> data)
        {
            decimal previousDebtor = 0;
            decimal previousCreditor = 0;

            foreach (var detail in data)
            {
                // انجام محاسبات مورد نیاز بر روی هر سطر
                detail.TotalDebtorValue = previousDebtor + detail.DebtorValue;
                detail.TotalCreditorValue = previousCreditor + detail.CreditorValue;
                detail.Finalbalance = Math.Abs((decimal)(detail.TotalDebtorValue - detail.TotalCreditorValue));
                detail.NatureFinalBalance =await CalculateNatureFinalBalance((decimal)detail.TotalDebtorValue, (decimal)detail.TotalCreditorValue);

                // به‌روزرسانی مقادیر قبلی برای استفاده در سطر بعدی
                previousDebtor = (decimal)detail.TotalDebtorValue;
                previousCreditor = (decimal)detail.TotalCreditorValue;
            }

            return  data;
        }



        public async Task<int> CalculateNatureFinalBalance(decimal totalDebtor, decimal totalCreditor)
        {
            if (totalDebtor == totalCreditor)
            {
                return 0; // مانده صفر
            }
            else if (totalDebtor > totalCreditor)
            {
                return 1; // مانده 1
            }
            else
            {
                return 2; // مانده 2
            }
        }




        public async Task<List<AccDocumentRowDataDTO>> GetDebtorAndCreditorByKolId(long kolId)
         {

            var detailsList = await EFCore.ToListAsync(AccDocmentDetailsRepository
               .GetEntitiesQuery()
               .Where(acc => acc.KolId == kolId && !acc.IsDelete && !acc.IsUpdate)
               .OrderBy(acc => acc.AccDocumentRowDate) // مرتب‌سازی بر اساس تاریخ
                .ThenBy(acc => acc.AccDocumentRowNumber)); // مرتب‌سازی بر اساس شماره سند


            // ایجاد لیست برای نگهداری اطلاعات
            var result = new List<AccDocumentRowDataDTO>();

            foreach (var detail in detailsList)
            {
                var rowData = new AccDocumentRowDataDTO
                {
                    AccDocumentRowDate= (DateTime)detail.AccDocumentRowDate,
                    AccDocumentRowNumber= (int)detail.AccDocumentRowNumber,
                    AccDocumentNumber= (int)detail.AccDocumentNumber,
                    Inflection= (int)detail.Inflection,
                    DebtorValue = (decimal)detail.DebtorValue,
                    CreditorValue = (decimal)detail.CreditorValue
                };

                result.Add(rowData);
            }

            return result;
        }



        #endregion

        #region Dispose
        public void Dispose()
        {

            AccDocmentDetailsRepository?.Dispose();
          

        }



        #endregion
    }
}
