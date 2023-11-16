using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace TransConnect
{
    class Program
    {
        static void Main(string[] args)
        {

            //base des salarie
            List<Salarie> baseSalarie = RemplirbaseSalarie();
            //base de donnée client
            List<Client> baseClient = RemplirBaseClient();
            //base des destination
            List<France> baseFrance = RemplirbaseVille();
            //Liste des métier à transconnect,possibilité d'ajouter ou retirer des postes
            Arbre basehierarchie = RemplirbaseArbre(baseSalarie);
            baseClient[8].Historique.Add(new Commande(baseClient[8], baseSalarie[6], new Livraison(new France("PARIS", "LYON", 700), false, false), new Voiture(5), new DateTime(2022, 12, 02)));

            Console.WriteLine("Bienvenue dans le système de TransConnect !");
            string sortie;
            do
            {
                Console.WriteLine("Quelle action souhaitez-vous réaliser ?" + "\n1.Consulter les données d'un salarié \n2.Consulter les données d'un client \n3.Gestion d'une commande \n4.Gestion de l'organigramme \n5.Afficher les statistique de votre entreprise" +
                       "\n");
                string saisie;
                do
                {

                    Console.WriteLine("Veuillez saisir le numéro corresoondant à votre choix.");
                    saisie = Console.ReadLine();
                } while (saisie != "1" && saisie != "2" && saisie != "3" && saisie != "4" && saisie != "5");
                switch (saisie)
                {
                    case "1": //Consulter donnée salarié

                        do
                        {
                            Console.WriteLine("Quelle action souhaitez-vous réaliser ? \n1-Rechercher les données d'un salarié par son nom  \n2.Consulter l'emploi du temps d'un chauffeur");
                            saisie = Console.ReadLine();
                        } while (saisie != "1" && saisie != "2");
                        if (saisie == "1")
                        {
                            Console.WriteLine("Voici la liste des salariés de votre entreprise");
                            baseSalarie.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                            do
                            {
                                Console.WriteLine("Veuillez saisir le nom du salarié souhaité.");
                                saisie = Console.ReadLine().ToUpper();
                            } while (baseSalarie.Exists(x => x.Nom == saisie) == false);
                            Salarie aConsulter = baseSalarie.Find(x => x.Nom == saisie);
                            Console.WriteLine(aConsulter.ToString());
                        }
                        else if (saisie == "2")
                        {
                            Console.WriteLine("Voici la liste des chauffeurs de votre entreprise");
                            List<Salarie> listeChauffeur = baseSalarie.FindAll(x => x.Poste == "chauffeur");
                            listeChauffeur.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom + "     Expérience: " + x.Anciennete() + "ans"));
                            do
                            {
                                Console.WriteLine("Saisir le nom du chauffeur pour lequel vous souhaitez afficher son emploi du temps.");
                                saisie = Console.ReadLine().ToUpper();
                            } while (listeChauffeur.Exists(x => x.Nom == saisie) == false);
                            Salarie aConsulter = listeChauffeur.Find(x => x.Nom == saisie);
                            aConsulter.EmploiDuTemps.Sort();
                            Console.WriteLine(aConsulter.AfficherEmploiDuTemps());
                        }
                        break;
                    case "2"://Consulter donnée client
                        Console.WriteLine("Consultation donnée client:");
                        do
                        {
                            Console.WriteLine("Quel action souhaitez-vous réaliser ? \n1.Consulter le dossier d'un client par son nom \n2.Effectuer un affichage par tri \n3.Ajouter un client \n4.Supprimer un client");
                            saisie = Console.ReadLine();
                        } while (saisie != "1" && saisie != "2" && saisie != "3" && saisie != "4");
                        if (saisie == "1") //Consultation donnée
                        {
                            Console.WriteLine("Voici la liste des clients de votre entreprise");
                            baseClient.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                            do
                            {
                                Console.WriteLine("Veuillez saisir le nom du client souhaité.");
                                saisie = Console.ReadLine().ToUpper();
                            } while (baseClient.Exists(x => x.Nom == saisie) == false);
                            Client aConsulter = baseClient.Find(x => x.Nom == saisie);
                            Console.WriteLine(aConsulter.ToString()+"\n\nMontant total des achats cumulés: "+aConsulter.MontantTotalCommande());
                            do
                            {
                                Console.WriteLine("Souhaitez-vous afficher l'historique des commandes de ce client ? Si oui tapez 1 sinon tappez 0");
                                saisie = Console.ReadLine();
                            } while (saisie != "1" && saisie != "0");
                            if (saisie == "1")
                            {
                                Console.WriteLine("Historique des commandes de " + aConsulter.Nom + " " + aConsulter.Prenom + " : ");
                                aConsulter.Historique.ForEach(x => Console.WriteLine(x));

                            }
                        }
                        else if (saisie == "2") //Efecctuer un affichage par tri
                        {
                            do
                            {
                                Console.WriteLine("Quel affichage souhaitez-vous effectuer ? Saisir le numéro correspondant. \n1.Tri par nom  \n2.Tri par ville  \n3.Tri par montant cumulé des achats  \n4.Afficher tout simultanement");
                                saisie = Console.ReadLine();
                            } while (saisie != "1" && saisie != "2" && saisie != "3" && saisie != "4");
                            if (saisie == "1") { baseClient.Sort((x, y) => x.Nom.CompareTo(y.Nom)); Console.WriteLine("Voici le tri par nom des clients:"); baseClient.ForEach(x => Console.WriteLine(x.ToStringAffichage())); }
                            if (saisie == "2") { baseClient.Sort((x, y) => x.Adresse.CompareTo(y.Adresse)); Console.WriteLine("Voici le tri par ville des clients:"); baseClient.ForEach(x => Console.WriteLine(x.ToStringAffichage() + " " + x.Adresse)); }
                            if (saisie == "3") { baseClient.Sort((x, y) => x.MontantTotalCommande().CompareTo(y.MontantTotalCommande())); Console.WriteLine("Voici le tri par montant total des commandes:"); baseClient.ForEach(x => Console.WriteLine(x.ToStringAffichage() + " Montant cumulé des achats: " + x.MontantTotalCommande() + "euros")); }
                            if (saisie == "4") //Affichage simmultanné
                            {
                                baseClient.Sort((x, y) => x.Nom.CompareTo(y.Nom)); Console.WriteLine("Voici le tri par nom des clients:"); baseClient.ForEach(x => Console.WriteLine(x.ToStringAffichage()));
                                baseClient.Sort((x, y) => x.Adresse.CompareTo(y.Adresse)); Console.WriteLine("Voici le tri par ville des clients:"); baseClient.ForEach(x => Console.WriteLine(x.ToStringAffichage() + " " + x.Adresse));
                                baseClient.Sort((x, y) => x.MontantTotalCommande().CompareTo(y.MontantTotalCommande())); Console.WriteLine("Voici le tri par montant total des commandes:"); baseClient.ForEach(x => Console.WriteLine(x.ToStringAffichage() + " Montant cumulé des achats: " + x.MontantTotalCommande() + "euros"));
                            }
                        }
                        else if (saisie == "3")
                        { //Ajout client
                            Client ajouter =AjoutClient();
                            baseClient.Add(ajouter); Console.WriteLine("Voici la liste après modification: "); baseClient.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                        }
                        else
                        { //Suppression client
                            Console.WriteLine("Voici la liste des clients de votre entreprise");
                            baseClient.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                            do
                            {
                                Console.WriteLine("Veuillez saisir le nom du client souhaité.");
                                saisie = Console.ReadLine().ToUpper();
                            } while (baseClient.Exists(x => x.Nom == saisie) == false);
                            Client aConsulter = baseClient.Find(x => x.Nom == saisie);
                            baseClient.Remove(aConsulter);
                            Console.WriteLine("Voici la base des clients après modification:");
                            baseClient.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                        }
                        break;
                    case "3"://Creer une commande
                        do
                        {
                            Console.WriteLine("Quelle action souhaitez-vous réaliser ? \n1.Créer une commande  \n2.Afficher une commande");
                            saisie = Console.ReadLine();
                        } while (saisie == "1" && saisie == "2");
                        if (saisie == "1")
                        {
                            Console.WriteLine("Vous aller créer une commande");
                            Commande c = CreationCommande(baseClient, baseFrance, baseSalarie);
                            Console.WriteLine("La commande a été effectuer avec succès. Voici un récapitulatif de la commande: \n" + c.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Voici la liste des clients de votre entreprise");
                            baseClient.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                            do
                            {
                                Console.WriteLine("Veuillez saisir le nom du client sur lequel a été effectué la commande.");
                                saisie = Console.ReadLine().ToUpper();
                            } while (baseClient.Exists(x => x.Nom == saisie) == false);
                            Client aConsulter = baseClient.Find(x => x.Nom == saisie);
                            Console.WriteLine("Voici les commandes effectués par ce client:");
                            aConsulter.Historique.ForEach(x => Console.WriteLine(x));
                            do
                            {
                                Console.WriteLine("Souhaitez-vous supprimer une commande de ce client ? Si oui tappez 1 sinon tappez 0.");
                                saisie = Console.ReadLine();
                            } while (saisie == "0" && saisie != "1");
                            if (saisie == "1")
                            {
                                Console.WriteLine("Saisir ville de départ de la commande");
                                string dep = Console.ReadLine().ToUpper();
                                Console.WriteLine("Saisir ville d'arrivée de la commande");
                                string arr = Console.ReadLine().ToUpper();
                                Console.WriteLine("Saisir la date de la commande");
                                DateTime dat = CreationDate();
                                bool test = aConsulter.Historique.Exists(x => x.Livrer.DepartArrivee.Depart == dep && x.Livrer.DepartArrivee.Arrivee == arr && x.DateCommande == dat);
                                if (test == true)
                                {
                                    Commande c = aConsulter.Historique.Find(x=> x.Livrer.DepartArrivee.Depart==dep && x.Livrer.DepartArrivee.Arrivee == arr && x.DateCommande == dat);
                                    aConsulter.Historique.Remove(c);
                                    c.S.EmploiDuTemps.Remove(c.DateCommande);
                                    Console.WriteLine("Historique des commandes après modification:");
                                    aConsulter.Historique.ForEach(x => Console.WriteLine(x));
                                }
                                else { Console.WriteLine("Commande inexistante"); }
                            }
                        }
                        break;
                    case "4"://Afficher organigramme
                        Console.WriteLine("Voici l'organigramme de votre entreprise.");
                        basehierarchie.Afficher(basehierarchie.Racine);
                        do
                        {
                            Console.WriteLine("Souhaitez vous apporter une modification ? \n1.Remplacer un salarié par un nouveau \n2.Ajouter un salarié  \n3.Quittez");
                            saisie = Console.ReadLine();
                        } while (saisie != "1" && saisie != "2" && saisie != "3");
                        if (saisie == "1")
                        {
                            Console.WriteLine("Voici la liste des salariés actuels de votre entreprise.?");
                            baseSalarie.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                            do
                            {
                                Console.WriteLine("Veuillez saisir le nom du salarié souhaité.");
                                saisie = Console.ReadLine().ToUpper();
                            } while (baseSalarie.Exists(x => x.Nom == saisie) == false);
                            Salarie aRemplacer = baseSalarie.Find(x => x.Nom == saisie);
                            baseSalarie.Remove(aRemplacer);
                            do
                            {
                                Console.WriteLine("Si vous souhaitez le remplacer par un salarié de l'entreprise tappez 1. Si le remplaçant est un nouveau salarié tappez 0.");
                                saisie = Console.ReadLine();
                            } while (saisie != "0" && saisie != "1");
                            Salarie nouveau;
                            if (saisie == "0")
                            {
                                nouveau = AjoutSalarie();
                                baseSalarie.Add(nouveau);
                            }
                            else
                            {
                                Console.WriteLine("Voici la liste des salariés de votre entreprise.");
                                baseSalarie.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                                do
                                {
                                    Console.WriteLine("Veuillez saisir le nom du salarié remplaçant");
                                    saisie = Console.ReadLine().ToUpper();
                                } while (baseSalarie.Exists(x => x.Nom == saisie) == false);
                                nouveau = baseSalarie.Find(x => x.Nom == saisie);
                            }
                            basehierarchie.RemplacerSalarie(basehierarchie.Racine, aRemplacer, nouveau);       
                            Console.WriteLine("Voici l'organigramme après modification.");
                            basehierarchie.Afficher(basehierarchie.Racine);

                        }
                        if (saisie == "2")
                        {
                            Console.WriteLine("Vous allez ajouter un nouveau salarié.");
                            Salarie aAjouter = AjoutSalarie();
                            Console.WriteLine("Voici la liste des salariés de votre entreprise.");
                            baseSalarie.ForEach(x => Console.WriteLine(x.Nom + " " + x.Prenom));
                            do
                            {
                                Console.WriteLine("A quel salarié va-t-il succeder ? Saisir son nom");
                                saisie = Console.ReadLine().ToUpper();
                            } while (baseSalarie.Exists(x => x.Nom == saisie) == false);
                            Salarie apres = baseSalarie.Find(x => x.Nom == saisie);
                            do
                            {
                                Console.WriteLine("Si le nouveau salarié est un successeur de " + apres.Nom + " tappez 0. Sinon tappez 1");
                                saisie = Console.ReadLine();
                            } while (saisie != "0" && saisie != "1");
                            if (saisie == "0") { basehierarchie.InsererSucesseurA(basehierarchie.Racine, apres, aAjouter); }
                            else { basehierarchie.InsererFrereA(basehierarchie.Racine, apres, aAjouter); }
                            baseSalarie.Add(aAjouter);

                        }
                        break;
                    case "5":
                        Statistique stat = new Statistique(baseSalarie, baseClient);
                        Console.WriteLine("Nombre de client dans votre société: " + stat.nbClient());
                        Console.WriteLine("Nombre de salarié dans votre société: " + stat.nbSalarie() + "  dont " + stat.nbChauffeur() + " chauffeur.");
                        Console.WriteLine("Affichage du nombre de livraison effectué par chauffeur:");
                        stat.nbLivraisonChauffeur();
                        Console.WriteLine("Nous allons afficher le nombre de commande effectuée sur une période de temps. \nSaisir la première date.");
                        DateTime a = CreationDate();
                        Console.WriteLine("Saisir la deuxième date.");
                        DateTime b = CreationDate();
                        Console.WriteLine("Nombre de commande effectué entre " + a.ToString("dd/MM/yyyy") + " et " + b.ToString("dd/MM/yyyy") + " est de " + stat.nbCommandePeriode(a, b));
                        Console.WriteLine("Moyenne compte client est de: " + stat.MoyenneCompteClient());
                        break;
                }

                Console.WriteLine("Souhaitez-vous quitter la console ? Si oui tappez 1. Pour revenir au menu princiapl appuyer sur une autre touche.");
                sortie = Console.ReadLine();
            } while (sortie !="1");

        }

        public static Commande CreationCommande(List<Client> baseClient, List<France> baseFrance, List<Salarie> baseSalarie)
        {
            Client a;
            France f;
            Vehicule v;
            Livraison l;

            #region choix client
            string nouveauClient;
            baseClient.ForEach(a => Console.WriteLine(a.ToStringAffichage()));
            do
            {
                Console.WriteLine("Voici la liste des clients inscrits dans la base de donnée. \nSur quel nom souhaitez-vous effectuer la commande ? \nSi votre client est inscrit sur notre base tappez 0 sinon tappez 1.");
                nouveauClient = Console.ReadLine();

            } while (nouveauClient != "0" && nouveauClient != "1");

            if (nouveauClient == "1")
            {
                a = AjoutClient();
                baseClient.Add(a);
            }
            else
            {
                bool existe;
                Console.WriteLine("Saisir le numéro de sécurité social correspondant au client.");
                int choixClient = Convert.ToInt32(Console.ReadLine());
                existe = baseClient.Exists(x => x.NumSecu == choixClient);
                if (existe) a = baseClient.Find(x => x.NumSecu == choixClient);  //cas ou numero SS est correct
                else
                {
                    Console.WriteLine("Le numéro saisie n'est pas dans notre base de donnée. Nous allons creer un nouveau dossier.");
                    a = AjoutClient();
                    baseClient.Add(a);  //cas incorect on ajoute un nouveau client à la base
                }
            }
            #endregion

            #region choixVehicule
            string choixVehicule;
            string usage;
            do
            {
                Console.WriteLine("Quel type de véhicule ? \n1-Voiture (transport de passager) \n2-Camionnette \n3-Camion " +
                    "\nVeuillez saisir le chiffre correspondant à votre choix.");
                choixVehicule = Console.ReadLine();
            } while (choixVehicule != "1" && choixVehicule != "2" && choixVehicule != "3");
            if (choixVehicule == "1")
            {
                int nbPassager;
                do
                {
                    Console.WriteLine("Vous avez choisi une voiture. Saisir le nombre de passager entre 1 et 8 ?");
                    nbPassager = Convert.ToInt32(Console.ReadLine());
                } while (nbPassager < 1 || nbPassager > 8);
                v = new Voiture(nbPassager);


            }
            else if (choixVehicule == "2")
            {
                Console.WriteLine("Vous avez choisi une camionnette. Veuillez saisir l'usage de ce véhicule.");
                usage = Console.ReadLine();
                v = new Camionette(usage);
            }
            else
            {
                string choixCamion;
                double volume;
                int nbCuve;
                do
                {
                    Console.WriteLine("Vous avez choisi un camion. Que voulez-vous transporter ? " +
                        "\n1-Du liquide ou produit gazeux \n2-Matériel de travaux \n3-Produit périssable \nSaisir le numéro correspondant.");
                    choixCamion = Console.ReadLine();
                } while (choixCamion != "1" && choixCamion != "2" && choixCamion != "3");
                if (choixCamion == "1")
                {
                    do
                    {
                        Console.WriteLine("Vous aurez besoin d'un camion citerne. Volume transporté ?(maximum 25m^3)");
                        volume = Convert.ToInt32(Console.ReadLine());
                    } while (volume < 0 || volume > 25);
                    v = new CamionCiterne("LIQUIDE/GAZEUX", volume);
                }
                else if (choixCamion == "2")
                {
                    do
                    {
                        Console.WriteLine("Vous aurez besoin d'un camion benne. Volume transporté ?(maximum 20m^3)");
                        volume = Convert.ToInt32(Console.ReadLine());
                    } while (volume < 0 || volume > 20);
                    Console.WriteLine("Nombre de cuve souhaité ?");
                    nbCuve = Convert.ToInt32(Console.ReadLine());
                    v = new CamionBenne("MATERIEL TRAVAIL", volume, nbCuve);
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Vous aurez besoin d'un camion frigorifique. Volume transporté ?(maximum 35m^3)");
                        volume = Convert.ToInt32(Console.ReadLine());
                    } while (volume < 0 || volume > 35);
                    v = new CamionFrigo("PERISSABLE", volume);
                }
            }
            #endregion

            #region Detail livraison
            Console.WriteLine("Voici les destination proposé par TransConnect. Pour rappel, le déplacement dans les deux sens est possible.");
            //Afficher un sur deux éléments car même distance dans les deux sens
            for(int i = 0; i < baseFrance.Count; i += 2)
            {
                Console.WriteLine(baseFrance[i]);
            }
            bool present = false;
            string depart;
            string arrivee;
            bool intemperie;
            bool travaux;
            Console.WriteLine("Ville de départ de votre livraison ?");
            depart = Console.ReadLine().ToUpper();
            Console.WriteLine("Ville d'arrivée de votre livraison ?");
            arrivee = Console.ReadLine().ToUpper();
            present = baseFrance.Exists(x => x.Depart == depart && x.Arrivee == arrivee);
            if (present == true) f = baseFrance.Find(x => x.Depart == depart && x.Arrivee == arrivee);
            else
            {
                Console.WriteLine("Votre destination n'est pas dans notre liste. Nous allons l'ajouter");
                f = AjoutDestination();
                baseFrance.Add(f);


            }
            string test;
            do
            {
                Console.WriteLine("Des intempéries sont-ils à prévoir sur la route ? Si oui tappez true, sinon tappez false");
                test = Console.ReadLine().ToLower();

            } while (test != "true" && test != "false");
            intemperie = Convert.ToBoolean(test);
            do
            {
                Console.WriteLine("Des travaux sont-ils à prévoir sur la route ? Si oui tappez true, sinon tappez false");
                test = Console.ReadLine().ToLower();

            } while (test != "true" && test != "false");
            travaux = Convert.ToBoolean(test);
            l = new Livraison(f, intemperie, travaux);
            #endregion

            //Saisie de la date de commande
            Console.WriteLine("Vous allez maintenant saisir la date de la commande.");
            DateTime dateCommande = CreationDate();

            #region choix du chauffeur
            string nomChauffeur;
            List<Salarie> chauffeurdispo = baseSalarie.FindAll(x => x.Poste == "chauffeur" && x.EstDipo(dateCommande));
            if(chauffeurdispo==null)
            {
                Console.WriteLine("Aucun chauffeur n'est disponible pour la date saisie. Veuillez choisir une nouvelle date.");
                dateCommande = CreationDate();
            }
            Console.WriteLine("Voici la liste des chauffeurs disponible ainsi que son tarif supplémentaire.Veuillez saisir le nom de celui/celle qui vous intéresse.");
            chauffeurdispo.ForEach(x => Console.WriteLine(x.Nom+" "+x.Prenom+ "  Expérience du conducteur: "+x.Anciennete()+"ans   Tarif supplémentaire: "+x.PrixAnciennetChauffeur()));
            do
            {
                nomChauffeur = Console.ReadLine().Trim().ToUpper();
            } while (chauffeurdispo.Exists(x => x.Nom == nomChauffeur) == false);
            Salarie chauffeur = chauffeurdispo.Find(x => x.Nom == nomChauffeur);

            //ajouter à son emploi du temps la date de la commande
            chauffeur.EmploiDuTemps.Add(dateCommande);
           
            #endregion
            Commande retour =new Commande(a,chauffeur, l, v, dateCommande);
            //Ajouter à l'historique de la commande du client si date est inférieur à aujourd'hui
            if (dateCommande <= DateTime.Now) a.Historique.Add(retour);
            
            return retour;

        }

        public static DateTime CreationDate()
        {
            Console.WriteLine("Saisir l'année");
            int annee = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisir le mois");
            int mois = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisir le jour");
            int jour = Convert.ToInt32(Console.ReadLine());
            DateTime retour = new DateTime(annee, mois,jour);
            return retour;
        }

        public static List<France> RemplirbaseVille()
        {
            List<France> baseVille = new List<France>();
            StreamReader fichier = new StreamReader("/Users/suvetaa/Projects/TransConnect/TransConnect/Distance.txt");
            string ligne = fichier.ReadLine();
            string[] temp = ligne.Split(",");
            for(int i = 0; i < temp.Length; i+=3)
            {
                baseVille.Add(new France(temp[i].ToUpper(), temp[i + 1].ToUpper(), Convert.ToDouble(temp[i + 2])));
                baseVille.Add(new France(temp[i + 1].ToUpper(), temp[i].ToUpper(), Convert.ToDouble(temp[i + 2])));
            }
            return baseVille;
        }

        public static List<Salarie>  RemplirbaseSalarie()
        {
            List<Salarie> baseSalarie = new List<Salarie>();
            StreamReader file = new StreamReader("/Users/suvetaa/Projects/TransConnect/TransConnect/basesalarie.txt");
            string ligne = file.ReadLine();
            string[] temp = ligne.Split(",");
            //for(int i = 0; i < temp.Length; i++)
            //{
            //    Console.Write(temp[i]+"  ");
            //}
            
            for(int i = 0; i <temp.Length; i += 14)
            {
                baseSalarie.Add(new Salarie(Convert.ToInt32(temp[i]), temp[i + 1].ToUpper(), temp[i + 2].ToUpper(),
                    new DateTime(Convert.ToInt32(temp[i + 3]), Convert.ToInt32(temp[i + 4]), Convert.ToInt32(temp[i + 5])), temp[i + 6],
                    temp[i + 7], Convert.ToInt64(temp[i + 8]), new DateTime(Convert.ToInt32(temp[i+9]), Convert.ToInt32(temp[i+10]), Convert.ToInt32(temp[i+11])),
                    Convert.ToDouble(temp[i+12]),temp[i+13]));
            }
            baseSalarie[6].AjouterListEmploi(new List<DateTime>() { new DateTime(2022, 12, 12), new DateTime(2022, 12, 26), new DateTime(2023, 01, 12), new DateTime(2022, 10, 22), new DateTime(2023, 03, 01), new DateTime(2023, 02, 10) });
            baseSalarie[7].AjouterListEmploi(new List<DateTime>() { new DateTime(2022, 12, 10), new DateTime(2023, 01, 26), new DateTime(2023, 05, 28), new DateTime(2023, 01, 12), new DateTime(2023, 02, 11) });
            baseSalarie[8].AjouterListEmploi(new List<DateTime>() { new DateTime(2022, 12, 01), new DateTime(2023, 01, 26), new DateTime(2023, 02, 15), new DateTime(2023, 03, 17), new DateTime(2023, 01, 03), new DateTime(2023, 02, 12) });
            baseSalarie[10].AjouterListEmploi(new List<DateTime>() { new DateTime(2022, 11, 13), new DateTime(2023, 04, 05), new DateTime(2023, 02, 05), new DateTime(2023, 02, 13) });
            baseSalarie[11].AjouterListEmploi(new List<DateTime>() { new DateTime(2023, 01, 01), new DateTime(2023, 03, 01), new DateTime(2023, 02, 20), new DateTime(2023, 02, 01), new DateTime(2023, 02, 07), new DateTime(2023, 02, 14),new DateTime(2023,02,05) });
            return baseSalarie;            
        }

        public static Arbre RemplirbaseArbre(List<Salarie> baseSalarie)
        {
            Arbre structure = new Arbre(new Noeud(baseSalarie[0]));//Dupond
            structure.InsereSuccesseur(structure.Racine, new Noeud(baseSalarie[1]));//Fiesta
            structure.InsererFrere(structure.Racine.Successeur, new Noeud(baseSalarie[2]));//Forge
            structure.InsererFrere(structure.Racine.Successeur, new Noeud(baseSalarie[3]));//fermi
            structure.InsereSuccesseur(structure.Racine.Successeur, new Noeud(baseSalarie[4]));//Fetard
            structure.InsereSuccesseur(structure.Racine.Successeur.Successeur, new Noeud(baseSalarie[5]));//Royal
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur, new Noeud(baseSalarie[6]));//Romu
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur, new Noeud(baseSalarie[7]));//Romi
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur, new Noeud(baseSalarie[8]));//Roma
            structure.InsereSuccesseur(structure.Racine.Successeur.Successeur.Successeur, new Noeud(baseSalarie[9]));//Prince
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[10]));//Rome
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[11]));//Rimou
            structure.InsereSuccesseur(structure.Racine.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[12]));//Joyeuse
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[13]));//Couleur
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[14]));//Toutlemonde
            structure.InsereSuccesseur(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[15]));//Gripsous
            structure.InsereSuccesseur(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[16]));//Picsous
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[17]));//Fournier
            structure.InsererFrere(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[18]));//Gautier
            structure.InsereSuccesseur(structure.Racine.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur.Successeur, new Noeud(baseSalarie[19]));//Grosous
            return structure;
        }

        public static List<Client> RemplirBaseClient()
        {
            Client c1 = new Client(4829291, "LEMAITRE", "Marc", new DateTime(2000, 04, 12), "PARIS", "marc@outlook.fr", 0678990930);
            Client c2 = new Client(4890290, "DUPONT", "Luc", new DateTime(1998, 05, 10), "SEVRAN", "luc@outlook.fr", 0783998299);
            Client c3 = new Client(4982990, "BIC", "Pierre", new DateTime(1996, 03, 20), "PARIS", "pierre@outlook.fr", 0988299189);
            Client c4 = new Client(4485859, "LETIMBRE", "Michel", new DateTime(2000, 04, 12), "CERGY", "michel@outlook.fr", 0678929201);
            Client c5 = new Client(4458490, "BOSS", "Mike", new DateTime(1996, 05, 15), "PARIS", "mike@outlook.fr", 0783943229);
            Client c6 = new Client(4484839, "BICER", "Tom", new DateTime(1994, 02, 22), "LILLE", "tom@outlook.fr", 0988239299);
            Client c7 = new Client(4868494, "MAITRE", "Hugo", new DateTime(2000, 03, 17), "MARSEILLE", "hugo@outlook.fr", 0672389920);
            Client c8 = new Client(4493932, "CHAMPAGNE", "Victor", new DateTime(1969, 05, 06), "NANTES", "victor@outlook.fr", 072382919);
            Client c9 = new Client(4599302, "CHAMPS", "Julie", new DateTime(1974, 03, 10), "SEVRAN", "juliee@outlook.fr", 0988299384);
            Client c10 = new Client(48299202, "SUBRA", "Suj", new DateTime(2000, 04, 03), "PARIS", "sujsubra@outlook.fr", 078893890);
            List<Client> baseclient = new List<Client>() { c1,c2,c3,c4,c5,c6,c7,c8,c9,c10};
            return baseclient;

        }

        public static Client AjoutClient()
        {

            Console.WriteLine("Vous allez inscrire un nouveau client. Veuillez saisir son numéro de sécurité sociale.");
            int numSecu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisir son nom");
            string nom = Console.ReadLine();
            Console.WriteLine("Saisir son prenom.");
            string prenom = Console.ReadLine();
            Console.WriteLine("Date de naissance ? ");
            DateTime dateNaissance = CreationDate();
            Console.WriteLine("Saisir son adresse.");
            string adresse = Console.ReadLine();
            Console.WriteLine("Saisir son mail.");
            string mail = Console.ReadLine();
            Console.WriteLine("Saisir son numéro de téléphone.");
            int tel = Convert.ToInt32(Console.ReadLine());
            Client retour = new Client(numSecu, nom.ToUpper(), char.ToUpper(prenom[0]) + prenom.Substring(1).ToLower(), dateNaissance, adresse.ToLower(), mail.ToLower(), tel);
            return retour;
        }

        public static France AjoutDestination()
        {
            Console.WriteLine("Votre destination n'est pas proposé par nos service ? Veuillez saisir la ville de départ.");
            string depart = Console.ReadLine().ToUpper().Trim();
            Console.WriteLine("Veuillez saisir la ville d'arrivée.");
            string arrivee = Console.ReadLine().ToUpper().Trim();
            double distance;
            do
            {
                Console.WriteLine("Distance entre les deux villes (en km). Attention distance maximale ets de 850 km");
                distance = Convert.ToDouble(Console.ReadLine());
            } while (distance < 0 || distance > 850);

            return new France(depart, arrivee, distance);
        }
        public static Salarie AjoutSalarie()
        {
            Console.WriteLine("Vous allez inscrire un nouveau salarié. Veuillez saisir son numéro de sécurité sociale.");
            int numSecu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Saisir son nom");
            string nom = Console.ReadLine();
            Console.WriteLine("Saisir son prenom.");
            string prenom = Console.ReadLine();
            Console.WriteLine("Date de naissance ?");
            DateTime dateNaissance = CreationDate();
            Console.WriteLine("Saisir son adresse.");
            string adresse = Console.ReadLine();
            Console.WriteLine("Saisir son mail.");
            string mail = Console.ReadLine();
            Console.WriteLine("Saisir son numéro de téléphone.");
            int tel = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Date entrée ?");
            DateTime dateEntree = CreationDate();
            Console.WriteLine("Saisir son salaire");
            double salaire = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Saisir son poste");
            string poste = Console.ReadLine();
            return new Salarie(numSecu, nom.ToUpper(), prenom.ToUpper(), dateNaissance, adresse.ToLower(), mail.ToLower(), tel, dateEntree, salaire, poste);
        }
       
    }
}
