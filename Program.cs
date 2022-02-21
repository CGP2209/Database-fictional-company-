using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace grundforløbsprojekt
{
    class Program
    {
        //Hovedemenu er lavet som en methode, da den bliver kaldt flere gange.
        static void mainMenu()
        {
            Console.SetCursorPosition(10, 12); Console.WriteLine("Velkommen til Bent og Bentes genbrugsbutik ApS");
            Console.SetCursorPosition(10, 17); Console.Write("Vælg: [O] Opret kunde    [A]Administrator    [Q] Afslut");
        }
        //Admin menu'en er også lavet som en methode, da den bliver kaldt flere gange.
        static void adminMenu()
        {
            Console.SetCursorPosition(10, 12); Console.WriteLine("Bent og Bentes Genbrugsbutik ApS - ADMINISTRATOR");
            Console.SetCursorPosition(10, 17); Console.Write("Vælg: [1] Find kunde på navn   [3] Find kunde på telefon nr.  [5] Vis alle kunder  [9] Afslut");
        }
        //kontrolAfTLFNR kontrollere telefon nr. Og returnere telfonNr når det er "godkendt" Bliver brugt 2 gange.
        static string kontrolAfTLFNR(string telefonNr)
        {
            //2 lokale variabler der kun bliver brugt i denne metode
            string tegn = "";
            string forbudteTegn = "QWERTYUIOPÅASDFGHJKLÆØZXCVBNMqwertyuiopåasdfghjklæøzxcvbnm,.;:+-*/!\"\\#¤%&()=?@£$€{[]}";

            //For-løkken løber telefonNr igennem og kigger efter om det indholder et "tegn" der ikke er et tal.
            for (int pos = 0; pos < telefonNr.Length; pos++)
            {
                tegn = telefonNr.Substring(pos, 1);

                if (forbudteTegn.Contains(tegn))
                {
                    break;
                }
            }

            //While loop der kontrollere om længende på den string der hedder "telefonNr" er mindre eller større end 8.
            //Hvis "telefonNr.length" er 8 så springes loopen over"
            while (telefonNr.Length < 8 || telefonNr.Length > 8 || forbudteTegn.Contains(tegn))
            {
                //Hvis "telefonNr længenden er mindre end 8 køres koden i "IF'en" bruge gøres opmærksompå at det indtastet nummer er for kort
                if (telefonNr.Length < 8)
                {
                    Console.Clear();
                    Console.SetCursorPosition(10, 5); Console.WriteLine("Telefon nr: " + telefonNr);
                    Console.SetCursorPosition(10, 7); Console.WriteLine("Det indtastede telefon nummer er for kort.");
                    Console.SetCursorPosition(10, 8); Console.Write("Indtast dit otte cifrede telefon nr: ");
                    telefonNr = Console.ReadLine();
                }
                //Hvis "telefonNr længenden er mindre end 8 køres koden i "IF'en", bruge gøres opmærksompå at det indtastet nummer er for langt
                else if (telefonNr.Length > 8)
                {
                    Console.Clear();
                    Console.SetCursorPosition(10, 5); Console.WriteLine("Telefon nr: " + telefonNr);
                    Console.SetCursorPosition(10, 7); Console.WriteLine("Det indtastede telefon nummer er for langt.");
                    Console.SetCursorPosition(10, 8); Console.Write("Indtast dit otte cifrede telefon nr: ");
                    telefonNr = Console.ReadLine();
                }
                //Hvis telfon NR indholder et ikke "tal", køres koden i "IF'en", bruge gøres opmærksompå at det indtastet nummer indholder et tegn der ikke er et tal. 
                else if (forbudteTegn.Contains(tegn))
                {
                    Console.Clear();
                    Console.SetCursorPosition(10, 5); Console.WriteLine("Telefon nr: " + telefonNr);
                    Console.SetCursorPosition(10, 7); Console.WriteLine("Det indtastede telefon nummer må ikke indholde tegn.");
                    Console.SetCursorPosition(10, 8); Console.Write("Indtast dit otte cifrede telefon nr: ");
                    telefonNr = Console.ReadLine();

                    //For-løkken løber telefonNr igennem og kigger efter om det indholder et "tegn" der ikke er et tal.
                    for (int pos = 0; pos < telefonNr.Length; pos++)
                    {
                        tegn = telefonNr.Substring(pos, 1);

                        if (forbudteTegn.Contains(tegn))
                        {
                            break;
                        }
                    }
                }
            }
            //Metoden er nået til ende og returnere telefonNr.
            return telefonNr;
        }

        static void Main(string[] args)
        {
            //Definering af variabler. Opdelt efter typer (string, char, int og string array)
            string telefonNr = "1234";
            string adminKode = "Ab123456";
            string forNavn, efterNavn, adresse, postnr, by, email, søgeNavn, login;
            string samletKundeInfo = "";
            string søgning = "";

            char valg = 'g';
            //char søgeValg = 'g';
            char afslutVisning = 'g';

            int pos, tæller;
            int antalFundet;

            string[] kundeDatabase = File.ReadAllLines(@"c:\data\infobase.txt", Encoding.Unicode);

            //Her kaldes valg menuen metoden
            mainMenu();

            //Programmets hovede loop starter fordi valg fra start er sat til 'g'
            while (valg != 'Q')
            {
                //4 linjer. -Cursor skjules. -Metoden mainMenu køres. - Tasttryk skjules og gemmes i "valg". - "Valg" tvinges til at være stort bogstav (tegn)
                Console.CursorVisible = false;
                mainMenu();
                valg = Console.ReadKey(true).KeyChar;
                valg = char.ToUpper(valg);

                //Her starter switch
                switch (valg)
                {
                    case 'O':
                        //Opret bruger.
                        //4 linjer: 1. Clear skærmen, 2. Gør cursor synlig, 3. Placer cursoren hvor vi vil have den og be'r brugeren indtaste sit tlf nr,
                        //4. Aflæser brugers indtastning og gemmer det i "telefonNr"
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(10, 5); Console.Write("Indtast dit otte cifrede telefon nr: ");
                        telefonNr = Console.ReadLine();

                        //Her kaldes metoden "kontrolAfTLFNR" for at sikre at tlf.nr har den rette længde.
                        telefonNr = kontrolAfTLFNR(telefonNr);

                        //Tvinger koden til at gå igennem foreach loopet når "telefonNr" længden er 8
                        if (telefonNr.Length == 8)
                        {
                            //gennem går arrayet "kundeDatase" for "telefonNr"
                            foreach (var item in kundeDatabase)
                            {
                                Console.CursorVisible = false;
                                søgning = item;
                                //Hvis "søgning" indholder "telefonNr" så går koden ind i "if" og afslutter "Foreach" loopet
                                if (søgning.Contains(telefonNr))
                                {
                                    //Udskriver brugeren er i databasen, skjuler tastetryk
                                    Console.SetCursorPosition(10, 15); Console.WriteLine("Du findes allerede i vores database");
                                    Console.SetCursorPosition(10, 17); Console.Write("Tryk på en tast for at forsætte til start menuen ");
                                    Console.ReadKey(true);
                                    break;
                                }
                            }
                        }
                        //Hvis "Søgning" IKKE indholder "telefonNR" Så starter Oprettelsen af en bruger
                        if (!søgning.Contains(telefonNr))
                        {
                            //2. linjer: Renser skærmen og sætter cursoren til synlig, så brugeren kan se hvor denne skriver:
                            Console.Clear();
                            Console.CursorVisible = true;
                            //Først udskrevet "menuen" med det indtastede tlf nr.
                            Console.SetCursorPosition(10, 5); Console.WriteLine("Telefon nr     	:  	{0} 	OK! 	Kan oprettes.", telefonNr);
                            Console.SetCursorPosition(10, 6); Console.WriteLine("Navn              	:");
                            Console.SetCursorPosition(10, 7); Console.WriteLine("Efternavn         	:");
                            Console.SetCursorPosition(10, 8); Console.WriteLine("Adresse        	:");
                            Console.SetCursorPosition(10, 9); Console.WriteLine("Postnr         	:");
                            Console.SetCursorPosition(10, 10); Console.WriteLine("By             	:");
                            Console.SetCursorPosition(10, 11); Console.WriteLine("E-mail        	:");
                            //Så tvinger vi cursoren tilbage til de steder hvor brugeren skal indtaste sin infomationer
                            Console.SetCursorPosition(40, 6); forNavn = Console.ReadLine();
                            Console.SetCursorPosition(40, 7); efterNavn = Console.ReadLine();
                            Console.SetCursorPosition(40, 8); adresse = Console.ReadLine();
                            Console.SetCursorPosition(40, 9); postnr = Console.ReadLine();
                            Console.SetCursorPosition(40, 10); by = Console.ReadLine();
                            Console.SetCursorPosition(40, 11); email = Console.ReadLine();

                            //De følgende 4 linjer sørger for at forbogstavet i "navn, efternavn, adresse og by" bliver stort og de resterne små.
                            forNavn = forNavn.Substring(0, 1).ToUpper() + forNavn.Substring(1).ToLower();
                            efterNavn = efterNavn.Substring(0, 1).ToUpper() + efterNavn.Substring(1).ToLower();
                            adresse = adresse.Substring(0, 1).ToUpper() + adresse.Substring(1).ToLower();
                            by = by.Substring(0, 1).ToUpper() + by.Substring(1).ToLower();

                            //De samlede infomationer bliver gemt i "samletKundeInfo", som så bliver gemt i filen "infobase"
                            samletKundeInfo = "\n" + telefonNr + ";" + forNavn + "," + efterNavn + ";" + adresse + ";" + postnr + ";" + by + ";" + email;
                            File.AppendAllText(@"c:\data\infobase.txt", samletKundeInfo, Encoding.Unicode);
                            Console.SetCursorPosition(10, 15); Console.WriteLine("Du er nu oprettet i vores kunde database !");
                        }
                        //Arrayet "kundeDatabase" bliver opdateret efter at der nu er blevet oprettet en bruger i filen.
                        kundeDatabase = File.ReadAllLines(@"C:\Users\Caspe\OneDrive\Skrivebord\TEC - Programmering afleveringer\Random datainfobase.txt", Encoding.Unicode);
                        Console.SetCursorPosition(10, 17); Console.Write("Tryk på en tast for at forsætte til start menuen ");
                        //4 linjer: Cursoren bliver skjult, tastetryk bliver også skjult, så rydder vi konsolen og kalder "mainMenu"
                        Console.CursorVisible = false;
                        Console.ReadKey(true);
                        Console.Clear();
                        mainMenu();
                        break;

                    case 'A':
                        //3 linjer: Clear konsolen, vi sikre at "login" ikke indholder noget og så gør vi curor synlig til brugeren.
                        Console.Clear();
                        login = "";
                        Console.CursorVisible = true;

                        //4 linjer: Der blivber bedt om et login, mens teksten skrifter farve som baggrundfarven, for at skjule koden (ikke optim, koden gemmes
                        //i login og cursor farven bliver igen hvid
                        Console.SetCursorPosition(10, 5); Console.Write("Login: ");
                        Console.SetCursorPosition(17, 5); Console.ForegroundColor = Console.BackgroundColor;
                        login = Console.ReadLine();
                        Console.SetCursorPosition(17, 5); Console.ForegroundColor = ConsoleColor.White;

                        //Hvis login stemmer overens med adminKode forsætter programmet ind i "if"
                        if (login == adminKode)
                        {
                            //Konsolen cleares
                            Console.Clear();
                            //Så længe "valg" er alt andet end '9' så køre programmet ind i while-løkken.
                            while (valg != '9')
                            {
                                //Cursoren skjules. - Metoden adminMenu køre og udskriver admin menuen. -Brugerens tastetryk gemmes i "valg".
                                //-Valg bliver gjort til store bogstaver
                                Console.CursorVisible = false;
                                adminMenu();
                                valg = Console.ReadKey(true).KeyChar;
                                valg = char.ToUpper(valg);

                                switch (valg)
                                {
                                    //Find bruger på navn
                                    case '1':
                                        Console.Clear();
                                        Console.CursorVisible = true;
                                        //Programmet beder om bruger et navn den kan søge på
                                        Console.SetCursorPosition(10, 5); Console.Write("Indtast navn: "); søgeNavn = Console.ReadLine();
                                        søgeNavn = søgeNavn.Substring(0, 1).ToUpper() + søgeNavn.Substring(1).ToLower();
                                        Console.CursorVisible = false;
                                        //Sætter "tæller", "pos", "antalFundet" til 0, alle 3 variabler er brugt til at styre søge funktionen.
                                        tæller = 0;
                                        pos = 7;
                                        antalFundet = 0;
                                        //programmet køre igennem en FOREACH løkke for at gennemgå arrayet "kundeDatabase" for brugerens input
                                        foreach (var item in kundeDatabase)
                                        {
                                            //string søgning bliver til indholdet af arrayets index
                                            søgning = item;
                                            //Hvis "søgning" indholder brugerens input som er gemt i "søgeNavn"
                                            if (søgning.Contains(søgeNavn))
                                            {
                                                //Udskriver "søgning" på posistion 10, "pos" 
                                                Console.SetCursorPosition(10, pos); Console.WriteLine(søgning);
                                                //"pos" og "antalFundet" får lagt en til sig. "pos" bliver brugt til at styre hvis søgningen skal
                                                //udskrive mere end 2 linjer
                                                pos++;
                                                antalFundet++;
                                            }
                                        }
                                        //Hvis "antalFundet" er lig med 0 så går programmet ind og fortæller at brugeren ikke findes,
                                        /// samt sender brunger tilbage til admin menuen
                                        if (antalFundet == 0)
                                        {
                                            Console.Clear();
                                            Console.SetCursorPosition(10, 6); Console.WriteLine("{0} findes ikke i databasen:", søgeNavn);
                                        }
                                        Console.SetCursorPosition(10, 15); Console.Write("Tryk på en tast for at reture til Administrator menuen ");
                                        Console.ReadKey(false);
                                        Console.Clear();
                                        Console.SetCursorPosition(10, 17); adminMenu();

                                        break;
                                    //Find bruger på telefon nr
                                    case '3':
                                        //4. Linjer. Clear konsolen. - Cursor bliver synlig, så bruger kan se hvor de skriver.
                                        //Programmet beder om et input (tlf. nr.) - Cursor bliver skjult igen.
                                        Console.Clear();
                                        Console.CursorVisible = true;
                                        Console.SetCursorPosition(10, 5); Console.Write("Indtast telefon nummer: "); telefonNr = Console.ReadLine();
                                        Console.CursorVisible = false;

                                        //Køre metoden "kontrolAfTLFNR" (Forklaring ligger oppe under metoden)
                                        telefonNr = kontrolAfTLFNR(telefonNr);

                                        //Tvinger koden til at gå igennem foreach loopet når "telefonNr" længden er 8 tegn
                                        if (telefonNr.Length == 8)
                                        {
                                            //gennem går arrayet "kundeDatase" 
                                            foreach (var item in kundeDatabase)
                                            {
                                                //gennem går arrayet "kundeDatase" for "telefonNr"
                                                søgning = item;
                                                //Hvis "søgning" indholder "telefonNr" så går koden ind i "if" og afslutter "Foreach" loopet
                                                if (søgning.Contains(telefonNr))
                                                {
                                                    //3. Linjer - Clear konsollen - printer ud at brugerns input er fundet i databasen, sat dataen fra
                                                    //array'et
                                                    Console.Clear();
                                                    Console.SetCursorPosition(10, 6); Console.WriteLine("{0} er fundet i databasen:", telefonNr);
                                                    Console.SetCursorPosition(10, 8); Console.WriteLine(item);
                                                    break;
                                                }
                                            }
                                        }
                                        //Hvis "søgning" ikke indholde "telefonNr" udføres koden i IF'en
                                        if (!søgning.Contains(telefonNr))
                                        {
                                            //3 linjer: Cursor skjules - konsolen clears for tekst. - Udskriver at brugerens input ikke er fundet på en bestem position.
                                            Console.CursorVisible = false;
                                            Console.Clear();
                                            Console.SetCursorPosition(10, 6); Console.WriteLine("{0} er ikke i databasen:", telefonNr);
                                        }
                                        //4 linjer - Beder om et tastetryk for at forsætte. - Tastetryk skjules. - Konsolen skjules. - køre metoden "adminMenu" på 
                                        //position 10,17
                                        Console.SetCursorPosition(10, 15); Console.Write("Tryk på en tast for at forsætte til Administrator menuen ");
                                        Console.ReadKey(true);
                                        Console.Clear();
                                        Console.SetCursorPosition(10, 17); adminMenu();
                                        break;

                                    //Søg efter bruger
                                    case '5':
                                        //2 linjer. -Clear konsolen. -Fortæller bruger hvor meget kunder der er i databasen.
                                        Console.Clear();
                                        Console.SetCursorPosition(10, 5); Console.WriteLine("Der er {0} kunder i vores database", kundeDatabase.Length);

                                        //Tæller" og "pos" bliver "resat" til de værdier vi vil have de starter på. "afslutVisning" bliver resat til 'g' i
                                        //tilfælde programmet har været kørt igennem engang
                                        tæller = 0;
                                        pos = 7;
                                        afslutVisning = 'g';

                                        //
                                        for (int i = 0; i < kundeDatabase.Length; i++)
                                        {
                                            Console.SetCursorPosition(10, 5); Console.WriteLine("Der er {0} kunder i vores database", kundeDatabase.Length);
                                            Console.SetCursorPosition(10, pos); Console.WriteLine(kundeDatabase[i]);

                                            //"tæller" og "pos" får lagt 1 til deres værdi.
                                            //Tæller styre hvor mange gange "for-løkken skal køre før den pauser"
                                            //"pos" styre hvor på skærmen der skal printes ud.
                                            //"tæller" styre hvornår søgningen skal pauses - i dette tilfælde når der er udskrevet 15 kunder.
                                            tæller++;
                                            pos++;

                                            //Når tæller er lig med 15 så pauses programmet
                                            if (tæller == 15)
                                            {
                                                //5 linjer: - Sætter cursor positionen og fortæller brugeren programmet afventer et tastetryk eller at denne kan taste '7'
                                                //for at afslutte søgningen. tastetryk skjules, aflæses og gemme i "afslutVisning"
                                                //tæller bliver sat til 0, for at "FOR-løkken" kan udskrive de næste 15 linjer
                                                //pos sættes til 7 så udskrivningen af de næste 15 linjer starter fra 7
                                                //konsolen cleares.
                                                Console.SetCursorPosition(10, pos + 1); Console.Write("Tryk på en tast for at forsætte: ");
                                                Console.SetCursorPosition(10, pos + 3); Console.Write("Tryk på 7 for at afslut visning! ");
                                                afslutVisning = Console.ReadKey().KeyChar; ;
                                                tæller = 0;
                                                pos = 7;
                                                Console.Clear();
                                            }
                                            //hvis brugeren har valgt og afslutte søgningen før den er løbet hele databasen igennem:
                                            if (afslutVisning == '7')
                                            {
                                                //i fra FOR-løkkebn sættes til længden på "kundeDatabase" så FOR-løkken slutter
                                                i = kundeDatabase.Length;
                                            }
                                            //Hvis i har samme værdig som "kundeDatabase":
                                            if (i == kundeDatabase.Length)
                                            {
                                                //Cursor bliver skjult og der udskrives at listen er nået til ende, på en bestemt position
                                                Console.CursorVisible = false;
                                                Console.SetCursorPosition(10, pos + 5); Console.Write("Slut på listen.");
                                            }
                                        }
                                        //5 linjer. -Beder bruger lave et tastetryk for at programmet forsætter.
                                        //cursoren skjules. - Tastetryk bliver skjult. - konsolen cleares. - metoden adminMenu køres og udskriver adminMenuen.
                                        Console.SetCursorPosition(10, 17); Console.Write("Tryk på en tast for at forsætte til Administrator menuen ");
                                        Console.CursorVisible = false;
                                        Console.ReadKey(true);
                                        Console.Clear();
                                        adminMenu();
                                        break;

                                    // Afslut admin-menuen og sender brugeren til hovedmenuen
                                    case '9':
                                        //Konsolen clears. -Cursoren bliver skjult. -Brugeren informers at denne har valgt at afslutte admin menuen og
                                        //at programmet afventer et tastetryk. - Tastetryk skjules. -Konsolen cleares
                                        Console.Clear();
                                        Console.CursorVisible = false;
                                        Console.SetCursorPosition(10, 5); Console.WriteLine("Du har valgt og afslutte administator menuen");
                                        Console.SetCursorPosition(10, 6); Console.Write("Tryk på en tast for at returnere til hovedmenuen: ");
                                        Console.ReadKey(true);
                                        Console.Clear();
                                        break;

                                    //Default-adminMenu (Vises hvis ikke taster et korrekt menu valg
                                    default:
                                        //Konsolen cleares. -Cursor skjules. -Brugeren gøres opmærksom på deres valg er ugyldigt. -Brugeren senderes tilbage til admin menuen.
                                        Console.Clear();
                                        Console.CursorVisible = false;
                                        Console.SetCursorPosition(10, 5); Console.WriteLine("Ugyldigt valg! Vælg fra menuen:");
                                        Console.SetCursorPosition(10, 17); adminMenu();
                                        break;
                                }
                            }
                        }
                        //Hvis ikke login stemmer ens med adminKode så får bruger besked og sendes nu retur til hovedmenuen
                        else
                        {
                            //Konsolen cleares. -Cursoren skjules. -Brugeren informeres om at koden var forkert og at denne sendes retur til hovedmenuen ved næste tastetryk
                            //Tastetryk skjules
                            Console.Clear();
                            Console.CursorVisible = false;
                            Console.SetCursorPosition(10, 5); Console.Write("Forkert kode!");
                            Console.SetCursorPosition(10, 6); Console.WriteLine("Tryk på en tast for at returere til menuen: ");
                            Console.ReadKey(true);
                        }
                        //"login" og "valg" bliver sat til værdier vi ved ikke bliver brugt i koden. Herefter køres metoden mainMenu.
                        login = "g";
                        valg = 'g';
                        mainMenu();
                        break;
                    //Quit
                    case 'Q':
                        //Her gør bruger påmærksom på at denne har valgt og afslutte programmet.
                        //"Hovedløkken" afsluttes og programmet afsluttes efter et tastetryk.
                        Console.Clear();
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(10, 5); Console.WriteLine("Du har valgt og afslutte");
                        Console.SetCursorPosition(10, 6); Console.Write("Tryk på en tast for at lukke konsolen: ");
                        break;
                    //Default (Vises hvis ikke brugeren taster et korrekt menu valg
                    default:
                        Console.Clear();
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(10, 5); Console.WriteLine("Ugyldigt valg! Vælg fra menuen:");
                        Console.SetCursorPosition(10, 17); mainMenu();
                        break;
                }
            }
            //Afventer et tastetryk for at afslutte konsollen.
            Console.ReadKey();
            console.Readkey();
        }
    }
}

