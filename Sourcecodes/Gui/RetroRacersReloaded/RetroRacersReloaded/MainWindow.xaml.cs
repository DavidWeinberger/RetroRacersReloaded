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
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;


namespace RetroRacersReloaded
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow startWindow = null;
        private Thread fred;
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public MainWindow()
        {
            //runb();
            InitializeComponent();
            fred = new Thread(new ThreadStart(extractZips));
            fred.Start();
            Thread.Sleep(500);
            startWindow = this;
            this.WindowState = WindowState.Maximized;
            this.ShowInTaskbar = true;
            string path = Directory.GetCurrentDirectory();
            if (!fred.IsAlive)
            {
                path += "\\Shop\\money.txt";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                else
                {
                    string[] money = File.ReadAllLines(path);
                    if (money.Length != 0)
                    {
                        moneyLabel.Content = "Money: " + money[0] + " Coins";
                    }
                }
            }
            
        }

        public void extractZips()
        {
            
            string pathZip = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(pathZip);
            if (File.Exists(pathZip + "\\Levels.zip") && !Directory.Exists(pathZip + "\\Levels"))
            {
                ZipFile.ExtractToDirectory(pathZip + "\\Levels.zip", pathZip);
            }
            if (File.Exists(pathZip + "\\Shop.zip") && !Directory.Exists(pathZip + "\\Shop"))
            {
                ZipFile.ExtractToDirectory(pathZip + "\\Shop.zip", pathZip);

            }
           
        }

        

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public MainWindow getStartWindow()
        {
            return startWindow;
        }


        private void runb()
        {
            string tempFilename = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".bat");
            using (StreamWriter writer = new StreamWriter(tempFilename))
            {
                writer.WriteLine("DIR");
                writer.WriteLine("PAUSE");
            }
            Process process = Process.Start(tempFilename);
            process.WaitForExit();
            File.Delete(tempFilename);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }


        

        private void HOSFButton_Click(object sender, RoutedEventArgs e)
        {
            
            HallOfSF HOSFwindow = new HallOfSF();
            HOSFwindow.Show();
            
            System.Threading.Thread.Sleep(500);
            this.Close();
        }


        private void SPButton_Click(object sender, RoutedEventArgs e)
        {
            if (fred.IsAlive)
            {
                MessageBox.Show("Einige Datein werden noch entpackt, dies könnte etwas dauern. Versuchen sie es Später erneuert", "Datein werden entpackt", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }        
            Window window = new singlePlayer(this.ActualHeight);
            window.WindowState = WindowState;
            window.Show();
            System.Threading.Thread.Sleep(500);
            this.Close();
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            if (fred.IsAlive)
            {
                MessageBox.Show("Einige Datein werden noch entpackt, dies könnte etwas dauern. Versuchen sie es Später erneuert", "Datein werden entpackt", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            Window window = new ShopWindow(this.ActualHeight, this.ActualWidth);
            window.WindowState = WindowState;
            window.Show();
            System.Threading.Thread.Sleep(500);
            this.Close();
        }

        private void MPButton_Click(object sender, RoutedEventArgs e)
        {
            if (fred.IsAlive)
            {
                MessageBox.Show("Einige Datein werden noch entpackt, dies könnte etwas dauern. Versuchen sie es Später erneuert", "Datein werden entpackt", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            Window window = new multiplayer(this.ActualHeight-14);
            window.WindowState = WindowState;
            window.Show();
            System.Threading.Thread.Sleep(500);
            this.Close();
        }
    }
}
