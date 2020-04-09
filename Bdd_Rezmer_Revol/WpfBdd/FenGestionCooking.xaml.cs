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

        void recette_Combo()
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT nomRecette FROM recette;";
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                string nomRecette = reader.GetString(0);
                comboBoxRecette.Items.Add(nomRecette);
            }
            reader.Close();
        }

        void cuisinier_Combo()
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT idCuisinier FROM cuisinier;";
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                string nomCuisinier = reader.GetString(0);
                comboBoxRecette.Items.Add(nomCuisinier);
            }
            reader.Close();
        }


        private void comboBoxRecette_DropDownClosed(object sender, EventArgs e)
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "DELETE * FROM recette WHERE nomRecette='"+ comboBoxRecette.Text.ToString() + "';";
            commande.ExecuteNonQuery();
        }

        private void comboBoxCuisinier_DropDownClosed(object sender, EventArgs e)
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "DELETE * FROM cuisinier WHERE idCuisinier='" + comboBoxCuisinier.Text.ToString() + "';";
            commande.ExecuteNonQuery();
        }
        //Name="btnSupprimerRecette" Click="btnSupprimerRecette_Click"
        //Name="btnSupprimerCuisinier" Click="btnSupprimerCuisinier_Click"

    }
}
