using System;
using System.Collections.Generic;
namespace TransConnect
{
	internal class Client:Personne,IComparable<Client>
	{
        List<Commande> historique; //au début null

        public List<Commande> Historique { get { return historique; } set { historique = value; } }
    
        public Client(int numSecu, string nom, string prenom, DateTime dateNaissance, string adresse, string mail, long tel, List<Commande> historique=null) :base(numSecu,nom,prenom,dateNaissance,adresse,mail,tel)
		{
            this.historique = new List<Commande>();
		}
        
        public override string ToString()
        {
            return base.ToString();
        }

        public override string ToStringAffichage()
        {
            return base.ToStringAffichage();
        }
        
        public int CompareTo(Client? other)
        {
            string nomPrenomThis = this.nom + this.prenom;
            string nomPrenomOther = other.nom + other.prenom;
            return nomPrenomThis.CompareTo(nomPrenomOther);
        }

        //Calculer le montant total des commande effectué par le client
        public double MontantTotalCommande()
        {
            double retour = 0;
            if(historique != null)
            {
                for(int i = 0; i < historique.Count; i++)
                {
                    retour += historique[i].PrixCommande;
                }
            }
            return retour;
        }

       
        public override DateTime CreationDate()
        {
            return base.CreationDate();
        }
    }
}

