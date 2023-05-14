using ScoreBoardLibrary;
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

namespace QuestionnaireApp.Pages
{
    /// <summary>
    /// Interaction logic for EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        public EndPage(List<Score> TopScores)
        {
            InitializeComponent();

            for (int i = 0; i < TopScores.Count; i++)
            {
                TextBlock scoreEntry = new TextBlock();
                scoreEntry.Text = $"{i+1} - {TopScores[i].Name} - {TopScores[i].Points}";
                Scoreboard.Children.Add(scoreEntry);
            }

        }

        void OnClickMainMenu (object sender, EventArgs e)
        {
            // Call ToMainMenu eventhandler
            ToMainMenu(this, EventArgs.Empty);
        }

        // Create eventhandler that will be called when main menu button is pressed
        public event EventHandler ToMainMenu;
    }
}
