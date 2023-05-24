using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

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
