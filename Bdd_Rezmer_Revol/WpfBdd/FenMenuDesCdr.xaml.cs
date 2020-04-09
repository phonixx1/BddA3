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
    /// Logique d'interaction pour FenMenuDesCdr.xaml
    /// </summary>
    public partial class FenMenuDesCdr : Window
    {
        MySqlConnection connexion;
        string idClient;
        public FenMenuDesCdr(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
            this.idClient = MainWindow.IdCurrentClient;
        }
        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnMesRecettes_Click(object sender, RoutedEventArgs e)
        {
            FenMesRecettes mesRecette = new FenMesRecettes(this.connexion);
            mesRecette.Owner = this;
            this.Hide();
            mesRecette.ShowDialog();
            if (mesRecette.IsActive == false)
            {
                this.ShowDialog();
            }
        }

        private void btnAjoutRecette_Click(object sender, RoutedEventArgs e)
        {
            FenAjoutRecetteCdr ajoutRecette = new FenAjoutRecetteCdr(this.connexion);
            ajoutRecette.Owner = this;
            this.Hide();
            ajoutRecette.ShowDialog();
            if (ajoutRecette.IsActive == false)
            {
                this.ShowDialog();
            }
        }

        private void btnSoldeCook_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataReader reader = null;
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT soldeCook FROM client WHERE idCompte=\"" + idClient + "\";";
            reader = command.ExecuteReader();
            reader.Read();
            string soldeCook="";
            soldeCook = reader.GetValue(0).ToString();
            reader.Close();
            MessageBox.Show("Vous avez actuellement "+soldeCook+" cook(s)", "Attention", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}


