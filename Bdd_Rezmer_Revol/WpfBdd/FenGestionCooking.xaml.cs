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
using System.ComponentModel;
using System.Data;


namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenGestionCooking.xaml
    /// </summary>
    public partial class FenGestionCooking : Window
    {
        MySqlConnection connexion;
        DataTable tableTop5;
        public FenGestionCooking(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
            ConnexionAdmin fenConnexion = new ConnexionAdmin(this.connexion);
            /*
            this.Hide();
            fenConnexion.ShowDialog();
            if (fenConnexion.DialogResult == true)
            {
                InitializeComponent();
                MessageBox.Show("Connexion réussie");
                this.ShowDialog();
            }
            else
            {
                MessageBox.Show("Echec de la connexion");
                this.Close();
            }
            */
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT nomRecette, type, prixDeVente, compteur, idCuisinier FROM recette ORDER BY compteur DESC;";
            commande.ExecuteNonQuery();
            tableTop5 = new DataTable("Top 5 des recettes");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableTop5);
            dataGridTop5.ItemsSource = tableTop5.DefaultView;
            commande.CommandText = "SELECT nom, MAX(compteur), client.idCompte, recette.idCompte FROM client, recette WHERE client.idCompte=recette.idCompte;";
            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();
            string cdrSemaine = reader.GetString(0);
            txtCdrWeek.Text = cdrSemaine;
            txtGoldenCdr.Text = cdrSemaine;
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnSupprimerRecette_Click(object sender, RoutedEventArgs e)
        {
            FenSupRecette supRecette = new FenSupRecette(this.connexion);
            this.Hide();
            supRecette.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            supRecette.Owner = this;
            supRecette.ShowDialog();
            this.ShowDialog();
        }
        private void btnSupprimerCuisinier_Click(object sender, RoutedEventArgs e)
        {
            FenSupCuisinier supCuisinier = new FenSupCuisinier(this.connexion);
            this.Hide();
            supCuisinier.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            supCuisinier.Owner = this;
            supCuisinier.ShowDialog();
            this.ShowDialog();
        }

    }
}
