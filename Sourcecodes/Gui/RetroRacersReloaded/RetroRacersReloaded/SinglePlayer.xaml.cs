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
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace RetroRacersReloaded
{
    /// <summary>
    /// Interaction logic for singlePlayer.xaml
    /// </summary>
    public partial class singlePlayer : Window
    {
        private string place;
        private string timeBestLapString;

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public singlePlayer(double inHeight)
        {
            InitializeComponent();
            string tmp = Directory.GetCurrentDirectory();
            string[] folder = Directory.GetDirectories(tmp);
            string rankingDir = tmp + "\\Ranking";
            if (!Directory.Exists(rankingDir))
            {
                Directory.CreateDirectory(rankingDir);
            }
            tmp = tmp + "\\Levels";
            if (Directory.Exists(tmp))
            {
                folder = Directory.GetDirectories(tmp);
                for (int x = 0; x < folder.Length; x++)
                {
                    string map = folder[x].Remove(0, tmp.Length + 1);
                    map = map.Split('_')[0];
                    if (!Combobx.Items.Contains(map))
                    {
                        Combobx.Items.Add(map);
                        if (!File.Exists(rankingDir + "\\" + map + ".txt"))
                        {
                            File.Create(rankingDir + "\\" + map + ".txt");
                        }
                    }
                }
            }
            double height = inHeight - 14;
            double fixHeigt = 1500;
            if (height != fixHeigt)
            {
                double perc = (height - fixHeigt) / (fixHeigt / 100);
                ImagePlace.Height += (ImagePlace.Height / 100) * perc;
            }

            this.Combobx.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxChanged);
            
        }
        private void OnMyComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            
            string path = Directory.GetCurrentDirectory();
            path += "\\Levels\\";

            if (Directory.Exists(path))
            {
                path += text + "_1\\";
                if (Directory.Exists(path))
                {
                    path += text + "_1.png";
                    if (File.Exists(path))
                    {
                        ImagePlace.Source = new BitmapImage(new Uri(path));
                    }
                    else
                    {
                        ImagePlace.Source = null;
                    }

                }
            }
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
            string rankingPath = path+ "\\Ranking";
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dirPath = System.IO.Path.Combine(appDataPath, @"\Roaming\RetroRacerReloaded\");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            
            if (Combobx.SelectedItem != null)
            {
                string selectedMap = Combobx.SelectedItem.ToString();
                path += "\\Levels\\";
                Process p = null;
                Thread t = null;
                if (Directory.Exists(path))
                {
                    path += selectedMap + "_1\\";
                    if (Directory.Exists(path))
                    {
                        path += selectedMap + "_1.exe";
                        t = new Thread(new ThreadStart(addMoney));
                        t.Start();
                        p = Process.Start(path);
                    }
                }
                this.ShowInTaskbar = false;
                this.WindowState = WindowState.Minimized;
                Thread.Sleep(1000);
                p.WaitForExit();
                t.Abort();
                int exit = p.ExitCode;

                enterDB(selectedMap, dirPath, rankingPath);
                
                

                this.ShowInTaskbar = true;
                this.WindowState = WindowState.Maximized;
                

                back_Click(null, null);
               

            }
            else
            {
                mapEmpty.Content = "keine Map ausgewählt";
            }
            
            
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


        private void enterDB(string selectedMap, string dirPath, string rankingPath)
        {
            
            if (File.Exists(dirPath + "\\time.txt"))
            {
                string[] input = File.ReadAllLines(dirPath + "\\time.txt");
                string[] inputParts = input[0].Split(';');
                rankingPath = rankingPath + "\\" + selectedMap + ".txt";
                int bestLapTime = Convert.ToInt32(inputParts[2]);
                string bestLapTimeString = inputParts[1];

                Thread.Sleep(500);
                string[] dbTimes = File.ReadAllLines(rankingPath);
                if (dbTimes.Length == 0)
                {
                    File.WriteAllText(rankingPath, "1;" + bestLapTimeString);
                    place = "1";
                    timeBestLapString = bestLapTimeString;
                    Thread t = new Thread(new ThreadStart(messagePlace));
                    t.Start();
                    return;
                }
                else
                {
                    string[] minuteParts = null;
                    string[] secParts;
                    string time;
                    int intTime = 0;
                    string[] timeParts = dbTimes[dbTimes.Length - 1].Split(';');
                    if (timeParts.Length >= 2)
                    {
                        minuteParts = timeParts[1].Split(':');
                        if (minuteParts.Length >= 2)
                        {
                            secParts = minuteParts[1].Split('.');
                            if (secParts.Length >= 2)
                            {
                                time = minuteParts[0] + secParts[0] + secParts[1];
                                intTime = Convert.ToInt32(time);
                            }
                        }
                    }

                    if (intTime < bestLapTime)
                    {
                        if (dbTimes.Length < 10)
                        {
                            List<string> list = new List<string>();
                            for (int i = 0; i < dbTimes.Length; i++)
                            {
                                timeParts = dbTimes[i].Split(';');
                                list.Add(list.Count + 1 + ";" + timeParts[1]);
                            }
                            list.Add(list.Count + 1 + ";" + bestLapTimeString);
                            File.WriteAllLines(rankingPath, list.ToArray());
                            
                            place = (list.Count + 1).ToString();
                            timeBestLapString = bestLapTimeString;
                            Thread t = new Thread(new ThreadStart(messagePlace));
                            t.Start();
                        }
                    }
                    else if (intTime > bestLapTime)
                    {
                        bool inList = false;
                        List<string> list = new List<string>();
                        for (int i = 0; i < dbTimes.Length && i < 9; i++)
                        {
                            timeParts = dbTimes[i].Split(';');
                            if (timeParts[1].CompareTo(bestLapTimeString) == 1 && inList == false)
                            {
                                inList = true;
                                list.Add(list.Count + 1 + ";" + bestLapTimeString);
                                place = (list.Count + 1).ToString();
                                timeBestLapString = bestLapTimeString;
                                Thread t = new Thread(new ThreadStart(messagePlace));
                                t.Start();
                            }
                            list.Add(list.Count + 1 + ";" + timeParts[1]);
                            File.WriteAllLines(rankingPath, list.ToArray());
                        }
                    }
                }
                File.Delete(dirPath + "\\time.txt");
            }
        }

        private void messagePlace()
        {
            MessageBox.Show("Platz " + place + ": " + timeBestLapString, "Neue Bestzeit", MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
