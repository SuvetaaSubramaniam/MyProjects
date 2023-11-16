using System;
using System.Collections.Generic;
namespace TransConnect
{
	internal class Salarie:Personne
	{
		 //poste en string ?
		 
		 DateTime dateEntree;						 
		 double salaire;								 
		 string poste;  				 
		 List<DateTime> emploiDuTemps;				 
													 
		public DateTime DateEntree { get { return dateEntree; } set { dateEntree = value; } }
		public double Salaire { get { return salaire; } set { salaire = value; } }
		public string Poste { get { return poste; } set { poste = value; } }
		public List<DateTime> EmploiDuTemps { get { return emploiDuTemps; } set { emploiDuTemps = value; } }


		public Salarie(int numSecu, string nom, string prenom,DateTime dateNaissance, string adresse, string mail, long tel,DateTime dateEntree,double salaire,string poste,List<DateTime>emploiDuTemps=null):base(numSecu,nom,prenom,dateNaissance,adresse,mail,tel)
		{
			this.dateEntree = dateEntree;
			this.salaire = salaire;
			this.poste = poste;
			this.emploiDuTemps = new List<DateTime>() { dateEntree};  //au début null
		}

		//Seulement afficher si c'est un chauffeur
		public string AfficherEmploiDuTemps()
		{
			string retour="Emploi du temp: ";
			emploiDuTemps.Sort();
			if (poste != "chauffeur") return null;
			for(int i = 0; i < emploiDuTemps.Count; i++)
			{
				retour += emploiDuTemps[i].ToString("yyyy/MM/dd")+"   ";
			}
			return retour;
		}

		//méthode pour me permettre de remplir les emplois du temps des chauffeurs plus facilement
		public void AjouterListEmploi(List<DateTime> aAjouter)
		{
			for(int i = 0; i < aAjouter.Count; i++)
			{
				emploiDuTemps.Add(aAjouter[i]);
			}
		}

        public override string ToString()
        {
			return base.ToString() + "\nDate entrée: "+dateEntree.ToString("dd/MM/yyyy")+"\nSalaire: " + salaire + "\nPoste: " + poste+"\n";
        }


		//fonction qui montre si le salarié est disponible le jour de la commande
		public bool EstDipo(DateTime date)
		{
			bool dispo = true;
			for (int i = 0; i < emploiDuTemps.Count; i++)
			{
				if (emploiDuTemps[i] == date) dispo = false;
			}
			return dispo;
		}

		//méthode permettant de calculer l'ancienneté du chauffeur
		public int Anciennete()
		{
			return DateTime.Now.Year - dateEntree.Year;
		}

		//Prix du chauffeur varie en fonction de son anciennete
		public double PrixAnciennetChauffeur()
		{
			double retour = 0;
			if (poste == "chauffeur")
			{
				if (Anciennete() > 3 && Anciennete() < 10) retour += 5 * Anciennete();
				if (Anciennete() >= 10) retour += 50;
			}
            return retour;
        }
        
        public override DateTime CreationDate()
        {
			return base.CreationDate();
        }


    }

	


}
