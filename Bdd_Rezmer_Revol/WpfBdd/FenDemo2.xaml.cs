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
    /// Logique d'interaction pour FenDemo2.xaml
    /// </summary>
    public partial class FenDemo2 : Window
    {
        MySqlConnection connexion;
        DataTable tableCdR;
        public FenDemo2(MySqlConnection connexion)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.connexion = connexion;
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT COUNT(soldeCook) FROM client;";
            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();
            string nbCdR = reader.GetString(0);
            reader.Close();
            txtNbClient.Text = nbCdR;
            commande.CommandText = "SELECT nom, SUM(compteur) FROM client, recette WHERE client.idCompte=recette.idCompte GROUP BY nom;";
            commande.ExecuteNonQuery();
            tableCdR = new DataTable("Récapitulatif");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableCdR);
            dataGridCdR.ItemsSource = tableCdR.DefaultView;
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            FenDemo3 demo3 = new FenDemo3(this.connexion);
            this.Close();
            demo3.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            demo3.Show();
            this.Close();
        }

        private void dataGridCdR_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(e.PropertyName== "nom")
            {
                e.Column.IsReadOnly = true;
            }
        }
    }
}
