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
using System.IO;

namespace RetroRacersReloaded
{
    /// <summary>
    /// Interaction logic for multiplayer.xaml
    /// </summary>
    public partial class multiplayer : Window
    {
        

        /// <summary>
        /// Constructer
        /// </summary>
        public multiplayer(double inHeight)
        {
            InitializeComponent();
            string tmp = Directory.GetCurrentDirectory();
            string[] folder = Directory.GetDirectories(tmp);
            for (int i = 0; i < folder.Length; i++)
            {
                if (folder[i].Contains("Levels"))
                {
                    tmp = tmp + "\\Levels";
                }
            }
            folder = Directory.GetDirectories(tmp);
            for (int x = 0; x < folder.Length; x++)
            {
                string map = folder[x].Remove(0, tmp.Length + 1);
                map = map.Split('_')[0];
                if (!Combobx.Items.Contains(map))
                {
                    Combobx.Items.Add(map);
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
            
            if (Combobx.SelectedItem != null)
            {
                
                string selectedMap = Combobx.SelectedItem.ToString();
                
                Window newWin = new playerInitialize(selectedMap);
                newWin.WindowState = WindowState;
                newWin.Show();
                this.Close();
                System.Threading.Thread.Sleep(500);
                
            }
            else
            {
                mapSelectFail.Content = "keine Map ausgewählt";
            }
           
            
        }
    }
}
