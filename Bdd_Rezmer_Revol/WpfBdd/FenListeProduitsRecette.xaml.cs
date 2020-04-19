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
    /// Logique d'interaction pour FenListeProduitsRecette.xaml
    /// </summary>
    public partial class FenListeProduitsRecette : Window
    {
        MySqlConnection connexion;
        DataTable tableProduit;
        DataTable tableChoix;
        string idNew;
        
        public FenListeProduitsRecette(MySqlConnection connexion, string idNew)
        {
            InitializeComponent();
            this.connexion = connexion;
            this.idNew = idNew;
            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "DELETE FROM estConstitue WHERE idRecette=\"" + idNew + "\";";
            commande.ExecuteNonQuery();
            commande.CommandText = "SELECT idProduit, nomProduit, categorie, uniteDeQuantite FROM produit;";
            commande.ExecuteNonQuery();

            tableProduit=new DataTable("Produit(s) disponible(s)");
            tableChoix = new DataTable("Produit(s) ajouté(s)");
            tableChoix.Columns.Add(new DataColumn("idProduit", typeof(String)));
            tableChoix.Columns.Add(new DataColumn("nomProduit", typeof(String)));
            tableChoix.Columns.Add(new DataColumn("categorie", typeof(String)));
            tableChoix.Columns.Add(new DataColumn("quantite", typeof(String)));


            MySqlDataAdapter dataAdp = new MySqlDataAdapter(commande);
            dataAdp.Fill(tableProduit);

            dataGridProduit.ItemsSource = tableProduit.DefaultView;
            dataGridProduitChoisi.ItemsSource = tableChoix.DefaultView;
            // dataAdp.Update(dt);
        }
        private void btnAjout_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = tableChoix.NewRow();
            List<int> SelectedIndexes = dataGridProduit
                              .SelectedItems
                              .Cast<DataRowView>()
                              .Select(view => tableProduit.Rows.IndexOf(view.Row))
                              .ToList();
            foreach (DataRow row in tableProduit.Rows)
            {
                string idProduit1 = row["idProduit"].ToString();
                string nomProduit1 = row["nomProduit"].ToString();
                string categorie1 = row["categorie"].ToString();
                string unitedequantite1 = row["uniteDeQuantite"].ToString();
                if (SelectedIndexes.Contains(tableProduit.Rows.IndexOf(row)))
                {
                    tableChoix.Rows.Add(row.ItemArray);
                    tableChoix.Rows[tableChoix.Rows.Count-1]["quantite"] = 1;

                    //tableProduit.Rows.Remove(row);
                    //dataGridProduit.Items.Refresh();
                }
                dataGridProduitChoisi.Items.Refresh();
            }
            dataGridProduitChoisi.Items.Refresh();
        }

        private void dataGridProduit_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
             if (e.PropertyName == "idProduit")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "uniteDeQuantite")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "nomProduit")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "categorie")
            {
                e.Column.IsReadOnly = true;
            }
            
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow row in tableChoix.Rows)
            {
                string idProduit1 = row["idProduit"].ToString();
                int quantite1 = Convert.ToInt32(row["quantite"]);
                MySqlCommand commande = this.connexion.CreateCommand();
                commande.CommandText = "INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES (@idRecette, @idProduit, @quantite);";
                commande.Parameters.AddWithValue("@idRecette", idNew);
                commande.Parameters.AddWithValue("@idProduit", idProduit1);
                commande.Parameters.AddWithValue("@quantite", quantite1);
                commande.ExecuteNonQuery();
            }
            MessageBox.Show("Les produits ont bien été ajoutés à votre recette", "Information", MessageBoxButton.OK);
            this.DialogResult = true;
        }
    }
}
