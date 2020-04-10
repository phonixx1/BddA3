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
    /// Logique d'interaction pour FenDemo.xaml
    /// </summary>
    public partial class FenDemo : Window
    {
        MySqlConnection connexion;
        public FenDemo(MySqlConnection connexion)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.connexion = connexion;
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT COUNT(nom) FROM client;";
            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();
            string nbClients = reader.GetString(0);
            reader.Close();
            txtNbClient.Text = nbClients;
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            FenDemo2 demo2 = new FenDemo2(this.connexion);
            demo2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            demo2.Show();
            this.Close();
        }
    }
}
