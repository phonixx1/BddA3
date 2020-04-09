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
        
        public FenListeProduitsRecette(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;

            MySqlCommand commande = this.connexion.CreateCommand();
            commande.CommandText = "SELECT idProduit,nomProduit,categorie,uniteDeQuantite FROM produit;";
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
                //row["idProduit"]
                if (SelectedIndexes.Contains(tableProduit.Rows.IndexOf(row)))
                {
                    tableChoix.Rows.Add(row.ItemArray);
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
    }
}
