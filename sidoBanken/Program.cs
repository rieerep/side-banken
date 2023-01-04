using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace sidoBanken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Users user1 = new Users("Fred", "123");
            Users user2 = new Users("Kjell", "321");
            Users user3 = new Users("Bo", "321");

            Users[] users = new Users[] { user1, user2, user3 };

            string[] userNames = new string[] { "Fred", "Kjell", "Bo", "Tor", "Hel", "Bal" };
            string[] passwords = new string[] { "123", "123456", "321", "321", "321", "321" };
            string[][] accountNames = new string[][] {
                new string[] { "SparkontoF", "LönekontoF" },
                new string[] { "SparkontoK", "LönekontoK", "ISK" },
                new string[] { "SparkontoB", "LönekontoB", "ISK", "ISK" },
                new string[] { "SparkontoT", "LönekontoT", "ISK", "ISK", "ISK" },
                new string[] { "SparkontoH", "LönekontoH", "ISK", "ISK", "ISK", "ISK" },
                new string[] { "SparkontoBa", "LönekontoBa", "ISK", "Sparkonto 2", "ISK", "ISK", "ISK" }
            };
            double[][] accountBalances = new double[][] {
             new double[] { 2001, 5000 },
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

        }

        static void withdrawMoney(string[] accounts, double[] balances)
        {

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
            int userAmount = int.Parse(userInput);
            // kolla om användaren har täckning på kontot

            if (userAmount > balances[userChoice])
            {
                Console.WriteLine("Du har inte så mycket pengar");
            } else
            {

                balances[userChoice] -= userAmount;
                Console.WriteLine($"Saldo {balances[userChoice]}");
            }
            Console.WriteLine("Kvarstående saldo: " );
         
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
            Console.WriteLine($"Du valde {amountFrom}");

            Console.WriteLine("Välj ett konto att sätta in pengar på: ");
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
            int userAmount = int.Parse(userInput);
            // kolla om användaren har täckning på kontot

            if (userAmount > balances[userChoice])
            {
                Console.WriteLine("Du har inte så mycket pengar");
            }
            else
            {

                balances[userChoice] -= userAmount;
                Console.WriteLine($"Saldo {balances[userChoice]}");
            }
            Console.WriteLine("Kvarstående saldo: ");

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
                string user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                Console.Write("Skriv in ditt lösenord: ");
                string pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass' 
                for (int i = 0; i < passwordArray.Length; i++)
                {
                    //Console.WriteLine(userNames[i]);
                    //Console.WriteLine(passwords[i]);

                    if (user == userArray[i] && pass == passwordArray[i])
                    {
                        //contains = true;
                        //Console.Clear();
                        Console.WriteLine("Du loggades in!");
                        // anropa logged in
                        // nollställ loginattempts = 2
                        
                        LoggedIn(i, userArray, passwordArray, accountArray, balanceArray);
                        //flytta ut mainMenu = false utanför loopen
                        //mainMenu = false;
                        break;
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
                loginAttempts--; // todo: flytta utanför for-loopen, så att alla konton kollas för en lyckas inloggning INNAN attempts minskar
                Console.WriteLine("Invalid input. Försök igen. Försök kvar:" + loginAttempts);
                Console.Write("Skriv in ditt användarnamn: ");
                user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                Console.Write("Skriv in ditt lösenord: ");
                pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass'
                                           // TODO: Här kan du veta om användaren ej lyckaades logga in
                                           // minska loginattempts
                                           // ge en ny chans om an har attempts kvar

                if (loginAttempts == 0)
                {
                    Console.WriteLine("Dina inloggningsförsök har tagit slut.Programmet stängs ner!");
                    Console.ReadLine();
                    // todo: stängs programmet verkligen ner? Vad avgör?
                }              
                
            }
        }

        /* static void MainLogin()
        {
            bool mainMenu = true;
            int loginAttempts = 2;

            while (mainMenu)
            {
                //string[] userNames = new string[] { "Fred", "Kjell", "Bo", "Tor", "Hel", "Bal" };
                //string[] passwords = new string[] { "123", "123456", "321" };

                //bool contains = false;
                Console.Write("Skriv in ditt användarnamn: ");
                string user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                Console.Write("Skriv in ditt lösenord: ");
                string pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass' 
                for (int i = 0; i < passwords.Length; i++)
                {
                    //Console.WriteLine(userNames[i]);
                    //Console.WriteLine(passwords[i]);

                    if (user == userNames[i] && pass == passwords[i])
                    {
                        //contains = true;
                        //Console.Clear();
                        Console.WriteLine("Du loggades in!");
                        // anropa logged in
                        // nollställ loginattempts = 2
                        LoggedIn(i);
                        //flytta ut mainMenu = false utanför loopen
                        mainMenu = false;
                        break;
                    }
                    else
                    {
                        loginAttempts--; // todo: flytta utanför for-loopen, så att alla konton kollas för en lyckas inloggning INNAN attempts minskar
                        Console.WriteLine("Invalid input");
                        Console.Write("Skriv in ditt användarnamn: ");
                        user = Console.ReadLine(); // current user = användarnamnet som är i string-variabeln 'user' 
                        Console.Write("Skriv in ditt lösenord: ");
                        pass = Console.ReadLine(); // current pass = lösenordet som är i string-variabeln 'pass'

                    }

                }
                // TODO: Här kan du veta om användaren ej lyckaades logga in
                // minska loginattempts
                // ge en ny chans om an har attempts kvar

                if (loginAttempts == 0)
                {
                    Console.WriteLine("Programmet stängs ner!");
                    Console.ReadLine();
                    // todo: stängs programmet verkligen ner? Vad avgör?
                }

            }
        } */

         static void LoggedIn(int currentUserIndex, string[] userArray, string[] passwordArray, string[][] accountArray, double[][] balanceArray)

        {
            //int[] account1 = new int[] { 10000, 20000, 30000 };
            //int[] account2 = new int[] { 23232, 232323, 23223 };
            //Console.WriteLine("Du är inloggad!");
            //Console.WriteLine("Tryck på enter för att få menyval.");
            
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Logga ut");

                string userChoice = Console.ReadLine();
                //int nrChoice = int.Parse(userChoice);
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("1. Se dina konton och saldo");
                        showAccounts(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;

                    case "2":
                        Console.WriteLine("2. Överföring mellan konton");
                        transferMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;
                    case "3":
                        Console.WriteLine("3. Ta ut pengar");
                        withdrawMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;

                    case "4":
                        Console.WriteLine("4. Logga ut");
                        //MainLogin(array, array2);
                        loggedIn = false;
                        continue;
                        break;

                    default:
                        Console.WriteLine("Gör något av ovanstående val");
                        continue;
                }
            }
            
        }

        static void TransferMoney(int[] account1, int[] account2, int currentUser)
        {
            Console.WriteLine("Account1 " + account1[currentUser]);
            Console.ReadKey();
        }

        static void Withdraw() // Kom åt användarens olika konton på något sätt
        {
            Console.WriteLine("Du vill ta ut pengar");
            Console.ReadKey();
        }

    } 
}
