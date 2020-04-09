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
    /// Logique d'interaction pour FenGererCompte.xaml
    /// </summary>
    public partial class FenGererCompte : Window
    {
        MySqlConnection connexion;
        string idClient;
        bool modifie = false;
        public FenGererCompte(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
            this.idClient = MainWindow.IdCurrentClient;
        }
        private void btnChangerId_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "UPDATE client SET idCompte =\"" + txtNewId.Text + "\" WHERE idCompte=\"" + idClient + "\";";
            command.ExecuteNonQuery();
            MessageBox.Show("Votre identifiant a bien été modifié");
            modifie = true;
            this.Owner.Owner.Show();
            FenMenuClients.test = true;
            this.Owner.Close();
        }
        private void btnChangerMdp_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "UPDATE client SET mdp =\"" + txtNewMdp.Text + "\" WHERE idCompte=\"" + idClient + "\";";
            command.ExecuteNonQuery();
            MessageBox.Show("Votre mot de passe a bien été modifié");
            modifie = true;
        }
        private void btnChangerNumTel_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "UPDATE client SET numeroTel =\"" + txtNumTel.Text + "\" WHERE idCompte=\"" + idClient + "\";";
            command.ExecuteNonQuery();
            MessageBox.Show("Votre numéro de téléphone a bien été modifié");
            modifie = true;
        }
        private void btnDeleteCompte_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Souhaitez-vous supprimer définitivement votre compte cook ?";
            MessageBoxResult result = MessageBox.Show(msg, "Attention", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Cancel)
            {
               
            }
            else
            {
                MySqlCommand command1 = connexion.CreateCommand();
                command1.CommandText = "DELETE FROM client WHERE idCompte =\"" + idClient + "\";";
                command1.ExecuteNonQuery();
                MessageBox.Show("Votre compte a bien été supprimé. Vous allez retourner sur la fenêtre principale.");
                this.Close();
            }
        }
        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        void Fermeture(object sender, CancelEventArgs e)
        {
            if (modifie == false)
            {
                string msg = "Voulez-vous vraiment quitter ?";
                MessageBoxResult result = MessageBox.Show(msg, "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    
                }
            }
        }
    }
}
