using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BevasarloLista
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class ItemModel
    {
        public string Nev { get; set; }
        public int Mennyiseg { get; set; }
        public int Ar { get; set; }
        public string Kategoria { get; set; }
        public int Osszesen
        {
            get { return Mennyiseg * Ar; }
        }
        public ItemModel(string nev, int mennyiseg, int ar, string kategoria)
        {
            Nev = nev;
            Mennyiseg = mennyiseg;
            Ar = ar;
            Kategoria = kategoria;
        }
    }
    public partial class MainWindow : Window
    {
        List<ItemModel> termekek = new List<ItemModel>();
        public MainWindow()
        {

            InitializeComponent();
            termekek.Add(new ItemModel("Tej", 5, 450, "A"));
            termekek.Add(new ItemModel("Kenyer", 10, 350, "B"));
            termekek.Add(new ItemModel("Sajt", 2, 1200, "A"));
            termekek.Add(new ItemModel("Alma", 20, 200, "C"));
            termekek.Add(new ItemModel("Narancs", 15, 300, "C"));
            termekek.Add(new ItemModel("Hús", 3, 2500, "D"));
            termekek.Add(new ItemModel("Csokoládé", 7, 900, "B"));
            termekek.Add(new ItemModel("Kenyér", 1, 450, "B"));
            termekek.Add(new ItemModel("Tej", 12, 400, "A"));
            termekek.Add(new ItemModel("Sajt", 5, 1500, "D"));
            dataGrid.ItemsSource = termekek;
        }


        private void HozzaAdas_Click(object sender, RoutedEventArgs e)
        {
            var ujtermek = new HozzaAdas();
            
            if (ujtermek.ShowDialog()==true)
            {
                termekek.Add(ujtermek.ujTermek);
                dataGrid.Items.Refresh();
            }
        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem!=null)
            {
                termekek.Remove((ItemModel)dataGrid.SelectedItem);
                dataGrid.Items.Refresh();
            }
        }

        private void htld_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource=termekek.Where(item => item.Kategoria == "A").OrderByDescending(x=> x.Osszesen).ToList();
        }

        private void t5oesz_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderByDescending(x => x.Osszesen).Take(5);
        }
    }
}