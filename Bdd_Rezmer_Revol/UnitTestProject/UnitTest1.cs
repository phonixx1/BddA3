using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfBdd;
using MySql.Data.MySqlClient;
using System.Security.Policy;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        MySqlConnection connexion = null;
        public static string iD = "root";    // votre iD
        public  string mdp1 = ""; // VOTRE MOT DE PASSE

        [TestMethod]
        public void TestOuvertureConnection()
        {
            bool ouverture;
            
           
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=" + iD + ";PASSWORD=" + mdp1;
                connexion = new MySqlConnection(connexionString);
                connexion.Open();
                connexion.Close();
                ouverture = true;

            }
            catch
            {
                ouverture = false;
            }
            Assert.AreEqual(ouverture, true);
            
        }

        [TestMethod]
        public void TestInsertionBd()
        {
            string connexionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=" + iD + ";PASSWORD=" + mdp1;
            connexion = new MySqlConnection(connexionString);
            connexion.Open();
            string id = "iDtest";
            string nom = "nomTest";
            string tel = "0654986532";
            string mdp = "mdpTest";
            bool testReussi;
            MySqlCommand commande = null;
            commande = connexion.CreateCommand();
            commande.CommandText = "INSERT INTO `cooking`.`client` (`idCompte`, `nom`, `numeroTel`, `mdp`, `soldeCook`) VALUES(@id, @nom, @tel, @mdp,@cdr)";
            commande.Parameters.AddWithValue("@id", id);
            commande.Parameters.AddWithValue("@nom", nom);
            commande.Parameters.AddWithValue("@tel", tel);
            commande.Parameters.AddWithValue("@mdp", mdp);
            commande.Parameters.AddWithValue("@cdr", null);
            try
            {
                commande.ExecuteNonQuery();
                connexion.Close();
                testReussi = true;
            }
            catch
            {
                testReussi = false;
                connexion.Close();
            }
            Assert.AreEqual(testReussi, true);

        }

        [TestMethod]
        public void TestSuppression()
        {
            string id = "iDtest";
            bool testReussi;
            string connexionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=" + iD + ";PASSWORD=" + mdp1;
            connexion = new MySqlConnection(connexionString);
            connexion.Open();
            MySqlCommand commande = connexion.CreateCommand();
            commande.CommandText = "DELETE FROM client WHERE idCompte =\"" + id + "\";";
            try
            {
                commande.ExecuteNonQuery();
                connexion.Close();
                testReussi = true;
            }
            catch
            {
                testReussi = false;
                connexion.Close();
            }
            Assert.AreEqual(testReussi, true);
        }

    }
}
