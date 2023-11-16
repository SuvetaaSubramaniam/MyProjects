using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace Scrabble
{
    public class Jeu
    {
        #region attributs
        Dictionnaire mondico;
        Plateau monplateau;
        Sac_Jetons monsac_jetons;
        #endregion
        #region propriétés
        public Dictionnaire Mondico
        {
            get { return this.mondico; }
            set { this.mondico = value; }
        }
        public Plateau Monplateau
        {
            get { return this.monplateau; }
            set { this.monplateau = value; }
        }
        public Sac_Jetons Monsac_jetons
        {
            get { return this.monsac_jetons; }
            set { this.monsac_jetons = value; }
        }
        #endregion
        #region constructeurs
        /// <summary>
        /// Constructeur naturel (début partie)
        /// </summary>
        public Jeu()
        {
            this.mondico = new Dictionnaire("Francais.txt");
            this.monplateau = new Plateau();
            this.monsac_jetons = new Sac_Jetons();
        }
        /// <summary>
        /// Constructeur partie en cours
        /// </summary>
        /// <param name="fileplateau">prend en parametre d'abord le nom du fichier plateau</param>
        /// <param name="filesacjetons">puis le fichier du sac de jetons</param>
        public Jeu(string fileplateau, string filesacjetons)
        {
            this.mondico = new Dictionnaire("Francais.txt");
            this.monplateau = new Plateau(fileplateau);
            this.monsac_jetons = new Sac_Jetons(filesacjetons);
        }
        #endregion
        /// <summary>
        /// initialise le timer
        /// </summary>
        /// <param name="temps"></param>
        public void SetTimer(int temps)
        {
            Timer aTimer = new Timer(temps);
            aTimer.Elapsed +=EventTimer ;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        /// <summary>
        /// lorsque le timer est fini, cette fonction s'exécute et affiche qu'il est fini
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EventTimer(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Temps écoulé, pressez 'ok' pour continuer");
            //empecher le joueur de reprendre
        }
    }
}
