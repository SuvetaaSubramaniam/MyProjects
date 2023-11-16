using System;
namespace TransConnect
{
	class Noeud
	{
		Salarie employe;
		Noeud successeur;
		Noeud frere;

		public Salarie Employe { get { return employe; }set { employe=value; } }
		public Noeud Successeur { get { return successeur; } set { successeur = value; } }
		public Noeud Frere { get { return frere; } set { frere = value; } }

		public Noeud(Salarie employe,Noeud successeur=null,Noeud frere=null)
		{
			this.employe = employe;
			this.successeur = successeur;
			this.frere = frere;
		}

        public override string ToString()
        {
			return employe.Nom+" "+employe.Poste;
        }


		//fonction pour déterminer si un noeud possèdes des frere (aide pour la suppression d'un noeud)
		public bool FilsUnique()
		{
			bool retour = false;
			if (Frere == null) retour = true;
			return retour;
		}

		public bool Feuille()
		{
			bool retour = false;
			if (Successeur == null) retour = true;
			return retour;
		}
    }
}

