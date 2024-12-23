using Application.DTOS.ACC.AccDocmentDetails;
using Application.DTOS.ACC.Tafzili3;
using Application.Services.Interfaces.ACC.ITafzili3;
using Common.Utilities.Paging;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.ACC.Tafzili3
{
    public class Tafzili3Service : ITafzili3Service
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.Tafzili3> _Tafzili3Repository;
        private IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> AccDocmentDetailsRepository;
        public Tafzili3Service(IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> accDocmentDetailsRepository, IGenericRepository<Domain.Entities.ACC.Tafzili3> tafzili3Repository)
        {
            _Tafzili3Repository = tafzili3Repository;
            AccDocmentDetailsRepository = accDocmentDetailsRepository;
        }

        #endregion

        public async Task<FilterTafzili3DTO> FilterTafzili3(FilterTafzili3DTO filter)
        {
            var tafzili3Query = _Tafzili3Repository.GetEntitiesQuery()    
                              .Select(tafzili3Query => new Tafzili3DTO
                              {
                                  Id = tafzili3Query.Id,
                                  Tafzili3Code = tafzili3Query.Tafzili3Code,
                                  Tafzili3Name = tafzili3Query.Tafzili3Name,
                                  BusinessId = tafzili3Query.BusinessId,


                                  FirstTotalDebtorValue = tafzili3Query.FirstTotalDebtorValue,
                                  FirstTotalCreditorValue = tafzili3Query.FirstTotalCreditorValue,
                                  TotalDebtorValue = tafzili3Query.TotalDebtorValue,
                                  TotalCreditorValue = tafzili3Query.TotalCreditorValue,
                                  NatureFinalBalance = tafzili3Query.NatureFinalBalance,
                                  Finalbalance = tafzili3Query.Finalbalance,



                              })
                        .AsQueryable();

            switch (filter.OrderBy)
            {
                case Tafzili3OrderBy.CodeAsc:
                    tafzili3Query = tafzili3Query.OrderBy(s => s.Tafzili3Code);
                    break;
                case Tafzili3OrderBy.CodeDec:
                    tafzili3Query = tafzili3Query.OrderByDescending(s => s.Tafzili3Code);
                    break;
            }



            if (!string.IsNullOrEmpty(filter.Title))
                tafzili3Query = tafzili3Query.Where(s => s.Tafzili3Name.Contains(filter.Title));

            if (filter.Tafzili3Code > 0 && filter.Tafzili3Code != null)

                tafzili3Query = tafzili3Query.Where(s => s.Tafzili3Code == filter.Tafzili3Code);

            

            if ( filter.Tafzili2Id!!=0 && filter.Tafzili2Id! !=null)
            {
                if(filter.TafziliId != 0 && filter.TafziliId != null)
                {
                    if (filter.MoeinId != 0 && filter.MoeinId != null)
                    {

                        if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                        {
                            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                            {
                                #region Tafzili3
                                var Tafzili3ListByMoeinWithDateStartAndEnd = tafzili3Query.ToList();

                                var AccDocmentDetailsQueryByMoeinWithDateStartAndEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                                  !AccDocmentDetails.IsUpdate &&
                                                                  AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                                  AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                                  AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                                  );
                                var AccDocmentDetailsListByMoeinWithDateStartAndEnd = AccDocmentDetailsQueryByMoeinWithDateStartAndEnd.ToList();

                                foreach (var tafzili3 in Tafzili3ListByMoeinWithDateStartAndEnd)
                                {


                                    decimal totalDebtor = 0;
                                    decimal totalCreditor = 0;
                                    decimal firsttotalDebtor = 0;
                                    decimal firsttotalCreditor = 0;

                                    for (int i = 0; i < AccDocmentDetailsListByMoeinWithDateStartAndEnd.Count; i++)
                                    {
                                        if (AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].Tafzili3Id == tafzili3.Id)
                                        {
                                            if (AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].AccDocumentType == 1)
                                            {
                                                totalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].DebtorValue;
                                                totalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].CreditorValue;
                                            }

                                            if (AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].AccDocumentType == 2)
                                            {
                                                firsttotalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].DebtorValue;
                                                firsttotalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].CreditorValue;
                                            }

                                        }
                                    }

                                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                                    tafzili3.TotalDebtorValue = totalDebtor;
                                    tafzili3.TotalCreditorValue = totalCreditor;
                                    tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                                    tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                                    tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                                    tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                                }

                                Tafzili3ListByMoeinWithDateStartAndEnd = Tafzili3ListByMoeinWithDateStartAndEnd
                                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                                    .ToList();

                                var count2ByMoeinWithDateStartAndEnd = (int)Math.Ceiling(Tafzili3ListByMoeinWithDateStartAndEnd.Count() / (double)filter.TakeEntity);

                                var pager2ByMoeinWithDateStartAndEnd = Pager.Build(count2ByMoeinWithDateStartAndEnd, filter.PageId, filter.TakeEntity);

                                var TafziliListEnumerableByMoeinWithDateStartAndEnd = Tafzili3ListByMoeinWithDateStartAndEnd.AsEnumerable();
                                var AccDocmentDetailsByMoeinWithDateStartAndEnd = TafziliListEnumerableByMoeinWithDateStartAndEnd.Paging(pager2ByMoeinWithDateStartAndEnd).ToList();

                                return filter.SetTafzili3(AccDocmentDetailsByMoeinWithDateStartAndEnd).SetPaging(pager2ByMoeinWithDateStartAndEnd);
                                #endregion
                            }

                            #region Tafzili3
                            var Tafzili3ListByMoeinWithDateEnd = tafzili3Query.ToList();

                            var AccDocmentDetailsQueryByMoeinWithDateEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                                .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                              AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                              !AccDocmentDetails.IsUpdate &&
                                                              AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                              AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                              AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                              AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date 
                                                             
                                                              );
                            var AccDocmentDetailsListByMoeinWithDateEnd = AccDocmentDetailsQueryByMoeinWithDateEnd.ToList();

                            foreach (var tafzili3 in Tafzili3ListByMoeinWithDateEnd)
                            {


                                decimal totalDebtor = 0;
                                decimal totalCreditor = 0;
                                decimal firsttotalDebtor = 0;
                                decimal firsttotalCreditor = 0;

                                for (int i = 0; i < AccDocmentDetailsListByMoeinWithDateEnd.Count; i++)
                                {
                                    if (AccDocmentDetailsListByMoeinWithDateEnd[i].Tafzili3Id == tafzili3.Id)
                                    {
                                        if (AccDocmentDetailsListByMoeinWithDateEnd[i].AccDocumentType == 1)
                                        {
                                            totalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].DebtorValue;
                                            totalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].CreditorValue;
                                        }

                                        if (AccDocmentDetailsListByMoeinWithDateEnd[i].AccDocumentType == 2)
                                        {
                                            firsttotalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].DebtorValue;
                                            firsttotalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].CreditorValue;
                                        }

                                    }
                                }

                                // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                                tafzili3.TotalDebtorValue = totalDebtor;
                                tafzili3.TotalCreditorValue = totalCreditor;
                                tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                                tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                                tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                                tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                                // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                            }

                            Tafzili3ListByMoeinWithDateEnd = Tafzili3ListByMoeinWithDateEnd
                            .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                                .ToList();

                            var count2ByMoeinWithDateEnd = (int)Math.Ceiling(AccDocmentDetailsQueryByMoeinWithDateEnd.Count() / (double)filter.TakeEntity);

                            var pager2ByMoeinWithDateEnd = Pager.Build(count2ByMoeinWithDateEnd, filter.PageId, filter.TakeEntity);

                            var TafziliListEnumerableByMoeinWithDateEnd = Tafzili3ListByMoeinWithDateEnd.AsEnumerable();
                            var AccDocmentDetailsByMoeinWithDateEnd = TafziliListEnumerableByMoeinWithDateEnd.Paging(pager2ByMoeinWithDateEnd).ToList();

                            return filter.SetTafzili3(AccDocmentDetailsByMoeinWithDateEnd).SetPaging(pager2ByMoeinWithDateEnd);
                            #endregion
                        }

                        if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                        {
                            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                            {
                                #region Tafzili3
                                var Tafzili3ListByMoeinWithDateStartAndEnd = tafzili3Query.ToList();

                                var AccDocmentDetailsQueryByMoeinWithDateStartAndEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                                  !AccDocmentDetails.IsUpdate &&
                                                                  AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                                  AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                                  AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                                  );
                                var AccDocmentDetailsListByMoeinWithDateStartAndEnd = AccDocmentDetailsQueryByMoeinWithDateStartAndEnd.ToList();

                                foreach (var tafzili3 in Tafzili3ListByMoeinWithDateStartAndEnd)
                                {


                                    decimal totalDebtor = 0;
                                    decimal totalCreditor = 0;
                                    decimal firsttotalDebtor = 0;
                                    decimal firsttotalCreditor = 0;

                                    for (int i = 0; i < AccDocmentDetailsListByMoeinWithDateStartAndEnd.Count; i++)
                                    {
                                        if (AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].Tafzili3Id == tafzili3.Id)
                                        {
                                            if (AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].AccDocumentType == 1)
                                            {
                                                totalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].DebtorValue;
                                                totalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].CreditorValue;
                                            }

                                            if (AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].AccDocumentType == 2)
                                            {
                                                firsttotalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].DebtorValue;
                                                firsttotalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateStartAndEnd[i].CreditorValue;
                                            }

                                        }
                                    }

                                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                                    tafzili3.TotalDebtorValue = totalDebtor;
                                    tafzili3.TotalCreditorValue = totalCreditor;
                                    tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                                    tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                                    tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                                    tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                                }

                                Tafzili3ListByMoeinWithDateStartAndEnd = Tafzili3ListByMoeinWithDateStartAndEnd
                                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                                    .ToList();

                                var count2ByMoeinWithDateStartAndEnd = (int)Math.Ceiling(Tafzili3ListByMoeinWithDateStartAndEnd.Count() / (double)filter.TakeEntity);

                                var pager2ByMoeinWithDateStartAndEnd = Pager.Build(count2ByMoeinWithDateStartAndEnd, filter.PageId, filter.TakeEntity);

                                var TafziliListEnumerableByMoeinWithDateStartAndEnd = Tafzili3ListByMoeinWithDateStartAndEnd.AsEnumerable();
                                var AccDocmentDetailsByMoeinWithDateStartAndEnd = TafziliListEnumerableByMoeinWithDateStartAndEnd.Paging(pager2ByMoeinWithDateStartAndEnd).ToList();

                                return filter.SetTafzili3(AccDocmentDetailsByMoeinWithDateStartAndEnd).SetPaging(pager2ByMoeinWithDateStartAndEnd);
                                #endregion
                            }

                            #region Tafzili3
                            var Tafzili3ListByMoeinWithDateEnd = tafzili3Query.ToList();

                            var AccDocmentDetailsQueryByMoeinWithDateEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                                .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                              AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                              !AccDocmentDetails.IsUpdate &&
                                                              AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                              AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                              AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                              AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date

                                                              );
                            var AccDocmentDetailsListByMoeinWithDateEnd = AccDocmentDetailsQueryByMoeinWithDateEnd.ToList();

                            foreach (var tafzili3 in Tafzili3ListByMoeinWithDateEnd)
                            {


                                decimal totalDebtor = 0;
                                decimal totalCreditor = 0;
                                decimal firsttotalDebtor = 0;
                                decimal firsttotalCreditor = 0;

                                for (int i = 0; i < AccDocmentDetailsListByMoeinWithDateEnd.Count; i++)
                                {
                                    if (AccDocmentDetailsListByMoeinWithDateEnd[i].Tafzili3Id == tafzili3.Id)
                                    {
                                        if (AccDocmentDetailsListByMoeinWithDateEnd[i].AccDocumentType == 1)
                                        {
                                            totalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].DebtorValue;
                                            totalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].CreditorValue;
                                        }

                                        if (AccDocmentDetailsListByMoeinWithDateEnd[i].AccDocumentType == 2)
                                        {
                                            firsttotalDebtor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].DebtorValue;
                                            firsttotalCreditor += (decimal)AccDocmentDetailsListByMoeinWithDateEnd[i].CreditorValue;
                                        }

                                    }
                                }

                                // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                                tafzili3.TotalDebtorValue = totalDebtor;
                                tafzili3.TotalCreditorValue = totalCreditor;
                                tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                                tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                                tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                                tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                                // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                            }

                            Tafzili3ListByMoeinWithDateEnd = Tafzili3ListByMoeinWithDateEnd
                            .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                                .ToList();

                            var count2ByMoeinWithDateEnd = (int)Math.Ceiling(AccDocmentDetailsQueryByMoeinWithDateEnd.Count() / (double)filter.TakeEntity);

                            var pager2ByMoeinWithDateEnd = Pager.Build(count2ByMoeinWithDateEnd, filter.PageId, filter.TakeEntity);

                            var TafziliListEnumerableByMoeinWithDateEnd = Tafzili3ListByMoeinWithDateEnd.AsEnumerable();
                            var AccDocmentDetailsByMoeinWithDateEnd = TafziliListEnumerableByMoeinWithDateEnd.Paging(pager2ByMoeinWithDateEnd).ToList();

                            return filter.SetTafzili3(AccDocmentDetailsByMoeinWithDateEnd).SetPaging(pager2ByMoeinWithDateEnd);
                            #endregion
                        }

                        #region Tafzili3
                        var Tafzili3ListByMoein = tafzili3Query.ToList();

                        var AccDocmentDetailsQueryByMoein = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&
                                                          AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                          AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                          AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id
                                                          );
                        var AccDocmentDetailsListByMoein = AccDocmentDetailsQueryByMoein.ToList();

                        foreach (var tafzili3 in Tafzili3ListByMoein)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByMoein.Count; i++)
                            {
                                if (AccDocmentDetailsListByMoein[i].Tafzili3Id == tafzili3.Id)
                                {
                                    if (AccDocmentDetailsListByMoein[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByMoein[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByMoein[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByMoein[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByMoein[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByMoein[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili3.TotalDebtorValue = totalDebtor;
                            tafzili3.TotalCreditorValue = totalCreditor;
                            tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                            tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili3ListByMoein = Tafzili3ListByMoein
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var count2ByMoein = (int)Math.Ceiling(Tafzili3ListByMoein.Count() / (double)filter.TakeEntity);

                        var pager2ByMoein = Pager.Build(count2ByMoein, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByMoein = Tafzili3ListByMoein.AsEnumerable();
                        var AccDocmentDetailsByMoein = TafziliListEnumerableByMoein.Paging(pager2ByMoein).ToList();

                        return filter.SetTafzili3(AccDocmentDetailsByMoein).SetPaging(pager2ByMoein);
                        #endregion


                    }

                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                    {
                        if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                        {
                            #region Tafzili3

                            var Tafzili3ListByTafziliByDateStartWithEnd = tafzili3Query.ToList();

                            var AccDocmentDetailsQueryByTafziliByDateStartWithEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                                .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                              AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                              !AccDocmentDetails.IsUpdate &&
                                                              AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                              AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                              AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                              AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date

                                                              );
                            var AccDocmentDetailsListByTafziliByDateStartWithEnd = AccDocmentDetailsQueryByTafziliByDateStartWithEnd.ToList();

                            foreach (var tafzili3 in Tafzili3ListByTafziliByDateStartWithEnd)
                            {


                                decimal totalDebtor = 0;
                                decimal totalCreditor = 0;
                                decimal firsttotalDebtor = 0;
                                decimal firsttotalCreditor = 0;

                                for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStartWithEnd.Count; i++)
                                {
                                    if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].Tafzili3Id == tafzili3.Id)
                                    {
                                        if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 1)
                                        {
                                            totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                            totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                        }

                                        if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 2)
                                        {
                                            firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                            firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                        }

                                    }
                                }

                                // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                                tafzili3.TotalDebtorValue = totalDebtor;
                                tafzili3.TotalCreditorValue = totalCreditor;
                                tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                                tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                                tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                                tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                                // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                            }

                            Tafzili3ListByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd
                            .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                                .ToList();

                            var count2ByTafziliByDateStartWithEnd = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStartWithEnd.Count() / (double)filter.TakeEntity);

                            var pager2ByTafziliByDateStartWithEnd = Pager.Build(count2ByTafziliByDateStartWithEnd, filter.PageId, filter.TakeEntity);

                            var TafziliListEnumerableByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd.AsEnumerable();
                            var AccDocmentDetailsByTafziliByDateStartWithEnd = TafziliListEnumerableByTafziliByDateStartWithEnd.Paging(pager2ByTafziliByDateStartWithEnd).ToList();

                            return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStartWithEnd).SetPaging(pager2ByTafziliByDateStartWithEnd);

                            #endregion
                        }

                        #region Tafzili3

                        var Tafzili3ListByTafziliByDateStart = tafzili3Query.ToList();

                        var AccDocmentDetailsQueryByTafziliByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&
                                                          AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                          AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                           AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                          );
                        var AccDocmentDetailsListByTafziliByDateStart = AccDocmentDetailsQueryByTafziliByDateStart.ToList();

                        foreach (var tafzili3 in Tafzili3ListByTafziliByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByTafziliByDateStart[i].Tafzili3Id == tafzili3.Id)
                                {
                                    if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili3.TotalDebtorValue = totalDebtor;
                            tafzili3.TotalCreditorValue = totalCreditor;
                            tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                            tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili3ListByTafziliByDateStart = Tafzili3ListByTafziliByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var count2ByTafziliByDateStart = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStart.Count() / (double)filter.TakeEntity);

                        var pager2ByTafziliByDateStart = Pager.Build(count2ByTafziliByDateStart, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByTafziliByDateStart = Tafzili3ListByTafziliByDateStart.AsEnumerable();
                        var AccDocmentDetailsByTafziliByDateStart = TafziliListEnumerableByTafziliByDateStart.Paging(pager2ByTafziliByDateStart).ToList();

                        return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStart).SetPaging(pager2ByTafziliByDateStart);

                        #endregion
                    }

                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                    {
                        if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                        {
                            #region Tafzili3

                            var Tafzili3ListByTafziliByDateStartWithEnd = tafzili3Query.ToList();

                            var AccDocmentDetailsQueryByTafziliByDateStartWithEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                                .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                              AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                              !AccDocmentDetails.IsUpdate &&
                                                              AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                              AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                              AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                              AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date

                                                              );
                            var AccDocmentDetailsListByTafziliByDateStartWithEnd = AccDocmentDetailsQueryByTafziliByDateStartWithEnd.ToList();

                            foreach (var tafzili3 in Tafzili3ListByTafziliByDateStartWithEnd)
                            {


                                decimal totalDebtor = 0;
                                decimal totalCreditor = 0;
                                decimal firsttotalDebtor = 0;
                                decimal firsttotalCreditor = 0;

                                for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStartWithEnd.Count; i++)
                                {
                                    if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].Tafzili3Id == tafzili3.Id)
                                    {
                                        if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 1)
                                        {
                                            totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                            totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                        }

                                        if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 2)
                                        {
                                            firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                            firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                        }

                                    }
                                }

                                // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                                tafzili3.TotalDebtorValue = totalDebtor;
                                tafzili3.TotalCreditorValue = totalCreditor;
                                tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                                tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                                tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                                tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                                // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                            }

                            Tafzili3ListByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd
                            .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                                .ToList();

                            var count2ByTafziliByDateStartWithEnd = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStartWithEnd.Count() / (double)filter.TakeEntity);

                            var pager2ByTafziliByDateStartWithEnd = Pager.Build(count2ByTafziliByDateStartWithEnd, filter.PageId, filter.TakeEntity);

                            var TafziliListEnumerableByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd.AsEnumerable();
                            var AccDocmentDetailsByTafziliByDateStartWithEnd = TafziliListEnumerableByTafziliByDateStartWithEnd.Paging(pager2ByTafziliByDateStartWithEnd).ToList();

                            return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStartWithEnd).SetPaging(pager2ByTafziliByDateStartWithEnd);

                            #endregion
                        }

                        #region Tafzili3

                        var Tafzili3ListByTafziliByDateStart = tafzili3Query.ToList();

                        var AccDocmentDetailsQueryByTafziliByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&
                                                          AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                          AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByTafziliByDateStart = AccDocmentDetailsQueryByTafziliByDateStart.ToList();

                        foreach (var tafzili3 in Tafzili3ListByTafziliByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByTafziliByDateStart[i].Tafzili3Id == tafzili3.Id)
                                {
                                    if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili3.TotalDebtorValue = totalDebtor;
                            tafzili3.TotalCreditorValue = totalCreditor;
                            tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                            tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili3ListByTafziliByDateStart = Tafzili3ListByTafziliByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var count2ByTafziliByDateStart = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStart.Count() / (double)filter.TakeEntity);

                        var pager2ByTafziliByDateStart = Pager.Build(count2ByTafziliByDateStart, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByTafziliByDateStart = Tafzili3ListByTafziliByDateStart.AsEnumerable();
                        var AccDocmentDetailsByTafziliByDateStart = TafziliListEnumerableByTafziliByDateStart.Paging(pager2ByTafziliByDateStart).ToList();

                        return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStart).SetPaging(pager2ByTafziliByDateStart);

                        #endregion
                    }

                    #region Tafzili3

                    var Tafzili3ListByTafzili = tafzili3Query.ToList();

                    var AccDocmentDetailsQueryByTafzili = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                      AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id
                                                      );
                    var AccDocmentDetailsListByTafzili = AccDocmentDetailsQueryByTafzili.ToList();

                    foreach (var tafzili3 in Tafzili3ListByTafzili)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByTafzili.Count; i++)
                        {
                            if (AccDocmentDetailsListByTafzili[i].Tafzili3Id == tafzili3.Id)
                            {
                                if (AccDocmentDetailsListByTafzili[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByTafzili[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByTafzili[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByTafzili[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByTafzili[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByTafzili[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili3.TotalDebtorValue = totalDebtor;
                        tafzili3.TotalCreditorValue = totalCreditor;
                        tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                        tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili3ListByTafzili = Tafzili3ListByTafzili
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var count2ByTafzili = (int)Math.Ceiling(Tafzili3ListByTafzili.Count() / (double)filter.TakeEntity);

                    var pager2ByTafzili = Pager.Build(count2ByTafzili, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByTafzili = Tafzili3ListByTafzili.AsEnumerable();
                    var AccDocmentDetailsByTafzili = TafziliListEnumerableByTafzili.Paging(pager2ByTafzili).ToList();

                    return filter.SetTafzili3(AccDocmentDetailsByTafzili).SetPaging(pager2ByTafzili);

                    #endregion

                }


                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                    {
                        #region Tafzili3
                        var Tafzili3ListByTafzili2DateStartWithEnd = tafzili3Query.ToList();

                        var AccDocmentDetailsQueryByTafzili2DateStartWithEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&
                                                          AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByTafzili2DateStartWithEnd = AccDocmentDetailsQueryByTafzili2DateStartWithEnd.ToList();

                        foreach (var tafzili3 in Tafzili3ListByTafzili2DateStartWithEnd)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByTafzili2DateStartWithEnd.Count; i++)
                            {
                                if (AccDocmentDetailsListByTafzili2DateStartWithEnd[i].Tafzili3Id == tafzili3.Id)
                                {
                                    if (AccDocmentDetailsListByTafzili2DateStartWithEnd[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByTafzili2DateStartWithEnd[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili3.TotalDebtorValue = totalDebtor;
                            tafzili3.TotalCreditorValue = totalCreditor;
                            tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                            tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili3ListByTafzili2DateStartWithEnd = Tafzili3ListByTafzili2DateStartWithEnd
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var count2ByTafzili2DateStartWithEnd = (int)Math.Ceiling(Tafzili3ListByTafzili2DateStartWithEnd.Count() / (double)filter.TakeEntity);

                        var pager2ByTafzili2DateStartWithEnd = Pager.Build(count2ByTafzili2DateStartWithEnd, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByTafzili2DateStartWithEnd = Tafzili3ListByTafzili2DateStartWithEnd.AsEnumerable();
                        var AccDocmentDetailsByTafzili2DateStartWithEnd = TafziliListEnumerableByTafzili2DateStartWithEnd.Paging(pager2ByTafzili2DateStartWithEnd).ToList();

                        return filter.SetTafzili3(AccDocmentDetailsByTafzili2DateStartWithEnd).SetPaging(pager2ByTafzili2DateStartWithEnd);

                        #endregion
                    }
                    #region Tafzili3
                    var Tafzili3ListByTafzili2DateStart = tafzili3Query.ToList();

                    var AccDocmentDetailsQueryByTafzili2DateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                       AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                      );
                    var AccDocmentDetailsListByTafzili2DateStart = AccDocmentDetailsQueryByTafzili2DateStart.ToList();

                    foreach (var tafzili3 in Tafzili3ListByTafzili2DateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByTafzili2DateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByTafzili2DateStart[i].Tafzili3Id == tafzili3.Id)
                            {
                                if (AccDocmentDetailsListByTafzili2DateStart[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByTafzili2DateStart[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili3.TotalDebtorValue = totalDebtor;
                        tafzili3.TotalCreditorValue = totalCreditor;
                        tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                        tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili3ListByTafzili2DateStart = Tafzili3ListByTafzili2DateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var count2ByTafzili2DateStart = (int)Math.Ceiling(Tafzili3ListByTafzili2DateStart.Count() / (double)filter.TakeEntity);

                    var pager2ByTafzili2DateStart = Pager.Build(count2ByTafzili2DateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByTafzili2DateStart = Tafzili3ListByTafzili2DateStart.AsEnumerable();
                    var AccDocmentDetailsByTafzili2DateStart = TafziliListEnumerableByTafzili2DateStart.Paging(pager2ByTafzili2DateStart).ToList();

                    return filter.SetTafzili3(AccDocmentDetailsByTafzili2DateStart).SetPaging(pager2ByTafzili2DateStart);

                    #endregion
                }

                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                    {
                        #region Tafzili3
                        var Tafzili3ListByTafzili2DateStartWithEnd = tafzili3Query.ToList();

                        var AccDocmentDetailsQueryByTafzili2DateStartWithEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&
                                                          AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByTafzili2DateStartWithEnd = AccDocmentDetailsQueryByTafzili2DateStartWithEnd.ToList();

                        foreach (var tafzili3 in Tafzili3ListByTafzili2DateStartWithEnd)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByTafzili2DateStartWithEnd.Count; i++)
                            {
                                if (AccDocmentDetailsListByTafzili2DateStartWithEnd[i].Tafzili3Id == tafzili3.Id)
                                {
                                    if (AccDocmentDetailsListByTafzili2DateStartWithEnd[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByTafzili2DateStartWithEnd[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStartWithEnd[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili3.TotalDebtorValue = totalDebtor;
                            tafzili3.TotalCreditorValue = totalCreditor;
                            tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                            tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili3ListByTafzili2DateStartWithEnd = Tafzili3ListByTafzili2DateStartWithEnd
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var count2ByTafzili2DateStartWithEnd = (int)Math.Ceiling(Tafzili3ListByTafzili2DateStartWithEnd.Count() / (double)filter.TakeEntity);

                        var pager2ByTafzili2DateStartWithEnd = Pager.Build(count2ByTafzili2DateStartWithEnd, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByTafzili2DateStartWithEnd = Tafzili3ListByTafzili2DateStartWithEnd.AsEnumerable();
                        var AccDocmentDetailsByTafzili2DateStartWithEnd = TafziliListEnumerableByTafzili2DateStartWithEnd.Paging(pager2ByTafzili2DateStartWithEnd).ToList();

                        return filter.SetTafzili3(AccDocmentDetailsByTafzili2DateStartWithEnd).SetPaging(pager2ByTafzili2DateStartWithEnd);

                        #endregion
                    }
                    #region Tafzili3
                    var Tafzili3ListByTafzili2DateStart = tafzili3Query.ToList();

                    var AccDocmentDetailsQueryByTafzili2DateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByTafzili2DateStart = AccDocmentDetailsQueryByTafzili2DateStart.ToList();

                    foreach (var tafzili3 in Tafzili3ListByTafzili2DateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByTafzili2DateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByTafzili2DateStart[i].Tafzili3Id == tafzili3.Id)
                            {
                                if (AccDocmentDetailsListByTafzili2DateStart[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByTafzili2DateStart[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByTafzili2DateStart[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili3.TotalDebtorValue = totalDebtor;
                        tafzili3.TotalCreditorValue = totalCreditor;
                        tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                        tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili3ListByTafzili2DateStart = Tafzili3ListByTafzili2DateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var count2ByTafzili2DateStart = (int)Math.Ceiling(Tafzili3ListByTafzili2DateStart.Count() / (double)filter.TakeEntity);

                    var pager2ByTafzili2DateStart = Pager.Build(count2ByTafzili2DateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByTafzili2DateStart = Tafzili3ListByTafzili2DateStart.AsEnumerable();
                    var AccDocmentDetailsByTafzili2DateStart = TafziliListEnumerableByTafzili2DateStart.Paging(pager2ByTafzili2DateStart).ToList();

                    return filter.SetTafzili3(AccDocmentDetailsByTafzili2DateStart).SetPaging(pager2ByTafzili2DateStart);

                    #endregion
                }

                #region Tafzili3
                var Tafzili3ListByTafzili2 = tafzili3Query.ToList();

                var AccDocmentDetailsQueryByTafzili2 = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                  AccDocmentDetails.Tafzili2Id == filter.Tafzili2Id);
                var AccDocmentDetailsListByTafzili2 = AccDocmentDetailsQueryByTafzili2.ToList();

                foreach (var tafzili3 in Tafzili3ListByTafzili2)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByTafzili2.Count; i++)
                    {
                        if (AccDocmentDetailsListByTafzili2[i].Tafzili3Id == tafzili3.Id)
                        {
                            if (AccDocmentDetailsListByTafzili2[i].AccDocumentType == 1)
                            {
                                totalDebtor += (decimal)AccDocmentDetailsListByTafzili2[i].DebtorValue;
                                totalCreditor += (decimal)AccDocmentDetailsListByTafzili2[i].CreditorValue;
                            }

                            if (AccDocmentDetailsListByTafzili2[i].AccDocumentType == 2)
                            {
                                firsttotalDebtor += (decimal)AccDocmentDetailsListByTafzili2[i].DebtorValue;
                                firsttotalCreditor += (decimal)AccDocmentDetailsListByTafzili2[i].CreditorValue;
                            }

                        }
                    }

                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                    tafzili3.TotalDebtorValue = totalDebtor;
                    tafzili3.TotalCreditorValue = totalCreditor;
                    tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                    tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                Tafzili3ListByTafzili2 = Tafzili3ListByTafzili2
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var count2ByTafzili2 = (int)Math.Ceiling(Tafzili3ListByTafzili2.Count() / (double)filter.TakeEntity);

                var pager2ByTafzili2 = Pager.Build(count2ByTafzili2, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByTafzili2 = Tafzili3ListByTafzili2.AsEnumerable();
                var AccDocmentDetailsByTafzili2 = TafziliListEnumerableByTafzili2.Paging(pager2ByTafzili2).ToList();

                return filter.SetTafzili3(AccDocmentDetailsByTafzili2).SetPaging(pager2ByTafzili2);



                #endregion

            }

            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    #region Tafzili3

                    var Tafzili3ListByTafziliByDateStartWithEnd = tafzili3Query.ToList();

                    var AccDocmentDetailsQueryByTafziliByDateStartWithEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date

                                                      );
                    var AccDocmentDetailsListByTafziliByDateStartWithEnd = AccDocmentDetailsQueryByTafziliByDateStartWithEnd.ToList();

                    foreach (var tafzili3 in Tafzili3ListByTafziliByDateStartWithEnd)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStartWithEnd.Count; i++)
                        {
                            if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].Tafzili3Id == tafzili3.Id)
                            {
                                if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili3.TotalDebtorValue = totalDebtor;
                        tafzili3.TotalCreditorValue = totalCreditor;
                        tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                        tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili3ListByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var count2ByTafziliByDateStartWithEnd = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStartWithEnd.Count() / (double)filter.TakeEntity);

                    var pager2ByTafziliByDateStartWithEnd = Pager.Build(count2ByTafziliByDateStartWithEnd, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd.AsEnumerable();
                    var AccDocmentDetailsByTafziliByDateStartWithEnd = TafziliListEnumerableByTafziliByDateStartWithEnd.Paging(pager2ByTafziliByDateStartWithEnd).ToList();

                    return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStartWithEnd).SetPaging(pager2ByTafziliByDateStartWithEnd);

                    #endregion
                }

                #region Tafzili3

                var Tafzili3ListByTafziliByDateStart = tafzili3Query.ToList();

                var AccDocmentDetailsQueryByTafziliByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                  
                                                   AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                  );
                var AccDocmentDetailsListByTafziliByDateStart = AccDocmentDetailsQueryByTafziliByDateStart.ToList();

                foreach (var tafzili3 in Tafzili3ListByTafziliByDateStart)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStart.Count; i++)
                    {
                        if (AccDocmentDetailsListByTafziliByDateStart[i].Tafzili3Id == tafzili3.Id)
                        {
                            if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 1)
                            {
                                totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                            }

                            if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 2)
                            {
                                firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                            }

                        }
                    }

                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                    tafzili3.TotalDebtorValue = totalDebtor;
                    tafzili3.TotalCreditorValue = totalCreditor;
                    tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                    tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                Tafzili3ListByTafziliByDateStart = Tafzili3ListByTafziliByDateStart
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var count2ByTafziliByDateStart = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStart.Count() / (double)filter.TakeEntity);

                var pager2ByTafziliByDateStart = Pager.Build(count2ByTafziliByDateStart, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByTafziliByDateStart = Tafzili3ListByTafziliByDateStart.AsEnumerable();
                var AccDocmentDetailsByTafziliByDateStart = TafziliListEnumerableByTafziliByDateStart.Paging(pager2ByTafziliByDateStart).ToList();

                return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStart).SetPaging(pager2ByTafziliByDateStart);

                #endregion
            }

            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    #region Tafzili3

                    var Tafzili3ListByTafziliByDateStartWithEnd = tafzili3Query.ToList();

                    var AccDocmentDetailsQueryByTafziliByDateStartWithEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date

                                                      );
                    var AccDocmentDetailsListByTafziliByDateStartWithEnd = AccDocmentDetailsQueryByTafziliByDateStartWithEnd.ToList();

                    foreach (var tafzili3 in Tafzili3ListByTafziliByDateStartWithEnd)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStartWithEnd.Count; i++)
                        {
                            if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].Tafzili3Id == tafzili3.Id)
                            {
                                if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByTafziliByDateStartWithEnd[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStartWithEnd[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili3.TotalDebtorValue = totalDebtor;
                        tafzili3.TotalCreditorValue = totalCreditor;
                        tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                        tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili3ListByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var count2ByTafziliByDateStartWithEnd = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStartWithEnd.Count() / (double)filter.TakeEntity);

                    var pager2ByTafziliByDateStartWithEnd = Pager.Build(count2ByTafziliByDateStartWithEnd, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByTafziliByDateStartWithEnd = Tafzili3ListByTafziliByDateStartWithEnd.AsEnumerable();
                    var AccDocmentDetailsByTafziliByDateStartWithEnd = TafziliListEnumerableByTafziliByDateStartWithEnd.Paging(pager2ByTafziliByDateStartWithEnd).ToList();

                    return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStartWithEnd).SetPaging(pager2ByTafziliByDateStartWithEnd);

                    #endregion
                }

                #region Tafzili3

                var Tafzili3ListByTafziliByDateStart = tafzili3Query.ToList();

                var AccDocmentDetailsQueryByTafziliByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                 
                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                  );
                var AccDocmentDetailsListByTafziliByDateStart = AccDocmentDetailsQueryByTafziliByDateStart.ToList();

                foreach (var tafzili3 in Tafzili3ListByTafziliByDateStart)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByTafziliByDateStart.Count; i++)
                    {
                        if (AccDocmentDetailsListByTafziliByDateStart[i].Tafzili3Id == tafzili3.Id)
                        {
                            if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 1)
                            {
                                totalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                totalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                            }

                            if (AccDocmentDetailsListByTafziliByDateStart[i].AccDocumentType == 2)
                            {
                                firsttotalDebtor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].DebtorValue;
                                firsttotalCreditor += (decimal)AccDocmentDetailsListByTafziliByDateStart[i].CreditorValue;
                            }

                        }
                    }

                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                    tafzili3.TotalDebtorValue = totalDebtor;
                    tafzili3.TotalCreditorValue = totalCreditor;
                    tafzili3.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili3.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili3.Finalbalance = Math.Abs((decimal)((tafzili3.TotalDebtorValue + tafzili3.FirstTotalDebtorValue) - (tafzili3.TotalCreditorValue + tafzili3.FirstTotalCreditorValue)));
                    tafzili3.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili3.TotalDebtorValue, (decimal)tafzili3.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                Tafzili3ListByTafziliByDateStart = Tafzili3ListByTafziliByDateStart
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var count2ByTafziliByDateStart = (int)Math.Ceiling(Tafzili3ListByTafziliByDateStart.Count() / (double)filter.TakeEntity);

                var pager2ByTafziliByDateStart = Pager.Build(count2ByTafziliByDateStart, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByTafziliByDateStart = Tafzili3ListByTafziliByDateStart.AsEnumerable();
                var AccDocmentDetailsByTafziliByDateStart = TafziliListEnumerableByTafziliByDateStart.Paging(pager2ByTafziliByDateStart).ToList();

                return filter.SetTafzili3(AccDocmentDetailsByTafziliByDateStart).SetPaging(pager2ByTafziliByDateStart);

                #endregion
            }





            var count = (int)Math.Ceiling(tafzili3Query.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var tafzili3s =  tafzili3Query.Paging(pager).ToList();
            return filter.SetTafzili3(tafzili3s).SetPaging(pager);
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



        public async Task<List<Domain.Entities.ACC.Tafzili3>> GetTafzili3ByBusinessId(long BusinessId)
        {
            return await _Tafzili3Repository.GetEntitiesQuery()
             .Where(tafzili3 => tafzili3.BusinessId == BusinessId && !tafzili3.IsDelete)
             .ToListAsync();
        }      


    
        public async Task<Domain.Entities.ACC.Tafzili3> GetTafzili3s(long Tafzili3Id)
        {
            return await _Tafzili3Repository.GetEntitiesQuery().AsQueryable().
              SingleOrDefaultAsync(tafzili3 => !tafzili3.IsDelete && tafzili3.Id == Tafzili3Id);
        }

     


        #region Dispose
        public void Dispose()
        {
            _Tafzili3Repository.Dispose();
            AccDocmentDetailsRepository.Dispose();
        }

        #endregion
    }
}
