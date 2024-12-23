using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Persistence.Contexts;
using Domain.Entities.ACC;
using Domain.Entities.FIN;
using Domain.Entities.PRO;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using Application.DTOS.PRO.Contract;
using Application.Services.Implementations.PRO.Contract;
using Application.Services.Interfaces.PRO.IContract;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repository;
using System.Globalization;
using Application.DTOS.ACC.Tazili;
using Application.Services.Interfaces.ACC.ITafzili;
using Application.DTOS.PRO.CostList;
using Application.Services.Interfaces.PRO.ICostListDetails;
using Application.Services.Implementations.PRO.CostListDetails;
using System.Net;
using Application.Services.Interfaces.ACC.IAccDocmentDetails;
using Application.Services.Interfaces.FIN.IReceive;
using Application.Services.Interfaces.FIN.IPayment;
using Application.DTOS.Fin.ReceiveCheque;
using Application.DTOS.Fin.PaymentCheque;
using Application.Services.Interfaces.PRO.IMaterialCirculation;
using Application.DTOS.PRO.Material;
using Application.DTOS.PRO.MaterialCirculation;
using Application.Services.Interfaces.ACC.ITafziliGroup;
using Application.DTOS.ACC.TafziliGroup;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Atf.UI;
using Application.Services.Interfaces.ACC.ITafzili2;
using Application.Services.Interfaces.ACC.ITafzili3;
using Application.Services.Interfaces.PRO.MaterialGroup;
using Application.Services.Interfaces.PRO.IMaterial;
using Application.DTOS.PRO.CostListDetails;
using Application.DTOS.ACC.AccDocmentDetails;
using Application.DTOS.PRO.Store;
using Application.Services.Interfaces.PRO.IStore;
using Application.DTOS.ACC.Tafzili2;
using Application.DTOS.ACC.Tafzili3;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Collections;
using System.Windows.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using HandyControl.Controls;
using DocumentFormat.OpenXml.Vml;




namespace WindowsApp
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly AbnieSoftDbContext _context;

        private IContractService _contractService;
        private ITafziliService _tafziliService;
        private ITafzili2Service _tafzili2Service;
        private ITafzili3Service _tafzili3Service;
        private ICostListDetailsService _costListDetailsService;
        private IAccDocmentDetailsService _accDocmentDetailsService;
        private IReceiveService _receiveService;
        private IPaymentService _paymentService;
        private IMaterialCirculationService _materialCirculationService;
        private IStoreService _storeService;
        private ITafziliGroupService _tafziliGroupService;
        private IMaterialGroupService _materialGroupService;
        private IMaterialService _materialService;

        public MainWindow()
        {
            InitializeComponent();

            // تنظیم وابستگی‌ها به صورت دستی

            _contractService = App.ServiceProvider.GetRequiredService<IContractService>();
            _tafziliService = App.ServiceProvider.GetRequiredService<ITafziliService>();
            _tafzili2Service = App.ServiceProvider.GetRequiredService<ITafzili2Service>();
            _tafzili3Service = App.ServiceProvider.GetRequiredService<ITafzili3Service>();
            _tafziliGroupService = App.ServiceProvider.GetRequiredService<ITafziliGroupService>();
            _costListDetailsService = App.ServiceProvider.GetRequiredService<ICostListDetailsService>();
            _accDocmentDetailsService = App.ServiceProvider.GetRequiredService<IAccDocmentDetailsService>();
            _receiveService = App.ServiceProvider.GetRequiredService<IReceiveService>();
            _paymentService = App.ServiceProvider.GetRequiredService<IPaymentService>();
            _materialCirculationService = App.ServiceProvider.GetRequiredService<IMaterialCirculationService>();
            _materialGroupService = App.ServiceProvider.GetRequiredService<IMaterialGroupService>();
            _materialService = App.ServiceProvider.GetRequiredService<IMaterialService>();
            _storeService = App.ServiceProvider.GetRequiredService<IStoreService>();
            _context = App.ServiceProvider.GetRequiredService<AbnieSoftDbContext>();
            loadData();





        }

        #region JsonFile
        private void LoadJsonFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "انتخاب فایل JSON"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;             

                LoadJsonFile_Click(filePath);
            }
        }
        private void LoadJsonFile_Click(string filePath)
        {
            try
            {
                // خواندن محتوای فایل JSON
                string jsonContent = File.ReadAllText(filePath);
                var rootObject = JsonConvert.DeserializeObject<RootModel>(jsonContent);

                // غیرفعال کردن بررسی محدودیت‌های کلید خارجی
                _context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = OFF;");

                // ابتدا حذف داده‌های قبلی از دیتابیس
                _context.AccDocmentDetails.RemoveRange(_context.AccDocmentDetails);
                _context.Kol.RemoveRange(_context.Kol);
                _context.Moein.RemoveRange(_context.Moein);
                _context.Tafzili.RemoveRange(_context.Tafzili);
                _context.Tafzili2.RemoveRange(_context.Tafzili2);
                _context.Tafzili3.RemoveRange(_context.Tafzili3);
                _context.TafziliGroup.RemoveRange(_context.TafziliGroup);
                _context.TafziliType.RemoveRange(_context.TafziliType);
                _context.MoeinTafziliGroups.RemoveRange(_context.MoeinTafziliGroups);
                _context.Tender.RemoveRange(_context.Tender);
                _context.Contract.RemoveRange(_context.Contract);
                _context.Material.RemoveRange(_context.Material);
                _context.MaterialGroup.RemoveRange(_context.MaterialGroup);
                _context.MaterialUnit.RemoveRange(_context.MaterialUnit);
                _context.ProjectStatusFactor.RemoveRange(_context.ProjectStatusFactor);
                _context.CostList.RemoveRange(_context.CostList);
                _context.CostListDetails.RemoveRange(_context.CostListDetails);
                _context.PaymentCheque.RemoveRange(_context.PaymentCheque);
                _context.ReceiveCheque.RemoveRange(_context.ReceiveCheque);
                _context.Store.RemoveRange(_context.Store);
                _context.MaterialCirculation.RemoveRange(_context.MaterialCirculation);

                // ذخیره تغییرات بعد از حذف
                _context.SaveChanges();

                // افزودن داده‌های جدید به دیتابیس
                foreach (var table in rootObject.Tables)
                {
                    switch (table.TableName)
                    {
                        case "AccDocmentDetails":
                            var accDocmentDetails = JsonConvert.DeserializeObject<List<AccDocmentDetails>>(JsonConvert.SerializeObject(table.Rows));
                            _context.AccDocmentDetails.AddRange(accDocmentDetails);
                            break;
                        case "Kol":
                            var kols = JsonConvert.DeserializeObject<List<Kol>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Kol.AddRange(kols);
                            break;
                        case "Moein":
                            var moeins = JsonConvert.DeserializeObject<List<Moein>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Moein.AddRange(moeins);
                            break;
                        case "Tafzili":
                            var tafzilis = JsonConvert.DeserializeObject<List<Tafzili>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Tafzili.AddRange(tafzilis);
                            break;
                        case "Tafzili2":
                            var tafzili2s = JsonConvert.DeserializeObject<List<Tafzili2>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Tafzili2.AddRange(tafzili2s);
                            break;
                        case "Tafzili3":
                            var tafzili3s = JsonConvert.DeserializeObject<List<Tafzili3>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Tafzili3.AddRange(tafzili3s);
                            break;
                        case "TafziliGroup":
                            var tafziliGroups = JsonConvert.DeserializeObject<List<TafziliGroup>>(JsonConvert.SerializeObject(table.Rows));
                            _context.TafziliGroup.AddRange(tafziliGroups);
                            break;
                        case "TafziliType":
                            var tafziliTypes = JsonConvert.DeserializeObject<List<TafziliType>>(JsonConvert.SerializeObject(table.Rows));
                            _context.TafziliType.AddRange(tafziliTypes);
                            break;
                        case "MoeinTafziliGroups":
                            var moeinTafziliGroups = JsonConvert.DeserializeObject<List<MoeinTafziliGroups>>(JsonConvert.SerializeObject(table.Rows));
                            _context.MoeinTafziliGroups.AddRange(moeinTafziliGroups);
                            break;
                        case "Tender":
                            var tenders = JsonConvert.DeserializeObject<List<Tender>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Tender.AddRange(tenders);
                            break;
                        case "Contract":
                            var contracts = JsonConvert.DeserializeObject<List<Contract>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Contract.AddRange(contracts);
                            break;
                        case "Material":
                            var materials = JsonConvert.DeserializeObject<List<Material>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Material.AddRange(materials);
                            break;
                        case "MaterialGroup":
                            var materialGroups = JsonConvert.DeserializeObject<List<MaterialGroup>>(JsonConvert.SerializeObject(table.Rows));
                            _context.MaterialGroup.AddRange(materialGroups);
                            break;
                        case "MaterialUnit":
                            var materialUnits = JsonConvert.DeserializeObject<List<MaterialUnit>>(JsonConvert.SerializeObject(table.Rows));
                            _context.MaterialUnit.AddRange(materialUnits);
                            break;
                        case "ProjectStatusFactor":
                            var projectStatusFactors = JsonConvert.DeserializeObject<List<ProjectStatusFactor>>(JsonConvert.SerializeObject(table.Rows));
                            _context.ProjectStatusFactor.AddRange(projectStatusFactors);
                            break;
                        case "CostList":
                            var costLists = JsonConvert.DeserializeObject<List<CostList>>(JsonConvert.SerializeObject(table.Rows));
                            _context.CostList.AddRange(costLists);
                            break;
                        case "CostListDetails":
                            var costListDetails = JsonConvert.DeserializeObject<List<CostListDetails>>(JsonConvert.SerializeObject(table.Rows));
                            _context.CostListDetails.AddRange(costListDetails);
                            break;
                        case "PaymentCheque":
                            var paymentCheques = JsonConvert.DeserializeObject<List<PaymentCheque>>(JsonConvert.SerializeObject(table.Rows));
                            _context.PaymentCheque.AddRange(paymentCheques);
                            break;
                        case "ReceiveCheque":
                            var receiveCheques = JsonConvert.DeserializeObject<List<ReceiveCheque>>(JsonConvert.SerializeObject(table.Rows));
                            _context.ReceiveCheque.AddRange(receiveCheques);
                            break;
                        case "Store":
                            var stores = JsonConvert.DeserializeObject<List<Store>>(JsonConvert.SerializeObject(table.Rows));
                            _context.Store.AddRange(stores);
                            break;
                        case "MaterialCirculation":
                            var materialCirculations = JsonConvert.DeserializeObject<List<MaterialCirculation>>(JsonConvert.SerializeObject(table.Rows));
                            _context.MaterialCirculation.AddRange(materialCirculations);
                            break;
                        default:
                            System.Windows.MessageBox.Show($"جدول ناشناخته: {table.TableName}");
                            break;
                    }
                }

                // ذخیره داده‌های جدید در دیتابیس
                _context.SaveChanges();

                // فعال کردن دوباره بررسی محدودیت‌های کلید خارجی
                _context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");

                System.Windows.MessageBox.Show("داده‌ها با موفقیت ذخیره شدند!");
                loadData();

                // راه‌اندازی مجدد نرم‌افزار
                string appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                System.Diagnostics.Process.Start(appPath);

                // خروج از نرم‌افزار جاری
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"خطا در پردازش JSON: {ex.Message}");
            }
        }
        #endregion


        #region loadData
        private async Task loadData()
        {
            LoadContracts();
            LoadPersons();
            LoadBanks();
            LoadInstantPayments();
            LoadCosts();
            LoadIncoms();
            LoadCostListDetailsFilters();
            LoadAccDocumentDetailsFilters();
            LoadReceiveCheque();
            LoadPaymentCheque();
            LoadMaterialCirculationFilters();
        }
        #endregion

        #region Contract
        private async Task LoadContracts()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FilterContractDTO
                {
                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var contractsDto = await _contractService.FilterContract(filter);

                if (contractsDto.Contracts != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < contractsDto.Contracts.Count; i++)
                    {
                        contractsDto.Contracts[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }


                    ProjectsDataGrid.ItemsSource = contractsDto.Contracts; // نمایش داده‌ها در DataGrid


                    // افزودن پروژه ها به لیست پروژه های هزینه ها 

                    // افزودن گزینه "همه پروژه ها" به عنوان آیتم اول در هزینه ها
                    var allContractItem = new ComboBoxItem
                    {
                        Content = "همه پروژه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchProjectInCost.Items.Add(allContractItem);

                    foreach (var group in contractsDto.Contracts)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.ContractTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.MoeinId // شناسه گروه
                        };
                        SearchProjectInCost.Items.Add(comboBoxItem);
                    }

                    // افزودن پروژه ها به لیست پروژه های گردش هزینه ها 
                    foreach (var group in contractsDto.Contracts)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.ContractTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.MoeinId // شناسه گروه
                        };
                        SearchProjectIncostListDetails.Items.Add(comboBoxItem);
                    }

                    // افزودن گزینه "همه پروژه ها" به عنوان آیتم اول
                    var allContractItemInReceiveCheque = new ComboBoxItem
                    {
                        Content = "همه پروژه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchProjectInReceiveCheque.Items.Add(allContractItemInReceiveCheque);

                    // افزودن پروژه ها به لیست پروژه های چک های دریافتی 
                    foreach (var group in contractsDto.Contracts)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.ContractTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchProjectInReceiveCheque.Items.Add(comboBoxItem);
                    }


                    // افزودن گزینه "همه پروژه ها" به عنوان آیتم اول
                    var allContractItemInPaymentCheque = new ComboBoxItem
                    {
                        Content = "همه پروژه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchProjectInPaymentCheque.Items.Add(allContractItemInPaymentCheque);

                    // افزودن پروژه ها به لیست پروژه های چک های دریافتی 
                    foreach (var group in contractsDto.Contracts)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.ContractTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchProjectInPaymentCheque.Items.Add(comboBoxItem);
                    }


                    // افزودن گزینه "همه پروژه ها" به عنوان آیتم اول در مصالح مصرفی
                    var allContractItemInMaterialCirculation = new ComboBoxItem
                    {
                        Content = "همه پروژه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchProjectIncostMaterialCirculation.Items.Add(allContractItemInMaterialCirculation);

                    // افزودن پروژه ها به لیست پروژه های چک های دریافتی 
                    foreach (var group in contractsDto.Contracts)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.ContractTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchProjectIncostMaterialCirculation.Items.Add(comboBoxItem);
                    }


                }
                else
                {
                     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                }

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }
        #endregion

        #region Person


        private async Task LoadPersons()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    TafziliType = 5,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafziliDto = await _tafziliService.FilterTafzili(filter);

                if (tafziliDto.Tafzilis != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < tafziliDto.Tafzilis.Count; i++)
                    {
                        tafziliDto.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }
                    // نمایش داده‌ها در DataGrid
                    PersonsDataGrid.ItemsSource = tafziliDto.Tafzilis;
                }



                var GroupFilter = new FilterTafziliGroupDTO
                {
                    TafziliTypes = new List<long> { 5 },
                    TakeEntity = 10000,
                };
                var tafziliGroupDTO = await _tafziliGroupService.FilterTafziliGroup(GroupFilter);
                if (tafziliGroupDTO.TafziliGroups != null && tafziliGroupDTO.TafziliGroups.Any())
                {
                    // افزودن گزینه "همه اشخاص" به عنوان آیتم اول
                    var allPersonsItem = new ComboBoxItem
                    {
                        Content = "همه اشخاص",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInPerson.Items.Add(allPersonsItem);

                    // افزودن آیتم‌های دیگر به کمبوباکس
                    foreach (var group in tafziliGroupDTO.TafziliGroups)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliGroupName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInPerson.Items.Add(comboBoxItem);
                    }



                    // افزودن گزینه "همه اشخاص" به عنوان آیتم اول
                    var allPersonsItem2 = new ComboBoxItem
                    {
                        Content = "همه اشخاص",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInSuppliercostListDetails.Items.Add(allPersonsItem2);
                    // افزودن اشخاص  به لیست تامین کننده گردش هزینه ها 
                    foreach (var group in tafziliDto.Tafzilis)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInSuppliercostListDetails.Items.Add(comboBoxItem);
                    }


                    // افزودن گزینه "همه اشخاص" به عنوان آیتم اول در گردش کالاعا
                    var allPersonsItem3 = new ComboBoxItem
                    {
                        Content = "همه اشخاص",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInSupplierMaterialCirculation.Items.Add(allPersonsItem3);
                    // افزودن اشخاص  به لیست تامین کننده گردش هزینه ها 
                    foreach (var group in tafziliDto.Tafzilis)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInSupplierMaterialCirculation.Items.Add(comboBoxItem);
                    }
                }



            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersPerson();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد TextBox
        private void TxtfilterdataPerson_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersPerson();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد RadioButton
        private void RadioButtonPerson_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFiltersPerson();
        }

        private async Task ApplyFiltersPerson()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای گروه اشخاص
                var selectedGroupCode = (SearchInPerson.SelectedItem as ComboBoxItem)?.Tag as long?;

                // متن واردشده در TextBox نام شخص
                var searchText = txtfilterdataPerson?.Text?.Trim() ?? string.Empty;

                // مقدار پیش‌فرض NatureFinalBalance (null برای همه)
                int? natureFinalBalance = null;



                // بررسی وضعیت انتخاب شده در RadioButton
                if (DebtorRadioButton.IsChecked == true)
                {
                    natureFinalBalance = 1; // بدهکاران
                }
                else if (CreditorRadioButton.IsChecked == true)
                {
                    natureFinalBalance = 2; // بستانکاران
                }
                else if (ALLRadioButton.IsChecked == true)
                {
                    natureFinalBalance = 0; // همه
                }




                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    Title = !string.IsNullOrEmpty(searchText) ? searchText : null,
                    TafziliGroups = selectedGroupCode.HasValue ? new List<long> { selectedGroupCode.Value } : null,
                    NatureFinalBalance = natureFinalBalance,
                    TafziliType = 5,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _tafziliService.FilterTafzili(filter);

                // نمایش داده‌ها
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.Tafzilis.Count; i++)
                {
                    filteredData.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }
                PersonsDataGrid.ItemsSource = filteredData.Tafzilis ?? new List<TafziliDTO>();

                // نمایش پیام در صورت عدم وجود داده‌ها
                //if (filteredData.Tafzilis == null || !filteredData.Tafzilis.Any())
                //{
                //    ShowToast("هیچ داده‌ای برای نمایش یافت نشد.");
                //}




            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }



        #endregion

        #region Bank

        private async Task LoadBanks()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {

                    TafziliType = 3,
                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafziliDto = await _tafziliService.FilterTafzili(filter);

                if (tafziliDto.Tafzilis != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < tafziliDto.Tafzilis.Count; i++)
                    {
                        tafziliDto.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }

                    BanksDataGrid.ItemsSource = tafziliDto.Tafzilis; // نمایش داده‌ها در DataGrid
                }

                var GroupFilter = new FilterTafziliGroupDTO
                {
                    TafziliTypes = new List<long> { 3 },
                    TakeEntity = 10000,
                };
                var tafziliGroupDTO = await _tafziliGroupService.FilterTafziliGroup(GroupFilter);
                if (tafziliGroupDTO.TafziliGroups != null && tafziliGroupDTO.TafziliGroups.Any())
                {
                    // افزودن گزینه "همه اشخاص" به عنوان آیتم اول
                    var allBankItem = new ComboBoxItem
                    {
                        Content = "همه بانک ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInbank.Items.Add(allBankItem);

                    // افزودن آیتم‌های دیگر به کمبوباکس
                    foreach (var group in tafziliGroupDTO.TafziliGroups)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliGroupName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInbank.Items.Add(comboBoxItem);
                    }
                }

                //else
                //{
                //     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                //}

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInbank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersBank();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد TextBox
        private void Txtfilterdatabank_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersBank();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد RadioButton
        private void RadioButtonbank_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFiltersBank();
        }

        private async Task ApplyFiltersBank()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای گروه اشخاص
                var selectedGroupCode = (SearchInbank.SelectedItem as ComboBoxItem)?.Tag as long?;

                // متن واردشده در TextBox نام شخص
                var searchText = txtfilterdatabank?.Text?.Trim() ?? string.Empty;

                // مقدار پیش‌فرض NatureFinalBalance (null برای همه)
                int? natureFinalBalance = null;



                // بررسی وضعیت انتخاب شده در RadioButton
                if (DebtorRadioButtonbank.IsChecked == true)
                {
                    natureFinalBalance = 1; // بدهکاران
                }
                else if (CreditorRadioButtonbank.IsChecked == true)
                {
                    natureFinalBalance = 2; // بستانکاران
                }
                else if (ALLRadioButtonbank.IsChecked == true)
                {
                    natureFinalBalance = 0; // همه
                }




                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    Title = !string.IsNullOrEmpty(searchText) ? searchText : null,
                    TafziliGroups = selectedGroupCode.HasValue ? new List<long> { selectedGroupCode.Value } : null,
                    NatureFinalBalance = natureFinalBalance,
                    TafziliType = 3,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _tafziliService.FilterTafzili(filter);

                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.Tafzilis.Count; i++)
                {
                    filteredData.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }

                // نمایش داده‌ها
                BanksDataGrid.ItemsSource = filteredData.Tafzilis ?? new List<TafziliDTO>();

                // نمایش پیام در صورت عدم وجود داده‌ها
                //if (filteredData.Tafzilis == null || !filteredData.Tafzilis.Any())
                //{
                //    ShowToast("هیچ داده‌ای برای نمایش یافت نشد.");
                //}




            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }
        #endregion

        #region InstantPayment
        private async Task LoadInstantPayments()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {

                    TafziliType = 2,
                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafziliDto = await _tafziliService.FilterTafzili(filter);

                if (tafziliDto.Tafzilis != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < tafziliDto.Tafzilis.Count; i++)
                    {
                        tafziliDto.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }

                    InstantPaymentDataGrid.ItemsSource = tafziliDto.Tafzilis; // نمایش داده‌ها در DataGrid
                }

                var GroupFilter = new FilterTafziliGroupDTO
                {
                    TafziliTypes = new List<long> { 2 },
                    TakeEntity = 10000,
                };
                var tafziliGroupDTO = await _tafziliGroupService.FilterTafziliGroup(GroupFilter);
                if (tafziliGroupDTO.TafziliGroups != null && tafziliGroupDTO.TafziliGroups.Any())
                {
                    // افزودن گزینه "همه تنخواه ها" به عنوان آیتم اول
                    var allInstantPaymentItem = new ComboBoxItem
                    {
                        Content = "همه تنخواه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInInstantPayment.Items.Add(allInstantPaymentItem);

                    // افزودن آیتم‌های دیگر به کمبوباکس
                    foreach (var group in tafziliGroupDTO.TafziliGroups)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliGroupName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInInstantPayment.Items.Add(comboBoxItem);
                    }
                }
                //else
                //{
                //     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                //}

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInInstantPayment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersInstantPayment();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد TextBox
        private void TxtfilterdataInstantPayment_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersInstantPayment();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد RadioButton
        private void RadioButtonInstantPayment_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFiltersInstantPayment();
        }

        private async Task ApplyFiltersInstantPayment()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای گروه اشخاص
                var selectedGroupCode = (SearchInInstantPayment.SelectedItem as ComboBoxItem)?.Tag as long?;

                // متن واردشده در TextBox نام شخص
                var searchText = txtfilterdataInstantPayment?.Text?.Trim() ?? string.Empty;

                // مقدار پیش‌فرض NatureFinalBalance (null برای همه)
                int? natureFinalBalance = null;



                // بررسی وضعیت انتخاب شده در RadioButton
                if (DebtorRadioButtonInstantPayment.IsChecked == true)
                {
                    natureFinalBalance = 1; // بدهکاران
                }
                else if (CreditorRadioButtonInstantPayment.IsChecked == true)
                {
                    natureFinalBalance = 2; // بستانکاران
                }
                else if (ALLRadioButtonInstantPayment.IsChecked == true)
                {
                    natureFinalBalance = 0; // همه
                }




                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    Title = !string.IsNullOrEmpty(searchText) ? searchText : null,
                    TafziliGroups = selectedGroupCode.HasValue ? new List<long> { selectedGroupCode.Value } : null,
                    NatureFinalBalance = natureFinalBalance,
                    TafziliType = 2,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _tafziliService.FilterTafzili(filter);
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.Tafzilis.Count; i++)
                {
                    filteredData.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }
                // نمایش داده‌ها
                InstantPaymentDataGrid.ItemsSource = filteredData.Tafzilis ?? new List<TafziliDTO>();

                // نمایش پیام در صورت عدم وجود داده‌ها
                //if (filteredData.Tafzilis == null || !filteredData.Tafzilis.Any())
                //{
                //    ShowToast("هیچ داده‌ای برای نمایش یافت نشد.");
                //}




            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }
        #endregion

        #region Cost
        private async Task LoadCosts()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {

                    TafziliType = 7,
                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafziliDto = await _tafziliService.FilterTafzili(filter);

                if (tafziliDto.Tafzilis != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < tafziliDto.Tafzilis.Count; i++)
                    {
                        tafziliDto.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }

                    CostDataGrid.ItemsSource = tafziliDto.Tafzilis; // نمایش داده‌ها در DataGrid
                }

                var GroupFilter = new FilterTafziliGroupDTO
                {
                    TafziliTypes = new List<long> { 7 },
                    TakeEntity = 10000,
                };
                var tafziliGroupDTO = await _tafziliGroupService.FilterTafziliGroup(GroupFilter);
                if (tafziliGroupDTO.TafziliGroups != null && tafziliGroupDTO.TafziliGroups.Any())
                {
                    // افزودن گزینه "همه تنخواه ها" به عنوان آیتم اول
                    var allCostItem = new ComboBoxItem
                    {
                        Content = "همه هزینه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInCost.Items.Add(allCostItem);

                    // افزودن آیتم‌های دیگر به کمبوباکس
                    foreach (var group in tafziliGroupDTO.TafziliGroups)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliGroupName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInCost.Items.Add(comboBoxItem);
                    }



                    // افزودن گزینه "همه تنخواه ها" به عنوان آیتم اول
                    var allCostItem2 = new ComboBoxItem
                    {
                        Content = "همه هزینه ها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    // افزودن گزینه "همه تنخواه ها" به عنوان آیتم اول                   
                    SearchIncostListDetails.Items.Add(allCostItem2);

                    // افزودن اشخاص  به لیست تامین کننده گردش هزینه ها 
                    foreach (var group in tafziliDto.Tafzilis)
                    {

                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchIncostListDetails.Items.Add(comboBoxItem);
                    }
                }



            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersCost();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد TextBox
        private void TxtfilterdataCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersCost();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد RadioButton
        private void RadioButtonCost_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFiltersCost();
        }

        private async Task ApplyFiltersCost()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای گروه اشخاص
                var selectedGroupCode = (SearchInCost.SelectedItem as ComboBoxItem)?.Tag as long?;

                // متن واردشده در TextBox نام شخص
                var searchText = txtfilterdataCost?.Text?.Trim() ?? string.Empty;

                // مقدار پیش‌فرض NatureFinalBalance (null برای همه)
                int? natureFinalBalance = null;

                var selectedProject = (SearchProjectInCost.SelectedItem as ComboBoxItem)?.Tag as long?;

                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    MoeinId = selectedProject,
                    Title = !string.IsNullOrEmpty(searchText) ? searchText : null,
                    TafziliGroups = selectedGroupCode.HasValue ? new List<long> { selectedGroupCode.Value } : null,
                    NatureFinalBalance = natureFinalBalance,
                    TafziliType = 7,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _tafziliService.FilterTafzili(filter);
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.Tafzilis.Count; i++)
                {
                    filteredData.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }

                // نمایش داده‌ها
                CostDataGrid.ItemsSource = filteredData.Tafzilis ?? new List<TafziliDTO>();


                if (filteredData.Tafzilis.Count > 0)
                {
                    double totalCost = 0; // مقداردهی اولیه به totalCost

                    // حلقه برای محاسبه مجموع ستون مبلغ
                    for (int i = 0; i < filteredData.Tafzilis.Count; i++)
                    {
                        // دسترسی به مقدار هر ردیف از ستون "مبلغ"
                        double cost = (double)filteredData.Tafzilis[i].Finalbalance;

                        // اضافه کردن مقدار "مبلغ" به totalCost
                        totalCost += cost;

                        TotalCostLabel.Content = $"مجموع هزینه‌ها: {totalCost:N0}"; // فرمت عدد با هزارگان
                    }


                }
                else
                {

                    TotalCostLabel.Content = $"مجموع هزینه‌ها: 0"; // فرمت عدد با هزارگان
                }



            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }
        #endregion

        #region Incom
        private async Task LoadIncoms()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {

                    TafziliType = 6,
                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafziliDto = await _tafziliService.FilterTafzili(filter);

                if (tafziliDto.Tafzilis != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < tafziliDto.Tafzilis.Count; i++)
                    {
                        tafziliDto.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }

                    IncomDataGrid.ItemsSource = tafziliDto.Tafzilis; // نمایش داده‌ها در DataGrid
                }
                var GroupFilter = new FilterTafziliGroupDTO
                {
                    TafziliTypes = new List<long> { 6 },
                    TakeEntity = 10000,
                };
                var tafziliGroupDTO = await _tafziliGroupService.FilterTafziliGroup(GroupFilter);
                if (tafziliGroupDTO.TafziliGroups != null && tafziliGroupDTO.TafziliGroups.Any())
                {
                    // افزودن گزینه "همه تنخواه ها" به عنوان آیتم اول
                    var allIncomItem = new ComboBoxItem
                    {
                        Content = "همه درآمدها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInIncom.Items.Add(allIncomItem);

                    // افزودن آیتم‌های دیگر به کمبوباکس
                    foreach (var group in tafziliGroupDTO.TafziliGroups)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = group.TafziliGroupName, // فرض بر این است که Title متن مورد نظر است
                            Tag = group.Id // شناسه گروه
                        };
                        SearchInIncom.Items.Add(comboBoxItem);
                    }
                }
                //else
                //{
                //     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش در جدول درآمد وجود ندارد.");
                //}

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }


        // برای رویداد ComboBox
        private async void SearchInIncom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersIncom();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد TextBox
        private void TxtfilterdataIncom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersIncom();  // فراخوانی ApplyFilters بدون پارامتر
        }

        // برای رویداد RadioButton
        private void RadioButtonIncom_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFiltersIncom();
        }

        private async Task ApplyFiltersIncom()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای گروه اشخاص
                var selectedGroupCode = (SearchInIncom.SelectedItem as ComboBoxItem)?.Tag as long?;

                // متن واردشده در TextBox نام شخص
                var searchText = txtfilterdataIncom?.Text?.Trim() ?? string.Empty;

                // مقدار پیش‌فرض NatureFinalBalance (null برای همه)
                int? natureFinalBalance = null;



                // بررسی وضعیت انتخاب شده در RadioButton
                //if (DebtorRadioButtonCost.IsChecked == true)
                //{
                //    natureFinalBalance = 1; // بدهکاران
                //}
                //else if (CreditorRadioButtonCost.IsChecked == true)
                //{
                //    natureFinalBalance = 2; // بستانکاران
                //}
                //else if (ALLRadioButtonCost.IsChecked == true)
                //{
                //    natureFinalBalance = 0; // همه
                //}




                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    Title = !string.IsNullOrEmpty(searchText) ? searchText : null,
                    TafziliGroups = selectedGroupCode.HasValue ? new List<long> { selectedGroupCode.Value } : null,
                    NatureFinalBalance = natureFinalBalance,
                    TafziliType = 6,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _tafziliService.FilterTafzili(filter);
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.Tafzilis.Count; i++)
                {
                    filteredData.Tafzilis[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }

                // نمایش داده‌ها
                IncomDataGrid.ItemsSource = filteredData.Tafzilis ?? new List<TafziliDTO>();

                // نمایش پیام در صورت عدم وجود داده‌ها
                //if (filteredData.Tafzilis == null || !filteredData.Tafzilis.Any())
                //{
                //    ShowToast("هیچ داده‌ای برای نمایش یافت نشد.");
                //}




            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }
        #endregion

        #region CostListDetails

        private async Task LoadCostListDetailsFilters()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filterTafzili2 = new Application.DTOS.ACC.Tafzili2.FilterTafzili2DTO
                {
                    TakeEntity = 100000
                };
                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafzili2Dto = await _tafzili2Service.FilterTafzili2(filterTafzili2);

                if (tafzili2Dto.Tafzili2s != null)
                {
                    // افزودن گزینه "همه مراکز هزینه 1" به عنوان آیتم اول
                    var allCostItem = new ComboBoxItem
                    {
                        Content = "همه مراکز هزینه 1",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchIncostSenter1ListDetails.Items.Add(allCostItem);

                    foreach (var tafzili2 in tafzili2Dto.Tafzili2s)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = tafzili2.Tafzili2Name, // فرض بر این است که Title متن مورد نظر است
                            Tag = tafzili2.Id // شناسه گروه
                        };
                        SearchIncostSenter1ListDetails.Items.Add(comboBoxItem);
                    }
                }


                // ایجاد فیلتر برای درخواست داده‌ها
                var filterTafzili3 = new Application.DTOS.ACC.Tafzili3.FilterTafzili3DTO
                {
                    TakeEntity = 100000
                };
                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafzili3Dto = await _tafzili3Service.FilterTafzili3(filterTafzili3);

                if (tafzili3Dto.Tafzili3s != null)
                {
                    // افزودن گزینه "همه مراکز هزینه 2" به عنوان آیتم اول
                    var allCostItem = new ComboBoxItem
                    {
                        Content = "همه مراکز هزینه 2",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchIncostSenter2ListDetails.Items.Add(allCostItem);

                    // افزودن پروژه ها به لیست پروژه های گردش هزینه ها 
                    foreach (var tafzili3 in tafzili3Dto.Tafzili3s)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = tafzili3.Tafzili3Name, // فرض بر این است که Title متن مورد نظر است
                            Tag = tafzili3.Id // شناسه گروه
                        };
                        SearchIncostSenter2ListDetails.Items.Add(comboBoxItem);
                    }
                }


                // ایجاد فیلتر برای درخواست داده‌ها
                var filterMaterialGroup = new Application.DTOS.PRO.MaterialGroup.FilterMaterialGroupDTO
                {
                    TakeEntity = 100000
                };
                // فراخوانی سرویس برای گرفتن داده‌ها
                var materialGroupDto = await _materialGroupService.FilterMaterialGroup(filterMaterialGroup);

                if (materialGroupDto.MaterialGroups != null)
                {

                    // افزودن گزینه "همه گروه کالاها" به عنوان آیتم اول
                    var allCostItem = new ComboBoxItem
                    {
                        Content = "همه گروه کالاها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchIncostMaterialGroupListDetails.Items.Add(allCostItem);

                    foreach (var materialGroup in materialGroupDto.MaterialGroups)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = materialGroup.MaterialGroupTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = materialGroup.Id // شناسه گروه
                        };
                        SearchIncostMaterialGroupListDetails.Items.Add(comboBoxItem);
                    }



                    // افزودن گزینه "همه گروه کالاها" به عنوان آیتم اول در گردش کالاها
                    var allCostItem2 = new ComboBoxItem
                    {
                        Content = "همه گروه کالاها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchIncostMaterialGroupMaterialCirculation.Items.Add(allCostItem2);

                    foreach (var materialGroup in materialGroupDto.MaterialGroups)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = materialGroup.MaterialGroupTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = materialGroup.Id // شناسه گروه
                        };
                        SearchIncostMaterialGroupMaterialCirculation.Items.Add(comboBoxItem);
                    }
                }

                // ایجاد فیلتر برای درخواست داده‌ها
                var filterMaterial = new Application.DTOS.PRO.Material.FilterMaterialDTO
                {
                    TakeEntity = 100000
                };
                // فراخوانی سرویس برای گرفتن داده‌ها
                var materialDto = await _materialService.FilterMaterial(filterMaterial);

                if (materialDto.Materials != null)
                {

                    // افزودن گزینه "همه گروه کالاها" به عنوان آیتم اول
                    var allCostItem = new ComboBoxItem
                    {
                        Content = "همه کالاها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInMaterialListDetails.Items.Add(allCostItem);
                    // افزودن پروژه ها به لیست پروژه های گردش هزینه ها 
                    foreach (var material in materialDto.Materials)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = material.MaterialTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = material.Id // شناسه گروه
                        };
                        SearchInMaterialListDetails.Items.Add(comboBoxItem);
                    }

                    // افزودن گزینه "همه گروه کالاها" به عنوان آیتم اول
                    var allCostItem2 = new ComboBoxItem
                    {
                        Content = "همه کالاها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInMaterialCirculation.Items.Add(allCostItem2);
                    // افزودن پروژه ها به لیست پروژه های گردش هزینه ها 
                    foreach (var material in materialDto.Materials)
                    {
                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = material.MaterialTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = material.Id // شناسه گروه
                        };
                        SearchInMaterialCirculation.Items.Add(comboBoxItem);
                    }
                }


            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }
        private async void LoadCostListDetails(object sender, RoutedEventArgs e)
        {
            try
            {


                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new Application.DTOS.PRO.CostListDetails.FilterCostListDetailsDTO
                {

                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var costListDetailsDto = await _costListDetailsService.FilterCostListDetails(filter);

                if (costListDetailsDto.CostListDetails != null)
                {

                    costListDetailsGrid.ItemsSource = costListDetailsDto.CostListDetails; // نمایش داده‌ها در DataGrid
                }
                else
                {
                     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                }

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInCostListDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersCostListDetails();  // فراخوانی ApplyFilters بدون پارامتر
        }

        private async Task ApplyFiltersCostListDetails()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای پروژه
                var selectedProject = (SearchProjectIncostListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;



                // مقدار انتخاب‌شده از کمبوباکس برای هزینه
                var selectedCost = (SearchIncostListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;


                // مقدار انتخاب‌شده از کمبوباکس برای مرکز هزینه1
                var selectedCostSenter1 = (SearchIncostSenter1ListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای مرکز هزینه2
                var selectedCostSenter2 = (SearchIncostSenter2ListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای تامین کننده
                var selectedSupplier = (SearchInSuppliercostListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای گروه کالا
                var selectedMaterialGroup = (SearchIncostMaterialGroupListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای  کالا
                var selectedMaterial = (SearchInMaterialListDetails.SelectedItem as ComboBoxItem)?.Tag as long?;

                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new Application.DTOS.PRO.CostListDetails.FilterCostListDetailsDTO
                {

                    MoeinId = selectedProject,
                    TafziliId = selectedCost,
                    Tafzili2Id = selectedCostSenter1,
                    Tafzili3Id = selectedCostSenter2,
                    SupplierId = selectedSupplier,
                    MaterialGroupId = selectedMaterialGroup,
                    MaterialId = selectedMaterial,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _costListDetailsService.FilterCostListDetails(filter);
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.CostListDetails.Count; i++)
                {
                    filteredData.CostListDetails[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }

                // نمایش داده‌ها
                costListDetailsGrid.ItemsSource = filteredData.CostListDetails ?? new List<CostListDetailsDTO>();

                if (filteredData.CostListDetails.Count > 0)
                {
                    double totalCost = 0; // مقداردهی اولیه به totalCost

                    // حلقه برای محاسبه مجموع ستون مبلغ
                    for (int i = 0; i < filteredData.CostListDetails.Count; i++)
                    {
                        // دسترسی به مقدار هر ردیف از ستون "مبلغ"
                        double cost = (double)filteredData.CostListDetails[i].CostListRowfinalValue;

                        // اضافه کردن مقدار "مبلغ" به totalCost
                        totalCost += cost;

                        TotalCostDetailsLabel.Content = $"مجموع هزینه‌ها: {totalCost:N0}"; // فرمت عدد با هزارگان
                    }

                }
                else
                {
                    TotalCostDetailsLabel.Content = $"مجموع هزینه‌ها: 0"; // فرمت عدد با هزارگان
                }



            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }

        #endregion

        #region AccDocumentDetails
        private async Task LoadAccDocumentDetailsFilters()
        {
            try
            {

                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FiltercostListDetailsDTO
                {
                    TakeEntity = 100000
                };

                // فراخوانی سرویس برای گرفتن داده‌ها
                var tafziliDto = await _tafziliService.FilterTafzili(filter);

                if (tafziliDto.Tafzilis != null)
                {
                    foreach (var tafzili in tafziliDto.Tafzilis)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = tafzili.TafziliName, // فرض بر این است که Title متن مورد نظر است
                            Tag = tafzili.Id // شناسه گروه
                        };
                        SearchInvoice.Items.Add(comboBoxItem);
                    }
                }

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        private async Task LoadAccDocumentDetails()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new Application.DTOS.ACC.AccDocmentDetails.FilterAccDocmentDetailsDTO
                {
                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var accDocumentDetailsDto = await _accDocmentDetailsService.FilterAccDocmentDetails(filter);

                if (accDocumentDetailsDto.Invoices != null)
                {

                    accDocumentDetailsGrid.ItemsSource = accDocumentDetailsDto.Invoices; // نمایش داده‌ها در DataGrid
                }
                else
                {
                     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                }

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInInvice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersInvoice();  // فراخوانی ApplyFilters بدون پارامتر
        }

        private async Task ApplyFiltersInvoice()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای پروژه
                var selectedTafzili = (SearchInvoice.SelectedItem as ComboBoxItem)?.Tag as long?;


                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new Application.DTOS.ACC.AccDocmentDetails.FilterAccDocmentDetailsDTO
                {


                    TafziliId = selectedTafzili,
                    OrderBy = (AccDocumentDetailsOrderBy?)1,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _accDocmentDetailsService.FilterAccDocmentDetails(filter);
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.Invoices.Count; i++)
                {
                    filteredData.Invoices[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }

                // نمایش داده‌ها
                accDocumentDetailsGrid.ItemsSource = filteredData.Invoices ?? new List<AccDocmentDetailsDTO>();





            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }

        #endregion

        #region ReceiveCheque
        private async void SearchInReceiveCheque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await LoadReceiveCheque();  // فراخوانی ApplyFilters بدون پارامتر
        }
        private void TxtSearchInReceiveCheque_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadReceiveCheque();  // فراخوانی ApplyFilters بدون پارامتر
        }
        private async Task LoadReceiveCheque()
        {
            try
            {
                var selectedItemType = SearchReceiveCheque.SelectedItem as ComboBoxItem;
                var selectedItemProject = SearchProjectInReceiveCheque.SelectedItem as ComboBoxItem;
                // متن واردشده در TextBox نام شخص
                var searchText = txtSearchReceiveChequeNumberInReceiveCheque.Text;

                int? receiveChequeLastState = null;
                int? receiveChequeProject = null;
              

                if (selectedItemType?.Tag != null && int.TryParse(selectedItemType.Tag.ToString(), out int tagValue))
                {
                    receiveChequeLastState = tagValue;
                }

                if (selectedItemProject?.Tag != null && int.TryParse(selectedItemProject.Tag.ToString(), out int tagValueProject))
                {
                    receiveChequeProject = tagValueProject;
                }
               

                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FilterReceiveChequeDTO
                {
                    ReceiveChequeNumber = searchText,
                    ReceiveChequeLastState = receiveChequeLastState,
                    ContractId= receiveChequeProject,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس برای گرفتن داده‌ها
                var receiveChequeDTO = await _receiveService.FilterReceiveCheques(filter);

                if (receiveChequeDTO.ReceiveCheques != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < receiveChequeDTO.ReceiveCheques.Count; i++)
                    {
                        receiveChequeDTO.ReceiveCheques[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }


                    receiveChequeGrid.ItemsSource = receiveChequeDTO.ReceiveCheques; // نمایش داده‌ها در DataGrid
                }
                else
                {
                     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                }

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }
        #endregion

        #region ReceiveCheque
        private async void SearchInPaymentCheque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await LoadPaymentCheque();  // فراخوانی ApplyFilters بدون پارامتر
        }
        private void TxtSearchInPaymentCheque_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadPaymentCheque();  // فراخوانی ApplyFilters بدون پارامتر
        }
        private async Task LoadPaymentCheque()
        {
            try
            {
                var selectedItemType = SearchPaymentCheque.SelectedItem as ComboBoxItem;
                var selectedItemProject = SearchProjectInPaymentCheque.SelectedItem as ComboBoxItem;
                // متن واردشده در TextBox نام شخص
                var searchText = txtSearchPaymentChequeNumberInPaymentCheque.Text;

                int? paymentChequeLastState = null;
                int? paymentChequeProject = null;


                if (selectedItemType?.Tag != null && int.TryParse(selectedItemType.Tag.ToString(), out int tagValue))
                {
                    paymentChequeLastState = tagValue;
                }

                if (selectedItemProject?.Tag != null && int.TryParse(selectedItemProject.Tag.ToString(), out int tagValueProject))
                {
                    paymentChequeProject = tagValueProject;
                }


                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FilterPaymentChequeDTO
                {
                    PaymentChequeNumber = searchText,
                    PaymentChequeLastState = paymentChequeLastState,
                    ContractId = paymentChequeProject,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس برای گرفتن داده‌ها
                var paymentChequeDTO = await _paymentService.FilterPaymentCheques(filter);

                if (paymentChequeDTO.PaymentCheques != null)
                {
                    // اضافه کردن شماره ردیف به هر رکورد در لیست
                    for (int i = 0; i < paymentChequeDTO.PaymentCheques.Count; i++)
                    {
                        paymentChequeDTO.PaymentCheques[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                    }


                    paymentChequeGrid.ItemsSource = paymentChequeDTO.PaymentCheques; // نمایش داده‌ها در DataGrid
                }
                else
                {
                     System.Windows.MessageBox.Show("هیچ داده‌ای برای نمایش وجود ندارد.");
                }

            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }
        #endregion


        #region MaterialCirculation

        private async Task LoadMaterialCirculationFilters()
        {
            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new FilterStoreDTO
                {

                    TakeEntity = 100000
                };


                // فراخوانی سرویس برای گرفتن داده‌ها
                var filterDTO = await _storeService.FilterStore(filter);

                if (filterDTO.Stores != null)
                {

                    // افزودن گزینه "همه مراکز هزینه 1" به عنوان آیتم اول
                    var allStoreItem = new ComboBoxItem
                    {
                        Content = "همه انبارها",
                        Tag = null // یا یک مقدار خاص که نشان‌دهنده همه باشد
                    };
                    SearchInStoreMaterialCirculation.Items.Add(allStoreItem);

                    foreach (var Store in filterDTO.Stores)
                    {


                        var comboBoxItem = new ComboBoxItem
                        {
                            Content = Store.StoreTitle, // فرض بر این است که Title متن مورد نظر است
                            Tag = Store.Id // شناسه گروه
                        };
                        SearchInStoreMaterialCirculation.Items.Add(comboBoxItem);
                    }

                }



            }
            catch (Exception ex)
            {
                // نمایش خطا در صورت بروز هرگونه مشکل
                 System.Windows.MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}");
            }
        }

        // برای رویداد ComboBox
        private async void SearchInMaterialCirculation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ApplyFiltersMaterialCirculation();  // فراخوانی ApplyFilters بدون پارامتر
        }
       

        private async Task ApplyFiltersMaterialCirculation()
        {

            try
            {
                // ایجاد فیلتر برای درخواست داده‌ها
                // بررسی اینکه آیتمی انتخاب شده است
                // مقدار انتخاب‌شده از کمبوباکس برای پروژه
                var selectedProject = (SearchProjectIncostMaterialCirculation.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای تامین کننده
                var selectedSupplier = (SearchInSupplierMaterialCirculation.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای گروه کالا
                var selectedMaterialGroup = (SearchIncostMaterialGroupMaterialCirculation.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای  کالا
                var selectedMaterial = (SearchInMaterialCirculation.SelectedItem as ComboBoxItem)?.Tag as long?;

                // مقدار انتخاب‌شده از کمبوباکس برای انبار
                var selectedStore = (SearchInStoreMaterialCirculation.SelectedItem as ComboBoxItem)?.Tag as long?;


             

                // ایجاد فیلتر برای درخواست داده‌ها
                var filter = new Application.DTOS.PRO.MaterialCirculation.FilterMaterialCirculationDTO
                {

                    ContractId = selectedProject,                   
                    MaterialGroupId = selectedMaterialGroup,
                    MaterialId = selectedMaterial,
                    SupplierId= selectedSupplier,
                    StoreId = selectedStore,
                    TakeEntity = 100000
                };

                // فراخوانی سرویس با فیلتر
                var filteredData = await _materialCirculationService.FilterMaterialCirculation(filter);
                // اضافه کردن شماره ردیف به هر رکورد در لیست
                for (int i = 0; i < filteredData.MaterialCirculations.Count; i++)
                {
                    filteredData.MaterialCirculations[i].RowNumber = i + 1; // شماره ردیف از 1 شروع می‌شود
                }

                // نمایش داده‌ها
                materialCirculationGrid.ItemsSource = filteredData.MaterialCirculations ?? new List<MaterialCirculationDTO>();

                //if (filteredData.MaterialCirculations.Count > 0)
                //{
                //    double totalCost = 0; // مقداردهی اولیه به totalCost

                //    // حلقه برای محاسبه مجموع ستون مبلغ
                //    for (int i = 0; i < filteredData.CostListDetails.Count; i++)
                //    {
                //        // دسترسی به مقدار هر ردیف از ستون "مبلغ"
                //        double cost = (double)filteredData.CostListDetails[i].CostListRowfinalValue;

                //        // اضافه کردن مقدار "مبلغ" به totalCost
                //        totalCost += cost;

                //        TotalCostDetailsLabel.Content = $"مجموع هزینه‌ها: {totalCost:N0}"; // فرمت عدد با هزارگان
                //    }

                //}
                //else
                //{
                //    TotalCostDetailsLabel.Content = $"مجموع هزینه‌ها: 0"; // فرمت عدد با هزارگان
                //}



            }
            catch (Exception ex)
            {
                // نمایش پیام خطا در صورت بروز مشکل
                 System.Windows.MessageBox.Show($"خطا در اعمال فیلترها: {ex.Message}");
            }
        }


        #endregion

        #region Button
        private void OnButtonAppClick(object sender, RoutedEventArgs e)
        {
            string url = "https://acc.abniesoft.com/";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void OnButtonSiteClick(object sender, RoutedEventArgs e)
        {
            string url = "https://www.abniesoft.com/";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void OnButtonWebinarClick(object sender, RoutedEventArgs e)
        {
            string url = "https://event.alocom.co/class/abniesoft/53103aa0";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        #endregion

        #region  ExportToExcel

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // انتخاب مسیر ذخیره فایل
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "ذخیره فایل اکسل"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // ایجاد یک Workbook جدید
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("DataGrid Export");

                        // اضافه کردن Header
                        for (int i = 0; i < accDocumentDetailsGrid.Columns.Count; i++)
                        {
                            var column = accDocumentDetailsGrid.Columns[i];
                            worksheet.Cell(1, i + 1).Value = column.Header.ToString();
                        }

                        // اضافه کردن داده‌ها
                        var items = accDocumentDetailsGrid.ItemsSource as IEnumerable;
                        int row = 2;

                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                for (int col = 0; col < accDocumentDetailsGrid.Columns.Count; col++)
                                {
                                    var column = accDocumentDetailsGrid.Columns[col];

                                    if (column is DataGridBoundColumn boundColumn && boundColumn.Binding is System.Windows.Data.Binding binding)
                                    {
                                        var propertyName = binding.Path.Path;
                                        var value = item.GetType().GetProperty(propertyName)?.GetValue(item, null);

                                        // تبدیل مقدار عددی ماهیت به متن
                                        if (propertyName == "NatureFinalBalance")
                                        {
                                            string natureText = value switch
                                            {
                                                1 => "بدهکار",
                                                2 => "بستانکار",
                                                0 => "بی‌حساب",
                                                _ => "نامشخص"
                                            };
                                            worksheet.Cell(row, col + 1).Value = natureText;
                                        }
                                        // استخراج فقط تاریخ از تاریخ‌وساعت
                                        else if (value is DateTime dateValue)
                                        {
                                            worksheet.Cell(row, col + 1).Value = dateValue.ToString("yyyy/MM/dd");
                                        }
                                        // جدا کردن مبالغ به صورت سه‌رقمی
                                        else if (value is decimal || value is double || value is int)
                                        {
                                            worksheet.Cell(row, col + 1).Value = $"{value:N0}";
                                        }
                                        else
                                        {
                                            worksheet.Cell(row, col + 1).Value = value?.ToString();
                                        }
                                    }
                                    // استخراج مقدار برای DataGridTemplateColumn (شرح سند)
                                    else if (column is DataGridTemplateColumn templateColumn && col == 2) // شماره ستون شرح سند
                                    {
                                        var descriptionProperty = item.GetType().GetProperty("AccDocumentRowDescription");
                                        var descriptionValue = descriptionProperty?.GetValue(item, null);
                                        worksheet.Cell(row, col + 1).Value = descriptionValue?.ToString();
                                    }
                                }
                                row++;
                            }
                        }

                        // ذخیره فایل اکسل
                        workbook.SaveAs(saveFileDialog.FileName);
                         System.Windows.MessageBox.Show("فایل اکسل با موفقیت ذخیره شد!", "موفقیت",  System.Windows.MessageBoxButton.OK,  System.Windows.MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                 System.Windows.MessageBox.Show($"خطا در هنگام خروجی گرفتن از اکسل: {ex.Message}", "خطا",  System.Windows.MessageBoxButton.OK,  System.Windows.MessageBoxImage.Error);
            }
        }

        private void PersonDataGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(PersonsDataGrid);
        }
        private void BankDataGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(BanksDataGrid);
        }
        private void InstantPaymentDataGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(InstantPaymentDataGrid);
        }
        private void CostDataGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(CostDataGrid);
        }
        private void IncomDataGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(IncomDataGrid);
        }

        private void costListDetailsGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(costListDetailsGrid);
        }

        private void receiveChequeGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(receiveChequeGrid);
        }

        private void paymentChequeGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(paymentChequeGrid);
        }

        private void materialCirculationGridExcel(object sender, RoutedEventArgs e)
        {
            // فراخوانی تابع صادر کردن داده‌ها به اکسل
            ExportDataGridToExcel(materialCirculationGrid);
        }

        public void ExportDataGridToExcel(DataGrid dataGrid)
        {
            try
            {
                // انتخاب مسیر ذخیره فایل
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "ذخیره فایل اکسل"
                };

                // اگر کاربر مسیری را انتخاب کند
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // ایجاد یک ورک‌بوک جدید
                    var workbook = new XLWorkbook();

                    // افزودن یک شیت جدید
                    var worksheet = workbook.Worksheets.Add("DataGrid Export");

                    // افزودن هدرها به شیت
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataGrid.Columns[i].Header.ToString();
                    }

                    // دسترسی به داده‌های جدول از ItemsSource (منبع داده‌ها)
                    var itemsSource = dataGrid.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        int row = 2; // شروع از ردیف دوم برای داده‌ها
                        foreach (var item in itemsSource)
                        {
                            for (int col = 0; col < dataGrid.Columns.Count; col++)
                            {
                                var column = dataGrid.Columns[col];

                                // دسترسی به داده‌های مربوط به هر ردیف
                                var cellValue = GetCellValue(item, column);
                                worksheet.Cell(row, col + 1).Value = cellValue;
                            }
                            row++;
                        }
                    }

                    // ذخیره کردن ورک‌بوک به فایل اکسل
                    workbook.SaveAs(filePath);
                    System.Windows.MessageBox.Show("فایل اکسل با موفقیت ذخیره شد!", "موفقیت", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"خطا در هنگام خروجی گرفتن از اکسل: {ex.Message}", "خطا",  System.Windows.MessageBoxButton.OK,  System.Windows.MessageBoxImage.Error);
            }
        }

        // این متد به شما کمک می‌کند تا مقادیر هر سلول را استخراج کنید
        private string GetCellValue(object item, DataGridColumn column)
        {
            string cellValue = string.Empty;

            // اگر ستون یک ستون متنی است
            if (column is DataGridTextColumn textColumn)
            {
                var binding = textColumn.Binding as Binding;
                if (binding != null)
                {
                    var propertyValue = item.GetType().GetProperty(binding.Path.Path)?.GetValue(item);
                    if (propertyValue != null)
                    {
                        // بررسی اینکه آیا این مقدار یک عدد است و تبدیل ماهیت به متن
                        if (binding.Path.Path == "NatureFinalBalance")
                        {
                            cellValue = propertyValue switch
                            {
                                1 => "بدهکار",
                                2 => "بستانکار",
                                0 => "بی‌حساب",
                                _ => "نامشخص"
                            };
                        }
                        // استخراج فقط تاریخ از تاریخ‌وساعت
                        else if (propertyValue is DateTime dateValue)
                        {
                            cellValue = dateValue.ToString("yyyy/MM/dd");
                        }
                        // اگر مقدار مالی است (decimal، double، یا int) سه‌رقمی کردن آن
                        else if (propertyValue is decimal || propertyValue is double || propertyValue is int)
                        {
                            cellValue = string.Format("{0:N0}", propertyValue); // تبدیل به سه‌رقمی
                        }
                        else
                        {
                            cellValue = propertyValue.ToString();
                        }
                    }
                }
            }
            // اینجا می‌توانید برای سایر نوع ستون‌ها نیز logic اضافه کنید، مانند ComboBox و غیره

            return cellValue;
        }








        #endregion







        // متد برای تبدیل تاریخ میلادی به شمسی
        public string ConvertToShamsi(DateTime dateTime)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            return string.Format("{0}/{1:D2}/{2:D2}",
                                 pc.GetYear(dateTime),
                                 pc.GetMonth(dateTime),
                                 pc.GetDayOfMonth(dateTime));
        }

        private void TabControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3 && PersonTab.IsSelected)
            {
                // فوکوس روی کمبوی موردنظر
                SearchInPerson.Focus();
                e.Handled = true; // جلوگیری از پردازش پیش‌فرض
            }
            if (e.Key == Key.F4 && PersonTab.IsSelected)
            {
                // فوکوس روی کمبوی موردنظر
                txtfilterdataPerson.Focus();
                e.Handled = true; // جلوگیری از پردازش پیش‌فرض
            }
        }

        // جلوگیری از ورود کاراکترهای غیر عددی
        private void NumericPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // فقط اعداد و جداکننده سه رقمی (کاما) مجاز است
            e.Handled = !char.IsDigit(e.Text, 0) && e.Text != ","; // یا استفاده از "." برای جداکننده دهگان
        }

        private void TxtShamsiDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // فقط اعداد و "/" مجاز هستند
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9/]");
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainTabControl.SelectedItem = InvoiceTab;
            }));

            var sourceGrid = sender as DataGrid;
            if (sourceGrid != null)
            {
                // ردیف انتخاب‌شده را دریافت کنید
                var selectedItem = sourceGrid.SelectedItem as TafziliDTO; // فرض کنید کلاس داده CostListDetails باشد

                if (selectedItem != null)
                {
                    // اطمینان از اینکه ItemsSource تهی نیست
                    if (SearchInvoice.Items != null)
                    {
                        // پیدا کردن شیء کامل در ItemsSource بر اساس Id
                        var matchedItem = SearchInvoice.Items.Cast<ComboBoxItem>()
                            .FirstOrDefault(item =>
                            {
                                var tagValue = item.Tag;                               
                                return tagValue != null && tagValue.Equals(selectedItem.Id);
                            });

                        if (matchedItem != null)
                        {
                            SearchInvoice.SelectedItem = matchedItem; // انتخاب آیتم پیدا شده
                        }
                       
                       
                    }
                    else
                    {
                        // If ItemsSource is null, handle it here (e.g., log, notify user, etc.)
                        Console.WriteLine("ItemsSource is null.");
                    }
                }
            }
        }

        private void ProjectsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainTabControl.SelectedItem = CostTab;
            }));

            var sourceGrid = sender as DataGrid;
            if (sourceGrid != null)
            {
                // ردیف انتخاب‌شده را دریافت کنید
                var selectedItem = sourceGrid.SelectedItem as ContractDTO; // فرض کنید کلاس داده CostListDetails باشد

                if (selectedItem != null)
                {
                    // اطمینان از اینکه ItemsSource تهی نیست
                    if (SearchProjectInCost.Items != null)
                    {
                        // پیدا کردن شیء کامل در ItemsSource بر اساس Id
                        var matchedItem = SearchProjectInCost.Items.Cast<ComboBoxItem>()
                            .FirstOrDefault(item =>
                            {
                                var tagValue = item.Tag;
                                return tagValue != null && tagValue.Equals(selectedItem.MoeinId);
                            });

                        if (matchedItem != null)
                        {
                            SearchProjectInCost.SelectedItem = matchedItem; // انتخاب آیتم پیدا شده
                        }


                    }
                    else
                    {
                        // If ItemsSource is null, handle it here (e.g., log, notify user, etc.)
                        Console.WriteLine("ItemsSource is null.");
                    }
                }
            }
        }

      
    }

    // مدل برای ساختار اصلی JSON
    public class RootModel
    {
        public List<JsonTableModel> Tables { get; set; }
    }

    // مدل برای هر جدول در JSON
    public class JsonTableModel
    {
        public string TableName { get; set; }
        public List<Dictionary<string, object>> Rows { get; set; }
    }

    public class DataItem
    {
        public string TafziliGroupName { get; set; }
        public string TafziliName { get; set; }
        public decimal FirstTotalDebtorValue { get; set; }
        public decimal FirstTotalCreditorValue { get; set; }
        public decimal TotalDebtorValue { get; set; }
        public decimal TotalCreditorValue { get; set; }
        public int NatureFinalBalance { get; set; }
        public decimal Finalbalance { get; set; }
    }



}
