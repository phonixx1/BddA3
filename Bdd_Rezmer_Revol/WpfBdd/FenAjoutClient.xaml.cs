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
    /// Logique d'interaction pour FenAjoutClient.xaml
    /// </summary>
    public partial class FenAjoutClient : Window
    {
        bool cree = false;
        MySqlConnection maconnexion;
        public FenAjoutClient(MySqlConnection connexion)
        {
            InitializeComponent();
            this.maconnexion = connexion;
        }

        

        private void btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataReader reader=null;
            MySqlCommand commande=null;
            string nom, tel, id, mdp;
            nom = txtBoxNom.Text;
            tel = txtBoxTel.Text;
            string message = "iDClient deja utilisé";
            
            mdp = boxMdp.Password.ToString();
            
            try
            {
                commande = maconnexion.CreateCommand();
                commande.CommandText = "SELECT * FROM client WHERE idCompte=@id;";
                commande.Parameters.AddWithValue("@id", txtBoxiD.Text.ToString());
                reader = commande.ExecuteReader();
                if (!reader.HasRows)
                {
                    id = txtBoxiD.Text.ToString();
                    commande.CommandText = "INSERT INTO `cooking`.`client` (`idCompte`, `nom`, `numeroTel`, `mdp`, `soldeCook`) VALUES(@id, @nom, @tel, @mdp,@cdr)";
                    commande.Parameters.AddWithValue("@nom", nom);
                    commande.Parameters.AddWithValue("@tel", tel);
                    commande.Parameters.AddWithValue("@mdp", mdp);
                    if (comboChoix.SelectedIndex == 0)
                    {
                       commande.Parameters.AddWithValue("@cdr", null);
                    }
                    else
                    {
                        commande.Parameters.AddWithValue("@cdr", 0);
                    }
                    reader.Close();
                    commande.ExecuteNonQuery();
                    MessageBox.Show("Creation de compte reussi");
                    cree = true;
                    this.Close();
                }
                else
                {
                    reader.Close();
                    MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }  
        }
        void Fermeture(object sender, CancelEventArgs e)
        {
            if (cree == false)
            {
                string msg = "Vous n'avez pas enregistré votre compte. Voulez vous vraiment quitter ?";
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
                string msg = "Bienvenue chez cooking ! Vous pouvez désormais effectuer votre première commande.";
                MessageBoxResult result = MessageBox.Show(msg, "Merci", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
