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

namespace WpfBdd
{
   
    /// <summary>
    /// Logique d'interaction pour FenMenuCommande.xaml
    /// </summary>
    public partial class FenMenuCommande : Window
    {
        List<String> listChoix=new List<String>();
        MySqlConnection connexion;
        string idClient;

        public FenMenuCommande(MySqlConnection connexion)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.connexion = connexion;
            this.idClient = MainWindow.IdCurrentClient;
            recette_Combo();
            listBoxCommande.ItemsSource = listChoix;  
        }

        void recette_Combo()
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT * FROM recette;";
            MySqlDataReader reader = commande.ExecuteReader();
            while(reader.Read())
            {
                string nomRecette = reader.GetString(0)+" "+ reader.GetString(1);
                comboBoxRecette.Items.Add(nomRecette);
            }
            reader.Close();
        }

        private void comboBoxRecette_DropDownClosed(object sender, EventArgs e)
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            if (comboBoxRecette.Text != "")
            {
                commande.CommandText = "SELECT * FROM recette WHERE idRecette='" + comboBoxRecette.Text.ToString().Substring(0, 4) + "';";


                MySqlDataReader reader = commande.ExecuteReader();
                reader.Read();

                string sIdRecette = reader.GetString(0);
                string sNomRecette = reader.GetString(1);
                string sTypeRecette = reader.GetString(2);
                string sPrixDeVente = reader.GetInt32(3).ToString();
                string sDescriptif = reader.GetString(4);

                txtBoxDescri.Text = sDescriptif;
                txtBoxNom.Text = sNomRecette;
                txtBoxNum.Text = sIdRecette;
                txtBoxType.Text = sTypeRecette;
                txtBoxPrix.Text = sPrixDeVente;


                reader.Close();
            }
        }

        private void btnAjout_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxRecette.SelectedItem == null)
            {
                MessageBox.Show("Veuillez d'abord choisir une recette dans le menu déroulant", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                listChoix.Add(txtBoxNum.Text + txtBoxNom.Text);
                listBoxCommande.Items.Refresh();
                comboBoxRecette.Items.Remove(comboBoxRecette.SelectedItem);
                comboBoxRecette.SelectedItem = null;
                txtBoxDescri.Text = "";
                txtBoxNom.Text = "";
                txtBoxNum.Text = "";
                txtBoxType.Text = "";
                txtBoxPrix.Text = "";
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            string choix2=listBoxCommande.SelectedItem as String;
            if (listChoix.Contains(choix2))
            {
                listChoix.Remove(choix2);
                comboBoxRecette.Items.Add(choix2);
                txtBoxDescri.Text = "";
                txtBoxNom.Text = "";
                txtBoxNum.Text = "";
                txtBoxType.Text = "";
                txtBoxPrix.Text = "";
            }
            
            listBoxCommande.Items.Refresh();
        }

        private void btnCommande_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            bool commandeBonne = true;
            string msg;
            FenNbDeCommande fenQuantite;
            string nombre;
            MySqlDataReader reader;
            List<String> aRetirer = new List<String>();
            List<String> quantiteList = new List<String>();
            int compteurRecette;
            List<String> idProduitAdecrementer= new List<String>();
            int prixRecette;

            foreach (string eleme in listChoix)
            {
                fenQuantite = new FenNbDeCommande(eleme);
                fenQuantite.ShowDialog();
                if(fenQuantite.DialogResult==false)
                {
                    nombre = "1";
                }
                else
                {
                    nombre = fenQuantite.Nombre;
                }

                commande.CommandText = "select idProduit from estconstitue natural join produit where idRecette='" + eleme.Substring(0, 4) + "' and "+ nombre + "*estconstitue.quantiteUtilisee>produit.stockActuel;";
                reader = commande.ExecuteReader();
                reader.Read();
                if(reader.HasRows)
                {
                    commandeBonne = false;
                    aRetirer.Add(eleme);
                    
                    msg = "Il n'y a pas assez de produits pour " + nombre + " " + eleme;
                    MessageBox.Show(msg);
                }
                reader.Close();
                if(listChoix.Count==0)
                {
                    break;
                }
                quantiteList.Add(nombre);
                

            }
            foreach(string elem in aRetirer)
            {
                listChoix.Remove(elem);
                comboBoxRecette.Items.Add(elem);
                listBoxCommande.Items.Refresh();
            }
            bool messageCommandeValidee = true;
            if(commandeBonne)
            {
                foreach( string elem in listChoix)
                {
                    bool solde = false;
                    int solde1 = 0;
                    commande.CommandText = "select prixDeVente from recette where idRecette='" + elem.Substring(0, 4) + "';";
                    reader = commande.ExecuteReader();
                    reader.Read();
                    prixRecette = reader.GetInt32(0);
                    reader.Close();
                    commande.CommandText = "select soldeCook from client where idCompte='" + idClient + "';";
                    reader = commande.ExecuteReader();
                    reader.Read();
                    solde = reader.IsDBNull(0);
                    reader.Close();
                    if (solde == false)
                    {
                        commande.CommandText = "select soldeCook from client where idCompte='" + idClient + "';";
                        reader = commande.ExecuteReader();
                        reader.Read();
                        solde1 = reader.GetInt32(0);
                        reader.Close();
                    }
                    if ((prixRecette * Convert.ToInt32(quantiteList[listChoix.IndexOf(elem)]) < solde1) || solde==true)
                    {
                        commande.CommandText = "UPDATE recette set compteur = compteur+" + quantiteList[listChoix.IndexOf(elem)] + " where idRecette ='" + elem.Substring(0, 4) + "';";
                        commande.ExecuteNonQuery();
                        commande.CommandText = "select * from commande where idRecette='" + elem.Substring(0, 4) + "' and idCompte='" + MainWindow.IdCurrentClient + "';";
                        reader = commande.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Close();
                            commande.CommandText = "UPDATE commande set quantite=quantite+" + quantiteList[listChoix.IndexOf(elem)] + " where idRecette='" + elem.Substring(0, 4) + "' and idCompte='" + MainWindow.IdCurrentClient + "';";
                        }
                        else
                        {
                            reader.Close();
                            commande.CommandText = "INSERT INTO `cooking`.`commande` (`idRecette`, `idCompte`,`quantite`) VALUES('" + elem.Substring(0, 4) + "', '" + MainWindow.IdCurrentClient + "', " + quantiteList[listChoix.IndexOf(elem)] + ");";
                        }
                        commande.ExecuteNonQuery();
                        commande.CommandText = "UPDATE client set soldeCook = soldeCook - " + quantiteList[listChoix.IndexOf(elem)] + "*" + Convert.ToString(prixRecette) + " where idCompte = '" + MainWindow.IdCurrentClient + "';";
                        commande.ExecuteNonQuery();
                        commande.CommandText = "select compteur from recette where idRecette='" + elem.Substring(0, 4) + "';";
                        reader = commande.ExecuteReader();
                        reader.Read();
                        compteurRecette = reader.GetInt32(0);
                        reader.Close();
                        if (compteurRecette > 10 && compteurRecette - Convert.ToInt32(quantiteList[listChoix.IndexOf(elem)]) < 10)
                        {
                            commande.CommandText = "UPDATE recette SET prixDeVente=prixDeVente+2  where idRecette='" + elem.Substring(0, 4) + "';";
                            commande.ExecuteNonQuery();

                        }
                        else if (compteurRecette > 50 && compteurRecette - Convert.ToInt32(quantiteList[listChoix.IndexOf(elem)]) < 50)
                        {
                            commande.CommandText = "UPDATE recette SET prixDeVente=prixDeVente+5  where idRecette='" + elem.Substring(0, 4) + "';";
                            commande.ExecuteNonQuery();

                        }

                        if (compteurRecette < 50)
                        {
                            commande.CommandText = "UPDATE client SET soldeCook=soldeCook+2*" + quantiteList[listChoix.IndexOf(elem)] + " where idCompte=(select idCompte from recette where idRecette='" + elem.Substring(0, 4) + "');";
                        }
                        else
                        {
                            commande.CommandText = "UPDATE client SET soldeCook=soldeCook+4*" + quantiteList[listChoix.IndexOf(elem)] + " where idCompte=(select idCompte from recette where idRecette='" + elem.Substring(0, 4) + "');";
                        }
                        commande.ExecuteNonQuery();
                        commande.CommandText = "UPDATE produit natural join estconstitue natural join recette SET stockActuel=stockActuel-" + quantiteList[listChoix.IndexOf(elem)] + "*estconstitue.quantiteUtilisee where recette.idRecette='" + elem.Substring(0, 4) + "' and  recette.idRecette=estconstitue.idRecette and estconstitue.idProduit=produit.idProduit;";
                        commande.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Vous n'avez pas assez de cooks pour payer votre commande. Cette dernière est annulée.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        messageCommandeValidee = false;
                        this.Close();
                    }
                }
                if (messageCommandeValidee)
                {
                    MessageBox.Show("Commande validée.\nTous les produits sont disponibles\nRedirection vers notre site de paiement sécurisé...");
                    this.Close();
                }
            }    
            else
            {
                MessageBox.Show("Les recettes dont les produits ne sont pas disponibles ont été enlevées. Veuillez vérifier votre commande.");
            }
         }
    }
}
