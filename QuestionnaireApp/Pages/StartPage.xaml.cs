using System;
using System.Windows;
using System.Windows.Controls;
using GameLibrary;

namespace QuestionnaireApp.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage(Difficulty previousDifficulty)
        {
            InitializeComponent();

            // Initialize ComboBox (difficulty selector)
            DifficultySelector.ItemsSource = Enum.GetNames(typeof(Difficulty));
            // Set default selected item to 0 (easy)
            DifficultySelector.SelectedIndex = (int)previousDifficulty;
        }

        void OnClickStart (object sender, RoutedEventArgs e)
        {
            // Get PlayerName from textbox
            string playerName = PlayerNameValue.Text;

            // Get Difficulty from combobox
            Difficulty difficulty = (Difficulty)DifficultySelector.SelectedIndex;

            // Call startgame eventhandler
            StartGame(this, new StartGameEventArgs(playerName, difficulty));
        }

        // Create eventhandler that will be called when start button is pressed
        public event EventHandler<StartGameEventArgs> StartGame;
    }

    public class StartGameEventArgs : EventArgs
    {
        public StartGameEventArgs(string PlayerName, Difficulty difficulty)
        {
            this.PlayerName = PlayerName;
            this.Difficulty = difficulty;
        }

        public string PlayerName { get; private set; }
        public Difficulty Difficulty { get; private set; }

        private string playerName;
        private Difficulty difficulty;
    }

}
