using System;
using System.Collections.Generic;
namespace TransConnect
{
	abstract class Personne
	{
		protected int numSecu;
		protected string nom; //modifiable
		protected string prenom;
		protected DateTime dateNaissance;
		protected string adresse;
		protected string mail;//modifiable
		protected long tel;//modifiable

		public int NumSecu { get { return numSecu; } }
		public string Nom { get { return nom; } set { nom = value; } }
		public string Prenom { get { return prenom; } }
		public DateTime DateNaissance { get { return dateNaissance; } }
		public string Adresse { get { return adresse; } }
		public string Mail { get { return mail; } set { mail = value; } }
		public long Tel { get { return tel; } set { tel = value; } }

		public Personne(int numSecu,string nom,string prenom,DateTime dateNaissance,string adresse,string mail, long tel)
		{
			this.numSecu = numSecu;
			this.nom = nom;
			this.prenom = prenom;
			this.dateNaissance = dateNaissance;
			this.adresse = adresse;
			this.mail = mail;
			this.tel = tel;
		}

        public override string ToString()
        {
			return "Numéro SS: "+numSecu+ "\nNom/Prénom: " + nom + " " + prenom + "  Date de naissance: " + dateNaissance.ToString("dd/MM/yyyy") + "\nAdresse: "
				+ adresse+"\nMail: "+mail+"  Telephone: "+tel;
        }

		public virtual string ToStringAffichage()
		{
			return "Numéro SS: " + numSecu + "  Nom: " + nom + "  Prénom: " + prenom;

        }

		public virtual DateTime CreationDate()
		{
            Console.WriteLine("Saisir l'année");
            int annee = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisir le mois");
            int mois = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisir le jour");
            int jour = Convert.ToInt32(Console.ReadLine());
            DateTime retour = new DateTime(annee, mois, jour);
            return retour;
        }
    }
}

