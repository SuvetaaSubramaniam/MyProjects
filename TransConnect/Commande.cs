using System;
using System.Collections.Generic;

namespace TransConnect
{
	class Commande
	{

		Client a;
		Salarie s;
		Livraison livrer; // a besoin de france et france a besoin de depart arrivee
		Vehicule v;
        DateTime dateCommande;
		double prixCommande;

		public Client A { get { return a; } set { a = value; } }
		public Salarie S { get { return s;} set { s = value; } }
		public Livraison Livrer { get { return livrer; } }
		public Vehicule V { get { return v; } }
		public DateTime DateCommande { get { return dateCommande; } }
		public double PrixCommande { get { return prixCommande; } }

		public Commande(Client a,Salarie s,Livraison livrer,Vehicule v,DateTime dateCommande,double prixCommande=1)
		{
			this.a = a;
			this.s = s;
			this.livrer = livrer;
			this.v = v;
            this.dateCommande = dateCommande;
			this.prixCommande = EstimationPrix();
		}

       public override string ToString()
        {
            string retour = "\nDETAIL DE LA LIVRAISON: \nPrix de la commande: "+prixCommande+"€"+"\nClient concerné:" + a.Nom+" "+a.Prenom+"\nLivrer de: "+livrer.DepartArrivee.Depart+
				" à "+livrer.DepartArrivee.Arrivee+"\nVehicule choisi pour la livraison:\n" + v.ToString() +"\nChauffeur attrivué à la commande"+s.ToString()+ "\nDate de la commande:\n"
                + dateCommande.ToString("yyyy/MM/dd");
            return retour;
        }

        //méthode pour établir le tarif en fonction kilometre et vehicule
        public double EstimationPrix()
		{
			//prix au kilometre = 0.18€
		    prixCommande = 0.18 * livrer.CalculDistance();
			//prix different en fonction du vehicule
			if (v is Voiture) prixCommande += (((Voiture)v).NbPassager * 10);
			if (v is Camionette) prixCommande += 30;
			if (v is Camion) prixCommande += 60;
			prixCommande += 50+s.PrixAnciennetChauffeur();
			return prixCommande;
		}

    }
}

