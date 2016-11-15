/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MasterAngler.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MasterAngler.ViewModel.Data;
using Microsoft.Practices.ServiceLocation;


namespace MasterAngler.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MenuPanelViewModel>();
            SimpleIoc.Default.Register<ConsoleViewModel>();
            SimpleIoc.Default.Register<HotkeysViewModel>();
            SimpleIoc.Default.Register<OptionsViewModel>();
            SimpleIoc.Default.Register<StatisticsViewModel>();
            SimpleIoc.Default.Register<ControlViewModel>();
            SimpleIoc.Default.Register<DataViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MenuPanelViewModel MenuPanel => ServiceLocator.Current.GetInstance<MenuPanelViewModel>();
        public ConsoleViewModel Console => ServiceLocator.Current.GetInstance<ConsoleViewModel>();
        public HotkeysViewModel KeyBindings => ServiceLocator.Current.GetInstance<HotkeysViewModel>();
        public OptionsViewModel Settings => ServiceLocator.Current.GetInstance<OptionsViewModel>();
        public StatisticsViewModel Statistics => ServiceLocator.Current.GetInstance<StatisticsViewModel>();
        public ControlViewModel Control => ServiceLocator.Current.GetInstance<ControlViewModel>();
        public DataViewModel Data => ServiceLocator.Current.GetInstance<DataViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup() {}
    }
}