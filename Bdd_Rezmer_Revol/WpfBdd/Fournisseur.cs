using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfBdd
{
    public class Fournisseur
    {
        private string nomF;
        private string idF;
        private List<Produit> listeProduits;

        public Fournisseur()
        {
           
        }

        public Fournisseur(string nomF1, string idF1, List<Produit> listeProduits1)
        {
            this.nomF = nomF1;
            this.idF = idF1;
            this.listeProduits = listeProduits1;
        }

        [XmlElement("Nom")]
        public string NomF
        {
            get { return this.nomF; }
            set { this.nomF = value; }
        }

        [XmlElement("IdFournisseur")]
        public string IdF
        {
            get { return idF; }
            set { this.idF = value; }
        }

        public List<Produit> ListeProduits
        {
            get { return listeProduits; }
            set { this.listeProduits = value; }
        }
    }
}
