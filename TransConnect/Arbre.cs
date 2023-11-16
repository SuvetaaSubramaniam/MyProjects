using System;
namespace TransConnect
{
	class Arbre
	{
		Noeud racine;
  

		public Noeud Racine { get { return racine; }set { racine = value; } }

		public Arbre(Noeud racine=null)
		{
			this.racine = racine;
		}

        public int Hauteur(Noeud start,Noeud arrivee)
        {
            if (start == null) return 0;
            if (start == arrivee) return 0;
            return 1+Hauteur(start.Frere, arrivee);
        }

        //affichage de l'organigramme sous la forme
        //Racine
        //Successeur
        //   Frere
        //   Frere
        //Successeur
        public void Afficher(Noeud start,string ajout="",bool t=false)
		{
			if (start != null)
			{
                
                Console.WriteLine(ajout+start);
                Afficher(start.Frere,ajout="  ");
                Afficher(start.Successeur);

            }
		}
        public bool InsererSucesseurA(Noeud start,Salarie s,Salarie nouveau)
        {
            if (Recherche(start, s) == false) return false;
            if (start == null) return false;
            if (start.Employe == s)
            {
                if (start.Successeur == null)
                {
                    start.Successeur = new Noeud(nouveau);
                    return true;
                }
                else return false;
            }
            return InsererSucesseurA(start.Frere, s, nouveau) || InsererSucesseurA(start.Successeur, s, nouveau);
        }

        public bool InsererFrereA(Noeud start,Salarie s,Salarie nouveau)
        {
            if (Recherche(Racine, s) == false) return false;
            if (start == null) return false;
            if (start.Employe == s)
            {
                if (start.Frere == null) start.Frere = new Noeud(nouveau);
                else
                {
                    while(start != null)
                    {
                        start = start.Frere;
                    }
                    start.Frere = new Noeud(nouveau);
                }
            }
            return InsererSucesseurA(start.Frere, s, nouveau) || InsererSucesseurA(start.Successeur, s, nouveau);

        }

        public bool InsereSuccesseur(Noeud start, Noeud nouveau)
        {
            if (start == null) return false;
            if (start.Successeur == null)
            {
                start.Successeur = nouveau;
                return true;
            }
            return false;
        }

        public bool InsererFrere(Noeud start, Noeud nouveau)
        {
            if (start == null) return false;
            if (start.Frere == null)
            {
                start.Frere = nouveau;
                return true;
            }
            return InsererFrere(start.Frere, nouveau);
        }

        public bool Recherche(Noeud start, Salarie s)
        {
            if (start == null) return false;
            if (start.Employe == s) return true;
            bool trouve = Recherche(start.Frere, s);
            if (trouve) return trouve;
            return Recherche(start.Successeur, s);

            //return Recherche(start.Frere) || Recherche(start.Successeur
        }

        //METHODE NON FONCTIONNELLE !
        public bool Supprimer(Noeud start, Salarie supprimer)
        {
            if (Recherche(start, supprimer) == false) return false;
            if (start == null) return false;
            if (start.Employe == supprimer) return true;
           if (start.Frere == null || start.Successeur == null) return false;
            if (start.Successeur.Employe == supprimer)
            {
                if (start.Successeur.Successeur != null) start.Successeur = start.Successeur.Successeur;
                else if (start.Successeur.Frere != null) start.Successeur = start.Successeur.Frere;
                else start.Successeur = null;
                return true;
            }
            if (start.Frere.Employe==supprimer)
            {
                if (start.Frere.Frere != null) start.Frere = start.Frere.Frere;
                else if (start.Frere.Successeur != null) start.Frere = start.Frere.Successeur;
                else start.Frere = null;
                return true;
            }
            
            return Supprimer(start.Frere, supprimer) && Supprimer(start.Successeur, supprimer);


            //if (start.Successeur != null) {
            //    if (start.Successeur.Employe == supprimer)
            //    {
            //        if (start.Successeur.FilsUnique()) start.Successeur = start.Successeur.Successeur;
            //        else if (start.Successeur.Feuille()) start.Successeur = start.Successeur.Frere;
            //        else if (start.Successeur.Feuille() && start.Successeur.FilsUnique()) start.Successeur = null;
            //        else start.Successeur = start.Successeur.Frere;
            //    }
            //}else if(start.Frere != null)
            //{
            //    if(start.Frere.Employe==supprimer)
            //    {
            //        if(start.Frere.)
            //    }
            //}


            //if(start.Successeur.Employe==supprimer)
            //{
            //    if(start.Successeur.FilsUnique() &&(start.Successeur.Feuille()==false))
            //    {
            //        start.Successeur = start.Successeur.Successeur;
            //        return true;
            //    }
            //    if (start.Successeur.FilsUnique()==false && start.Successeur.Feuille())
            //    {
            //        start.Successeur = start.Successeur.Frere;
            //        return true;
            //    }
            //    if (start.Successeur.FilsUnique() && start.Successeur.Feuille())
            //    {
            //        start.Successeur = null;
            //        return true;
            //    }
            //}


            //if (start.Employe == supprimer) start = null;
            //if (start.Successeur == null) Supprimer(start.Frere, supprimer);
            //if (start.Frere.Employe == supprimer) start.Successeur.Frere = start.Successeur.Frere.Frere;
            //if (start.Frere.Employe == supprimer) start.Frere = start.Frere.Frere;
            //return Supprimer(start.Frere, supprimer) || Supprimer(start.Successeur, supprimer);
        }


        //obligatoirement remplacer
        public bool RemplacerSalarie(Noeud start, Salarie asupprimer,Salarie aremplacer)
        {
            if (start == null) return false;
            if (start.Employe == asupprimer) start.Employe = aremplacer;
            return RemplacerSalarie(start.Successeur, asupprimer, aremplacer) || RemplacerSalarie(start.Frere,asupprimer,aremplacer);
        }
    }
}

