﻿using System;
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

namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenAjoutRecetteCdr.xaml
    /// </summary>
    public partial class FenAjoutRecetteCdr : Window
    {
        string idClient;
        bool creee = false;
        MySqlConnection connexion;
        public FenAjoutRecetteCdr(MySqlConnection connexion)
        {
            InitializeComponent();
            this.idClient = MainWindow.IdCurrentClient;
            this.connexion = connexion;
        }

        private void btnProduits_Click(object sender, RoutedEventArgs e)
        {
            FenListeProduitsRecette ajoutProduit = new FenListeProduitsRecette(this.connexion);
            ajoutProduit.Owner = this;
            this.Hide();
            ajoutProduit.ShowDialog();
            if (ajoutProduit.IsActive == false)
            {
                this.ShowDialog();
            }
        }

        private void btnValiderRecette_Click(object sender, RoutedEventArgs e)
        {
            string nomRecette, type, descriptif;
            int prixDeVente;
            nomRecette = txtBoxNomRecette.Text;
            type = comboType.Text;
            descriptif = txtBoxDescriptif.Text;
            prixDeVente = Convert.ToInt32(txtBoxPrix.Text);
            MySqlDataReader reader = null;
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM recette";
            reader = command.ExecuteReader();
            reader.Read();
            int nb = Convert.ToInt32(reader.GetValue(0));
            string idNew = "";
            if (nb < 10)
            {
                idNew = "R00"+(nb+1).ToString();
            }
            else if(nb>10 && nb < 100)
            {
                idNew = "R0" + (nb + 1).ToString();
            }
            else
            {
                idNew = "R" + (nb + 1).ToString();
            }
            command.CommandText = "INSERT INTO `cooking`.`recette` (`idRecette`, `nomRecette`, `type`, `prixDeVente`, `descriptif`,`compteur`,`idCompte`,`idCuisinier`) VALUES (@id, @nomRecette, @type, @prix, @descriptif,0,@idClient,'C002')";
            command.Parameters.AddWithValue("@nomRecette", nomRecette);
            command.Parameters.AddWithValue("@descriptif", descriptif);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@idClient", idClient);
            command.Parameters.AddWithValue("@prix", prixDeVente);
            command.Parameters.AddWithValue("@id", idNew);
            reader.Close();
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE client SET soldeCook = soldeCook + 4 WHERE idCompte=\"" + idClient + "\";";
            command.ExecuteNonQuery();
            MessageBox.Show("Creation de la recette réussie !");
            creee = true;
            this.Close();
        }

        void Fermeture(object sender, CancelEventArgs e)
        {
            if (creee == false)
            {
                string msg = "Vous n'avez pas enregistré votre recette. Voulez vous vraiment quitter ?";
                MessageBoxResult result = MessageBox.Show(msg, "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    string msg2 = "Vous avez quitté sans enregistrer";
                    MessageBox.Show(msg2, "Cooking App", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            else
            {
                string msg = "Merci pour votre contribution. Votre solde cook a été crédité de 4 cooks";
                MessageBoxResult result = MessageBox.Show(msg, "Merci", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
