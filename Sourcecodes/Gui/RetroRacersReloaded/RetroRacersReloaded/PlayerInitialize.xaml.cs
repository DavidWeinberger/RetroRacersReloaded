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
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace RetroRacersReloaded
{
    /// <summary>
    /// Interaction logic for playerInitialize.xaml
    /// </summary>
    public partial class playerInitialize : Window
    {
        private string level;
        private string amountOfPlayer;
        private int amount = 2;
        /// <summary>
        /// Interaction logic for playerInitialize.xaml
        /// </summary>
        public playerInitialize(String input)
        {
            level = input;
            InitializeComponent();
            amountOfPlayer = amount.ToString();
            PlayerAmount.Content = amountOfPlayer;
            printControls();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow start = new MainWindow();



            start.getStartWindow().Show();
            System.Threading.Thread.Sleep(500);
            this.Close();

        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory();



            string selectedMap = level;

            Process p = null;
            Thread t = null;
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dirPath = System.IO.Path.Combine(appDataPath, @"\Roaming\RetroRacerReloaded\");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            File.WriteAllText(dirPath + "\\Place.txt", "");
            if (Directory.Exists(path + "\\Levels\\" + selectedMap + "_" + amountOfPlayer))
            {
                path += "\\Levels\\" + selectedMap + "_" + amountOfPlayer;
                if (Directory.Exists(path))
                {
                    path += "\\"+selectedMap + "_"+amountOfPlayer+".exe";
                    t = new Thread(new ThreadStart(addMoney));
                    t.Start();
                    p = Process.Start(path);
                }
            }
            this.ShowInTaskbar = false;
            this.WindowState = WindowState.Minimized;
            Thread.Sleep(1000);
            if (p!= null)
            {
                p.WaitForExit();
            }
            if (t != null)
            {
                t.Abort();
            }            
            if (File.Exists(dirPath+"\\Place.txt"))
            {
                string lines = File.ReadAllText(dirPath + "\\Place.txt");
                if (lines != "")
                {
                    MessageBox.Show(lines, "Platzierung", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
                File.Delete(dirPath + "\\Place.txt");
            }


            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Maximized;


            back_Click(null, null);
        }

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        private void addMoney()
        {
            try
            {
                while (true)
                {
                    string moneyPath = Directory.GetCurrentDirectory() + "\\Shop\\Money.txt";
                    string[] moneyLines = File.ReadAllLines(moneyPath);
                    int money = 0;
                    if (moneyLines.Length != 0)
                    {
                        money = Convert.ToInt16(moneyLines[0]);
                    }
                    Thread.Sleep(30000);
                    money += 5;
                    File.WriteAllText(moneyPath, money.ToString());
                }
            }
            catch (ThreadAbortException)
            {

            }
        }

        private void printControls()
        {
            if (amount == 3)
            {
                p3.Content = "Steuerung Spieler Grün: I|K|J|L";
                p4.Content = "";
            }
            else if (amount == 4)
            {
                p3.Content = "Steuerung Spieler Grün: I|K|J|L";
                p4.Content = "Steuerung Spieler Gelb: 8|5|4|6";
            }
            else
            {
                p3.Content = "";
                p4.Content = "";
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (amount < 4)
            {
                amount++;
            }
            amountOfPlayer = amount.ToString();
            PlayerAmount.Content = amountOfPlayer;
            printControls();
        }

        private void rem_Click(object sender, RoutedEventArgs e)
        {
            if (amount > 2)
            {
                amount--;
            }
            amountOfPlayer = amount.ToString();
            PlayerAmount.Content = amountOfPlayer;
            printControls();
        }
    }
}
