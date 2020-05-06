using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBdd
{
    public class Commande
    {
        private List<Fournisseur> listeCommande;

        public Commande()
        {

        }

        public Commande(List<Fournisseur> listeCommande1)
        {
            this.listeCommande = listeCommande1;
        }

        public List<Fournisseur> ListeCommande
        {
            get { return this.listeCommande; }
            set { this.listeCommande = value; }
        }
    }
}
