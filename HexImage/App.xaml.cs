using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.Collections;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

namespace HexImage;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication {
    public new static App Current => (App)PrismApplication.Current;

    private static readonly IConfigurationRoot Root = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false, true)
        .Build();


    protected override void RegisterTypes(IContainerRegistry containerRegistry) {

        ViewModelLocationProvider.Register<MainWindow, MainViewModel>();
        containerRegistry.RegisterSingleton<IConfiguration>(sp => Root);
        
    }

    protected override Window CreateShell() {
        return Container.Resolve<MainWindow>();
    }
}
