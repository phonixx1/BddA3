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
using System.ComponentModel;

namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenMesRecettes.xaml
    /// </summary>
    public partial class FenMesRecettes : Window
    {
        MySqlConnection connexion;
        DataTable tableMesRecettes;
        string idClient;

        public FenMesRecettes(MySqlConnection connexion)
        {
            InitializeComponent();
            this.idClient = MainWindow.IdCurrentClient;
            this.connexion = connexion;
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT * FROM recette WHERE idCompte=\"" + idClient + "\";";
            commande.ExecuteNonQuery();
            tableMesRecettes = new DataTable("Produit(s) disponible(s)");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableMesRecettes);
            dataGridMesRecettes.ItemsSource = tableMesRecettes.DefaultView;
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        void Fermeture(object sender, CancelEventArgs e)
        {
            
        }
    }
}
