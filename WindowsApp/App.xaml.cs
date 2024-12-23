using Application.Services.Implementations.ACC.AccDocmentDetails;
using Application.Services.Implementations.ACC.IKol;
using Application.Services.Implementations.ACC.Moein;
using Application.Services.Implementations.ACC.MoeinTafziliGroup;
using Application.Services.Implementations.ACC.Tafzili;
using Application.Services.Implementations.ACC.Tafzili2;
using Application.Services.Implementations.ACC.Tafzili3;
using Application.Services.Implementations.ACC.TafziliGroup;
using Application.Services.Implementations.FIN.Payment;
using Application.Services.Implementations.FIN.Receive;
using Application.Services.Implementations.PRO.Contract;
using Application.Services.Implementations.PRO.CostListDetails;
using Application.Services.Implementations.PRO.Material;
using Application.Services.Implementations.PRO.MaterialCirculation;
using Application.Services.Implementations.PRO.MaterialGroup;
using Application.Services.Implementations.PRO.MaterialUnit;
using Application.Services.Implementations.PRO.ProjectStatusFactor;
using Application.Services.Implementations.PRO.Store;
using Application.Services.Interfaces.ACC.IAccDocmentDetails;
using Application.Services.Interfaces.ACC.IKol;
using Application.Services.Interfaces.ACC.IMoein;
using Application.Services.Interfaces.ACC.IMoeinTafziliGroup;
using Application.Services.Interfaces.ACC.ITafzili;
using Application.Services.Interfaces.ACC.ITafzili2;
using Application.Services.Interfaces.ACC.ITafzili3;
using Application.Services.Interfaces.ACC.ITafziliGroup;
using Application.Services.Interfaces.FIN.IPayment;
using Application.Services.Interfaces.FIN.IReceive;
using Application.Services.Interfaces.PRO.IContract;
using Application.Services.Interfaces.PRO.ICostListDetails;
using Application.Services.Interfaces.PRO.IMaterial;
using Application.Services.Interfaces.PRO.IMaterialCirculation;
using Application.Services.Interfaces.PRO.IMaterialUnit;
using Application.Services.Interfaces.PRO.IProjectStatusFactor;
using Application.Services.Interfaces.PRO.IStore;
using Application.Services.Interfaces.PRO.MaterialGroup;
using Domain.Entities.ACC;
using Domain.Entities.FIN;
using Domain.Entities.PRO;
using HandyControl.Tools;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System.Windows;
using WindowsApp;

namespace WindowsApp
{

    public partial class App : System.Windows.Application
    {

        // تعریف ServiceProvider به‌صورت static
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {


           FileManager.CheckAndCreateDriveAndFolder();
            var serviceCollection = new ServiceCollection();

            // استفاده از IConfiguration برای بارگذاری connectionString از appsettings.json
            var connectionString = @"Data Source=D:\AbnieSoft\AbnieSoftDatabase.db; Cache=Shared";

            // ثبت DbContext با استفاده از SQLite
            serviceCollection.AddDbContext<AbnieSoftDbContext>(options =>
                options.UseSqlite(connectionString));

            // ثبت مخزن‌ها
            serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // ثبت مخزن‌های خاص
            serviceCollection.AddScoped<IGenericRepository<Contract>>(provider =>
                new GenericRepository<Contract>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Tafzili>>(provider =>
               new GenericRepository<Tafzili>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Tafzili2>>(provider =>
                          new GenericRepository<Tafzili2>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Tafzili3>>(provider =>
              new GenericRepository<Tafzili3>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Kol>>(provider =>
               new GenericRepository<Kol>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Moein>>(provider =>
               new GenericRepository<Moein>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<AccDocmentDetails>>(provider =>
              new GenericRepository<AccDocmentDetails>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<TafziliGroup>>(provider =>
           new GenericRepository<TafziliGroup>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<TafziliType>>(provider =>
           new GenericRepository<TafziliType>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<MoeinTafziliGroups>>(provider =>
           new GenericRepository<MoeinTafziliGroups>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<CostListDetails>>(provider =>
           new GenericRepository<CostListDetails>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<MaterialGroup>>(provider =>
          new GenericRepository<MaterialGroup>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Material>>(provider =>
          new GenericRepository<Material>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<MaterialUnit>>(provider =>
          new GenericRepository<MaterialUnit>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<ProjectStatusFactor>>(provider =>
         new GenericRepository<ProjectStatusFactor>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Tender>>(provider =>
               new GenericRepository<Tender>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Moein>>(provider =>
               new GenericRepository<Moein>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<ReceiveCheque>>(provider =>
              new GenericRepository<ReceiveCheque>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<PaymentCheque>>(provider =>
              new GenericRepository<PaymentCheque>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<MaterialCirculation>>(provider =>
          new GenericRepository<MaterialCirculation>(connectionString));

            serviceCollection.AddScoped<IGenericRepository<Store>>(provider =>
            new GenericRepository<Store>(connectionString));

            // ثبت سرویس‌ها
            serviceCollection.AddScoped<IContractService, ContractService>();
            serviceCollection.AddScoped<ITafziliService, TafziliService>();
            serviceCollection.AddScoped<ITafzili2Service, Tafzili2Service>();
            serviceCollection.AddScoped<ITafzili3Service, Tafzili3Service>();
            serviceCollection.AddScoped<IKolService, KolService>();
            serviceCollection.AddScoped<IMoeinService, MoeinService>();
            serviceCollection.AddScoped<IAccDocmentDetailsService, AccDocmentDetailsService>();
            serviceCollection.AddScoped<ITafziliGroupService, TafziliGroupService>();
            serviceCollection.AddScoped<IMoeinTafziliGroups, MoeinTafziliGroup>();
            serviceCollection.AddScoped<ICostListDetailsService, CostListDetailsService>();
            serviceCollection.AddScoped<IMaterialGroupService, MaterialGroupService>();
            serviceCollection.AddScoped<IMaterialService, MaterialService>();
            serviceCollection.AddScoped<IMaterialUnitService, MaterialUnitService>();
            serviceCollection.AddScoped<IProjectStatusFactorService, ProjectStatusFactorService>();
            serviceCollection.AddScoped<IReceiveService, ReceiveService>();
            serviceCollection.AddScoped<IPaymentService, PaymentService>();
            serviceCollection.AddScoped<IMaterialCirculationService, MaterialCirculationService>();
            serviceCollection.AddScoped<IStoreService, StoreService>();

            // ثبت MainWindow
            serviceCollection.AddScoped<MainWindow>();

            // ساخت سرویس پرووایدر
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // دریافت MainWindow از DI و نمایش آن
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            //PersianCulture culture = new PersianCulture();
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;
            //ConfigHelper.Instance.SetLang(culture.IetfLanguageTag);

            base.OnStartup(e);
        }
    }

}
