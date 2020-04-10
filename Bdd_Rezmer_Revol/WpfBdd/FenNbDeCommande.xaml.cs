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

namespace WpfBdd
{
    /// <summary>
    /// Logique d'interaction pour FenNbDeCommande.xaml
    /// </summary>
    public partial class FenNbDeCommande : Window
    {
        string nombre;
        string element;
        
        public FenNbDeCommande(string element)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.element = element;
            lblNom.Content += this.element+" ?";
        }
        public string Nombre
        {
            get { return nombre; }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            nombre = textBoxNombre.Text;
            this.DialogResult = true;
        }
    }
}
