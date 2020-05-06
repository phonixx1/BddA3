using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfBdd
{
    public class Produit
    {
        private string nomP;
        private string idP;
        private string quantite;
        private string unite;
        public Produit()
        {

        }
        public Produit(string nomP1, string idP1, string quantite1, string unite1)
        {
            this.nomP = nomP1;
            this.idP = idP1;
            this.quantite = quantite1;
            this.unite = unite1;
        }

        [XmlElement("NomProduit")]
        public string NomP
        {
            get { return this.nomP; }
            set { this.nomP = value; }
        }

        [XmlElement("IdProduit")]
        public string IdP
        {
            get { return this.idP; }
            set { this.idP = value; }
        }

        public string Quantite
        {
            get { return this.quantite; }
            set { this.quantite = value; }
        }

        public string Unite
        {
            get { return unite; }
            set { this.unite = value; }
        }
    }
}
