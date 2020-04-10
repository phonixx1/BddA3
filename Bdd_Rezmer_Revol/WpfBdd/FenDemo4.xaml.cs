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
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenDemo4.xaml
    /// </summary>
    public partial class FenDemo4 : Window
    {
        MySqlConnection connexion;
        DataTable tableProduit;
        public FenDemo4(MySqlConnection connexion)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.connexion = connexion;
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT idProduit, nomProduit, stockActuel, stockMin FROM produit WHERE stockActuel <= 2*stockMin;";
            commande.ExecuteNonQuery();
            tableProduit = new DataTable("Récapitulatif");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableProduit);
            dataGridProduit.ItemsSource = tableProduit.DefaultView;
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            FenDemo5 demo5 = new FenDemo5(this.connexion,this.tableProduit);
            this.Hide();
            demo5.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            demo5.Owner = this;
            demo5.ShowDialog();
            this.ShowDialog();
        }

        private void dataGridProduit_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "nomProduit")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "stockActuel")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "stockMin")
            {
                e.Column.IsReadOnly = true;
            }
        }
    }
}
