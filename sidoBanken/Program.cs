using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace sidoBanken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool parseCheck;
            double a;
            Console.WriteLine("skriv in en siffra: ");
            string userInput = Console.ReadLine();
             
            parseCheck = double.TryParse(userInput, out a);
            Console.WriteLine("Strängen är en numerisk datatyp: " + parseCheck);
            // TODO: 
            Users user1 = new Users("Fred", "123");
            Users user2 = new Users("Kjell", "321");
            Users user3 = new Users("Bo", "321");

            Users[] users = new Users[] { user1, user2, user3 };

            string[] userNames = new string[] { "inky", "blinky", "pinky", "clyde", "pac", "man" };
            string[] passwords = new string[] { "1891", "1981", "6666", "7777", "1234", "0159" };
            string[][] accountNames = new string[][] {
                new string[] { "SparkontoI", "LönekontoI" },
                new string[] { "SparkontoB", "LönekontoB", "ISK-B" },
                new string[] { "SparkontoP", "LönekontoP", "ISK-P", "Ädelmetaller" },
                new string[] { "SparkontoC", "LönekontoC", "ISK-C", "ISK", "ISK" },
                new string[] { "SparkontoPa", "LönekontoPac", "ISK-Pa", "ISK", "ISK", "ISK" },
                new string[] { "SparkontoMa", "LönekontoMa", "ISK-Ma", "Sparkonto 2", "ISK", "ISK", "ISK" }
            };
            double[][] accountBalances = new double[][] {
             new double[] { 2001, 5000.1 },
             new double[] { 2002, 5000, 1000 },
             new double[] { 2003, 5000, 5000, 1000 },
             new double[] { 2004, 5000, 5000, 1000, 5000 },
             new double[] { 2005, 5000, 5000, 1000, 5000, 1000 },
             new double[] { 2006, 5000, 5000, 1000, 5000, 1000, 5000 }
        };
            
         //   int[] account1 = new int[] { 10000, 20000, 30000 };
         //   int[] account2 = new int[] { 23232, 232323, 23223 };
            //string currentUser = Console.ReadLine();
            //string currentPassword = Console.ReadLine();
            Console.WriteLine("Välkommen till banken!");
            MainLogin(userNames, passwords, accountNames, accountBalances);
            //LoggedIn(); // todo: flytta in till inuti if-satsen

        }

        static void showAccounts(string[] accounts, double[] balances)
        {
            // steg 1. skriv e for loop som loopar igenom accounts och skriver ut den
            // steg 2. skriv även ut samma index ur balances, OBS i samma for loop
            for (int index = 0; index < accounts.Length; index++)
            {
               /* for (int j = 0; j < accounts.Length; j++)
                {
                    Console.WriteLine(balances[j]);
                } */
                Console.WriteLine(accounts[index] + ": " + balances[index]);
                
            }
            Console.WriteLine("Tryck [Enter] för att komma till huvudmenyn");
            Console.ReadKey();


        }

        static void withdrawMoney(string[] accounts, double[] balances, string password) // ToDo: skicka in inloggad användares pinkod i metoden
        {
            Console.WriteLine("pw: " + password);
            
            
            // steg 1. Be användaren välja ett konto att ta ut pengar ifrån
            // steg 2. Be användaren skriva in en summa att ta ut 
            // steg 3. Ta bort den summan från kontot, visa kvarkvarande saldo
            Console.WriteLine("Välj ett konto att ta ut pengar ifrån: ");
            for (int index = 0; index < accounts.Length; index++)
            {
                /* for (int j = 0; j < accounts.Length; j++)
                 {
                     Console.WriteLine(balances[j]);
                 } */
                Console.WriteLine($"{index + 1} {accounts[index]} {balances[index]} index: {index}");
            }
            Console.Write("===> ");
            string userInput = Console.ReadLine();
            Console.WriteLine($"Du valde {userInput}");


            // Omvandla userInput till en int minus 1
            int userChoice = int.Parse(userInput) - 1;
            Console.WriteLine($"Ange en summa mellan 1 och {balances[userChoice]}");
            Console.Write("===> ");
            userInput = Console.ReadLine();
            double userAmount = double.Parse(userInput);
            // kolla om användaren har täckning på kontot

            Console.Write("Skriv in din pin: ");
            string inputPw = Console.ReadLine();
            if (inputPw == password)
            {
                Console.WriteLine("Rätt pin!");
            }
            else
            {
                return;
            }
            if (userAmount > balances[userChoice])
            {
                Console.WriteLine("Du har inte så mycket pengar");
            }
            else if (userAmount <= 0) // Todo: Shorthand med två uttryck och ett frågetecken
            {
                Console.WriteLine($"Värdet måste vara större än noll (0)");
                return;
            }
                else
            {

                balances[userChoice] -= userAmount;
                Console.WriteLine($"Saldo {balances[userChoice]}");
            }
            Console.WriteLine("Tryck [Enter] för att komma till huvudmenyn");
            Console.ReadKey();
            return;

        }

        static void transferMoney(string[] accounts, double[] balances)
        {

            // steg 1. Be användaren välja ett konto att flytta pengar från
            // steg 2. Be användaren skriva in en summa att flytta 
            // steg 3. Ta bort den summan från kontot, visa kvarkvarande saldo  
            Console.WriteLine("Välj ett konto att flytta pengar ifrån: ");
            for (int index = 0; index < accounts.Length; index++)
            {
                /* for (int j = 0; j < accounts.Length; j++)
                 {
                     Console.WriteLine(balances[j]);
                 } */
                Console.WriteLine($"{index + 1} {accounts[index]} {balances[index]} index: {index}");
            }
            Console.Write("===> ");
            string amountFrom = Console.ReadLine();
            int userChoiceFrom = int.Parse(amountFrom) - 1;
            Console.WriteLine($"Du valde {amountFrom}");

            Console.WriteLine("Välj ett konto att sätta in pengar på: ");
            for (int index = 0; index < accounts.Length; index++)
            {
                // Om index inte är lika med indexet användaren angav tidigare
                // ... skriv ut nedanstående
                if (index != userChoiceFrom)
                {
                    Console.WriteLine($"{index + 1} {accounts[index]} {balances[index]} index: {index}");
                }
                
            }
            Console.Write("===> ");
            string userInput = Console.ReadLine();
            Console.WriteLine($"Du valde {userInput}");


            // Omvandla userInput till en int minus 1
            int userChoiceTo = int.Parse(userInput) - 1;
            Console.WriteLine($"Ange en summa mellan 1 och {balances[userChoiceFrom]}");
            Console.Write("===> ");
            userInput = Console.ReadLine();
            double userAmount = double.Parse(userInput);
            // kolla om användaren har täckning på kontot

            if (userAmount > balances[userChoiceFrom])
            {
                Console.WriteLine("Du har inte så mycket pengar");
            }
            else
            {
                balances[userChoiceFrom] -= userAmount;
                balances[userChoiceTo] += userAmount;
                // Console.WriteLine($"Saldo, choice from: {balances[userChoiceFrom]}, choice to: {balances[userChoiceTo]}");
                Console.WriteLine($"{accounts[userChoiceFrom]} {balances[userChoiceFrom]}");
                Console.WriteLine($"{accounts[userChoiceTo]} {balances[userChoiceTo]}");
            }
            Console.WriteLine("Tryck [Enter] för att komma till huvudmenyn");
            Console.ReadKey();

        }
        static void MainLogin(string[] userArray, string[] passwordArray, string[][] accountArray, double[][] balanceArray)
        {
            bool mainMenu = true;
            int loginAttempts = 3;
           
            while (mainMenu)
            {
                
                //string[] userNames = new string[] { "Fred", "Kjell", "Bo", "Tor", "Hel", "Bal" };
                //string[] passwords = new string[] { "123", "123456", "321" };

                //bool contains = false;
                Console.Write("Skriv in ditt användarnamn: ");
                string inputFromUser = Console.ReadLine();
                string user = inputFromUser.ToLower();
                //string user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                Console.Write("Skriv in din pinkod: ");
                string pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass' 
                int logtest = 0;


                for (int i = 0; i < passwordArray.Length; i++)
                {
                    //Console.WriteLine(userNames[i]);
                    //Console.WriteLine(passwords[i]);

                    if (user == userArray[i] && pass == passwordArray[i])
                    {
                        logtest = 1;
                        //contains = true;
                        //Console.Clear();
                        Console.WriteLine("Du loggades in!");
                        // anropa logged in
                        // nollställ loginattempts = 2
                        
                        LoggedIn(i, userArray, passwordArray, accountArray, balanceArray);
                        //mainMenu= false;
                        break; // Varför fungerar detta?

                        //flytta ut mainMenu = false utanför loopen

                    }
                    /*else
                    {
                        loginAttempts--; // todo: flytta utanför for-loopen, så att alla konton kollas för en lyckas inloggning INNAN attempts minskar
                        Console.WriteLine("Invalid input");
                        Console.Write("Skriv in ditt användarnamn: ");
                        user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                        Console.Write("Skriv in ditt lösenord: ");
                        pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass'
                        
                    } */

                }
                if (logtest == 0)
                {
                    loginAttempts--; // todo: flytta utanför for-loopen, så att alla konton kollas för en lyckas inloggning INNAN attempts minskar 
                    if (loginAttempts > 0)
                    {
                        Console.WriteLine("Invalid input. Försök igen. Försök kvar:" + loginAttempts);
                    }
                }
                //user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                //Console.Write("Skriv in ditt lösenord: ");
                //pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass'
                // TODO: Här kan du veta om användaren ej lyckaades logga in
                // minska loginattempts
                // ge en ny chans om an har attempts kvar

                if (loginAttempts == 0)
                {
                    Console.WriteLine("Dina inloggningsförsök har tagit slut.Programmet stängs ner!");
                    mainMenu = false;
                    Console.ReadLine();
                    // todo: stängs programmet verkligen ner? Vad avgör?
                }              
                
            }
        }

         static void LoggedIn(int currentUserIndex, string[] userArray, string[] passwordArray, string[][] accountArray, double[][] balanceArray)

        {
            //int[] account1 = new int[] { 10000, 20000, 30000 };
            //int[] account2 = new int[] { 23232, 232323, 23223 };
            //Console.WriteLine("Du är inloggad!");
            //Console.WriteLine("Tryck på enter för att få menyval.");
            
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("Gör ditt val med knapparna 1-4");
                Console.WriteLine("1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Logga ut");

                string userChoice = Console.ReadLine();
                //int nrChoice = int.Parse(userChoice);
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("==========\nSe dina konton och saldo");
                        showAccounts(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;

                    case "2":
                        Console.WriteLine("==========\nÖverföring mellan konton");
                        transferMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;
                    case "3":
                        Console.WriteLine("==========\nTa ut pengar");
                        withdrawMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex], passwordArray[currentUserIndex]);
                        break;

                    case "4":
                        Console.WriteLine("==========\nLogga ut");
                        //MainLogin(array, array2);
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt menyval. Gör val med siffra 1-4");
                        continue;
                }
            }
            
        }

    } 
}
