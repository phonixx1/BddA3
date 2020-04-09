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
    /// Logique d'interaction pour FenSupCuisinier.xaml
    /// </summary>
    public partial class FenSupCuisinier : Window
    {
        MySqlConnection connexion;
        public FenSupCuisinier(MySqlConnection connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
        }
    }
}
