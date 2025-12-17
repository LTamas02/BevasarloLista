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
            progressBar.Minimum = termekek.Min(x => x.Ar);
            progressBar.Maximum = termekek.Max(x => x.Osszesen);
        }


        private void HozzaAdas_Click(object sender, RoutedEventArgs e)
        {
            var ujtermek = new HozzaAdas();
            
            if (ujtermek.ShowDialog()==true)
            {
                termekek.Add(ujtermek.ujTermek);
                dataGrid.ItemsSource = termekek;
                dataGrid.Items.Refresh();
                progressBar.Minimum = termekek.Min(x => x.Ar);
                progressBar.Maximum = termekek.Max(x => x.Osszesen);
            }
        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem!=null && dataGrid.SelectedItem is ItemModel)
            {
                termekek.Remove((ItemModel)dataGrid.SelectedItem);
                dataGrid.ItemsSource = termekek;
                dataGrid.Items.Refresh();
                progressBar.Minimum = termekek.Min(x => x.Ar);
                progressBar.Maximum = termekek.Max(x => x.Osszesen);
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

        private void MnM1_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Mennyiseg>1).Select(x => x.Ar);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek;
        }

        private void csokken_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderByDescending(x => new {Nev=x.Nev,Összeg=x.Osszesen});
        }

        private void n500_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Kategoria=="D" &&x.Osszesen < 500);
        }

        private void NeOABC_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev).Select(x=>new { x.Nev, x.Osszesen });
        }


        private void TipusRendezes_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev)
                .GroupBy(x => x.Kategoria)
                .Select(x => new{Típus = x.Key, Darab=x.Sum(x=>x.Mennyiseg), Összesen=x.Sum(x=>x.Osszesen)});
        }

        private void TipusAtlagAr_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek
                .GroupBy(x => x.Kategoria)
                .Select(x => new { Kategória = x.Key, Átlagár =Math.Round( x.Average(x => x.Ar) }));
        }
        private void LoK_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(x => x.Kategoria)
                .Select(x => new { Kategória = x.Key, Összérték=x.Max(x=>x.Osszesen)});
        }

        private void bct1000k_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => (x.Kategoria == "B" || x.Kategoria == "C") && x.Ar < 1000);
        }

        private void ofksz_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Ar>500).GroupBy(x=>x.Kategoria)
                .Select(x => new {Kategória=x.Key,TermékekSzáma=x.Count()});
        }
        private void kM10K1000_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Mennyiseg>10 && x.Ar<1000).OrderBy(x=>x.Ar);
        }

        private void nM2000ABC_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Ar > 2000).OrderBy(x => x.Nev);
        }

        private void tnTCs_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(x => new { Nev = x.Nev, Kategoria = x.Kategoria })
                .Select(x => new{TermekNev=x.Key.Nev,Kategoria=x.Key.Kategoria,Darab=x.Count()});
        }

        private void LET_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource= termekek.GroupBy(x => x.Kategoria).Select(x => new{Kategória=x.Key,Termek=x.Max(x=>x.Ar) });
        }

        private void OdbTk_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(x => x.Kategoria)
                .Select(x => new { Kategória = x.Key, Darab = x.Sum(x => x.Mennyiseg) });
        }

        private void nullaFt_Click(object sender, RoutedEventArgs e)
        {
            var nullaFtTermekek = termekek.Any(x => x.Ar == 0);
            if (nullaFtTermekek==false)
            {
                MessageBox.Show("Nincs nulla Ft-os termék");
            }
            else
            {
                MessageBox.Show("Van nulla Ft-os termék");
            }
        }

        private void CsK_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Nev.Contains("Kenyer")).OrderByDescending(x=>x.Ar);   
        }

        private void MegEgyAr_Click(object sender, RoutedEventArgs e)
        {
            var egyformak = termekek.GroupBy(x => x.Ar).Select(x => new {darab=x.Count()}).Any(x=>x.darab>1);
            if (egyformak==true)
            {
                MessageBox.Show("Vannak megegyező árú termékek");
            }
            else
            {
                MessageBox.Show("Nincsenek megegyező árú termékek");
            }
        }

        private void Valtozas(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Nev.ToLower().Contains(textBox.Text.ToLower())).ToList();
        }

        private void EgyTer_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(x => x.Nev)
                .Where(x=>x.Count()>1)
                .SelectMany(x=>x);
        }

        private void dg_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is ItemModel selectedItem)
            {
                progressBar.Value = selectedItem.Ar;
            }
        }

        private void NemC_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Kategoria != "C");
        }
        private void NevHossz_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev.Length);
        }

        private void AtOar_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Kategoria == "A").OrderBy(x => x.Nev).Select(x => new {Kategória=x.Kategoria, Név=x.Nev,Összes=x.Osszesen});
        }
    }
}