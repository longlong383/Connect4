using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace connect4
{
    /*
    Section 1:
    Create visual
    Section 2:
    Create userInput
    Section 3:
    Create button options
    -Restart
    -Quit
    Section 4;
    win scenarios
    -Horizontal
    -vertical
    -diagonal
    */
    public partial class Form1 : Form
    {
        //creating all variables to be used during gamePlay
        List<Panel> groups = new List<Panel>();
        List<List<PictureBox>> gameBoard = new List<List<PictureBox>>();
        List <List<int>> positions = new List<List<int>>();
        int playerID = 0;
        int column1 = 5;
        int column2 = 5;
        int column3 = 5;
        int column4 = 5;
        int column5 = 5;
        int column6 = 5;
        int column7 = 5;
        int secondTime = 0;


        public Form1()
        {
            InitializeComponent();
            playerInfo.Visible = false;
            restartButton.Visible = false;

            //adding all panels into a list
            groups.Add(panel1);
            groups.Add(panel3);
            groups.Add(panel5);
            groups.Add(panel7);
            groups.Add(panel11);
            groups.Add(panel9);
            groups.Add(panel8);
            foreach (Panel p in groups)
            {
                p.Visible = false;
            }

            //adding all pictureBoxes into lists of lists
            List<PictureBox> column1 = new List<PictureBox>();
            column1.Add(pictureBox2);
            column1.Add(pictureBox3);
            column1.Add(pictureBox4);
            column1.Add(pictureBox5);
            column1.Add(pictureBox6);
            column1.Add(pictureBox7);
            gameBoard.Add(column1);
            List<PictureBox> column2 = new List<PictureBox>();
            column2.Add(pictureBox25);
            column2.Add(pictureBox24);
            column2.Add(pictureBox23);
            column2.Add(pictureBox22);
            column2.Add(pictureBox21);
            column2.Add(pictureBox20);
            gameBoard.Add(column2);
            List<PictureBox> column3 = new List<PictureBox>();
            column3.Add(pictureBox37);
            column3.Add(pictureBox36);
            column3.Add(pictureBox35);
            column3.Add(pictureBox34);
            column3.Add(pictureBox33);
            column3.Add(pictureBox32);
            gameBoard.Add(column3);
            List<PictureBox> column4 = new List<PictureBox>();
            column4.Add(pictureBox48);
            column4.Add(pictureBox47);
            column4.Add(pictureBox46);
            column4.Add(pictureBox45);
            column4.Add(pictureBox44);
            column4.Add(pictureBox43);
            gameBoard.Add(column4);
            List<PictureBox> column5 = new List<PictureBox>();
            column5.Add(pictureBox72);
            column5.Add(pictureBox71);
            column5.Add(pictureBox70);
            column5.Add(pictureBox69);
            column5.Add(pictureBox68);
            column5.Add(pictureBox67);
            gameBoard.Add(column5);
            List<PictureBox> column6 = new List<PictureBox>();
            column6.Add(pictureBox60);
            column6.Add(pictureBox59);
            column6.Add(pictureBox58);
            column6.Add(pictureBox57);
            column6.Add(pictureBox56);
            column6.Add(pictureBox55);
            gameBoard.Add(column6);
            List<PictureBox> column7 = new List<PictureBox>();
            column7.Add(pictureBox78);
            column7.Add(pictureBox77);
            column7.Add(pictureBox76);
            column7.Add(pictureBox75);
            column7.Add(pictureBox74);
            column7.Add(pictureBox73);
            gameBoard.Add(column7);

            //making all pictureBoxes Invisible
            foreach (List<PictureBox> l in gameBoard)
            {
                foreach(PictureBox p in l)
                {
                    p.Visible = false;
                }
            }

            //making positions on the Connect 4 board
            for (int i = 0; i < 7; i++)
            {
                List <int> temp = new List<int>();
                temp = new List<int>() { -1, -1, -1, -1, -1, -1};
                positions.Add(temp);
            }
        }


        private void quitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //making all userInterfaces visible
            foreach (Panel group in groups)
            {
                group.Visible = true;
            }
            restartButton.Visible = true;
            startButton.Visible = false;
            intro.Visible = false;
            playerInfo.Visible = true;
            playerInfo.Text = "Player Red's Turn";

            //if a second round is played or more
            if (secondTime > 0)
            {
                //ensure the winner of the previous game gets to play if applicable and that both players will be able to go first 
                if (playerID == 1)
                {
                    playerInfo.Text = "Player Red's Turn";
                    playerID = 0;
                }
                else if (playerID == 0)
                {
                    playerID = 1;
                    playerInfo.Text = "Player Yellow's Turn";
                }
            }
        }
        private void checkAdder (int w, int j)
        {
            //if Panel in gameBoard is full
            if (w < 0)
            {
                MessageBox.Show("This column is full. \n Please enter another column");
                return;
            }


            gameBoard[j][w].Visible = true;

            //inserting game pieces into gameBoard
            if (playerID == 0)
            {
                gameBoard[j][w].Image = Properties.Resources.red_w_bg_77x77;
                positions[j][w] = 0;
                playerID = 1;
                playerInfo.Text = "Player Yellow's Turn";
            }
            else
            {
                gameBoard[j][w].Image = Properties.Resources.yellow_w_bg_77x77;
                positions[j][w] = 1;
                playerID = 0;
                playerInfo.Text = "Player Red's Turn";
            }

            //vertical check for wins
            int win = 0;

            for (int i = 0; i < 4; i++)
            {
                //check to see if the position being checked is still within the possible positions for a win
                if (w+i > 5)
                {
                    break;
                }
                //if it's the same piece, it adds one to the win counter
                if (positions[j][w] == positions[j][w + i])
                {
                    win++;

                }
            }

            //checks to see if win conditions are met
            winChecker(win);
            if (win == 4)
            {
                return;
            }
            win = 0;

            //horizontal checks for win

            //horizontal check to the right of the given game piece
            for (int i = 0; i < 4; i++)
            {
                //check to see if the position being checked is still within the possible positions for a win
                if (j + i > 6)
                {
                    break;
                }
                //if no other pieces to the right of it are the same, then it breaks and goes to check the left side
                if (positions[j][w] != positions[j + i][w])
                {
                    break;
                }
                if (positions[j][w] == positions[j+i][w])
                {
                    win++;

                }
            }

            //horizontal check to the left of the given game piece
            for (int i = 1; i < 4; i++)
            {
                if (j - i < 0)
                {
                    break;
                }
                if (positions[j][w] != positions[j - i][w])
                {
                    break;
                }
                if (positions[j][w] == positions[j - i][w])
                {
                    win++;

                }
            }
          
            winChecker(win);
            if (win == 4)
            {
                return;
            }
            win = 0;
            
            //diagonal in this orientation: \

            //diagonal going toward the bottom right of the given game piece
            for (int i = 0; i < 4; i++)
            {
                if (j + i > 6|| w+i > 5)
                {
                    break;
                }

                if (positions[j][w] != positions[j + i][w+i])
                {
                    break;
                }
                if (positions[j][w] == positions[j + i][w+i])
                {
                    win++;

                }
            }

            //diagonal going toward the top left of the given game piece
            for (int i = 1; i < 4; i++)
            {
                if (j - i < 0 || w - i < 0)
                {
                    break;
                }
                if (positions[j][w] != positions[j - i][w - i])
                {
                    break;
                }
                if (positions[j][w] == positions[j - i][w - i])
                {
                    win++;
                }
            }
            
            winChecker(win);
            if (win == 4)
            {
                return;
            }
            win = 0;


            //diagonal in this orientation: /

            //diagonal going towards the top right of the given game piece
            for (int i = 0; i < 4; i++)
            {
                if (j + i > 6 || w - i < 0)
                {
                    break;
                }

                if (positions[j][w] != positions[j + i][w - i])
                {
                    break;
                }
                if (positions[j][w] == positions[j + i][w - i])
                {
                    win++;

                }
            }

            //diagonal going towards the bottom left of the given game piece
            for (int i = 1; i < 4; i++)
            {
                if (j - i < 0 || w + i > 5)
                {
                    break;
                }
                if (positions[j][w] != positions[j - i][w + i])
                {
                    break;
                }
                if (positions[j][w] == positions[j - i][w + i])
                {
                    win++;
                }
            }
            winChecker(win);

        }
        private void winChecker(int l)
        {
            //win check
            if (l == 4 && playerID == 1)
            {
                playerInfo.Text = "Player Red Wins!";
                foreach (Panel group in groups)
                {
                    group.Enabled = false;
                }
            }
            else if (l == 4 && playerID == 0)
            {
                playerInfo.Text = "Player Yellow Wins!";
                foreach (Panel group in groups)
                {
                    group.Enabled = false;
                }
            }
        }
       
        //click methods for each Panel
        private void panel1_Click(object sender, EventArgs e)
        {
            checkAdder(column1, 0);
            column1--;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            checkAdder(column2, 1);
            column2--;
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            checkAdder(column3, 2);
            column3--;
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            checkAdder(column4, 3);
            column4--;
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            checkAdder(column5, 4);
            column5--;
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            checkAdder(column6, 5);
            column6--;
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            checkAdder(column7, 6);
            column7--;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            //resetting positions on gameBoard
            for (int i = 0; i < positions.Count; i++)
            {
                for (int z = 0; z < positions[i].Count; z++)
                {
                    positions[i][z] = -1;
                }
            }
            //resetting all pictureBoxes
            for (int i = 0; i < gameBoard.Count; i++)
            {
                for (int j = 0; j < gameBoard[i].Count; j++)
                {
                    gameBoard[i][j].Image = null;
                    gameBoard[i][j].Visible = false;
                }
            }
            //resetting panels
            foreach (Panel p in groups)
            {
                p.Visible = false;
            }
            //enabling any necesary controls and resetting any variables
            foreach (Panel group in groups)
            {
                group.Enabled = true;
            }
            column1 = 5;
            column2 = 5;
            column3 = 5;
            column4 = 5;
            column5 = 5;
            column6 = 5;
            column7 = 5;

            //reset all other userInterfaces
            restartButton.Visible = false;
            playerInfo.Visible = false;
            startButton.Visible = true;
            intro.Visible = true;
            intro.Text = "Welcome To Connect4!";
            secondTime++;

        }
    }
}
