using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Bdd_Rezmer_Revol
{
    class Program
    {
        static void Main(string[] args)
        {
           MySqlConnection connexion = CreationConnexion();
            connexion.Open();
            CommandeSql(connexion, "SELECT * FROM client");
            Console.ReadLine();
        }
        static MySqlConnection CreationConnexion()
        {
            Console.WriteLine("Entrez UID");
            string uid = Console.ReadLine();
            Console.WriteLine("Entrez MDP");
            string mdp = Console.ReadLine();
            string connexionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=" + uid + ";PASSWORD=" + mdp;
            MySqlConnection connexion = new MySqlConnection(connexionString);
            return connexion;

        }
        static void CommandeSql(MySqlConnection connection, string  commande)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commande;
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read()) // parcours ligne par ligne
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++) // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString(); // recuperation de la valeur de chaque cellule sous forme d' une string ( voir cependant les differentes methodes disponibles !!)
                    //string maj = valueAsString[0].ToString().ToUpper() + valueAsString.Substring(1).ToLower(); ;
                    currentRowAsString += valueAsString + " ";
                }
                Console.WriteLine(currentRowAsString); // affichage de la ligne ( sous forme d'une " grosse " string ) sur la sortie standard
            }

        }
    }
}