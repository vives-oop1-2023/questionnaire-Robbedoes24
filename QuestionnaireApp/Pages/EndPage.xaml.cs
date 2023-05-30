using GameLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QuestionnaireApp.Pages
{
    /// <summary>
    /// Interaction logic for EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        public EndPage(List<Player> TopScores)
        {
            InitializeComponent();

            for (int i = 0; i < TopScores.Count; i++)
            {
                // Get styles from page resources
                Style? elementStyle = FindResource("Leaderboard.List.Element") as Style;
                Style? textStyle = FindResource("Leaderboard.List.Element.Text") as Style;

                // Create Textblock that holds place
                TextBlock place = new TextBlock();
                place.Text = $"{i + 1}";
                place.Style = textStyle;
                place.TextAlignment = TextAlignment.Left;
                place.SetValue(Grid.ColumnProperty, 0);

                // Create Textblock that holds player name
                TextBlock player = new TextBlock();
                player.Text = $"{TopScores[i].Name}";
                player.Style = textStyle;
                player.SetValue(Grid.ColumnProperty, 1);

                // Create Textblock that holds player score
                TextBlock score = new TextBlock();
                score.Text = $"{TopScores[i].Score} PTS";
                score.Style = textStyle;
                score.TextAlignment = TextAlignment.Right;
                score.SetValue(Grid.ColumnProperty, 2);

                // Create grid
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });

                // Add texblocks to grid
                grid.Children.Add(place);
                grid.Children.Add(player);
                grid.Children.Add(score);

                // Create border
                Border border = new Border();
                border.Style = elementStyle;

                // Add grid to border
                border.Child = grid;

                // Add canvas to leaderboard
                LeaderboardList.Children.Add(border);
            }
        }

        void OnClickMainMenu (object sender, EventArgs e)
        {
            // Call ToMainMenu eventhandler
            ToMainMenu(this, EventArgs.Empty);
        }

        // Create eventhandler that will be called when main menu button is pressed
        public event EventHandler ToMainMenu = delegate { };
    }
}
