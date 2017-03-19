using System.Windows;

namespace GraduateWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var factory = new WindowsFactory();
            factory.OpenMainWindow();
        }
    }
}
