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
    /// Logique d'interaction pour FenDemo3.xaml
    /// </summary>
    public partial class FenDemo3 : Window
    {
        MySqlConnection connexion;
        public FenDemo3(MySqlConnection connexion)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.connexion = connexion;
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT COUNT(nomRecette) FROM recette;";
            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();
            string nbClients = reader.GetString(0);
            reader.Close();
            txtNbClient.Text = nbClients;
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            FenDemo4 demo4 = new FenDemo4(this.connexion);
            this.Close();
            demo4.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            demo4.Show();
            this.Close();
        }
    }
}
