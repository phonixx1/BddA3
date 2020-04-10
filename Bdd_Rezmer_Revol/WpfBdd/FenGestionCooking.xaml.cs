using System;
using System.IO;
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
using System.Xml;
using System.Xml.Serialization;


namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenGestionCooking.xaml
    /// </summary>
    public partial class FenGestionCooking : Window
    {
        
        MySqlConnection connexion;
        DataTable tableTop5;
        DataTable tableCommande;
        static string recetteSupprimee;
        static string cuisinierSupprime;
        public FenGestionCooking(MySqlConnection connexion)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
            reader.Close();
            txtCdrWeek.Text = cdrSemaine;
            txtGoldenCdr.Text = cdrSemaine;
            recette_Combo();
            cuisinier_Combo();
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
                comboBoxCuisinier.Items.Add(nomCuisinier);
            }
            reader.Close();
        }


        private void comboBoxRecette_DropDownClosed(object sender, EventArgs e)
        {
            recetteSupprimee = comboBoxRecette.Text.ToString();   
        }

        private void comboBoxCuisinier_DropDownClosed(object sender, EventArgs e)
        {
            cuisinierSupprime = comboBoxCuisinier.Text.ToString();
        }

        private void btnSupprimerRecette_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxRecette.SelectedItem != null)
            {
                MySqlCommand commande = this.connexion.CreateCommand();
                commande.CommandText = "DELETE FROM recette WHERE nomRecette=\"" + recetteSupprimee + "\";"; ;
                commande.ExecuteNonQuery();
                MessageBox.Show("La recette " + recetteSupprimee + " a bien été supprimée.", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                comboBoxRecette.Items.Remove(comboBoxRecette.SelectedItem);
                comboBoxRecette.SelectedItem = null;
                commande.CommandText = "SELECT nomRecette, type, prixDeVente, compteur, idCuisinier FROM recette ORDER BY compteur DESC;";
                commande.ExecuteNonQuery();
                tableTop5 = new DataTable("Top 5 des recettes");
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
                dataAdp.Fill(tableTop5);
                dataGridTop5.ItemsSource = tableTop5.DefaultView;
            }
            else
            {
                MessageBox.Show("Vous n'avez rien sélectionné", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void btnSupprimerCuisinier_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxCuisinier.SelectedItem != null)
            {
                MySqlCommand commande = this.connexion.CreateCommand();
                commande.CommandText = "DELETE FROM cuisinier WHERE idCuisinier=\"" + cuisinierSupprime + "\";"; ;
                commande.ExecuteNonQuery();
                MessageBox.Show("Le cusinier " + cuisinierSupprime + " a bien été supprimé.", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                comboBoxCuisinier.Items.Remove(comboBoxCuisinier.SelectedItem);
                comboBoxCuisinier.SelectedItem = null;
                commande.CommandText = "SELECT nomRecette, type, prixDeVente, compteur, idCuisinier FROM recette ORDER BY compteur DESC;";
                commande.ExecuteNonQuery();
                tableTop5 = new DataTable("Top 5 des recettes");
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
                dataAdp.Fill(tableTop5);
                dataGridTop5.ItemsSource = tableTop5.DefaultView;
            }
            else
            {
                MessageBox.Show("Vous n'avez rien sélectionné", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnXML_Click(object sender, RoutedEventArgs e)
        {

            string path = "D:/ESILV/2019 2020/Semestre 6/Base de données/TD/DM";
            //string path = Directory.GetCurrentDirectory();
            XmlWriter writer = XmlWriter.Create(path+"/test.xml");
            writer.Close();
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT produit.idProduit, nomProduit, produit.refFournisseur, nomF FROM produit, fournisseur WHERE produit.refFournisseur = fournisseur.refFournisseur AND produit.stockActuel < produit.stockMin ORDER BY produit.refFournisseur;";
            commande.ExecuteNonQuery();
            tableCommande = new DataTable("Fournisseur");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableCommande);
            tableCommande.WriteXml(path+"/test.xml");
            commande.CommandText = "SELECT produit.idProduit, nomProduit, produit.refFournisseur, nomF FROM produit, fournisseur WHERE produit.refFournisseur = fournisseur.refFournisseur AND produit.stockActuel < produit.stockMin ORDER BY produit.idProduit;";
            commande.ExecuteNonQuery();
            tableCommande = new DataTable("Produit");
            dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableCommande);
            tableCommande.WriteXml("D:/ESILV/2019 2020/Semestre 6/Base de données/TD/DM/test.xml");
            MessageBox.Show("Le fichier .XML a bien été généré dans le répertoire " + path,"Information");
        }
    }
}
