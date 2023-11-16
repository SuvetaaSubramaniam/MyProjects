using System;
using System.Collections.Generic;
namespace TransConnect
{
	abstract public class Vehicule
	{
        public Vehicule()
		{

		}        
    }

	internal class Voiture : Vehicule
	{
		int nbPassager;

		public int NbPassager { get { return nbPassager; } set { nbPassager = value; } }

		public Voiture(int nbPassager)
		{
			this.nbPassager = nbPassager;
		}

        public override string ToString()
        {
			return "Voiture avec " + nbPassager+"passagers\n";
        }

		
    }

	internal class Camionette : Vehicule
	{
		string usage;

		public string Usage { get { return usage; } set { usage = value; } }


		public Camionette(string usage)
		{
			this.usage = usage;
		}

        public override string ToString()
        {
			return "Camionette \nUsage camionette: " + usage +"\n";
        }
    }

	abstract class Camion : Vehicule
	{
		protected string matiere;
		protected double volume;
		protected double volumeMax;

		public string Matiere { get { return matiere; } set { matiere = value; } }
		public double Volume { get { return volume; } set{ volume = value; } }
		public double VolumeMax { get { return volumeMax; } }

		public Camion(string matiere,double volume,double volumeMax=0)
		{
			this.matiere = matiere;
			this.volume = volume;
			this.volumeMax = volumeMax;
		}

        public override string ToString()
        {
			return "Matiere transportée: " + matiere + " \nVolume: " + volume+"\n";
        }

    }

	internal class CamionCiterne : Camion
	{

		public CamionCiterne(string matiere, double volume,double volumeMax=25) : base(matiere, volume,volumeMax) { }

        public override string ToString()
        {
            return "Camion citerne\n"+base.ToString();
        }

		
    }

	internal class CamionBenne : Camion
	{
		int nbCuve;

		public int NbCuve { get { return nbCuve; } set { nbCuve = value; } }

		public CamionBenne(string matiere,double volume,int nbCuve, double volumeMax = 20) : base(matiere, volume,volumeMax)
		{
			this.nbCuve = nbCuve;
		}

        public override string ToString()
        {
            return "Camion benne\n"+base.ToString()+"\nNb de cuve: "+nbCuve;
        }

		
    }

	internal class CamionFrigo : Camion
	{

		public CamionFrigo(string matiere,double volume,double volumeMax=35) : base(matiere, volume,volumeMax) { }

        public override string ToString()
        {
            return "Camion frigo\n"+base.ToString();
        }

		
    }
}

