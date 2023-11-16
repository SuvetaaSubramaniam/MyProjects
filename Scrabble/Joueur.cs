using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Scrabble
{
    public class Joueur
    {
        #region attributs
        string nom;
        int score;
        List<string> motstrouves = new List<string>();
        List<Jeton> maincourante = new List<Jeton>();
        #endregion
        #region propriétés
        public string Nom
        {
            get
            {
                return this.nom;
            }
            set
            {
                this.nom = value;
            }
        }
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }
        public List<string> Motstrouves
        {
            get
            {
                return this.motstrouves;
            }
            set
            {
                this.motstrouves = value;
            }
        }
        public List<Jeton> Maincourante
        {
            get
            {
                return this.maincourante;
            }
            set
            {
                this.maincourante = value;
            }
        }
        #endregion
        #region Constructeurs
        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="nom">prend le nom du joueur, et met le reste des attributs à 0 ou null</param>
        public Joueur (string nom)
        {
            string nouveaunom=null; //on suppose qu'un joueur peut porter un nom avec des chiffres et des espaces
            nouveaunom = nom;
            while(nouveaunom.Trim(' ') == "") //vérifie si le nom donné est que des espaces ou null 
            {
                Console.WriteLine("Nom de joueur donné non valide, veuillez reéssayer");
                nouveaunom = Console.ReadLine();
            }
            this.nom = nouveaunom;
            this.score = 0;
        }
        /// <summary>
        /// Constructeur partie en cours
        /// </summary>
        /// <param name="">on initialise a partir du fichier Joueurs.txt pour reprendre une partie en cours</param>
        public Joueur(int i, string nomfichier) 
            //pour pouvoir utiliser le nom du fichier en parametre sans qu'il s'agisse du meme constructeur que le naturel, on met un nombre random que l'on n'utilisera pas
        {
            Sac_Jetons sacjetons = new Sac_Jetons();
            string[] filestrg= File.ReadAllLines(nomfichier);                //tableau des lignes du fichier donné
            string [] prenometscore=null;                                    //recuperera la premiere ligne avec le nom et le score
            string[] maincourantestring = new string[7];                     //recuperera la liste de caractere en main
            prenometscore= filestrg[0].Split(';');                           //met le nom et le score sur deux cases differentes
            this.nom = prenometscore[0];                                     //initialise le nom
            this.score = Convert.ToInt32(prenometscore[1]);                  //initialise le score
            this.motstrouves = filestrg[1].Split(';').ToList();              //la deuxieme ligne contenant les mots trouves est separee par mot et mis dans la liste des mots trouves
            maincourantestring = filestrg[2].Split(';');                     //met chaque caractere dans une case de tableau
            string [] filejetstrg = File.ReadAllLines("Jetons.txt");         //récupère les jetons existants par ligne
            for (int j = 0; j < 7; j++)                                      //on fait ca pour chaque caractere qu'il a en main
            {                                                                //il est censé avoir 7 caractères maximum       
                Jeton jetonmain = ConversionLettreJeton(maincourantestring[j],sacjetons);//chaque caractere va etre transforme en jeton pour etre ajoute à la main courante
                Add_Main_Courante(jetonmain);                               //une fois tous les attributs initialisés, on ajoute le jeton à la main courante
            } 

        }
        #endregion
        /// <summary>
        /// ajoute un mot aux mots trouvés
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot (string mot)
        {
             this.motstrouves.Add(mot);
        }
        /// <summary>
        /// affiche le nom, le score, et les mots trouvés par le joueur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string mottrouve = null;
            for(int i = 0; i < motstrouves.Count; i++)
            {
                mottrouve += " " +motstrouves[i];
            }
            if(mottrouve == null)
            {
                mottrouve = "aucun mot trouvé";
            }
            return ("Le joueur s'appelle " + this.nom + ", il a un score de "+ this.score + ", il a trouve ces mots : " + mottrouve ); //doit-on donner tous les mots trouvés
        }
        /// <summary>
        /// ajoute la valeur au score du joueur
        /// </summary>
        /// <param name="val"></param>
        public void Add_Score(int val)
        {
            score = score + val;
        }
        /// <summary>
        /// ajoute le jeton à la main courante
        /// </summary>
        /// <param name="monjeton"></param>
        public void Add_Main_Courante (Jeton monjeton)
        {
            if (monjeton != null)
            {
                this.maincourante.Add(monjeton);
            }
        } 
        /// <summary>
        /// retire le jeton de la main courante
        /// </summary>
        /// <param name="monjeton"></param>
        public void Remove_Main_Courante (Jeton monjeton)
        {
            if(monjeton != null)
            {
                this.maincourante.Remove(monjeton);
            }
        }
        /// <summary>
        /// convertit une lettre en un jeton
        /// </summary>
        /// <param name="lettre"></param>
        /// <param name="sacjetons"></param>
        /// <returns></returns>
        public Jeton ConversionLettreJeton(string lettre, Sac_Jetons sacjetons)
        {
            int valeur = 0;
            int nbocc = 0;
            char lettre1='0';
            lettre = lettre.ToUpper();                                                        //on le met en majuscule au cas ou c'est pas fait (pas de prb dans le cas du joker)
            int nbcase = -1;
            switch (lettre)
            {
                case "A":
                    nbcase = 0;
                    break;
                case "B":
                    nbcase = 1;
                    break;
                case "C":
                    nbcase = 2;
                    break;
                case "D":
                    nbcase = 3;
                    break;
                case "E":
                    nbcase = 4;
                    break;
                case "F":
                    nbcase = 5;
                    break;
                case "G":
                    nbcase = 6;
                    break;
                case "H":
                    nbcase = 7;
                    break;
                case "I":
                    nbcase = 8;
                    break;
                case "J":
                    nbcase = 9;
                    break;
                case "K":
                    nbcase = 10;
                    break;
                case "L":
                    nbcase = 11;
                    break;
                case "M":
                    nbcase = 12;
                    break;
                case "N":
                    nbcase = 13;
                    break;
                case "O":
                    nbcase = 14;
                    break;
                case "P":
                    nbcase = 15;
                    break;
                case "Q":
                    nbcase = 16;
                    break;
                case "R":
                    nbcase = 17;
                    break;
                case "S":
                    nbcase = 18;
                    break;
                case "T":
                    nbcase = 19;
                    break;
                case "U":
                    nbcase = 20;
                    break;
                case "V":
                    nbcase = 21;
                    break;
                case "W":
                    nbcase = 22;
                    break;
                case "X":
                    nbcase = 23;
                    break;
                case "Y":
                    nbcase = 24;
                    break;
                case "Z":
                    nbcase = 25;
                    break;
                case "*":
                    nbcase = 26;
                    break;
                default:
                    Console.WriteLine("Erreur dans le type de caractere du jeton");
                    break;
            }                                                                //suivant le caractere, récupère la bonne case de la liste jeton 
            if (nbcase != -1)
            {
                lettre1 = Convert.ToChar(lettre);                                             //initialise la lettre du caractere
                valeur = Convert.ToInt32(sacjetons.Sacdejetons[nbcase].Valeur);               //initialise la valeur de ce jeton
                nbocc = Convert.ToInt32(sacjetons.Sacdejetons[nbcase].Nboccurence);           //initialise le nombre d'occurences initial du jeton
            }
            else
            {
                Console.WriteLine("Erreur lors de l'initialisation du jeton, le nbcase n'a pas été trouvé");
            }
            Jeton jeton1 = new Jeton(lettre1, valeur, nbocc);
            return jeton1;
        }
        /// <summary>
        /// affiche la main courante
        /// </summary>
        /// <returns></returns>
        public string StringMainCourante()
        {
            string main = "";
            for(int i = 0; i < 7; i++)
            {
                main += this.maincourante[i].Lettre + " ";
            }
            return main;
        }
        
    }
}
