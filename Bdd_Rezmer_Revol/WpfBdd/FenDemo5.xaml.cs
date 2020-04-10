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
using System.Data;

namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenDemo5.xaml
    /// </summary>
    public partial class FenDemo5 : Window
    {
        MySqlConnection connexion;
        DataTable tableProduit;
        DataTable tableRecette;
        MySqlDataAdapter dataAdp;
        string sidproduit;
        public FenDemo5(MySqlConnection connexion, DataTable tableProduit)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.connexion = connexion;
            this.tableProduit = tableProduit;
            produit_Combo();
            
        }

        void produit_Combo()
        {
            foreach (DataRow row in tableProduit.Rows)
            {
                comboBoxProduit.Items.Add(row["idProduit"].ToString()+"  "+row["nomProduit"].ToString());
            }
        }

        private void comboBoxProduit_DropDownClosed(object sender, EventArgs e)
        {
            tableRecette = new DataTable("Récapitulatif");
            sidproduit = comboBoxProduit.Text.ToString().Substring(0,4);
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT nomRecette, quantiteUtilisee, uniteDeQuantite FROM recette, estConstitue, produit WHERE estConstitue.idProduit=\"" + sidproduit + "\"AND estConstitue.idProduit=produit.idProduit AND estConstitue.idRecette=recette.idRecette;";
            commande.ExecuteNonQuery();
            dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableRecette);
            dataGridProduit.ItemsSource = tableRecette.DefaultView;
        }
        /*
        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            FenDemo5 demo5 = new FenDemo5(this.connexion);
            this.Hide();
            demo5.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            demo5.Owner = this;
            demo5.ShowDialog();
            this.ShowDialog();
        }
        */
        private void btnFin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("La démo est terminée. Merci", "Démo terminée", MessageBoxButton.OK);
            this.Hide();
        }

        private void dataGridProduit_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "nomRecette")
            {
                e.Column.IsReadOnly = true;
            }

            if (e.PropertyName == "quantiteUtilisee")
            {
                e.Column.IsReadOnly = true;
            }

            if (e.PropertyName == "uniteDeQuantite")
            {
                e.Column.IsReadOnly = true;
            }
        }
    }
}
