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

namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenMenuClients.xaml
    /// </summary>
    public partial class FenMenuClients : Window
    {
        MySqlConnection connexion = null;
        string idClient;
        public static bool test = false;
        
        public FenMenuClients(MySqlConnection connexion)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.idClient = MainWindow.IdCurrentClient;
            this.connexion = connexion;
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCommande_Click(object sender, RoutedEventArgs e)
        {
            FenMenuCommande menuCommande = new FenMenuCommande(this.connexion);
            menuCommande.Owner = this;
            this.Hide();
            menuCommande.ShowDialog();
            if (menuCommande.IsActive == false)
            {
                this.ShowDialog();
            }
        }

        private void btnCdR_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataReader reader = null;
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT soldeCook FROM client WHERE idCompte=\"" + idClient + "\";";
            reader = command.ExecuteReader();
            reader.Read();
            bool notCdR = reader.IsDBNull(0);
            reader.Close();
            if (notCdR)
            {
                MessageBoxResult choix = MessageBox.Show("Erreur : vous n'êtes pas enregistré(e) en tant que créateur de recette. Voulez-vous devenir un CdR ?", "Erreur", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);
                if (choix == MessageBoxResult.OK)
                {
                    MySqlCommand commande = this.connexion.CreateCommand();
                    commande.CommandText = "UPDATE client SET soldeCook=0 WHERE idCompte=\"" + idClient + "\";";
                    commande.ExecuteNonQuery();
                    MessageBox.Show("Vous êtes désormais un CdR", "Cooking App", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                } 
            }
            else
            {
                FenMenuDesCdr menuCdR = new FenMenuDesCdr(this.connexion);
                menuCdR.Owner = this;
                this.Hide();
                menuCdR.ShowDialog();
                if (menuCdR.IsActive == false)
                {
                    this.ShowDialog();
                }
            }
        }
        private void btnGererCompte_Click(object sender, RoutedEventArgs e)
        {
            FenGererCompte monCompte = new FenGererCompte(this.connexion);
            monCompte.Owner = this;
            this.Hide();
            monCompte.ShowDialog();
            if (monCompte.IsActive == false)
            {
                try
                {
                    this.ShowDialog();
                }
                catch { }
            }
        }

        void Fermeture(object sender, CancelEventArgs e)
        {
            if(test==false)
            {
                FenMenuClients.test = true;
                string msg = "Vous allez être déconnecté(e). Voulez vous vraiment quitter ?";
                MessageBoxResult result = MessageBox.Show(msg, "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    string msg2 = "Merci de votre visite";
                    MessageBox.Show(msg2, "Cooking App", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }
    }
}
