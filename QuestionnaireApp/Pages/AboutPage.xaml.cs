using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace QuestionnaireApp.Pages
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        void ClickOnLink(object sender, RoutedEventArgs e)
        {
            string link = "https://github.com/vives-oop1-2023/questionnaire-Robbedoes24";
            Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string url = e.Uri.AbsoluteUri;
            // Navigate to url
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            // Set handled to true to prevent the default navigation behavior
            e.Handled = true; 
        }
    }
}
