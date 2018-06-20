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

namespace RetroRacersReloaded
{
    /// <summary>
    /// Interaction logic for HallOfSF.xaml
    /// </summary>
    public partial class HallOfSF : Window
    {
        private string path;
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public HallOfSF()
        {
            InitializeComponent();
            path = Directory.GetCurrentDirectory();
            path += "\\Ranking";
            if (Directory.Exists(path))
            {
                path += "\\";
                string[] files = Directory.GetFiles(path);
                foreach (string item in files)
                {
                    if (item.Contains(".txt"))
                    {
                        string[] parts = item.Split('\\');
                        comboBx.Items.Add(parts[parts.Length - 1].Split('.')[0]);
                    }
                }
            }
           
            
            /*string[] lines = File.ReadAllLines(@"C:\Users\David\Desktop\DatabaseRRR\Time.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts.Length >= 2)
                {
                    labelArray[i].Content = "Platz: " + parts[0] + "     Zeit: " + parts[1];
                }
            }*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow restart = new MainWindow();
            restart.getStartWindow().Show();
            this.Close();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.Delete(path + comboBx.SelectedItem.ToString() + ".txt");
                comboBx.Items.Remove(comboBx.SelectedItem);
            }
            catch(Exception ex)
            {
                File.WriteAllText(@".\log.txt", ex.ToString());
            }
        }

        private void comboBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox.FontSize = 30;
            listBox.Items.Clear();
            string text = comboBx.SelectedItem.ToString();
            string[] entrys = File.ReadAllLines(path + text + ".txt");
            for (int i = 0; i < entrys.Length && i < 10; i++)
            {
                listBox.Items.Add("Platz " + entrys[i].Split(';')[0]+ "   |    Zeit: "+ entrys[i].Split(';')[1]);
            }
        }
    }
}
