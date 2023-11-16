using System;
using System.Collections.Generic;
namespace TransConnect
{
	class Livraison
	{
		//double distance;
		France departArrivee;
		bool intemperie;
		bool travaux;

		//public double Distance { get { return distance; } }
		public France DepartArrivee{get{return departArrivee;} set { departArrivee = value; } }
		public bool Intemperie{ get { return intemperie; } set { intemperie = value; } }
		public bool Travaux { get { return travaux; } set { travaux = value; } }

		public Livraison(France departArrivee,bool intemperie,bool travaux)
		{
			//this.distance = distance;
			this.departArrivee = departArrivee;
			this.intemperie = intemperie;
			this.travaux = travaux;
		}

        public override string ToString()
        {
			return departArrivee.ToString()+ "\nIntemperie ? "+intemperie+"\nTravaux ? "+travaux;
        }

        public double CalculDistance()
		{
			double distance=departArrivee.Distance;
			//prendre en compte les intemperies
			if (intemperie == true) distance += 15;
			if (travaux == true) distance += 35;

			return distance;
		}
	}
}

