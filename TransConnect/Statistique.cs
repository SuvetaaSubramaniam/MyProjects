using System;
using System.Collections.Generic;
namespace TransConnect
{
	class Statistique
	{
        List<Salarie> baseSalarie;
        List<Client> baseClient;

        public Statistique(List<Salarie>baseSalarie,List<Client>baseClient)
		{
			this.baseSalarie = baseSalarie;
			this.baseClient = baseClient;	
		}

		public List<Salarie> BaseSalarie { get { return baseSalarie; } set { baseSalarie = value; } }
        public List<Client> BaseClient{ get { return baseClient; } set { baseClient = value; } }

		public int nbClient()
		{
			return baseClient.Count;
		}

		public int nbSalarie()
		{
			return baseSalarie.Count;
		}

		public int nbChauffeur()
		{
			List<Salarie> chauffeur = baseSalarie.FindAll(x => x.Poste == "chauffeur");
			return chauffeur.Count;
		}

		public int nbCommandePeriode(DateTime a,DateTime b)
		{
			int retour = 0;
			if (b < a)
			{
				DateTime c = a;
				a = b;
				b = c;
			}
			List<Commande> commande=new List<Commande>();
			for(int i = 0; i < baseClient.Count; i++)
			{
				commande=baseClient[i].Historique.FindAll(x => x.DateCommande >= a && x.DateCommande <= b);
				retour += commande.Count;
            }
			return retour;
		}

		public void nbLivraisonChauffeur()
		{
            List<Salarie> chauffeur = baseSalarie.FindAll(x => x.Poste == "chauffeur");
			for(int i = 0; i < chauffeur.Count; i++)
			{
				Console.WriteLine("Nombre de livraison effectué par" + chauffeur[i].Nom + " " + chauffeur[i].Prenom + " est de " + chauffeur[i].EmploiDuTemps.Count);
			}
        }

		public double MoyenneCompteClient()
		{
			double retour = 0;
			for(int i = 0; i < baseClient.Count; i++)
			{
				for (int j = 0; j < baseClient[i].Historique.Count; j++)
				{
					if (baseClient[i].Historique[j] != null)
					{
						retour += baseClient[i].Historique[j].PrixCommande;
					}
				}
            }
			return retour;
		}
    }
}

