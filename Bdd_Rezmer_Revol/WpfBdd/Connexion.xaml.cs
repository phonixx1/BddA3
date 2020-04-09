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
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        MySqlConnection connexion = null;
        public Connexion()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public MySqlConnection Connection
        {
            get { return this.connexion; }
        }

        private void btnValidation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=" + txtBoxId.Text.ToString() + ";PASSWORD=" +boxMdp.Password.ToString();
                connexion = new MySqlConnection(connexionString);
                connexion.Open();
                this.DialogResult = true;
            }
            catch
            {
                connexion = null;
            }
         }
        
        private void boxMdp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    string connexionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=" + txtBoxId.Text.ToString() + ";PASSWORD=" + boxMdp.Password.ToString();
                    connexion = new MySqlConnection(connexionString);
                    connexion.Open();
                    this.DialogResult = true;
                }
                catch
                {
                    connexion = null;
                }
            }
        }
    }
}
