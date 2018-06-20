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
using System.Threading;

namespace RetroRacersReloaded
{
    /// <summary>
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private List<string> picList = new List<string>();
        private int moneyAmount = 0;
        private int mapCount = 0;
        private string nameOfMap;
        public string deletPath;


        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public ShopWindow(double heightIn, double widthIn)
        {            
            InitializeComponent();
            string path = Directory.GetCurrentDirectory() + "\\Shop\\money.txt";

            if (!File.Exists(path))
            {
                File.Create(path);
            }
            else
            {
                string[] money = File.ReadAllLines(path);
                moneyShopLabel.Content = "Money: " + money[0] + " Coins";

                moneyAmount = Convert.ToInt32(money[0]);
            }
            if (moneyAmount < 100)
            {
                buyButton.IsEnabled = false;
            }

            string picPath = Directory.GetCurrentDirectory() + "\\Shop";
            string[] file = Directory.GetFiles(picPath);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains(".png"))
                {
                    string tmp = file[i].Remove(file[i].Length - 4);
                    if (Directory.Exists(tmp+ "_1") && Directory.Exists(tmp + "_2") && Directory.Exists(tmp + "_3") && Directory.Exists(tmp + "_4"))
                    {
                        picList.Add(file[i]);
                    }
                }
            }
            if (picList.Count != 0)
            {
                ShopPrev.Source = new BitmapImage(new Uri(picList[mapCount]));
                string[] parts = picList[mapCount].Split('\\');
                string name = parts[parts.Length - 1].Remove(parts[parts.Length - 1].Length - 4);
                mapName.Content = "Karten Name: " + name;
                nameOfMap = name;
            }
            else
            {
                nextMap_Click(null, null);
            }
            
            double height = heightIn - 14;
            double width = widthIn - 14;
            double fixHeigt = 1500;
            if (height != fixHeigt)
            {
                double perc = (height - fixHeigt) / (fixHeigt / 100);
                ShopPrev.Height += (ShopPrev.Height / 100) * perc;
            }


        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow start = new MainWindow();



            start.getStartWindow().Show();
            System.Threading.Thread.Sleep(500);
            this.Close();
        }

        private void backMap_Click(object sender, RoutedEventArgs e)
        {
            if (mapCount==0)
            {
                mapCount = picList.Count-1;
            }
            else
            {
                mapCount--;
            }
            ShopPrev.Source = new BitmapImage(new Uri(picList[mapCount]));
            string[] parts = picList[mapCount].Split('\\');
            string name = parts[parts.Length - 1].Remove(parts[parts.Length - 1].Length - 4);
            mapName.Content = "Karten Name: " + name;
            nameOfMap = name;
            if (moneyAmount < 100)
            {
                buyButton.IsEnabled = false;
            }
        }

        private void nextMap_Click(object sender, RoutedEventArgs e)
        {
            if (picList.Count != 0)
            {
                if (mapCount >= picList.Count - 1)
                {
                    mapCount = 0;
                }
                else
                {
                    mapCount++;
                }
                ShopPrev.Source = new BitmapImage(new Uri(picList[mapCount]));
                string[] parts = picList[mapCount].Split('\\');
                string name = parts[parts.Length - 1].Remove(parts[parts.Length - 1].Length - 4);
                mapName.Content = "Karten Name: " + name;
                nameOfMap = name;
                if (moneyAmount < 100)
                {
                    buyButton.IsEnabled = false;
                }
            }
            else
            {
                mapName.Content = "Keine Karten mehr zum kaufen";
                buyButton.IsEnabled = false;
                arrowLeft.IsEnabled = false;
                arrowRight.IsEnabled = false;
                ShopPrev.Source = null;
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            moneyAmount -= 100;
            picList.Remove(picList[mapCount]);
            moneyShopLabel.Content = "Money: " + moneyAmount + " Coins";
            string path = Directory.GetCurrentDirectory() + "\\Shop\\money.txt";
            File.WriteAllText(path, moneyAmount.ToString());
            string map = nameOfMap;
            string deletePath = Directory.GetCurrentDirectory() + "\\Shop\\" + map;
            DirectoryCopy(deletePath + "_1", Directory.GetCurrentDirectory() + "\\Levels\\" + map + "_1", true);
            Directory.Delete(deletePath + "_1", true);
            DirectoryCopy(deletePath + "_2", Directory.GetCurrentDirectory() + "\\Levels\\" + map + "_2", true);
            Directory.Delete(deletePath + "_2", true);
            DirectoryCopy(deletePath + "_3", Directory.GetCurrentDirectory() + "\\Levels\\" + map + "_3", true);
            Directory.Delete(deletePath + "_3", true);
            DirectoryCopy(deletePath + "_4", Directory.GetCurrentDirectory() + "\\Levels\\" + map + "_4", true);
            Directory.Delete(deletePath + "_4", true);
            nextMap_Click(null, null);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            deletPath = deletePath;

        }


      

        private static void DirectoryCopy(
        string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = System.IO.Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
