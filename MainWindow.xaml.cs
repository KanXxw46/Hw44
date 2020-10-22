using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hw444
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if (password.Password.Length < 8)
            {
                Content = "Password's length must be at least 8";
                Opacity = 100;
            }
            else
            {
                if (password.Password == repeatedPassword.Password)
                {
                    var user = new User();
                    try
                    {
                        user.FirstName = firstName.Text;
                        user.LastName = lastName.Text;
                        user.Login = login.Text;
                        user.Email = email.Text;
                        user.Password = password.Password;
                    }
                    catch (ArgumentException exception)
                    {
                        Content = exception.Message;
                        Opacity = 100;
                        return;
                    }

                    var service = new ConfirmationService();
                    var confirmationCode = service.GetCode();
                    try
                    {
                        service.SendMessageEmail(user.Email, confirmationCode);
                    }
                    catch
                    {
                        Content = "Wrong email";
                        Opacity = 100;
                        return;
                    }
                    var confirmationWindow = new ConfirmationWindow(this, confirmationCode);

                    if (confirmationWindow.ShowDialog() ?? false)
                    {
                        using (var context = new HW444())
                        {
                            context.Add(user);
                            context.SaveChanges();
                        }
                    }
                    IsEnabled = true;
                }
                else
                {
                    Content = "Passwords don't match";
                }
            }
        }
    }
}
