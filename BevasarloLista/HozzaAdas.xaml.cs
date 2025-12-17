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

namespace BevasarloLista
{
    /// <summary>
    /// Interaction logic for HozzaAdas.xaml
    /// </summary>
    public partial class HozzaAdas : Window
    {
        public ItemModel ujTermek;
        public HozzaAdas()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            string nev = termekTextBox.Text;
            int mennyiseg = int.Parse(mennyisegTextBox.Text);
            int ar = int.Parse(egysegarTextBox.Text);
            string kategoria = tipusComboBox.Text;

            if (nev == "" || mennyiseg == null || ar == null || kategoria == "")
            {
                MessageBox.Show("Nem megfelelő adatok/Hiányzó adatok");
            }
            else 
            { 
            ujTermek = new ItemModel(nev, mennyiseg, ar, kategoria);
            DialogResult = true;
            }
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
