using System;
using System.Windows;

namespace Moody.Snake
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private async void App_OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                await Application.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}