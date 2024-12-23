using Application.DTOS.ACC.AccDocmentDetails;
using Application.DTOS.ACC.Tafzili2;
using Application.DTOS.ACC.Tazili;
using Application.Services.Interfaces.ACC.ITafzili2;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.ACC.Tafzili2
{
    public class Tafzili2Service : ITafzili2Service
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.Tafzili2> _Tafzili2Repository;
        private IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> AccDocmentDetailsRepository;
        public Tafzili2Service(IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> accDocmentDetailsRepository,IGenericRepository<Domain.Entities.ACC.Tafzili2> tafzili2Repository)
        {
            _Tafzili2Repository = tafzili2Repository;
            AccDocmentDetailsRepository = accDocmentDetailsRepository;
        }

        #endregion

     
        public async Task<FilterTafzili2DTO> FilterTafzili2(FilterTafzili2DTO filter)
        {
            var tafzili2Query = _Tafzili2Repository.GetEntitiesQuery()                      
                              .Select(tafzili2Query => new Tafzili2DTO
                              {
                                  Id = tafzili2Query.Id,
                                  Tafzili2Code = tafzili2Query.Tafzili2Code,
                                  Tafzili2Name = tafzili2Query.Tafzili2Name,
                                  BusinessId = tafzili2Query.BusinessId,


                                  FirstTotalDebtorValue = tafzili2Query.FirstTotalDebtorValue,
                                  FirstTotalCreditorValue = tafzili2Query.FirstTotalCreditorValue,
                                  TotalDebtorValue = tafzili2Query.TotalDebtorValue,
                                  TotalCreditorValue = tafzili2Query.TotalCreditorValue,
                                  NatureFinalBalance = tafzili2Query.NatureFinalBalance,
                                  Finalbalance = tafzili2Query.Finalbalance,



                              })
                        .AsQueryable();

            switch (filter.OrderBy)
            {
                case Tafzili2OrderBy.CodeAsc:
                    tafzili2Query = tafzili2Query.OrderBy(s => s.Tafzili2Code);
                    break;
                case Tafzili2OrderBy.CodeDec:
                    tafzili2Query = tafzili2Query.OrderByDescending(s => s.Tafzili2Code);
                    break;
            }



            if (!string.IsNullOrEmpty(filter.Title))
                tafzili2Query = tafzili2Query.Where(s => s.Tafzili2Name.Contains(filter.Title));

            if (filter.Tafzili2Code > 0 && filter.Tafzili2Code != null)

                tafzili2Query = tafzili2Query.Where(s => s.Tafzili2Code == filter.Tafzili2Code);


            if ((filter.TafziliId == null || filter.TafziliId == 0))
            {


                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                    {
                        #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                        var Tafzili2ListByDateStart = tafzili2Query.ToList();

                        var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&

                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                        foreach (var tafzili2 in Tafzili2ListByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByDateStart[i].Tafzili2Id == tafzili2.Id)
                                {
                                    if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili2.TotalDebtorValue = totalDebtor;
                            tafzili2.TotalCreditorValue = totalCreditor;
                            tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                            tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili2ListByDateStart = Tafzili2ListByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var count2ByDateStartWithDateEnd = (int)Math.Ceiling(Tafzili2ListByDateStart.Count() / (double)filter.TakeEntity);

                        var pager2ByDateStartWithDateEnd = Pager.Build(count2ByDateStartWithDateEnd, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByDateStartWithDateEnd = Tafzili2ListByDateStart.AsEnumerable();
                        var AccDocmentDetailsByDateStartWithDateEnd = TafziliListEnumerableByDateStartWithDateEnd.Paging(pager2ByDateStartWithDateEnd).ToList();

                        return filter.SetTafzili2(AccDocmentDetailsByDateStartWithDateEnd).SetPaging(pager2ByDateStartWithDateEnd);






                        #endregion
                    }

                    #region AccDocumentRowDateStart WithOut AccDocumentRowDateEnd
                    var Tafzili2ListByDate = tafzili2Query.ToList();

                    var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                      );
                    var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                    foreach (var tafzili2 in Tafzili2ListByDate)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                        {
                            if (AccDocmentDetailsListByDate[i].Tafzili2Id == tafzili2.Id)
                            {
                                if (AccDocmentDetailsListByDate[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByDate[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili2.TotalDebtorValue = totalDebtor;
                        tafzili2.TotalCreditorValue = totalCreditor;
                        tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                        tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili2ListByDate = Tafzili2ListByDate
                      .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                          .ToList();

                    var count2ByDateStart = (int)Math.Ceiling(Tafzili2ListByDate.Count() / (double)filter.TakeEntity);

                    var pager2ByDateStart = Pager.Build(count2ByDateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateStart = Tafzili2ListByDate.AsEnumerable();
                    var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pager2ByDateStart).ToList();

                    return filter.SetTafzili2(AccDocmentDetailsByDateStart).SetPaging(pager2ByDateStart);

                    #endregion

                }



                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                    {
                        #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                        var Tafzili2ListByDateStart = tafzili2Query.ToList();

                        var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&

                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                        foreach (var tafzili2 in Tafzili2ListByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByDateStart[i].Tafzili2Id == tafzili2.Id)
                                {
                                    if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili2.TotalDebtorValue = totalDebtor;
                            tafzili2.TotalCreditorValue = totalCreditor;
                            tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                            tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili2ListByDateStart = Tafzili2ListByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var countByDateStart = (int)Math.Ceiling(Tafzili2ListByDateStart.Count() / (double)filter.TakeEntity);

                        var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByDateStart = Tafzili2ListByDateStart.AsEnumerable();
                        var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                        return filter.SetTafzili2(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                        #endregion
                    }

                    #region AccDocumentRowDateEnd WithOut AccDocumentRowDateStart
                    var Tafzili2ListByDate = tafzili2Query.ToList();

                    var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                       AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                    foreach (var tafzili2 in Tafzili2ListByDate)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                        {
                            if (AccDocmentDetailsListByDate[i].Tafzili2Id == tafzili2.Id)
                            {
                                if (AccDocmentDetailsListByDate[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByDate[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili2.TotalDebtorValue = totalDebtor;
                        tafzili2.TotalCreditorValue = totalCreditor;
                        tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                        tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili2ListByDate = Tafzili2ListByDate
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDate = (int)Math.Ceiling(Tafzili2ListByDate.Count() / (double)filter.TakeEntity);

                    var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDate = Tafzili2ListByDate.AsEnumerable();
                    var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                    return filter.SetTafzili2(AccDocmentDetailsByDate).SetPaging(pagerByDate);

                    #endregion

                }



                var count = (int)Math.Ceiling(tafzili2Query.Count() / (double)filter.TakeEntity);

                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var tafzili2s =  tafzili2Query.Paging(pager).ToList();
                return filter.SetTafzili2(tafzili2s).SetPaging(pager);
            }



            // با معین
            if ((filter.moeinId != null && filter.moeinId != 0))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                    {
                        #region AccDocumentRowDateStart With AccDocumentRowDateEnd With Moein
                        var Tafzili2ListByDateStart2 = tafzili2Query.ToList();

                        var AccDocmentDetailsQueryByDateStart2 = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&
                                                          AccDocmentDetails.MoeinId == filter.moeinId &&
                                                          AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByDateStart2 = AccDocmentDetailsQueryByDateStart2.ToList();

                        foreach (var tafzili2 in Tafzili2ListByDateStart2)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByDateStart2.Count; i++)
                            {
                                if (AccDocmentDetailsListByDateStart2[i].TafziliId == tafzili2.Id)
                                {
                                    if (AccDocmentDetailsListByDateStart2[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByDateStart2[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByDateStart2[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByDateStart2[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart2[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart2[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili2.TotalDebtorValue = totalDebtor;
                            tafzili2.TotalCreditorValue = totalCreditor;
                            tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                            tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili2ListByDateStart2 = Tafzili2ListByDateStart2
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var countByDateStart2 = (int)Math.Ceiling(Tafzili2ListByDateStart2.Count() / (double)filter.TakeEntity);

                        var pagerByDateStart2 = Pager.Build(countByDateStart2, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByDateStart2 = Tafzili2ListByDateStart2.AsEnumerable();
                        var AccDocmentDetailsByDateStart2 = TafziliListEnumerableByDateStart2.Paging(pagerByDateStart2).ToList();

                        return filter.SetTafzili2(AccDocmentDetailsByDateStart2).SetPaging(pagerByDateStart2);

                        #endregion
                    }

                    #region AccDocumentRowDateStart WithOut AccDocumentRowDateEnd With Moein
                    var Tafzili2ListByDateStart = tafzili2Query.ToList();

                    var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.MoeinId == filter.moeinId &&
                                                      AccDocmentDetails.TafziliId==filter.TafziliId &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                      );
                    var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                    foreach (var tafzili2 in Tafzili2ListByDateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByDateStart[i].Tafzili2Id == tafzili2.Id)
                            {
                                if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili2.TotalDebtorValue = totalDebtor;
                        tafzili2.TotalCreditorValue = totalCreditor;
                        tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                        tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili2ListByDateStart = Tafzili2ListByDateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDateStart = (int)Math.Ceiling(Tafzili2ListByDateStart.Count() / (double)filter.TakeEntity);

                    var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateStart = Tafzili2ListByDateStart.AsEnumerable();
                    var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                    return filter.SetTafzili2(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                    #endregion

                }

                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                    {
                        #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                        var Tafzili2ListByDateStart = tafzili2Query.ToList();

                        var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&

                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                        foreach (var tafzili2 in Tafzili2ListByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByDateStart[i].Tafzili2Id == tafzili2.Id)
                                {
                                    if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 1)
                                    {
                                        totalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                        totalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                    }

                                    if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 2)
                                    {
                                        firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                        firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                    }

                                }
                            }

                            // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                            tafzili2.TotalDebtorValue = totalDebtor;
                            tafzili2.TotalCreditorValue = totalCreditor;
                            tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                            tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        Tafzili2ListByDateStart = Tafzili2ListByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var countByDateStart = (int)Math.Ceiling(Tafzili2ListByDateStart.Count() / (double)filter.TakeEntity);

                        var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByDateStart = Tafzili2ListByDateStart.AsEnumerable();
                        var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                        return filter.SetTafzili2(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                        #endregion
                    }

                    #region AccDocumentRowDateEnd WithOut AccDocumentRowDateStart
                    var Tafzili2ListByDateEnd = tafzili2Query.ToList();

                    var AccDocmentDetailsQueryByDateEnd = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                       AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDateEnd = AccDocmentDetailsQueryByDateEnd.ToList();

                    foreach (var tafzili2 in Tafzili2ListByDateEnd)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDateEnd.Count; i++)
                        {
                            if (AccDocmentDetailsListByDateEnd[i].Tafzili2Id == tafzili2.Id)
                            {
                                if (AccDocmentDetailsListByDateEnd[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByDateEnd[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByDateEnd[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByDateEnd[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByDateEnd[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByDateEnd[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili2.TotalDebtorValue = totalDebtor;
                        tafzili2.TotalCreditorValue = totalCreditor;
                        tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                        tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili2ListByDateEnd = Tafzili2ListByDateEnd
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDateEnd = (int)Math.Ceiling(Tafzili2ListByDateEnd.Count() / (double)filter.TakeEntity);

                    var pagerByDateEnd = Pager.Build(countByDateEnd, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateEnd = Tafzili2ListByDateEnd.AsEnumerable();
                    var AccDocmentDetailsByDateEnd = TafziliListEnumerableByDateEnd.Paging(pagerByDateEnd).ToList();

                    return filter.SetTafzili2(AccDocmentDetailsByDateEnd).SetPaging(pagerByDateEnd);

                    #endregion

                }



                var Tafzili2ListByDate = tafzili2Query.ToList();

                var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                  AccDocmentDetails.MoeinId == filter.moeinId
                                                  
                                                  );
                var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                foreach (var tafzili2 in Tafzili2ListByDate)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                    {
                        if (AccDocmentDetailsListByDate[i].Tafzili2Id == tafzili2.Id)
                        {
                            if (AccDocmentDetailsListByDate[i].AccDocumentType == 1)
                            {
                                totalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                totalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                            }

                            if (AccDocmentDetailsListByDate[i].AccDocumentType == 2)
                            {
                                firsttotalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                firsttotalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                            }

                        }
                    }

                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                    tafzili2.TotalDebtorValue = totalDebtor;
                    tafzili2.TotalCreditorValue = totalCreditor;
                    tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                    tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                Tafzili2ListByDate = Tafzili2ListByDate
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var countByDate = (int)Math.Ceiling(Tafzili2ListByDate.Count() / (double)filter.TakeEntity);

                var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByDate = Tafzili2ListByDate.AsEnumerable();
                var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                return filter.SetTafzili2(AccDocmentDetailsByDate).SetPaging(pagerByDate);

             

            }

            // بدون معین و تفضیلی و دارای تاریخ  شروع یا پاین 

            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                    var Tafzili2ListByDateStart = tafzili2Query.ToList();

                    var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                       AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                    foreach (var tafzili2 in Tafzili2ListByDateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByDateStart[i].Tafzili2Id == tafzili2.Id)
                            {
                                if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili2.TotalDebtorValue = totalDebtor;
                        tafzili2.TotalCreditorValue = totalCreditor;
                        tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                        tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili2ListByDateStart = Tafzili2ListByDateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var count2ByDateStartWithDateEnd = (int)Math.Ceiling(Tafzili2ListByDateStart.Count() / (double)filter.TakeEntity);

                    var pager2ByDateStartWithDateEnd = Pager.Build(count2ByDateStartWithDateEnd, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateStartWithDateEnd = Tafzili2ListByDateStart.AsEnumerable();
                    var AccDocmentDetailsByDateStartWithDateEnd = TafziliListEnumerableByDateStartWithDateEnd.Paging(pager2ByDateStartWithDateEnd).ToList();

                    return filter.SetTafzili2(AccDocmentDetailsByDateStartWithDateEnd).SetPaging(pager2ByDateStartWithDateEnd);






                    #endregion
                }

                #region AccDocumentRowDateStart WithOut AccDocumentRowDateEnd
                var Tafzili2ListByDate = tafzili2Query.ToList();

                var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                   AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                  );
                var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                foreach (var tafzili2 in Tafzili2ListByDate)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                    {
                        if (AccDocmentDetailsListByDate[i].Tafzili2Id == tafzili2.Id)
                        {
                            if (AccDocmentDetailsListByDate[i].AccDocumentType == 1)
                            {
                                totalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                totalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                            }

                            if (AccDocmentDetailsListByDate[i].AccDocumentType == 2)
                            {
                                firsttotalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                firsttotalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                            }

                        }
                    }

                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                    tafzili2.TotalDebtorValue = totalDebtor;
                    tafzili2.TotalCreditorValue = totalCreditor;
                    tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                    tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                Tafzili2ListByDate = Tafzili2ListByDate
                  .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                      .ToList();

                var count2ByDateStart = (int)Math.Ceiling(Tafzili2ListByDate.Count() / (double)filter.TakeEntity);

                var pager2ByDateStart = Pager.Build(count2ByDateStart, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByDateStart = Tafzili2ListByDate.AsEnumerable();
                var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pager2ByDateStart).ToList();

                return filter.SetTafzili2(AccDocmentDetailsByDateStart).SetPaging(pager2ByDateStart);

                #endregion

            }



            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                    var Tafzili2ListByDateStart = tafzili2Query.ToList();

                    var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                       AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                    foreach (var tafzili2 in Tafzili2ListByDateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByDateStart[i].Tafzili2Id == tafzili2.Id)
                            {
                                if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 1)
                                {
                                    totalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                    totalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                }

                                if (AccDocmentDetailsListByDateStart[i].AccDocumentType == 2)
                                {
                                    firsttotalDebtor += (decimal)AccDocmentDetailsListByDateStart[i].DebtorValue;
                                    firsttotalCreditor += (decimal)AccDocmentDetailsListByDateStart[i].CreditorValue;
                                }

                            }
                        }

                        // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                        tafzili2.TotalDebtorValue = totalDebtor;
                        tafzili2.TotalCreditorValue = totalCreditor;
                        tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                        tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);
                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    Tafzili2ListByDateStart = Tafzili2ListByDateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDateStart = (int)Math.Ceiling(Tafzili2ListByDateStart.Count() / (double)filter.TakeEntity);

                    var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateStart = Tafzili2ListByDateStart.AsEnumerable();
                    var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                    return filter.SetTafzili2(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                    #endregion
                }

                #region AccDocumentRowDateEnd WithOut AccDocumentRowDateStart
                var Tafzili2ListByDate = tafzili2Query.ToList();

                var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                   AccDocmentDetails.TafziliId == filter.TafziliId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                   AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                  );
                var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                foreach (var tafzili2 in Tafzili2ListByDate)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                    {
                        if (AccDocmentDetailsListByDate[i].Tafzili2Id == tafzili2.Id)
                        {
                            if (AccDocmentDetailsListByDate[i].AccDocumentType == 1)
                            {
                                totalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                totalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                            }

                            if (AccDocmentDetailsListByDate[i].AccDocumentType == 2)
                            {
                                firsttotalDebtor += (decimal)AccDocmentDetailsListByDate[i].DebtorValue;
                                firsttotalCreditor += (decimal)AccDocmentDetailsListByDate[i].CreditorValue;
                            }

                        }
                    }

                    // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                    tafzili2.TotalDebtorValue = totalDebtor;
                    tafzili2.TotalCreditorValue = totalCreditor;
                    tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                    tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                Tafzili2ListByDate = Tafzili2ListByDate
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var countByDate = (int)Math.Ceiling(Tafzili2ListByDate.Count() / (double)filter.TakeEntity);

                var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByDate = Tafzili2ListByDate.AsEnumerable();
                var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                return filter.SetTafzili2(AccDocmentDetailsByDate).SetPaging(pagerByDate);

                #endregion

            }



            var Tafzili2List = tafzili2Query.ToList();

            var AccDocmentDetailsQuery = AccDocmentDetailsRepository.GetEntitiesQuery()
                .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                              AccDocmentDetails.BusinessId == filter.BusinessId &&
                                              !AccDocmentDetails.IsUpdate &&
                                              AccDocmentDetails.TafziliId == filter.TafziliId);
            var AccDocmentDetailsList = AccDocmentDetailsQuery.ToList();

            foreach (var tafzili2 in Tafzili2List)
            {


                decimal totalDebtor = 0;
                decimal totalCreditor = 0;
                decimal firsttotalDebtor = 0;
                decimal firsttotalCreditor = 0;

                for (int i = 0; i < AccDocmentDetailsList.Count; i++)
                {
                    if (AccDocmentDetailsList[i].Tafzili2Id == tafzili2.Id)
                    {
                        if (AccDocmentDetailsList[i].AccDocumentType == 1)
                        {
                            totalDebtor += (decimal)AccDocmentDetailsList[i].DebtorValue;
                            totalCreditor += (decimal)AccDocmentDetailsList[i].CreditorValue;
                        }

                        if (AccDocmentDetailsList[i].AccDocumentType == 2)
                        {
                            firsttotalDebtor += (decimal)AccDocmentDetailsList[i].DebtorValue;
                            firsttotalCreditor += (decimal)AccDocmentDetailsList[i].CreditorValue;
                        }

                    }
                }

                // مانده بدهکار و بستانکار را در تفضیلی جاری تنظیم کنید.
                tafzili2.TotalDebtorValue = totalDebtor;
                tafzili2.TotalCreditorValue = totalCreditor;
                tafzili2.FirstTotalDebtorValue = firsttotalDebtor;
                tafzili2.FirstTotalCreditorValue = firsttotalCreditor;

                tafzili2.Finalbalance = Math.Abs((decimal)((tafzili2.TotalDebtorValue + tafzili2.FirstTotalDebtorValue) - (tafzili2.TotalCreditorValue + tafzili2.FirstTotalCreditorValue)));
                tafzili2.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili2.TotalDebtorValue, (decimal)tafzili2.TotalCreditorValue);

                // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


            }

            Tafzili2List = Tafzili2List
            .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                .ToList();

            var count2 = (int)Math.Ceiling(Tafzili2List.Count() / (double)filter.TakeEntity);

            var pager2 = Pager.Build(count2, filter.PageId, filter.TakeEntity);

            var TafziliListEnumerable = Tafzili2List.AsEnumerable();
            var AccDocmentDetails = TafziliListEnumerable.Paging(pager2).ToList();

            return filter.SetTafzili2(AccDocmentDetails).SetPaging(pager2);

          

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

        public async Task<List<Domain.Entities.ACC.Tafzili2>> GetTafzili2ByBusinessId(long BusinessId)
        {
            return await _Tafzili2Repository.GetEntitiesQuery()
             .Where(tafzili2 => tafzili2.BusinessId == BusinessId && !tafzili2.IsDelete)
             .ToListAsync();
        }


        public async Task<Domain.Entities.ACC.Tafzili2> GetTafzili2s(long Tafzili2Id)
        {
            return await _Tafzili2Repository.GetEntitiesQuery().AsQueryable().
              SingleOrDefaultAsync(tafzili2 => !tafzili2.IsDelete && tafzili2.Id == Tafzili2Id);
        }


        

        #region Dispose
        public void Dispose()
        {
            _Tafzili2Repository.Dispose();
            AccDocmentDetailsRepository.Dispose();
        }


        #endregion
    }
}
