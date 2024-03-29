﻿using System;
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
using System.ComponentModel;
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
        static string recetteSupprimee;
        static string cdrSupprime;
        List<string> nomCdR = new List<string>();
        List<int> compteurCdR = new List<int>();

        public FenGestionCooking(MySqlConnection connexion)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.connexion = connexion;
            /*ConnexionAdmin fenConnexion = new ConnexionAdmin(this.connexion);
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
            }*/
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT nomRecette, type, prixDeVente, compteur AS nombreCommandes, idCompte, idCuisinier FROM recette ORDER BY compteur DESC;";
            commande.ExecuteNonQuery();
            tableTop5 = new DataTable("Top 5 des recettes");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableTop5);
            dataGridTop5.ItemsSource = tableTop5.DefaultView;
            commande.CommandText = "SELECT nom, SUM(compteur) FROM client, recette WHERE client.idCompte=recette.idCompte GROUP BY nom;";
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                nomCdR.Add(reader.GetString(0));
                compteurCdR.Add(reader.GetInt32(1));
            }
            reader.Close();
            int compteur = 0;
            string cdrSemaine = "";
            for (int i = 0; i < nomCdR.Count; i++)
            {
                int compteur1 = compteurCdR[i];
                if (compteur1 > compteur)
                {
                    compteur = compteur1;
                    cdrSemaine = nomCdR[i];
                }
            }
            txtCdrWeek.Text = cdrSemaine;
            txtGoldenCdr.Text = cdrSemaine;
            recette_Combo();
            cdr_Combo();
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

        void cdr_Combo()
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT idCompte, nom FROM client WHERE soldeCook IS NOT NULL;";
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                string nomCdR = reader.GetString(0) + " " + reader.GetString(1);
                comboBoxCdR.Items.Add(nomCdR);
            }
            reader.Close();
        }


        private void comboBoxRecette_DropDownClosed(object sender, EventArgs e)
        {
            recetteSupprimee = comboBoxRecette.Text.ToString();
        }

        private void comboBoxCdR_DropDownClosed(object sender, EventArgs e)
        {
            cdrSupprime = comboBoxCdR.Text.ToString();
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
                commande.CommandText = "SELECT nomRecette, type, prixDeVente, compteur AS nombreCommandes, idCompte, idCuisinier FROM recette ORDER BY compteur DESC;";
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

        private void btnSupprimerCdR_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxCdR.SelectedItem != null)
            {
                MySqlCommand commande = this.connexion.CreateCommand();
                commande.CommandText = "UPDATE client SET soldeCook=NULL WHERE idCompte=\"" + cdrSupprime.Substring(0, 4) + "\";"; ;
                commande.ExecuteNonQuery();
                commande.CommandText = "DELETE FROM recette WHERE idCompte=\"" + cdrSupprime.Substring(0, 4) + "\";"; ;
                commande.ExecuteNonQuery();
                MessageBox.Show(cdrSupprime + " n'est plus CdR. Ses recettes ont toutes été supprimées.", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                comboBoxCdR.Items.Remove(comboBoxCdR.SelectedItem);
                comboBoxCdR.SelectedItem = null;
                commande.CommandText = "SELECT nomRecette, type, prixDeVente, compteur AS nombreCommandes, idCompte, idCuisinier FROM recette ORDER BY compteur DESC;";
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
            genererXML(path);
            MessageBox.Show("Le fichier de commande .XML a bien été généré dans le répertoire " + path, "Information");
        }

        private void genererXML(string path)
        {
            List<Fournisseur> listeCommande = new List<Fournisseur>();
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT nomF, produit.refFournisseur FROM produit, fournisseur WHERE produit.refFournisseur = fournisseur.refFournisseur AND produit.stockActuel < produit.stockMin GROUP BY produit.refFournisseur ORDER BY produit.refFournisseur;";
            MySqlDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                Fournisseur nouveau = new Fournisseur();
                List<Produit> newListe = new List<Produit>();
                nouveau.NomF = reader.GetString(0);
                nouveau.IdF = reader.GetString(1);
                nouveau.ListeProduits = newListe;
                listeCommande.Add(nouveau);
            }
            reader.Close();
            Commande newCommande = new Commande();
            newCommande.ListeCommande = listeCommande;
            commande.CommandText = "SELECT produit.refFournisseur, produit.idProduit, nomProduit, produit.stockMax - produit.stockActuel AS quantiteCommandee, produit.uniteDeQuantite FROM produit, fournisseur WHERE produit.refFournisseur = fournisseur.refFournisseur AND produit.stockActuel < produit.stockMin ORDER BY produit.refFournisseur;";
            MySqlDataReader reader1 = commande.ExecuteReader();
            while (reader1.Read())
            {
                Produit nouveauP = new Produit();
                nouveauP.NomP = reader1.GetString(2);
                nouveauP.IdP = reader1.GetString(1);
                nouveauP.Quantite = reader1.GetString(3);
                nouveauP.Unite = reader1.GetString(4);
                foreach(Fournisseur f in listeCommande)
                {
                    if (f.IdF == reader1.GetString(0))
                    {
                        f.ListeProduits.Add(nouveauP);
                    }
                }
            }
            reader1.Close();
            XmlSerializer xs = new XmlSerializer(typeof(Commande));
            XmlWriter writer = XmlWriter.Create(path + "/commandes.xml");
            writer.Close();
            StreamWriter wr = new StreamWriter(path + "/commandes.xml");
            xs.Serialize(wr, newCommande);
            wr.Close();
        }
    }
}

