using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MajnSvýpr
{
    public partial class MainWindow : Window
    {
        private int xDim = 16;
        private int yDim = 16;
        private List<string> bombLoc = new List<string>();
        private List<string> clickLoc = new List<string>();
        public MainWindow()
        {
            GenBomb();
            GenBut();
        }
        private void GenBut() //vyplni grid tlacitkama a da jim do tagu jejich souradnice
        {
            InitializeComponent();

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Button myButton = new Button();
                    myButton.Tag = i + "," + j;

                    Grid.SetColumn(myButton, i);
                    Grid.SetRow(myButton, j);

                    myButton.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(CheckBut);

                    gridMain.Children.Add(myButton); //tvorba tlačítka, v daném čtverci                    
                }

            }
        }
        private void GenBomb() //generuje souradnice bomb
        {
            Random rnd = new Random();
            while (bombLoc.Count < 32)
            {
                int random1 = rnd.Next(0, 16);
                int random2 = rnd.Next(0, 16);
                if (!bombLoc.Contains(random1 + "," + random2))
                {
                    bombLoc.Add(random1 + "," + random2);
                }
            }
        }
        private void CheckBut(object sender, MouseButtonEventArgs e)
        {
            CheckBomb(sender as Button);
        }
        private void CheckBomb(Button myButton) //kontroluje zda je kliknuti na bombu nebo ne, pokud ano, bomby odhali, pokud ne, zobrazi hodnotu
        {
            if (bombLoc.Contains(myButton.Tag.ToString()))
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        for (int k = 0; k < 32; k++)
                        {
                            if (bombLoc[k].Equals(j + "," + i))
                            {
                                foreach (Button ThisButton in gridMain.Children)
                                {
                                    if (ThisButton.Tag.ToString().Equals(j + "," + i))
                                    {
                                        ThisButton.Background = new SolidColorBrush(Colors.Red);
                                        ThisButton.Content = "*";
                                    }
                                }
                            }
                        }
                    }
                }
                Console.Beep();
                MessageBox.Show("You lost!");
            }
            else
            {
                if (CheckVal(myButton.Tag.ToString()) == 0)
                {
                    myButton.Background = new SolidColorBrush(Colors.Gray);
                    myButton.Content = " ";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 1)
                {
                    myButton.Background = new SolidColorBrush(Colors.LightBlue);
                    myButton.Content = "1";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 2)
                {
                    myButton.Background = new SolidColorBrush(Colors.Green);
                    myButton.Content = "2";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 3)
                {
                    myButton.Background = new SolidColorBrush(Colors.Yellow);
                    myButton.Content = "3";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 4)
                {
                    myButton.Background = new SolidColorBrush(Colors.Orange);
                    myButton.Content = "4";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 5)
                {
                    myButton.Background = new SolidColorBrush(Colors.Brown);
                    myButton.Content = "5";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 6)
                {
                    myButton.Background = new SolidColorBrush(Colors.DarkGray);
                    myButton.Foreground = new SolidColorBrush(Colors.White);
                    myButton.Content = "6";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 7)
                {
                    myButton.Background = new SolidColorBrush(Colors.Black);
                    myButton.Foreground = new SolidColorBrush(Colors.White);
                    myButton.Content = "7";
                }
                else if (CheckVal(myButton.Tag.ToString()) == 8)
                {
                    myButton.Background = new SolidColorBrush(Colors.HotPink);
                    myButton.Content = "8";
                }
                CheckWin(myButton);
            }
        }
        private int CheckVal(string stringA) //pocita hodnotu policka
        {
            string[] tempStringAr = stringA.Split(',');
            int tXVal = Int32.Parse(tempStringAr[0]);
            int tYVal = Int32.Parse(tempStringAr[1]);

            int valCount = 0;

            for (int i = 0; i < 32; i++)
            {
                if (bombLoc[i].Equals((tXVal + 1) + "," + (tYVal - 1)) && tXVal < xDim && tYVal > 0)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals((tXVal + 1) + "," + tYVal) && tXVal < xDim)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals((tXVal + 1) + "," + (tYVal + 1)) && tXVal < xDim && tYVal < yDim)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals(tXVal + "," + (tYVal - 1)) && tYVal > 0)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals(tXVal + "," + (tYVal + 1)) && tYVal < yDim)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals((tXVal - 1) + "," + (tYVal - 1)) && tXVal > 0 && tYVal > 0)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals((tXVal - 1) + "," + tYVal) && tXVal > 0)
                {
                    valCount++;
                }
                else if (bombLoc[i].Equals((tXVal - 1) + "," + (tYVal + 1)) && tXVal > 0 && tYVal < yDim)
                {
                    valCount++;
                }
            }
            return valCount;
        }
        private void CheckWin(Button myButton) //kontroluje zda uzivatel vyhral
        {
            string first = myButton.Tag.ToString();
            string splitOne = first.Split(',')[0].ToString();
            string splitTwo = first.Split(',')[1].ToString();



            if (!clickLoc.Contains(myButton.Tag.ToString()))
            {
                clickLoc.Add(splitOne + "," + splitTwo);
            }
            if (clickLoc.Count >= 256 - 32)
            {
                MessageBox.Show("You won, cobratulations!");
            }
        }
    }
}
// ©Matouš Hála, SSPŠ 2.K