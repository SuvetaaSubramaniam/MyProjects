using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.Timers;
namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Jeu jeu;
            Random r = new Random();
            Joueur joueur1;
            Joueur joueur2;
            Joueur joueur3;
            Joueur joueur4;
            Console.WriteLine("Pour votre information, notez que vous allez jouer au scrabble en français.");
            #region demande partie en cours ou nouvelle et test bonne réponse
            Console.WriteLine("Souhaitez-vous commencez une nouvelle partie ou continuez une partie en cours ?" + '\n' + " pour une nouvelle partie entrez 'n' et pour une partie en cours entrez 'c' ");
            string a = Console.ReadLine();
            while(a!="n" && a != "c")
            {
                Console.WriteLine("Veuillez saisir une valeur juste : ");
                Console.WriteLine("Souhaitez-vous commencez une nouvelle partie ou continuez une partie en cours ?" + '\n' + " pour une nouvelle partie entrez 'n' et pour une partie en cours entrez 'c' ");
                a = Console.ReadLine();
            }
            #endregion
            if (a == "n")
            {
                jeu = new Jeu();
            }
            else
            {
                #region test et demande des fichiers à utiliser
                Console.WriteLine("Veuillez entrer le nom du fichier d'instance de plateau, n'oubliez pas le '.txt'");
                string fileplateau = Console.ReadLine();
                while (fileplateau.Substring(fileplateau.Length - 4) != ".txt")
                {
                    Console.WriteLine("Donnez un fichier valide svp :");
                    fileplateau = Console.ReadLine();
                }
                bool fileplatnotfound = true;
                while (fileplatnotfound == true)
                {
                    try
                    {
                        string[] testplat = File.ReadAllLines(fileplateau);
                        fileplatnotfound = false;
                    }
                    catch (FileNotFoundException)
                    {
                        fileplatnotfound = true;
                    }
                }
                Console.WriteLine("Veuillez donner le nom du fichier d'instance du sac de jeton, n'oubliez pas le '.txt' :");
                string filesacjeton = Console.ReadLine();
                while (filesacjeton.Substring(filesacjeton.Length - 4) != ".txt")
                {
                    Console.WriteLine("Donnez un fichier valide svp :");
                    filesacjeton = Console.ReadLine();
                }
                bool filesacnotfound = true;
                while (filesacnotfound == true)
                {
                    try
                    {
                        string[] testsac = File.ReadAllLines(filesacjeton);
                        filesacnotfound = false;
                    }
                    catch (FileNotFoundException)
                    {
                        filesacnotfound = true;
                    }
                }
                #endregion
                jeu = new Jeu(fileplateau, filesacjeton);
            }
            #region choix et vérification du nombre de joueurs
            Console.WriteLine("Combien de joueurs voulez-vous ? (entre 2 et 4 joueurs uniquement)");
            string nbjoueurstrg = Console.ReadLine(); //erreur si on affiche autre chose
            while(nbjoueurstrg !="2" && nbjoueurstrg !="3" && nbjoueurstrg != "4")
            {
                Console.WriteLine("Veuillez saisir un nombre valide :");
                nbjoueurstrg =Console.ReadLine();
            }
            #endregion
            int nbjoueurs = Convert.ToInt32(nbjoueurstrg);
            #region création joueurs
            if (a == "c")             //on considère que les parties en cours on déjà  leur main courante remplie complètement
            {
                Console.WriteLine("Veuillez donner le nom du fichier du premier joueur (n'oubliez pas le '.txt')");
                string filejoueur1 = Console.ReadLine();
                #region test fichier joueur1
                while (filejoueur1.Substring(filejoueur1.Length - 4) != ".txt")
                {
                    Console.WriteLine("Donnez un fichier valide svp :");
                    filejoueur1 = Console.ReadLine();
                }
                bool filejoueur1notfound = true;
                while (filejoueur1notfound == true)
                {
                    try
                    {
                        string[] testjoueur1 = File.ReadAllLines(filejoueur1);
                        filejoueur1notfound = false;
                    }
                    catch (FileNotFoundException)
                    {
                        filejoueur1notfound = true;
                    }
                }
                #endregion
                joueur1 = new Joueur(1,filejoueur1);
                Console.WriteLine("Veuillez donner le nom du fichier du deuxième joueur (n'oubliez pas le '.txt')");
                string filejoueur2 = Console.ReadLine();
                #region test fichier joueur2
                while (filejoueur2.Substring(filejoueur2.Length - 4) != ".txt")
                {
                    Console.WriteLine("Donnez un fichier valide svp :");
                    filejoueur2 = Console.ReadLine();
                }
                bool filejoueur2notfound = true;
                while (filejoueur2notfound == true)
                {
                    try
                    {
                        string[] testjoueur2 = File.ReadAllLines(filejoueur2);
                        filejoueur2notfound = false;
                    }
                    catch (FileNotFoundException)
                    {
                        filejoueur2notfound = true;
                    }
                }
                #endregion
                joueur2 = new Joueur(1,filejoueur2);
                if (nbjoueurs==3 || nbjoueurs == 4)
                {
                    Console.WriteLine("Veuillez donner le nom du fichier du troisième joueur (n'oubliez pas le '.txt')");
                    string filejoueur3 = Console.ReadLine();
                    #region test fichier joueur3
                    while (filejoueur3.Substring(filejoueur3.Length - 4) != ".txt")
                    {
                        Console.WriteLine("Donnez un fichier valide svp :");
                        filejoueur3 = Console.ReadLine();
                    }
                    bool filejoueur3notfound = true;
                    while (filejoueur3notfound == true)
                    {
                        try
                        {
                            string[] testjoueur3 = File.ReadAllLines(filejoueur3);
                            filejoueur3notfound = false;
                        }
                        catch (FileNotFoundException)
                        {
                            filejoueur3notfound = true;
                        }
                    }
                    #endregion
                    joueur3 = new Joueur(1,filejoueur3);
                    if (nbjoueurs == 4)
                    {
                        Console.WriteLine("Veuillez donner le nom du fichier du quatrième joueur (n'oubliez pas le '.txt')");
                        string filejoueur4 = Console.ReadLine();
                        #region test fichier joueur4
                        while (filejoueur4.Substring(filejoueur4.Length - 4) != ".txt")
                        {
                            Console.WriteLine("Donnez un fichier valide svp :");
                            filejoueur4 = Console.ReadLine();
                        }
                        bool filejoueur4notfound = true;
                        while (filejoueur4notfound == true)
                        {
                            try
                            {
                                string[] testjoueur4 = File.ReadAllLines(filejoueur4);
                                filejoueur4notfound = false;
                            }
                            catch (FileNotFoundException)
                            {
                                filejoueur4notfound = true;
                            }
                        }
                        #endregion
                        joueur4 = new Joueur(1,filejoueur4);
                    }
                }
            }
            else
            {
                
                Console.WriteLine("Veuillez donner le nom du joueur1 svp :");
                string nomjoueur1 = Console.ReadLine();
                joueur1 = new Joueur(nomjoueur1); //le test du nom valide est déjà inclus dans la fonction du constructeur joueur
                #region remplissage main courante joueur1
                for (int i = 0; i < 7; i++) //on rempli sa main courante
                {
                    Jeton jetontemp = jeu.Monsac_jetons.Retire_Jeton(r);
                    joueur1.Add_Main_Courante(jetontemp);
                }
                #endregion
                Console.WriteLine("Veuillez donner le nom du joueur2 svp :");
                string nomjoueur2 = Console.ReadLine();
                joueur2 = new Joueur(nomjoueur2);
                #region remplissage main courante joueur2
                for (int i = 0; i < 7; i++) //on rempli sa main courante
                {
                    Jeton jetontemp = jeu.Monsac_jetons.Retire_Jeton(r);
                    joueur2.Add_Main_Courante(jetontemp);
                }
                #endregion
                if (nbjoueurs==3 || nbjoueurs == 4)
                {
                    Console.WriteLine("Veuillez donner le nom du joueur3 svp :");
                    string nomjoueur3 = Console.ReadLine();
                    joueur3 = new Joueur(nomjoueur3);
                    #region remplissage main courante joueur3
                    for (int i = 0; i < 7; i++) //on rempli sa main courante
                    {
                        Jeton jetontemp = jeu.Monsac_jetons.Retire_Jeton(r);
                        joueur3.Add_Main_Courante(jetontemp);
                    }
                    #endregion
                    if (nbjoueurs == 4)
                    {
                        Console.WriteLine("Veuillez donner le nom du joueur4 svp :");
                        string nomjoueur4 = Console.ReadLine();
                        joueur4 = new Joueur(nomjoueur4);
                        #region remplissage main courante joueur4
                        for (int i = 0; i < 7; i++) //on rempli sa main courante
                        {
                            Jeton jetontemp = jeu.Monsac_jetons.Retire_Jeton(r);
                            joueur4.Add_Main_Courante(jetontemp);
                        }
                        #endregion
                    }
                }
            }
            #endregion
            Console.WriteLine("Combien de temps souhaitez-vous que le tour d'un joueur dure au maximum ?, entrez seulement le nombre (ex: pour 6 sec écriver '6000' car c'est en milliseconde)");
            int temps = Convert.ToInt32(Console.ReadLine());
            while (jeu.Monsac_jetons != null)
            {
                #region tour du joueur1
                Console.WriteLine("Le joueur1 va jouer:");
                Console.WriteLine("Pressez 'oui' pour commencer ");
                while (Console.ReadLine() != "oui")
                {

                }
                Console.WriteLine("Voici votre main courante :");
                Console.WriteLine(joueur1.ToString());
                jeu.SetTimer(temps);
                bool tempsecoule = false;
                while (tempsecoule==false)
                {
                    Console.WriteLine(joueur1.StringMainCourante());
                    Console.WriteLine("Voici le plateau actuellement :");
                    Console.WriteLine("notations : mot compte triple : 6 ; mot compte double : 4 ; lettre compte double : 2 ; lettre compte triple : 3");
                    Console.WriteLine(jeu.Monplateau.ToString());
                    Console.WriteLine("Quel mot voulez-vous formez ?");
                    string motaplacer = Console.ReadLine();
                    Console.WriteLine("Où voulez-vous le placer ? Donnez la ligne (si première ligne notez 0, si 2ème, notez 1...");
                    int lignemot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Donnez la colonne (si première colonne notez 0, si 2ème, notez 1...");
                    int colonnemot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Donnez la direction du mot : 'h' pour horizontal et 'v' pour vertical");
                    char direction = Convert.ToChar(Console.ReadLine());
                    if (jeu.Monplateau.Test_Plateau(motaplacer, lignemot, colonnemot,direction,jeu.Mondico,joueur1,jeu.Monplateau) == true)
                    {
                        jeu.Monplateau.AjouterMotauPlateau(motaplacer, lignemot, colonnemot, direction);
                        #region calcul score à ajouter
                        int ajoutscore = 0;
                        int multiplicateur = 1;
                        int multmot = 1;
                        for (int i = 0; i < motaplacer.Length - 1; i++)
                        {
                            Jeton temp = joueur1.ConversionLettreJeton(Convert.ToString(motaplacer[i]), jeu.Monsac_jetons);
                            if (direction == 'v')
                            {
                                switch (jeu.Monplateau.Plateaujeu[lignemot + i, colonnemot])
                                {
                                    case "2":
                                        multiplicateur = 2;
                                        break;
                                    case "4":
                                        multmot = 2;
                                        break;
                                    case "6":
                                        multmot = 3;
                                        break;
                                    case "3":
                                        multiplicateur = 3;
                                        break;
                                    default:
                                        multiplicateur = 1;
                                        break;
                                }
                            }
                            if (direction == 'h')
                            {
                                switch (jeu.Monplateau.Plateaujeu[lignemot, colonnemot + i])
                                {
                                    case "2":
                                        multiplicateur = 2;
                                        break;
                                    case "4":
                                        multmot = 2;
                                        break;
                                    case "6":
                                        multmot = 3;
                                        break;
                                    case "3":
                                        multiplicateur = 3;
                                        break;
                                    default:
                                        multiplicateur = 1;
                                        break;
                                }
                            }
                            ajoutscore += temp.Valeur * multiplicateur;
                        }
                        ajoutscore = ajoutscore * multmot;
                        joueur1.Add_Score(ajoutscore);
                        #endregion
                        joueur1.Add_Mot(motaplacer);
                        #region Retirer les jetons utilisés et ajouter de nouveaux
                        for(int q = 0; q < motaplacer.Length; q++)
                        {
                            for(int u = 0; u < 7; u++)
                            {
                                if (joueur1.Maincourante[u].Lettre == Convert.ToChar(motaplacer[q]))
                                {
                                    joueur1.Remove_Main_Courante(joueur1.Maincourante[u]);
                                    joueur1.Add_Main_Courante(jeu.Monsac_jetons.Retire_Jeton(r));
                                }
                            }
                        }
                        #endregion
                        break;
                    }
                }
                Console.Clear();
                #endregion

                #region tour du joueur2
                Console.WriteLine("Le joueur2 va jouer:");
                Console.WriteLine("Pressez 'oui' pour commencer ");
                while (Console.ReadLine() != "oui")
                {

                }
                Console.WriteLine("Voici votre main courante :");
                Console.WriteLine(joueur2.ToString());
                jeu.SetTimer(temps);
                bool tempsecoule2 = false;
                while (tempsecoule2 == false)
                {
                    Console.WriteLine(joueur2.StringMainCourante());
                    Console.WriteLine("Voici le plateau actuellement :");
                    Console.WriteLine("notations : mot compte triple : 6 ; mot compte double : 4 ; lettre compte double : 2 ; lettre compte triple : 3");
                    Console.WriteLine(jeu.Monplateau.ToString());
                    Console.WriteLine("Quel mot voulez-vous formez ?");
                    string motaplacer = Console.ReadLine();
                    Console.WriteLine("Où voulez-vous le placer ? Donnez la ligne (si première ligne notez 0, si 2ème, notez 1...");
                    int lignemot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Donnez la colonne (si première colonne notez 0, si 2ème, notez 1...");
                    int colonnemot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Donnez la direction du mot : 'h' pour horizontal et 'v' pour vertical");
                    char direction = Convert.ToChar(Console.ReadLine());
                    if (jeu.Monplateau.Test_Plateau(motaplacer, lignemot, colonnemot, direction, jeu.Mondico, joueur2, jeu.Monplateau) == true)
                    {
                        jeu.Monplateau.AjouterMotauPlateau(motaplacer, lignemot, colonnemot, direction);
                        #region calcul score à ajouter
                        int ajoutscore = 0;
                        int multiplicateur = 1;
                        int multmot = 1;
                        for (int i = 0; i < motaplacer.Length - 1; i++)
                        {
                            Jeton temp = joueur2.ConversionLettreJeton(Convert.ToString(motaplacer[i]), jeu.Monsac_jetons);
                            if (direction == 'v')
                            {
                                switch (jeu.Monplateau.Plateaujeu[lignemot + i, colonnemot])
                                {
                                    case "2":
                                        multiplicateur = 2;
                                        break;
                                    case "4":
                                        multmot = 2;
                                        break;
                                    case "6":
                                        multmot = 3;
                                        break;
                                    case "3":
                                        multiplicateur = 3;
                                        break;
                                    default:
                                        multiplicateur = 1;
                                        break;
                                }
                            }
                            if (direction == 'h')
                            {
                                switch (jeu.Monplateau.Plateaujeu[lignemot, colonnemot + i])
                                {
                                    case "2":
                                        multiplicateur = 2;
                                        break;
                                    case "4":
                                        multmot = 2;
                                        break;
                                    case "6":
                                        multmot = 3;
                                        break;
                                    case "3":
                                        multiplicateur = 3;
                                        break;
                                    default:
                                        multiplicateur = 1;
                                        break;
                                }
                            }
                            ajoutscore += temp.Valeur * multiplicateur;
                        }
                        ajoutscore = ajoutscore * multmot;
                        joueur2.Add_Score(ajoutscore);
                        #endregion
                        joueur2.Add_Mot(motaplacer);
                        #region Retirer les jetons utilisés et ajouter de nouveaux
                        for (int q = 0; q < motaplacer.Length; q++)
                        {
                            for (int u = 0; u < 7; u++)
                            {
                                if (joueur2.Maincourante[u].Lettre == Convert.ToChar(motaplacer[q]))
                                {
                                    joueur2.Remove_Main_Courante(joueur2.Maincourante[u]);
                                    joueur2.Add_Main_Courante(jeu.Monsac_jetons.Retire_Jeton(r));
                                }
                            }
                        }
                        #endregion
                        break;
                    }
                }
                Console.Clear();
                #endregion
                
                if (nbjoueurs >= 3)
                {
                #region tour du joueur3
                Console.WriteLine("Le joueur3 va jouer:");
                Console.WriteLine("Pressez 'oui' pour commencer ");
                while (Console.ReadLine() != "oui")
                {

                }
                Console.WriteLine("Voici votre main courante :");
                //Console.WriteLine(joueur3.ToString());
                jeu.SetTimer(temps);
                bool tempsecoule3 = false;
                while (tempsecoule3 != true)
                {
                    //Console.WriteLine(joueur3.StringMainCourante());
                    Console.WriteLine("Voici le plateau actuellement :");
                    Console.WriteLine("notations : mot compte triple : 6 ; mot compte double : 4 ; lettre compte double : 2 ; lettre compte triple : 3");
                    Console.WriteLine(jeu.Monplateau.ToString());
                    Console.WriteLine("Quel mot voulez-vous formez ?");
                    string motaplacer = Console.ReadLine();
                    Console.WriteLine("Où voulez-vous le placer ? Donnez la ligne (si première ligne notez 0, si 2ème, notez 1...");
                    int lignemot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Donnez la colonne (si première colonne notez 0, si 2ème, notez 1...");
                    int colonnemot = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Donnez la direction du mot : 'h' pour horizontal et 'v' pour vertical");
                    char direction = Convert.ToChar(Console.ReadLine());
                        if (jeu.Monplateau.Test_Plateau(motaplacer, lignemot, colonnemot, direction, jeu.Mondico, joueur1, jeu.Monplateau) == true)
                        {
                            jeu.Monplateau.AjouterMotauPlateau(motaplacer, lignemot, colonnemot, direction);
                            #region calcul score à ajouter
                            int ajoutscore = 0;
                            int multiplicateur = 1;
                            int multmot = 1;
                            for (int i = 0; i < motaplacer.Length - 1; i++)
                            {
                                //Jeton temp = joueur3.ConversionLettreJeton(Convert.ToString(motaplacer[i]), jeu.Monsac_jetons);
                                if (direction == 'v')
                                {
                                    switch (jeu.Monplateau.Plateaujeu[lignemot + i, colonnemot])
                                    {
                                        case "2":
                                            multiplicateur = 2;
                                            break;
                                        case "4":
                                            multmot = 2;
                                            break;
                                        case "6":
                                            multmot = 3;
                                            break;
                                        case "3":
                                            multiplicateur = 3;
                                            break;
                                        default:
                                            multiplicateur = 1;
                                            break;
                                    }
                                }
                                if (direction == 'h')
                                {
                                    switch (jeu.Monplateau.Plateaujeu[lignemot, colonnemot + i])
                                    {
                                        case "2":
                                            multiplicateur = 2;
                                            break;
                                        case "4":
                                            multmot = 2;
                                            break;
                                        case "6":
                                            multmot = 3;
                                            break;
                                        case "3":
                                            multiplicateur = 3;
                                            break;
                                        default:
                                            multiplicateur = 1;
                                            break;
                                    }
                                }
                                //ajoutscore += temp.Valeur * multiplicateur;
                            }
                            ajoutscore = ajoutscore * multmot;
                            //joueur3.Add_Score(ajoutscore);
                            Console.Clear();
                            break;
                            #endregion
                        }
                    }
                    #endregion
                    //prb initialisation joueur3
                    if (nbjoueurs == 4)
                    {
                        #region tour du joueur4
                        Console.WriteLine("Le joueur1 va jouer:");
                        Console.WriteLine("Pressez 'oui' pour commencer ");
                        while (Console.ReadLine() != "oui")
                        {

                        }
                        Console.WriteLine("Voici votre main courante :");
                        //Console.WriteLine(joueur4.ToString());
                        jeu.SetTimer(temps);
                        bool tempsecoule4 = false;
                        while (tempsecoule4 != true)
                        {
                            Console.WriteLine(joueur1.StringMainCourante());
                            Console.WriteLine("Voici le plateau actuellement :");
                            Console.WriteLine("notations : mot compte triple : 6 ; mot compte double : 4 ; lettre compte double : 2 ; lettre compte triple : 3");
                            Console.WriteLine(jeu.Monplateau.ToString());
                            Console.WriteLine("Quel mot voulez-vous formez ?");
                            string motaplacer = Console.ReadLine();
                            Console.WriteLine("Où voulez-vous le placer ? Donnez la ligne (si première ligne notez 0, si 2ème, notez 1...");
                            int lignemot = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Donnez la colonne (si première colonne notez 0, si 2ème, notez 1...");
                            int colonnemot = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Donnez la direction du mot : 'h' pour horizontal et 'v' pour vertical");
                            char direction = Convert.ToChar(Console.ReadLine());
                            if (jeu.Monplateau.Test_Plateau(motaplacer, lignemot, colonnemot, direction, jeu.Mondico, joueur1, jeu.Monplateau) == true)
                            {
                                jeu.Monplateau.AjouterMotauPlateau(motaplacer, lignemot, colonnemot, direction);
                                #region calcul score à ajouter
                                int ajoutscore = 0;
                                int multiplicateur = 1;
                                int multmot = 1;
                                for (int i = 0; i < motaplacer.Length - 1; i++)
                                {
                                    //Jeton temp = joueur4.ConversionLettreJeton(Convert.ToString(motaplacer[i]), jeu.Monsac_jetons);
                                    if (direction == 'v')
                                    {
                                        switch (jeu.Monplateau.Plateaujeu[lignemot + i, colonnemot])
                                        {
                                            case "2":
                                                multiplicateur = 2;
                                                break;
                                            case "4":
                                                multmot = 2;
                                                break;
                                            case "6":
                                                multmot = 3;
                                                break;
                                            case "3":
                                                multiplicateur = 3;
                                                break;
                                            default:
                                                multiplicateur = 1;
                                                break;
                                        }
                                    }
                                    if (direction == 'h')
                                    {
                                        switch (jeu.Monplateau.Plateaujeu[lignemot, colonnemot + i])
                                        {
                                            case "2":
                                                multiplicateur = 2;
                                                break;
                                            case "4":
                                                multmot = 2;
                                                break;
                                            case "6":
                                                multmot = 3;
                                                break;
                                            case "3":
                                                multiplicateur = 3;
                                                break;
                                            default:
                                                multiplicateur = 1;
                                                break;
                                        }
                                    }
                                    //ajoutscore += temp.Valeur * multiplicateur;
                                }
                                ajoutscore = ajoutscore * multmot;
                                //joueur4.Add_Score(ajoutscore);
                                Console.Clear();
                                break;
                                #endregion
                            }
                        }
                        #endregion 
                        //prb aussi avec l'initialisation du joueur4
                    }
                }
                Console.Clear();
            }
            Console.WriteLine("Fin de la partie, merci d'avoir joué !");

            


        }
        #region testunitaires
        static public void TestConstructeurNaturelJoueur()
        {
            string nomvalide = "toto";
            Joueur joueur1 = new Joueur(nomvalide);
            Console.WriteLine(joueur1.ToString());
        }
        static public void TestConstructeurPartieEnCoursJoueur()
        {
            Joueur joueur1 = new Joueur(1, "Joueurs.txt");
            Console.WriteLine(joueur1.ToString());
        }
        static public void TestAddMot()
        {
            string mot = "mot";
            Joueur joueur1 = new Joueur("toto");
            joueur1.Add_Mot(mot);
            Console.WriteLine(joueur1.Motstrouves[0]);
        }
        static public void TestRemoveMainCourante()
        {
            string filename = "Joueurs.txt";
            Joueur joueur1 = new Joueur(1, filename);
            Jeton jeton1 = new Jeton('A', 1, 9);
            Console.WriteLine(jeton1.ToString());
            joueur1.Remove_Main_Courante(jeton1);
            Jeton jeton2 = new Jeton('B', 3, 2);
            Console.WriteLine(joueur1.Maincourante[0] +" "+ joueur1.Maincourante[1]);
        }
        static public void Testconstructeursacjetons()
        {
            Sac_Jetons sacjetons = new Sac_Jetons();
            Console.WriteLine(sacjetons.ToString());
        }
        #endregion
    }
}
