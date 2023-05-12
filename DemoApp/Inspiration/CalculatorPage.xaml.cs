using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

namespace WPFFractionCalculator
{
    /// <summary>
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class CalculatorPage : Page
    {
        public CalculatorPage()
        {
            InitializeComponent();
            //ChangeButtonBackgrounds();
            //ChangeFractionsLayout();
            //UpdateMessageBoard("");
        }
        
        void OnClickOperator(object sender, RoutedEventArgs e)
        {
            /*
            Button button = (Button)sender;
            activeButton = button.Name;

            ChangeButtonBackgrounds();
            ChangeFractionsLayout();
            */
        }

        /*
        void ChangeButtonBackgrounds()
        {
            // define colors
            SolidColorBrush? activeBackground = this.FindResource("Button.Background.Active") as SolidColorBrush;
            SolidColorBrush? inactiveBackground = this.FindResource("Button.Background.Inactive") as SolidColorBrush;

            // set all button backgrounds to orange
            add.Background = inactiveBackground;
            subtract.Background = inactiveBackground;
            multiply.Background = inactiveBackground;
            divide.Background = inactiveBackground;
            invert.Background = inactiveBackground;
            reciprocal.Background = inactiveBackground;
            simplify.Background = inactiveBackground;

            // set background of the selected button to green
            switch (activeButton)
            {
                case "add":
                    add.Background = activeBackground;
                    break;

                case "subtract":
                    subtract.Background = activeBackground;
                    break;

                case "multiply":
                    multiply.Background = activeBackground;
                    break;

                case "divide":
                    divide.Background = activeBackground;
                    break;

                case "invert":
                    invert.Background = activeBackground;
                    break;

                case "reciprocal":
                    reciprocal.Background = activeBackground;
                    break;

                case "simplify":
                    simplify.Background = activeBackground;
                    break;

                default:
                    add.Background = activeBackground;
                    break;
            }
        }
        */

        /*
        void ChangeFractionsLayout()
        {
            // Set default visibility to visible and spacing
            operation.Visibility = Visibility.Visible;
            operation.Margin = new Thickness(20);
            fractions.ColumnDefinitions[0].Width = new GridLength(1.0, GridUnitType.Star);
            fraction1_grid.Visibility = Visibility.Visible;

            // set the text of 
            switch (activeButton)
            {
                case "add":
                    operation.Text = "+";
                    break;

                case "subtract":
                    operation.Text = "-";
                    break;

                case "multiply":
                    operation.Text = "x";
                    break;

                case "divide":
                    operation.Text = "/";
                    break;

                case "invert":
                    operation.Text = "-";
                    operation.Margin = new Thickness(0, 20, 20, 20);
                    fractions.ColumnDefinitions[0].Width = GridLength.Auto;
                    fraction1_grid.Visibility = Visibility.Collapsed;
                    break;

                case "reciprocal":
                    operation.Visibility = Visibility.Collapsed;
                    fractions.ColumnDefinitions[0].Width = GridLength.Auto;
                    fraction1_grid.Visibility = Visibility.Collapsed;
                    break;

                case "simplify":
                    operation.Visibility = Visibility.Collapsed;
                    fractions.ColumnDefinitions[0].Width = GridLength.Auto;
                    fraction1_grid.Visibility = Visibility.Collapsed;
                    break;

                default:
                    operation.Text = "+";
                    break;
            }
        }
        */

        void OnClickSolve(object sender, RoutedEventArgs e)
        {
            /*
            bool validInput = ValidUserInput(sender, e);

            if (validInput)
            {
                // Creating the fraction objects
                Fraction f1 = new Fraction();
                Fraction f2 = new Fraction();
                Fraction solution = new Fraction();

                try
                {
                    f1.Numerator = Convert.ToInt32(fraction1_numerator.Text);
                    f1.Denominator = Convert.ToInt32(fraction1_denominator.Text);
                }
                catch { } // no errorhandling yet (if nothing or wrong value is filled in will return 1/1 for f1)

                try
                {
                    f2.Numerator = Convert.ToInt32(fraction2_numerator.Text);
                    f2.Denominator = Convert.ToInt32(fraction2_denominator.Text);
                }
                catch { } // no errorhandling yet (if nothing or wrong value is filled in will return 1/1 for f2)

                switch (activeButton)
                {
                    case "add":
                        solution = f1.Add(f2).Simplify();
                        break;

                    case "subtract":
                        solution = f1.Subtract(f2).Simplify();
                        break;

                    case "multiply":
                        solution = f1.Multiply(f2).Simplify();
                        break;

                    case "divide":
                        solution = f1.Divide(f2).Simplify();
                        break;

                    case "invert":
                        solution = f2.Invert().Simplify();
                        break;

                    case "reciprocal":
                        solution = f2.Reciprocal().Simplify();
                        break;

                    case "simplify":
                        solution = f2.Simplify();
                        break;

                    default:
                        break;
                }

                solution_numerator.Text = solution.Numerator.ToString();
                solution_denominator.Text = solution.Denominator.ToString();
            }
            else
            {
                solution_numerator.Text = "";
                solution_denominator.Text = "";
            }
            */
        }

        /*
        bool ValidUserInput(object sender, RoutedEventArgs e)
        {
            // default state
            UpdateMessageBoard("");
            fraction1_numerator.BorderThickness = new Thickness(0);
            fraction1_denominator.BorderThickness = new Thickness(0);
            fraction2_numerator.BorderThickness = new Thickness(0);
            fraction2_denominator.BorderThickness = new Thickness(0);

            // check if string is valid
            bool Valid(string input, bool denominator)
            {
                // because the tryparse needs an out
                int i;
                if (input == "" || input.Contains(".") || input.Contains(".") || !int.TryParse(input, out i))
                {
                    return false;
                }
                if (denominator && i == 0)
                {
                    return false;
                }

                return true;
            }

            switch (activeButton)
            {
                case "invert":
                case "reciprocal":
                case "simplify":
                    if (!Valid(fraction2_numerator.Text, false))
                    {
                        fraction2_numerator.BorderThickness = new Thickness(1);
                        UpdateMessageBoard("The input for the numerator of the fraction is invalid");
                        return false;
                    }
                    if (!Valid(fraction2_denominator.Text, true))
                    {
                        fraction2_denominator.BorderThickness = new Thickness(1);
                        UpdateMessageBoard("The input for the denominator of the fraction is invalid");
                        return false;
                    }
                    break;

                default:
                    if (!Valid(fraction1_numerator.Text, false))
                    {
                        fraction1_numerator.BorderThickness = new Thickness(1);
                        UpdateMessageBoard("The input for the numerator of fraction 1 is invalid");
                        return false;
                    }
                    if (!Valid(fraction1_denominator.Text, true))
                    {
                        fraction1_denominator.BorderThickness = new Thickness(1);
                        UpdateMessageBoard("The input for the denominator of fraction 1 is invalid");
                        return false;
                    }
                    if (!Valid(fraction2_numerator.Text, false))
                    {
                        fraction2_numerator.BorderThickness = new Thickness(1);
                        UpdateMessageBoard("The input for the numerator of fraction 2 is invalid");
                        return false;
                    }
                    if (!Valid(fraction2_denominator.Text, true))
                    {
                        fraction2_denominator.BorderThickness = new Thickness(1);
                        UpdateMessageBoard("The input for the denominator of fraction 2 is invalid");
                        return false;
                    }
                    break;
            }
            return true;
        }
        */

        void OnClickClear(object sender, RoutedEventArgs e)
        {
            /*
            // clear value in fraction 1 textboxes
            fraction1_numerator.Text = "";
            fraction1_denominator.Text = "";

            // clear value in fraction 2 textboxes
            fraction2_numerator.Text = "";
            fraction2_denominator.Text = "";

            // clear value in solution textboxes
            solution_numerator.Text = "";
            solution_denominator.Text = "";

            // reset messageBoard
            UpdateMessageBoard("");

            // reset fraction borders
            fraction1_numerator.BorderThickness = new Thickness(0);
            fraction1_denominator.BorderThickness = new Thickness(0);
            fraction2_numerator.BorderThickness = new Thickness(0);
            fraction2_denominator.BorderThickness = new Thickness(0);
            */
        }

        /*
        void UpdateMessageBoard(string text)
        {
            if (text == "")
            {
                messageBoard.Visibility = Visibility.Collapsed;
            }
            else
            {
                messageBoard.Visibility = Visibility.Visible;
            }
            messageBoard.Text = text;
        }

        private string activeButton = "add";
        */
    }
}
