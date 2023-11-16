using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Scrabble
{
    public class Jeton
    {

        # region attributs
        char lettre;
        int valeur;
        int nboccurence;
        #endregion
        #region propriétés
        public char Lettre
        {
            get { return this.lettre; }
            set { this.lettre = value; }
        }
        public int Valeur
        {
            get { return this.valeur; }
            set { this.valeur = value; }
        }
        public int Nboccurence
        {
            get { return this.nboccurence; }
            set { this.nboccurence = value; }
        }
        #endregion
        #region constructeur
        /// <summary>
        /// Initialisation a partir de la lettre donnée
        /// </summary>
        /// <param name="lettre">on nous donne sous forme de string la lettre que l'on veut</param>
        public Jeton(char lettre, int val, int nbocc)
        {
            this.lettre = lettre;
            this.valeur = val;
            this.nboccurence = nbocc;
        }
        #endregion
        public override string ToString()
        {
            return "Le jeton est la letttre " + this.lettre + ",  il vaut " + this.valeur + " point(s) et il y a " + this.nboccurence + " duplicata(s).";
        }
       /* public static bool operator ==(Jeton jeton1, Jeton jeton2)
        {
            bool result = false;
            if (jeton1.Lettre == jeton2.Lettre && jeton1.Valeur == jeton2.Valeur && jeton1.Nboccurence == jeton1.Nboccurence)
            {
                result = true;
            }
            return result;
        }
        public static bool operator !=(Jeton jeton1, Jeton jeton2)
        {
            bool result = false;
            if (jeton1 != jeton2) result = true;
            return result;
        }*/
    }
}
