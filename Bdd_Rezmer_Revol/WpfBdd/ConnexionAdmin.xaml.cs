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
    /// Logique d'interaction pour ConnexionAdmin.xaml
    /// </summary>
    public partial class ConnexionAdmin : Window
    {
        MySqlConnection connexion;
        public ConnexionAdmin(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
        }

        private void boxMdpAdmin_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                if (boxMdpAdmin.Password.ToString() == "azertyqwerty92")
                {
                    boxMdpAdmin.Clear();
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Mot de passe incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } 
        }

        void Fermeture(object sender, CancelEventArgs e)
        {
            
        }
    }
}
