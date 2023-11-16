using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections;


namespace Scrabble
{
    public class Dictionnaire
    {
        #region attributs
        SortedList<int, List<string>> sortedlistdico=new SortedList<int, List<string>>();
        string langue;
        #endregion
        #region propriétés
        public SortedList<int, List<string>> Sortedlistdico
        {
            get { return this.sortedlistdico; }
            set { this.sortedlistdico = value; }
        }
        public string Langue
        {
            get { return this.langue; }
            set { this.langue = value; }
        }
        #endregion
        #region constructeurs
        public Dictionnaire(string filename)
        {
            if(filename != null)
            {
                List<string> listetemporaire1=new List<string>();
                List<string> listemotetnb = File.ReadAllLines(filename).ToList();
                int nbkey = 0;
                List<string>[] listtemp= new List<string>[15];
                for(int y = 0; y < 15; y++)
                {
                    listtemp[y] = new List<string>();
                }
                int caselistetemp = 0;
                for(int i=0; i < listemotetnb.Count; i++)                      //pour chaque ligne du document dictionnaire
                {
                    if (testnboustrg(listemotetnb[i]) == true)                 //si la ligne testée est un nombre
                    {
                        if (nbkey != 0)
                        {
                            sortedlistdico.Add(nbkey, listtemp[caselistetemp]);//on ajoute a notre liste du dico le string contenant les mots ainsi que leur key : le nombre de lettres qu'ils ont, mais uniquement si les mots n'ont pas 0 lettres pour pas que ce soit fait au début
                            caselistetemp++;                                   //on change de ligne de la liste
                        }                               
                        nbkey = Convert.ToInt32(listemotetnb[i]);              //comme la ligne est un nombre, on la convertit en entier
                    }
                    else                                                       //si la ligne testée n'est pas un nombre c'est donc un string de mots
                    {
                        listetemporaire1 = listemotetnb[i].Split(' ').ToList();
                        for (int g = 0; g < listetemporaire1.Count; g++)
                        {
                            listtemp[caselistetemp].Add(listetemporaire1[g]) ;  //on ajoute chacun des mots dans la liste générale au cas ou il y est plusieurs lignes de lettres
                        } 
                    }
                }
            }
            else
            {
                Console.WriteLine("Le fichier donné est null");
            }
            this.langue = filename;                                           //recupere le nom du fichier qui devrait etre le nom de la langue
            char[] typedoc = { 't', 'x', 't', '.' };                          // pour enlever le .txt du nom du document et garder que la langue
            this.langue = this.langue.TrimEnd(typedoc);
        }
        #endregion
        #region fonction supplementaires
        /// <summary>
        /// Test si l'entree est convertible en nombre ou pas
        /// </summary>
        /// <param name="entree">string pour voir si c'est convertible en nombre</param>
        /// <returns></returns>
        public bool testnboustrg(string entree)
        {
            bool sortie = false;
            try
            {
                int nbkey = Convert.ToInt32(entree);
                sortie = true;
            }
            catch (FormatException)
            {
                sortie = false;
            }
            return sortie;
        }
        #endregion
        /// <summary>
        /// affiche la langue et le nombre de mots par taille de mot du dictionnaire
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string nbmot = null;
            for(int i = 0; i < sortedlistdico.Count; i++)
            {
                nbmot = nbmot + '\n' + sortedlistdico.Keys[i] + " lettres : " +sortedlistdico.Values[i] + " mots"; 
            }
            return "la langue est " + this.langue + ", et le nombre de mots en fonction de la longueur : " + nbmot + ".";
        }
        /// <summary>
        /// teste si le mot existe dans le dictionnaire
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool RechDichoRecursif (string mot)
        {
            bool test = false;
            int a = this.sortedlistdico.IndexOfKey(mot.Length);
            for ( int i = 0; i < this.sortedlistdico.Values[a].Count; i++) //on fait ça pour chaque mot de la liste de mots correpondant à la taille du mot recherché
            {
                if (this.sortedlistdico.Values[a][i] == mot)              //on test si le mot de la liste est celui cherché
                {
                    test = true;                                              //le bouléen retourné ne passera pas à vrai tant que la condition ne sera pas vérifié 
                    break;                                                    //permet de sortir de la boucle si le mot est trouvé parce que pas besoin de tout tester
                }
            }
            return test;
        }
    }
}
