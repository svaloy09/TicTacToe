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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        #region Private Members


        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (x) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();


        }

        #endregion


        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            //Create new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;


            //Make sure player 1 starts the game 
            mPlayer1Turn = true;

            //Iterate every button in the grid...
            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {

                //Change background, foreground and content to default values
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;



            });

            //Make sure the game hasn't finished
            mGameEnded = false;

        }


        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;

            }
            //Cast the sender to a button
            var button = (Button)sender;
            //Finds the button in the array 
            var column = Grid.GetColumn(button);
            var Row = Grid.GetRow(button);


            var index = column + (Row * 3);

            //Don't do anything if it already has a value already 
            if (mResults[index] != MarkType.Free)
                return;


            //Set the cell value based on which player it  is
            if (mPlayer1Turn)
                mResults[index] = MarkType.Cross;
            else
                mResults[index] = MarkType.Nought;

            //Set button text to the right result
            button.Content = mPlayer1Turn ? "X" : "O";

            //chage noughts to green 
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //toggles players turns 
            if (mPlayer1Turn)
                mPlayer1Turn = false;
            else
                mPlayer1Turn = true;

            //Check for a winner 
            CheckForWinner();


        }
        /// <summary>
        /// Checks is there is a winner of a 3 line straight 
        /// </summary>
        private void CheckForWinner()
        {

            #region Horizontal wins

            
            //Checks for horizantal wins
            //
            // - Row 0 
            //

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

            }
            //
            // - Row 1
            //

            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }

            //
            // - Row 2
            //

            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }

            #endregion


            #region Vertical wins
            //Checks for vertical wins
            //
            // - column 0 
            //

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }


            //Checks for vertical wins
            //
            // - column 1
            //

            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Checks for vertical wins
            //
            // - column 2
            //

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion



            #region Diagonal Wins

            //Checks for diagonal wins
            //
            // - Top Left Bootom Right 
            //

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Checks for diagonal wins
            //
            // - Top Right Botom Left
            //

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {

                //ends game
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }


            #endregion


            #region No Winners 












            #endregion
            //Check for no winner and full board 
            if (!mResults.Any(f => f == MarkType.Free))
            {


                //game ended 
                mGameEnded = true;


                //Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {


                    Button.Background = Brushes.Yellow;


                });


            }
        }

    }

}