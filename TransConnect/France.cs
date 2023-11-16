using System;
using System.Collections.Generic;

namespace TransConnect
{
	class France
	{
		string depart;
		string arrivee;
		double distance;


		public France(string depart,string arrivee,double distance)
		{
			this.depart = depart;
			this.arrivee = arrivee;
			this.distance = distance;
		}
		
		public string Depart { get { return depart; } set { depart = value; } }
		public string Arrivee { get { return arrivee; } set{ arrivee = value;} }
		public double Distance { get { return distance; } set { distance = value; } }

        public override string ToString()
        {
			return "Ville de départ: " + depart + "  Ville d'arrivée: " + arrivee+"  Distance"+distance;
        }

	

		//verifie si la destination voulue est déja dans la base des donnees
		public bool Existe(List<France> baseFrance,string saisieDepart,string saisieArrivee)
		{
			bool test = false;
			for(int i = 0; i < baseFrance.Count; i++)
			{
				if (baseFrance[i].Depart == saisieDepart && baseFrance[i].Arrivee == saisieArrivee) test = true;
			}
			return test;
		}


    }
}

