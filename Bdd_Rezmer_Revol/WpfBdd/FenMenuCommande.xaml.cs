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
    /// Logique d'interaction pour FenMenuCommande.xaml
    /// </summary>
    public partial class FenMenuCommande : Window
    {
        List<String> listChoix=new List<String>();
        MySqlConnection connexion;
       
        public FenMenuCommande(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
            recette_Combo();
            listBoxCommande.ItemsSource = listChoix;
            
        }
        void recette_Combo()
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT * FROM recette;";
            MySqlDataReader reader = commande.ExecuteReader();
            while(reader.Read())
            {
                string nomRecette = reader.GetString(0)+" "+ reader.GetString(1);
                comboBoxRecette.Items.Add(nomRecette);
            }
            reader.Close();
        }

        

        private void comboBoxRecette_DropDownClosed(object sender, EventArgs e)
        {
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT * FROM recette WHERE idRecette='" + comboBoxRecette.Text.ToString().Substring(0, 4) + "';";


            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();

            string sIdRecette = reader.GetString(0);
            string sNomRecette = reader.GetString(1);
            string sTypeRecette = reader.GetString(2);
            string sPrixDeVente = reader.GetInt32(3).ToString();
            string sDescriptif = reader.GetString(4);

            txtBoxDescri.Text = sDescriptif;
            txtBoxNom.Text = sNomRecette;
            txtBoxNum.Text = sIdRecette;
            txtBoxType.Text = sTypeRecette;
            txtBoxPrix.Text = sPrixDeVente;


            reader.Close();
        }

        private void btnAjout_Click(object sender, RoutedEventArgs e)
        {
            listChoix.Add(txtBoxNum.Text + txtBoxNom.Text);
            listBoxCommande.Items.Refresh();

        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            string choix2=listBoxCommande.SelectedItem as String;
            if (listChoix.Contains(choix2))
              {
                    listChoix.Remove(choix2);
              }
            
            listBoxCommande.Items.Refresh();

        }
    }
}
