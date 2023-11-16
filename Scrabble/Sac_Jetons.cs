using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Scrabble
{
    public class Sac_Jetons
    {
        #region attributs
        string[] tabjetonsstrg = File.ReadAllLines("Jetons.txt");
        List<Jeton> sacdejetons= new List<Jeton>();
        #endregion
        #region propriétés
        public List<Jeton> Sacdejetons
        {
            get { return this.sacdejetons; }
            set { this.sacdejetons = value; }
        }
        #endregion
        #region constructeurs
        /// <summary>
        /// Constructeur d'après le fichier
        /// </summary>
        public Sac_Jetons() //constructeur rempli le sac de jetons
        {
            for (int i = 0; i < 27; i++)                                //on fait la même chose pour toutes les lignes du fichier jetons, donc 27 lignes (26 lettres et le joker)
            { 
                string[] tabtemp = tabjetonsstrg[i].Split(';');         //on récupère dans un tableau les valeur de la ligne
                Jeton jetontemp = new Jeton(Convert.ToChar(tabtemp[0]), Convert.ToInt32(tabtemp[1]), Convert.ToInt32(tabtemp[2]));
                //on crée un jeton temporaire qui va récupérer la lettre, la valeur et le nb d'occurence
                sacdejetons.Add(jetontemp); 
            }
        }
        public Sac_Jetons(string nomfichier)
        {
            string[] tabjetonsstrg = File.ReadAllLines(nomfichier);     //on utilise le fichier demandé et on met chaque ligne dans une case d'un tableau de string
            for (int i = 0; i < tabjetonsstrg.Length; i++)              //on fait la même chose pour toutes les lignes du fichier jetons (en utilisant la taille du tableau précédent)
            {
                string[] tabtemp = tabjetonsstrg[i].Split(';');         //on récupère dans un tableau les valeur de la ligne
                Jeton jetontemp = new Jeton(Convert.ToChar(tabtemp[0]), Convert.ToInt32(tabtemp[1]), Convert.ToInt32(tabtemp[2])); //on crée un jeton temporaire et on le rempli
                this.sacdejetons.Add(jetontemp);                        //une fois le jeton temporaire rempli, on l'ajoute au sac de jetons
            }
        }
        #endregion
        /// <summary>
        /// méthode qui tire un jeton au hasard le retourne et l'enlève du sac de jetons (nboccurence-=)
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Jeton Retire_Jeton(Random r)
        {
            int val = r.Next(0, sacdejetons.Count - 1);                            //choisis un nombre au hasard entre 0 et le nombre d'éléments de la liste du sac jetons pour prendre en compte si jamais des jetons ont été retiré
            Jeton retour= new Jeton(Convert.ToChar(sacdejetons[val].Lettre), Convert.ToInt32(sacdejetons[val].Valeur), Convert.ToInt32(sacdejetons[val].Nboccurence));
            Retire_jeton_sac(val, retour);                                         //retire le jeton du sac (reduit le nombre d'occurence)
            return retour;
        }
        /// <summary>
        /// méthode qui retire le jeton qui correspond à l'index donné du sac de jetons
        /// </summary>
        /// <param name="val"></param>
        /// <param name="jeton1"></param>
        public void Retire_jeton_sac(int val, Jeton jeton1)
        {
            if (jeton1.Nboccurence >= 1)
            {                      
                sacdejetons[val].Nboccurence = jeton1.Nboccurence - 1;          //on enlève 1 au nombre d'occurence
            }
            else
            {
                sacdejetons.RemoveAt(val);                                      //sinon on enlève le jeton car son nboccurence est nul
            }
        }
        /// <summary>
        /// fonction tostring du sac de jetons
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string lettrepresentesac = null;
            for(int i = 0; i <sacdejetons.Count; i++)
            {
                lettrepresentesac = lettrepresentesac + sacdejetons[i].ToString() + '\n' ;
            }
            return "Le sac contient " + lettrepresentesac; 
        }

    }
}
