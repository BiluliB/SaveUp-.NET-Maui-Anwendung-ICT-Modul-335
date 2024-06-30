using Microsoft.Extensions.Logging;
using SaveUp.Common.Helper;
using SaveUp.Interfaces;
using Syncfusion.Maui.Core.Hosting;
using SaveUp.Services;
using SaveUp.ViewModels;
using SaveUp.Views;

namespace SaveUp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();

            ResourceManager.RegisterSyncfusionLicense();

            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ISavedMoneyServiceAPI, SavedMoneyServiceAPI>();

            builder.Services.AddSingleton<AddPageViewModel>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ListPageViewModel>();

            builder.Services.AddSingleton<AddPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MorePage>();
            builder.Services.AddSingleton<ListPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
