using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scrabble
{
    public class Plateau
    {
        #region attributs
        string[,] plateau=new string[15,15];
        #endregion
        #region propriétés
        public string[,] Plateaujeu
        {
            get { return this.plateau; }
            set { this.plateau = value; }
        }
        #endregion
        #region constructeurs
        /// <summary>
        /// Constructeur naturel (initialement nul aveec poids associés)
        /// </summary>
        public Plateau()
        {
            //mot compte triple : 6 ; mot double : 4 ; lettre double : 2 ; lettre triple : 3
            #region mot compte triple
            plateau[0, 0] = "6";
            plateau[0, 7] = "6";
            plateau[0, 14] = "6";
            plateau[14, 0] = "6";
            plateau[14, 7] = "6";
            plateau[14, 14] = "6";
            #endregion
            #region mot compte double
            plateau[1, 1] = "4";
            plateau[2, 2] = "4";
            plateau[3,3] = "4";
            plateau[4,4] = "4";
            plateau[1,13] = "4";
            plateau[2,12] = "4";
            plateau[3, 11] = "4";
            plateau[4, 10] = "4";
            plateau[13, 1] = "4";
            plateau[12, 2] = "4";
            plateau[11, 3] = "4";
            plateau[10, 4] = "4";
            plateau[13, 13] = "4";
            plateau[12, 12] = "4";
            plateau[11, 11] = "4";
            plateau[10, 10] = "4";
            plateau[7,7] = "4";
            #endregion
            #region lettre compte double
            plateau[0,3]= "2";
            plateau[14,3] = "2";
            plateau[0, 11] = "2";
            plateau[14, 11] = "2";
            plateau[3, 0] = "2";
            plateau[11, 0] = "2";
            plateau[3, 14] = "2";
            plateau[11, 14] = "2";
            plateau[2,6] = "2";
            plateau[2, 8] = "2";
            plateau[3, 7] = "2";
            plateau[6,2] = "2";
            plateau[8, 2] = "2";
            plateau[7, 3] = "2";
            plateau[12,6] = "2";
            plateau[12,8] = "2";
            plateau[11,7] = "2";
            plateau[6, 12] = "2";
            plateau[8, 12] = "2";
            plateau[7, 11] = "2";
            plateau[6,6] = "2";
            plateau[8, 6] = "2";
            plateau[6, 8] = "2";
            plateau[8,8] = "2";
            #endregion
            #region lettre compte triple
            plateau[5,1]= "3";
            plateau[5, 5] = "3";
            plateau[5, 9] = "3";
            plateau[5, 12] = "3";
            plateau[9, 1] = "3";
            plateau[9, 5] = "3";
            plateau[9, 9] = "3";
            plateau[9, 12] = "3";
            plateau[1, 5] = "3";
            plateau[1, 9] = "3";
            plateau[13, 5] = "3";
            plateau[13, 9] = "3";
            #endregion
        }
        /// <summary>
        /// Constructeur avec instance plateau
        /// </summary>
        /// <param name="nomfichier"></param>
        public Plateau(string nomfichier) 
        { 
            string[] instanceplateaustrg = File.ReadAllLines(nomfichier);  //on met les différents poids des cases d'abord
            #region mot compte triple
            plateau[0, 0] = "6";
            plateau[0, 7] = "6";
            plateau[0, 14] = "6";
            plateau[14, 0] = "6";
            plateau[14, 7] = "6";
            plateau[14, 14] = "6";
            #endregion                                                        
            #region mot compte double
            plateau[1, 1] = "4";
            plateau[2, 2] = "4";
            plateau[3, 3] = "4";
            plateau[4, 4] = "4";
            plateau[1, 13] = "4";
            plateau[2, 12] = "4";
            plateau[3, 11] = "4";
            plateau[4, 10] = "4";
            plateau[13, 1] = "4";
            plateau[12, 2] = "4";
            plateau[11, 3] = "4";
            plateau[10, 4] = "4";
            plateau[13, 13] = "4";
            plateau[12, 12] = "4";
            plateau[11, 11] = "4";
            plateau[10, 10] = "4";
            plateau[7, 7] = "4";
            #endregion
            #region lettre compte double
            plateau[0, 3] = "2";
            plateau[14, 3] = "2";
            plateau[0, 11] = "2";
            plateau[14, 11] = "2";
            plateau[3, 0] = "2";
            plateau[11, 0] = "2";
            plateau[3, 14] = "2";
            plateau[11, 14] = "2";
            plateau[2, 6] = "2";
            plateau[2, 8] = "2";
            plateau[3, 7] = "2";
            plateau[6, 2] = "2";
            plateau[8, 2] = "2";
            plateau[7, 3] = "2";
            plateau[12, 6] = "2";
            plateau[12, 8] = "2";
            plateau[11, 7] = "2";
            plateau[6, 12] = "2";
            plateau[8, 12] = "2";
            plateau[7, 11] = "2";
            plateau[6, 6] = "2";
            plateau[8, 6] = "2";
            plateau[6, 8] = "2";
            plateau[8, 8] = "2";
            #endregion
            #region lettre compte triple
            plateau[5, 1] = "3";
            plateau[5, 5] = "3";
            plateau[5, 9] = "3";
            plateau[5, 12] = "3";
            plateau[9, 1] = "3";
            plateau[9, 5] = "3";
            plateau[9, 9] = "3";
            plateau[9, 12] = "3";
            plateau[1, 5] = "3";
            plateau[1, 9] = "3";
            plateau[13, 5] = "3";
            plateau[13, 9] = "3";
            #endregion
            for (int i = 0; i < 15; i++)
            {
                string[] tabtemp = instanceplateaustrg[i].Split(';');     //on récupère une ligne du fichier
                for(int j = 0; j < 15; j++)
                {                              //si le fichier ne contient ni un _ ni rien et qu'il s'agit bien d'un caractère on met la lettre dans la case
                    if (tabtemp[j].Length == 1 && tabtemp[j] != "" && tabtemp[j] != "_") plateau[i, j] = tabtemp[j];  
                    else
                    {
                        if (tabtemp[j].Length != 1)  //au cas ou le fichier donné était mal créé
                        {
                            Console.WriteLine("le fichier donné contenait des valeurs qui n'était pas de simple lettres");
                        }
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// fonction tostring affiche le plateau
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = null;
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    if (plateau[i, j] != null)
                    {
                        result +=plateau[i,j] + " ";
                    }
                    else
                    {
                        result += "_" + " ";
                    }
                }
                result += '\n';
            }
            return result;
        }
        /// <summary>
        /// teste pirncipal qui dit si un mot répond a tous les critères pour être placé
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <param name="direction"></param>
        /// <param name="dico"></param>
        /// <param name="joueur1"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction, Dictionnaire dico, Joueur joueur1, Plateau plateau)
        {
            bool result = false;
            if ((direction == 'h' && PlacementPossiblePlateauhorizontal(mot, plateau, ligne,colonne) == true) || (direction == 'v' && PlacementPossiblePlateauVertical(mot, plateau,ligne, colonne) == true))  //on a alors verifie aussi que le mot peut etre place en vertical ou horizontal
            {                                                                  
                if (dico.RechDichoRecursif(mot) == true)  //on vérifie l'appartenance du mot au dico
                {
                    if(LettredanslaMainCourante(mot, joueur1) == true) //on a donc vérifié à ce stade que les lettre du mot appartiennent à la main
                    {
                        result = true;
                        //manque test croisements
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// teste savoir si les lettres du mot sont bien present dans la main courante et donc utilisable
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="joueur1"></param>
        /// <returns></returns>
        public bool LettredanslaMainCourante(string mot,Joueur joueur1)
        {
            bool result = false;
            char[] lettresmot = new char[mot.Length];  //on cree un tableau avec les lettres du mot pour vérifier l'appartenance de toutes les lettres à la main courante
            for (int k = 0; k < mot.Length; k++) //on remplit les cases du tableau créé
            {
                lettresmot[k] = mot[k];
            }
            for (int i = 0; i < 7; i++) //on teste pour chaque lettre de la main courante si le joueur compte l'utiliser
            {
                for (int j = 0; j < mot.Length; j++) //si pour la ième lettre de la main courante, la jième lettre du mot correspond on remplace cette lettre par 0 pour bien faire attention aux lettres en double
                {
                    if (joueur1.Maincourante[i].Lettre == mot[j])
                    {
                        lettresmot[j] = '0'; //on met la case à 0 pour repérer que la lettre correspond bien à une lettre de la main courante
                        break; //on sort du for pour que aucune autre lettre ne soit retire car par exemple pour placer deux E il faut posseder deux E dans la main courante
                    }
                }
            }
            int resultpetitfor = 0;
            for (int l = 0; l < mot.Length; l++)
            {
                if (lettresmot[l] != '0')
                {
                    resultpetitfor++;
                }
            }
            if (resultpetitfor == 0) //on a donc vérifié à ce stade que le mot appartient au dictionnaire ET que les lettre du mot appartiennent à la main
            {
                result = true;
            }
                return result;
        }
        /// <summary>
        ///teste si le mot est compatible en horizontal avec les autres mots qu'il croise
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        public bool PlacementPossiblePlateauhorizontal(string mot, Plateau plateau, int ligne, int colonne)
        {
            bool result = false;
            int compteur = 0;
            for(int i = colonne; i < colonne + mot.Length; i++)
            {
                if(plateau.Plateaujeu[ligne,i]==null || plateau.Plateaujeu[ligne, i] == "2" || plateau.Plateaujeu[ligne, i] == "3"|| plateau.Plateaujeu[ligne, i] == "4" || plateau.Plateaujeu[ligne, i] == "6")
                {
                    compteur++;//si un espace libre, on ajoute 1 pour compter le nombre d'espace libre consecutif
                }
                else
                {
                    compteur = 0; //si l'espace n'est pas libre le compteur est remis à 0 pour être sur que les espaces libres sont consecutifs
                }
            }
            if (compteur == mot.Length) result = true;
            return result;
        }
        /// <summary>
        ///teste si le mot est compatible en vertical avec les autres mots qu'il croise
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        public bool PlacementPossiblePlateauVertical(string mot, Plateau plateau,int ligne,int colonne)
        {
            bool result = false;
            int compteur = 0;
            for (int i = ligne; i < ligne +mot.Length ; i++)
            {
                if (plateau.Plateaujeu[i, colonne] == null || plateau.Plateaujeu[i, colonne] == "2" || plateau.Plateaujeu[i, colonne] == "3" || plateau.Plateaujeu[i, colonne] == "4" || plateau.Plateaujeu[i, colonne] == "6")
                {
                    compteur++;//si un espace libre, on ajoute 1 pour compter le nombre d'espace libre consecutif
                }
                else
                {
                    compteur = 0; //si l'espace n'est pas libre le compteur est remis à 0 pour être sur que les espaces libres sont consecutifs
                }
            }
            if (compteur == mot.Length) result = true;
            return result;
        }
        /// <summary>
        /// teste si le mot en parametre peut etre posé quelque part en prenant en compte tous les mots qui le croise
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="plateau"></param>
        /// <param name="dico"></param>
        /// <returns></returns>
        public bool Croisementmots(string mot, Plateau plateau, Dictionnaire dico)
        {
            bool result = false;
            string[] temptab = null;
            int pos1 = -1;
            int pos2 = -1;
            List<string> listvertical  = Placementpossiblevertical(mot, plateau);
            List<string> listhorizontal = Placementpossiblehorizontal(mot, plateau);
            for (int i = 0; i < listvertical.Count; i++)
            {
                temptab = listvertical[i].Split(';');
                pos1 = Convert.ToInt32(temptab[0]);
                pos2 = Convert.ToInt32(temptab[1]);
                //il faut tester tous les contours du mot pour trouver tous les mots qui traversent
                //d'abord on regarde si il y a des lettres avant
                if(TestMotacompleter(mot, plateau, dico, pos1, pos2) == true) //on a verifier que le mot n'etait pas lie a un autre et si oui que ca appartenait au dico
                {

                }
                
            }
            return result;
        }
        /// <summary>
        ///cherche toutes les positions ou le mot peut etre mis en hoizontal et retourne les positions sous forme de liste
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        public List<string> Placementpossiblehorizontal (string mot, Plateau plateau)
        {
            List<string> result = new List<string>(); //meme mecanique que les fonctions de placement, sauf qu'on recupere les positions cette fois
            int compteur = 0;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (plateau.Plateaujeu[i, j] == null)
                    {
                        compteur++;
                    }
                    else
                    {
                        compteur = 0;
                    }
                    if (compteur >= mot.Length)
                    {
                        result.Add(Convert.ToString(i) + ';' + Convert.ToString(j-mot.Length));
                    }
                }
                compteur = 0;
            }
            return result;
        }
        /// <summary>
        ///cherche toutes les positions ou le mot peut etre mis en vertical et retourne les positions sous forme de liste
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        public List<string> Placementpossiblevertical(string mot, Plateau plateau)
        {
            List<string> result = new List<string>(); //meme mecanique que les fonctions de placement, sauf qu'on recupere les positions cette fois
            int compteur = 0;
            for (int j = 0; j < 15; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (plateau.Plateaujeu[i, j] == null)
                    {
                        compteur++;
                    }
                    else
                    {
                        compteur = 0;
                    }
                    if (compteur >= mot.Length)
                    {
                        result.Add(Convert.ToString(i-mot.Length) + ';' + Convert.ToString(j)+ " ");
                    }
                }
                compteur = 0;
            }
            return result;
        }
        /// <summary>
        ///regarde si le mot est un complement d'un autre mot et s'il est valable
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="plateau"></param>
        /// <param name="dico"></param>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public bool TestMotacompleter(string mot, Plateau plateau, Dictionnaire dico, int pos1, int pos2)
        {
            bool result = false;
            if (plateau.Plateaujeu[pos1 - 1, pos2] != null)
            {
                int i = 2;
                while (plateau.Plateaujeu[pos1 - i, pos2] != null && pos1 - i > 0)
                {
                    i++;
                }
                string motatest = null;
                for (int h = pos1 - i + 1; h < pos1 - 1; h++)
                {
                    motatest += Convert.ToString(plateau.Plateaujeu[h, pos2]);
                }
                motatest += mot;
                if (dico.RechDichoRecursif(motatest) == true)
                {
                    result = true;
                }
            }
            else
            {
                if(plateau.Plateaujeu[pos1 +mot.Length-1, pos2] != null)
                {
                    int i = 2;
                    while (plateau.Plateaujeu[pos1 +mot.Length-1+i, pos2] != null && pos1 +mot.Length-1+ i <15)
                    {
                        i++;
                    }
                    string motatest = mot;
                    for (int h = pos1+mot.Length; h < pos1+mot.Length+i-1; h++)
                    {
                        motatest += Convert.ToString(plateau.Plateaujeu[h, pos2]);
                    }
                    if (dico.RechDichoRecursif(motatest) == true)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }
        public void AjouterMotauPlateau(string mot, int ligne, int colonne, char direction)
        {
            if (direction == 'v')
            {
                int l = 0;
                for (int i = ligne; i < mot.Length - 1; i++)
                {
                    this.plateau[i, colonne] = Convert.ToString(mot[l]);
                    l++;
                }
            }
            if (direction == 'h')
            {
                int l = 0;
                for(int i = colonne; i< mot.Length - 1; i++)
                {
                    this.plateau[ligne, i] = Convert.ToString(mot[l]);
                    l++;
                }
            }
        }

    }
}
