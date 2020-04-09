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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.ComponentModel;


namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connexion = null;
        static string idCurrentClient;

        public static string IdCurrentClient
        {
            get { return idCurrentClient; }
        }

        public MainWindow()
        {
            
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Connexion fenConnexion = new Connexion();
            this.Hide();
            fenConnexion.ShowDialog();
            if (fenConnexion.DialogResult == true)
            {
                InitializeComponent();
                connexion = fenConnexion.Connection;
                MessageBox.Show("Connexion réussie");
                this.ShowDialog();
            }
            else
            {
                MessageBox.Show("Echec de la connexion");
                this.Close();
            }
        }

        void Fermeture(object sender, CancelEventArgs e)
        {

            string msg = "Voulez vous vraiment quitter ?";
            MessageBoxResult result = MessageBox.Show(msg, "Cooking App", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                string msg2 = "A bientôt sur les services Cooking";
                MessageBox.Show(msg2, "Cooking App", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                if (connexion != null)
                {
                    connexion.Close();
                }
            }
        }
        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            FenAjoutClient newClient = new FenAjoutClient(this.connexion);
            this.Hide();
            newClient.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newClient.Owner = this;
            newClient.ShowDialog();
            this.ShowDialog();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            FenGestionCooking gestion = new FenGestionCooking(this.connexion);
            this.Hide();
            gestion.Owner = this;
            gestion.ShowDialog();
            if (gestion.IsActive == false)
            {
                this.ShowDialog();
            }
        }

        public void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            
            MySqlDataReader reader = null;
            
            string msg;
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT * FROM client WHERE idCompte=\"" + txtBoxId.Text.ToString() + "\";";
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (boxMdp.Password.ToString() == reader.GetString("mdp"))
                {
                    idCurrentClient = txtBoxId.Text;
                    MessageBox.Show("Connexion reussie");
                    reader.Close();
                    txtBoxId.Clear();
                    boxMdp.Clear();
                    FenMenuClients menuClient = new FenMenuClients(this.connexion);
                    menuClient.Owner = this;
                    this.Hide();
                    menuClient.ShowDialog();
                    if (menuClient.IsActive == false)
                    {
                        this.ShowDialog();
                    }
                }
                else
                {
                    reader.Close();
                    msg = "Mot de passe incorrect";
                    MessageBox.Show(msg, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                reader.Close();
                msg = "Veulliez créer un compte -> Nouveau Client";
                MessageBox.Show(msg, "Pas de compte chez Cooking", MessageBoxButton.OK, MessageBoxImage.Warning);

            }  
        }

        private void boxMdp_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.Key == Key.Return)
            {
                MySqlDataReader reader = null;
                try
                {
                    string msg;
                    MySqlCommand command = connexion.CreateCommand();
                    command.CommandText = "SELECT * FROM client WHERE idCompte=\"" + txtBoxId.Text.ToString() + "\";";

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (boxMdp.Password.ToString() == reader.GetString("mdp"))
                        {
                            idCurrentClient = txtBoxId.Text;
                            MessageBox.Show("Connexion reussie");
                        }
                        else
                        {
                            msg = "Mot de passe incorrect";
                            MessageBox.Show(msg, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                    }
                    else
                    {
                        msg = "Veulliez créer un compte -> Nouveau Client";
                        MessageBox.Show(msg, "Pas de compte chez Cooking", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    reader.Close();
                }*/
            }
        
    }
}
