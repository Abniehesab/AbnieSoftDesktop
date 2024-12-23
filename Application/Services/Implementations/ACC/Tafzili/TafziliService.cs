using Application.DTOS.ACC.AccDocmentDetails;
using Application.DTOS.ACC.Kol;
using Application.DTOS.ACC.Tazili;
using Application.Services.Implementations.ACC.Moein;
using Application.Services.Interfaces.ACC.IMoeinTafziliGroup;
using Application.Services.Interfaces.ACC.ITafzili;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using Domain.Entities.FIN;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services.Implementations.ACC.Tafzili
{
    public class TafziliService : ITafziliService
    {
        #region constructor
       

        private IGenericRepository<Domain.Entities.ACC.Tafzili> _TafziliRepository;
        private IGenericRepository<Domain.Entities.ACC.TafziliGroup> _TafziliGroupRepository;
        private IGenericRepository<Domain.Entities.ACC.TafziliType> _TafziliTypeRepository;
        private IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> AccDocmentDetailsRepository;
      
        private IMoeinTafziliGroups _MoeinTafziliGroup;

        public TafziliService(
          
            IGenericRepository<Domain.Entities.ACC.AccDocmentDetails> accDocmentDetailsRepository,
            IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository,
            IGenericRepository<Domain.Entities.ACC.TafziliGroup> tafziliGroupRepository,
            IGenericRepository<Domain.Entities.ACC.TafziliType> tafziliTypeRepository,
         
            IMoeinTafziliGroups moeinTafziliGroups)
        {
        
            _TafziliRepository = tafziliRepository;
            _TafziliGroupRepository = tafziliGroupRepository;
            _TafziliTypeRepository = tafziliTypeRepository;
            AccDocmentDetailsRepository = accDocmentDetailsRepository;
          
            _MoeinTafziliGroup = moeinTafziliGroups;
        }



        #endregion

        #region Tafzili Section
  

        public async Task<FiltercostListDetailsDTO> FilterTafzili(FiltercostListDetailsDTO filter)
        {
            var tafziliQuery = _TafziliRepository.GetEntitiesQuery()                        
                     .Join(
                              _TafziliGroupRepository.GetEntitiesQuery(),
                                 tafzili => tafzili.TafziliGroupId,
                                 tafziliGroup => tafziliGroup.Id,
                                (tafzili, tafziliGroup) => new { Tafzili = tafzili, TafziliGroup = tafziliGroup }
                             )
                     .Join(
                         _TafziliTypeRepository.GetEntitiesQuery(), // Replace with your TafziliType repository
                            tafziliQuery => tafziliQuery.Tafzili.TafziliType,
                            tafziliType => tafziliType.TafziliTypeCode,
                             (tafziliQuery, tafziliType) => new { TafziliQuery = tafziliQuery, TafziliType = tafziliType }
                            ).Select(tafziliQuery => new TafziliDTO
                             {
                                 Id = tafziliQuery.TafziliQuery.Tafzili.Id,
                                 TafziliCode = tafziliQuery.TafziliQuery.Tafzili.TafziliCode,
                                 TafziliName = tafziliQuery.TafziliQuery.Tafzili.TafziliName,
                                 BusinessId = tafziliQuery.TafziliQuery.Tafzili.BusinessId,
                                 TafziliRef = tafziliQuery.TafziliQuery.Tafzili.TafziliRef,
                                 AccTafziliCode = tafziliQuery.TafziliQuery.Tafzili.AccTafziliCode,
                                 Tafzili2 = tafziliQuery.TafziliQuery.Tafzili.Tafzili2,
                                 Tafzili3 = tafziliQuery.TafziliQuery.Tafzili.Tafzili3,

                                 FirstTotalDebtorValue = tafziliQuery.TafziliQuery.Tafzili.FirstTotalDebtorValue,
                                 FirstTotalCreditorValue = tafziliQuery.TafziliQuery.Tafzili.FirstTotalCreditorValue,
                                 TotalDebtorValue = tafziliQuery.TafziliQuery.Tafzili.TotalDebtorValue,
                                 TotalCreditorValue = tafziliQuery.TafziliQuery.Tafzili.TotalCreditorValue,
                                 NatureFinalBalance = tafziliQuery.TafziliQuery.Tafzili.NatureFinalBalance,
                                 Finalbalance = tafziliQuery.TafziliQuery.Tafzili.Finalbalance,


                                 FirstTotalDebtorValueTafzili2 = tafziliQuery.TafziliQuery.Tafzili.FirstTotalDebtorValueTafzili2,
                                 FirstTotalCreditorValueTafzili2 = tafziliQuery.TafziliQuery.Tafzili.FirstTotalCreditorValueTafzili2,
                                 TotalDebtorValueTafzili2 = tafziliQuery.TafziliQuery.Tafzili.TotalDebtorValueTafzili2,
                                 TotalCreditorValueTafzili2 = tafziliQuery.TafziliQuery.Tafzili.TotalCreditorValueTafzili2,
                                 NatureFinalBalanceTafzili2 = tafziliQuery.TafziliQuery.Tafzili.NatureFinalBalanceTafzili2,
                                 FinalbalanceTafzili2 = tafziliQuery.TafziliQuery.Tafzili.FinalbalanceTafzili2,

                                 FirstTotalDebtorValueTafzili3 = tafziliQuery.TafziliQuery.Tafzili.FirstTotalDebtorValueTafzili3,
                                 FirstTotalCreditorValueTafzili3 = tafziliQuery.TafziliQuery.Tafzili.FirstTotalCreditorValueTafzili3,
                                 TotalDebtorValueTafzili3 = tafziliQuery.TafziliQuery.Tafzili.TotalDebtorValueTafzili3,
                                 TotalCreditorValueTafzili3 = tafziliQuery.TafziliQuery.Tafzili.TotalCreditorValueTafzili3,
                                 NatureFinalBalanceTafzili3 = tafziliQuery.TafziliQuery.Tafzili.NatureFinalBalanceTafzili3,
                                 FinalbalanceTafzili3 = tafziliQuery.TafziliQuery.Tafzili.FinalbalanceTafzili3,

                                 TafziliType = tafziliQuery.TafziliType.TafziliTypeCode, // Use TafziliTypeName from TafziliType
                                 TafziliTypeName = tafziliQuery.TafziliType.TafziliTypeName,

                                 TafziliGroupId = tafziliQuery.TafziliQuery.TafziliGroup.Id,
                                 TafziliGroupName = tafziliQuery.TafziliQuery.TafziliGroup.TafziliGroupName,
                             })
                       .AsQueryable();

            switch (filter.OrderBy)
            {
                case TaziliOrderBy.CodeAsc:
                    tafziliQuery = tafziliQuery.OrderBy(s => s.AccTafziliCode);
                    break;
                case TaziliOrderBy.CodeDec:
                    tafziliQuery = tafziliQuery.OrderByDescending(s => s.AccTafziliCode);
                    break;
            }

            if (filter.TafziliGroups != null && filter.TafziliGroups.Any())
            {
                tafziliQuery = tafziliQuery.Where(tafzili => filter.TafziliGroups.Contains(tafzili.TafziliGroupId));
            }
            if (filter.TafziliType.HasValue && filter.TafziliType.Value != 0)
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.TafziliType == filter.TafziliType);
                tafziliQuery = tafziliQuery.OrderByDescending(s => s.Finalbalance);

            }

            if ((filter.TafziliId.HasValue && filter.TafziliId.Value != 0))
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.Id == filter.TafziliId);
            }

            if ((filter.NatureFinalBalance.HasValue && filter.NatureFinalBalance.Value != 0))
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.NatureFinalBalance == filter.NatureFinalBalance);
            }

            if ((filter.FinalBalanceStart.HasValue && filter.FinalBalanceStart.Value != 0))
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.Finalbalance >= filter.FinalBalanceStart);
            }

            if ((filter.FinalBalanceEnd.HasValue && filter.FinalBalanceEnd.Value != 0))
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.Finalbalance <= filter.FinalBalanceEnd);
            }

            if (filter.Tafzili2 == false && filter.Tafzili3 == false)
            {
                tafziliQuery = tafziliQuery.Where(Tafzili => Tafzili.Tafzili2 == false && Tafzili.Tafzili3 == false);
            }

            if (filter.Tafzili2 == true)
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.Tafzili2 == true);
            }

            if (filter.Tafzili3 == true)
            {
                tafziliQuery = tafziliQuery.Where(tafzili => tafzili.Tafzili3 == true);
            }

            if (!string.IsNullOrEmpty(filter.Title))
                tafziliQuery = tafziliQuery.Where(s => s.TafziliName.Contains(filter.Title));

            if (filter.TafziliCode > 0 && filter.TafziliCode != null)

                tafziliQuery = tafziliQuery.Where(s => s.AccTafziliCode == filter.TafziliCode);

            if ((filter.MoeinId == null || filter.MoeinId == 0))
            {
                
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                    {
                        #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                        var TafziliListByDateStart = tafziliQuery.ToList();

                        var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&

                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                        foreach (var tafzili in TafziliListByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByDateStart[i].TafziliId == tafzili.Id)
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
                            tafzili.TotalDebtorValue = totalDebtor;
                            tafzili.TotalCreditorValue = totalCreditor;
                            tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                            tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        TafziliListByDateStart = TafziliListByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var countByDateStart = (int)Math.Ceiling(TafziliListByDateStart.Count() / (double)filter.TakeEntity);

                        var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByDateStart = TafziliListByDateStart.AsEnumerable();
                        var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                        return filter.SetTazilis(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                        #endregion
                    }                                  

                    #region AccDocumentRowDateStart WithOut AccDocumentRowDateEnd
                    var TafziliListByDate = tafziliQuery.ToList();

                    var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date                                                     
                                                      );
                    var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                    foreach (var tafzili in TafziliListByDate)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                        {
                            if (AccDocmentDetailsListByDate[i].TafziliId == tafzili.Id)
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
                        tafzili.TotalDebtorValue = totalDebtor;
                        tafzili.TotalCreditorValue = totalCreditor;
                        tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                        tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    TafziliListByDate = TafziliListByDate
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDate = (int)Math.Ceiling(TafziliListByDate.Count() / (double)filter.TakeEntity);

                    var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDate = TafziliListByDate.AsEnumerable();
                    var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                    return filter.SetTazilis(AccDocmentDetailsByDate).SetPaging(pagerByDate);

                    #endregion

                }



                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                    {
                        #region AccDocumentRowDateStart With AccDocumentRowDateEnd
                        var TafziliListByDateStart = tafziliQuery.ToList();

                        var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                            .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                          AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                          !AccDocmentDetails.IsUpdate &&

                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                          AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                          );
                        var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                        foreach (var tafzili in TafziliListByDateStart)
                        {


                            decimal totalDebtor = 0;
                            decimal totalCreditor = 0;
                            decimal firsttotalDebtor = 0;
                            decimal firsttotalCreditor = 0;

                            for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                            {
                                if (AccDocmentDetailsListByDateStart[i].TafziliId == tafzili.Id)
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
                            tafzili.TotalDebtorValue = totalDebtor;
                            tafzili.TotalCreditorValue = totalCreditor;
                            tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                            tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                            tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                            tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                            // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                        }

                        TafziliListByDateStart = TafziliListByDateStart
                        .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                            .ToList();

                        var countByDateStart = (int)Math.Ceiling(TafziliListByDateStart.Count() / (double)filter.TakeEntity);

                        var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                        var TafziliListEnumerableByDateStart = TafziliListByDateStart.AsEnumerable();
                        var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                        return filter.SetTazilis(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                        #endregion
                    }

                    #region AccDocumentRowDateEnd WithOut AccDocumentRowDateStart
                    var TafziliListByDate = tafziliQuery.ToList();

                    var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                       AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                    foreach (var tafzili in TafziliListByDate)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                        {
                            if (AccDocmentDetailsListByDate[i].TafziliId == tafzili.Id)
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
                        tafzili.TotalDebtorValue = totalDebtor;
                        tafzili.TotalCreditorValue = totalCreditor;
                        tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                        tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    TafziliListByDate = TafziliListByDate
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDate = (int)Math.Ceiling(TafziliListByDate.Count() / (double)filter.TakeEntity);

                    var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDate = TafziliListByDate.AsEnumerable();
                    var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                    return filter.SetTazilis(AccDocmentDetailsByDate).SetPaging(pagerByDate);

                    #endregion

                }


                var count = (int)Math.Ceiling(tafziliQuery.Count() / (double)filter.TakeEntity);

                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var tafzilis = tafziliQuery.Paging(pager).ToList();

                return filter.SetTazilis(tafzilis).SetPaging(pager);

            }



              // با معین 
            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
                {
                    #region AccDocumentRowDateStart With AccDocumentRowDateEnd With Moein
                    var TafziliListByDateStart = tafziliQuery.ToList();

                    var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                    foreach (var tafzili in TafziliListByDateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByDateStart[i].TafziliId == tafzili.Id)
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
                        tafzili.TotalDebtorValue = totalDebtor;
                        tafzili.TotalCreditorValue = totalCreditor;
                        tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                        tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    TafziliListByDateStart = TafziliListByDateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDateStart = (int)Math.Ceiling(TafziliListByDateStart.Count() / (double)filter.TakeEntity);

                    var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateStart = TafziliListByDateStart.AsEnumerable();
                    var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                    return filter.SetTazilis(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                    #endregion
                }

                #region AccDocumentRowDateStart WithOut AccDocumentRowDateEnd With Moein
                var TafziliListByDate = tafziliQuery.ToList();

                var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                  AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                  AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date
                                                  );
                var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                foreach (var tafzili in TafziliListByDate)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                    {
                        if (AccDocmentDetailsListByDate[i].TafziliId == tafzili.Id)
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
                    tafzili.TotalDebtorValue = totalDebtor;
                    tafzili.TotalCreditorValue = totalCreditor;
                    tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                    tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                TafziliListByDate = TafziliListByDate
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var countByDate = (int)Math.Ceiling(TafziliListByDate.Count() / (double)filter.TakeEntity);

                var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByDate = TafziliListByDate.AsEnumerable();
                var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                return filter.SetTazilis(AccDocmentDetailsByDate).SetPaging(pagerByDate);

                #endregion

            }



            if (!string.IsNullOrEmpty(filter.AccDocumentRowDateEnd.ToString()))
            {
                if (!string.IsNullOrEmpty(filter.AccDocumentRowDateStart.ToString()))
                {
                    #region AccDocumentRowDateStart With AccDocumentRowDateEnd With Moein
                    var TafziliListByDateStart = tafziliQuery.ToList();

                    var AccDocmentDetailsQueryByDateStart = AccDocmentDetailsRepository.GetEntitiesQuery()
                        .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                      AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                      !AccDocmentDetails.IsUpdate &&
                                                      AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date >= filter.AccDocumentRowDateStart.Value.Date &&
                                                      AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                      );
                    var AccDocmentDetailsListByDateStart = AccDocmentDetailsQueryByDateStart.ToList();

                    foreach (var tafzili in TafziliListByDateStart)
                    {


                        decimal totalDebtor = 0;
                        decimal totalCreditor = 0;
                        decimal firsttotalDebtor = 0;
                        decimal firsttotalCreditor = 0;

                        for (int i = 0; i < AccDocmentDetailsListByDateStart.Count; i++)
                        {
                            if (AccDocmentDetailsListByDateStart[i].TafziliId == tafzili.Id)
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
                        tafzili.TotalDebtorValue = totalDebtor;
                        tafzili.TotalCreditorValue = totalCreditor;
                        tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                        tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                        tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                        tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                        // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                    }

                    TafziliListByDateStart = TafziliListByDateStart
                    .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                        .ToList();

                    var countByDateStart = (int)Math.Ceiling(TafziliListByDateStart.Count() / (double)filter.TakeEntity);

                    var pagerByDateStart = Pager.Build(countByDateStart, filter.PageId, filter.TakeEntity);

                    var TafziliListEnumerableByDateStart = TafziliListByDateStart.AsEnumerable();
                    var AccDocmentDetailsByDateStart = TafziliListEnumerableByDateStart.Paging(pagerByDateStart).ToList();

                    return filter.SetTazilis(AccDocmentDetailsByDateStart).SetPaging(pagerByDateStart);

                    #endregion
                }

                #region AccDocumentRowDateEnd WithOut AccDocumentRowDateStart With Moein
                var TafziliListByDate = tafziliQuery.ToList();

                var AccDocmentDetailsQueryByDate = AccDocmentDetailsRepository.GetEntitiesQuery()
                    .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                                  AccDocmentDetails.BusinessId == filter.BusinessId &&
                                                  !AccDocmentDetails.IsUpdate &&
                                                   AccDocmentDetails.MoeinId == filter.MoeinId &&
                                                   AccDocmentDetails.AccDocumentRowDate.Value.Date <= filter.AccDocumentRowDateEnd.Value.Date
                                                  );
                var AccDocmentDetailsListByDate = AccDocmentDetailsQueryByDate.ToList();

                foreach (var tafzili in TafziliListByDate)
                {


                    decimal totalDebtor = 0;
                    decimal totalCreditor = 0;
                    decimal firsttotalDebtor = 0;
                    decimal firsttotalCreditor = 0;

                    for (int i = 0; i < AccDocmentDetailsListByDate.Count; i++)
                    {
                        if (AccDocmentDetailsListByDate[i].TafziliId == tafzili.Id)
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
                    tafzili.TotalDebtorValue = totalDebtor;
                    tafzili.TotalCreditorValue = totalCreditor;
                    tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                    tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                    tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                    tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                    // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


                }

                TafziliListByDate = TafziliListByDate
                .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                    .ToList();

                var countByDate = (int)Math.Ceiling(TafziliListByDate.Count() / (double)filter.TakeEntity);

                var pagerByDate = Pager.Build(countByDate, filter.PageId, filter.TakeEntity);

                var TafziliListEnumerableByDate = TafziliListByDate.AsEnumerable();
                var AccDocmentDetailsByDate = TafziliListEnumerableByDate.Paging(pagerByDate).ToList();

                return filter.SetTazilis(AccDocmentDetailsByDate).SetPaging(pagerByDate);

                #endregion

            }
           
            
            var TafziliList = tafziliQuery.ToList();

            var AccDocmentDetailsQuery = AccDocmentDetailsRepository.GetEntitiesQuery()
                .Where(AccDocmentDetails => AccDocmentDetails.IsDelete == false &&
                                              
                                              AccDocmentDetails.MoeinId == filter.MoeinId
                                              
                                              );
            var AccDocmentDetailsList = AccDocmentDetailsQuery.ToList();

            foreach (var tafzili in TafziliList)
            {


                decimal totalDebtor = 0;
                decimal totalCreditor = 0;
                decimal firsttotalDebtor = 0;
                decimal firsttotalCreditor = 0;

                for (int i = 0; i < AccDocmentDetailsList.Count; i++)
                {
                    if (AccDocmentDetailsList[i].TafziliId == tafzili.Id)
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
                tafzili.TotalDebtorValue = totalDebtor;
                tafzili.TotalCreditorValue = totalCreditor;
                tafzili.FirstTotalDebtorValue = firsttotalDebtor;
                tafzili.FirstTotalCreditorValue = firsttotalCreditor;

                tafzili.Finalbalance = Math.Abs((decimal)((tafzili.TotalDebtorValue + tafzili.FirstTotalDebtorValue) - (tafzili.TotalCreditorValue + tafzili.FirstTotalCreditorValue)));
                tafzili.NatureFinalBalance = await CalculateNatureFinalBalance((decimal)tafzili.TotalDebtorValue, (decimal)tafzili.TotalCreditorValue);

                // افزودن کد مربوط به مانده‌های دیگر اگر لازم باشد.


            }

            TafziliList = TafziliList
            .Where(t => t.TotalDebtorValue != 0 || t.TotalCreditorValue != 0 || t.FirstTotalDebtorValue != 0 || t.FirstTotalCreditorValue != 0)
                .ToList();

            var count2 = (int)Math.Ceiling(TafziliList.Count() / (double)filter.TakeEntity);

            var pager2 = Pager.Build(count2, filter.PageId, filter.TakeEntity);

            var TafziliListEnumerable = TafziliList.AsEnumerable();
            var AccDocmentDetails = TafziliListEnumerable.Paging(pager2).ToList();

            return filter.SetTazilis(AccDocmentDetails).SetPaging(pager2);

        }
        public async Task<Domain.Entities.ACC.Tafzili> GetTafzilis(long TafziliId)
        {
            return await _TafziliRepository.GetEntitiesQuery().AsQueryable().
               SingleOrDefaultAsync(tafzili => !tafzili.IsDelete && tafzili.Id == TafziliId);
        }
        public async Task<List<Domain.Entities.ACC.Tafzili>> GetTafziliByBusinessId(long BusinessId)
        {
            return await _TafziliRepository.GetEntitiesQuery()
             .Where(tafzili => tafzili.BusinessId == BusinessId && !tafzili.IsDelete)
             .ToListAsync();
        }
        public async Task<List<Domain.Entities.ACC.Tafzili>> GetTafzili2ByBusinessId(long BusinessId)
        {
            return await _TafziliRepository.GetEntitiesQuery()
            .Where(tafzili => tafzili.BusinessId == BusinessId && !tafzili.IsDelete && tafzili.Tafzili2)
            .ToListAsync();
        }

        public async Task<List<Domain.Entities.ACC.Tafzili>> GetTafzili3ByBusinessId(long BusinessId)
        {
            return await _TafziliRepository.GetEntitiesQuery()
            .Where(tafzili => tafzili.BusinessId == BusinessId && !tafzili.IsDelete && tafzili.Tafzili3)
            .ToListAsync();
        }
        public async Task<long> GetTafziliBy(int TafziliCode, long BusinessId)
        {
            var Tafzilis = await GetTafziliByBusinessId(BusinessId);
            var Tafzili = Tafzilis.Where(Tafzili => Tafzili.TafziliCode == TafziliCode).FirstOrDefault();
            return (long)Tafzili.Id;
        }
        public async Task<bool> ExistsTafziliWithTafziliGroup(long TafziliGroupId)
        {
            return await _TafziliRepository.GetEntitiesQuery()
                 .AnyAsync(Tafzili => Tafzili.TafziliGroupId == TafziliGroupId && !Tafzili.IsDelete);

        }
        public async Task<long?> GetTafziliId(int AccTafziliCode, long BusinessId)
        {
            var Tafzilis = await GetTafziliByBusinessId(BusinessId);
            var Tafzili = Tafzilis.FirstOrDefault(Tafzili => Tafzili.AccTafziliCode == AccTafziliCode);

            // Check if Tafzili is null and return null if not found
            if (Tafzili != null)
            {
                return Tafzili.Id;
            }

            // If Tafzili is not found, return null or handle it as needed
            return null;
        }

        public async Task<bool> ExistsTafziliGroupWithTafzili(long TafziliGroupId)
        {
            return await _TafziliRepository.GetEntitiesQuery()
                .AnyAsync(tafzili => tafzili.TafziliGroupId == TafziliGroupId && !tafzili.IsDelete);
        }
        public async Task<List<Domain.Entities.ACC.Tafzili>> GetTafziliByMoein(List<long> MoeinIds)
        {
            List<long> TafziliGroupId = await _MoeinTafziliGroup.TafziliGroupIdWithMoeinId(MoeinIds);
            // یافتن تمامی Tafzili هایی که در لیست MoeinIds وجود دارند
            var tafziliList = await _TafziliRepository.GetEntitiesQuery()
                .Where(tafzili => !tafzili.IsDelete && TafziliGroupId.Contains((long)tafzili.TafziliGroupId) && !tafzili.Tafzili2 && !tafzili.Tafzili3)
                .ToListAsync();

            return tafziliList;
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



     
     

      


        


    



        #endregion

        #region Dispose
        public void Dispose()
        {
            _TafziliGroupRepository.Dispose();
            _TafziliRepository.Dispose();
            _TafziliTypeRepository.Dispose();
        }


        #endregion
    }
}
