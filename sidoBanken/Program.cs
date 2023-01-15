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

            // Hårdkodat in användare och lösenord i varsin array
            // Då deras antal konton skiljer sig används en jagged array för att tilldela mängden konton
            // Ytterligare en jagged array används för att tilldela varje konton ett saldo (balances)
            string[] userNames = new string[] { "inky", "blinky", "pinky", "clyde", "pac", "man" };
            string[] passwords = new string[] { "1891", "1981", "6666", "7777", "1234", "0159" };
            string[][] accountNames = new string[][] {
                new string[] { "Sparkonto", "Lönekonto" },
                new string[] { "Sparkonto", "Lönekonto", "ISK" },
                new string[] { "Sparkonto", "Lönekonto", "ISK", "Pensionssparkonto" },
                new string[] { "Sparkonto", "Lönekonto", "ISK", "Pensionssparkonto", "Kapitalförsäkring" },
                new string[] { "Sparkonto", "Lönekonto", "ISK", "Pensionssparkonto", "Kapitalförsäkring", "Aktiedepå" },
                new string[] { "Sparkonto", "Lönekonto", "ISK", "Pensionssparkonto", "Kapitalförsäkring", "Aktiedepå", "P2P-konto" }
            };
            double[][] accountBalances = new double[][] {
             new double[] { 2001, 5000 },
             new double[] { 2002, 2000, 22000 },
             new double[] { 2003, 3987, 39888, 78999,45 },
             new double[] { 2004, 40000, 87332,88, 1000, 5000 },
             new double[] { 2005, 5550, 5000, 1000, 5000, 733 },
             new double[] { 2006, 6786.77, 153689,98, 1000, 65478, 46,58, 6666 } };
            
            Console.WriteLine("Välkommen till banken\nSkapad av Rickard Eriksson");
            MainLogin(userNames, passwords, accountNames, accountBalances);
        }
        
        static string FormatMoney(double amount) // Funktion som används för att endast visa användaren två decimaler.
        {
            return String.Format("{0:.00}", amount);
        }
        static void ShowAccounts(string[] accounts, double[] balances) // Metoden visar den inloggade användarens olika konton och dess saldon genom en array
        {
            Console.Clear();
            for (int index = 0; index < accounts.Length; index++)
                {
                    Console.WriteLine(accounts[index] + ": " + balances[index] + " kr");
            }
            bool inputCheck = true;
            do
            {
                Console.WriteLine("\nTryck [Enter] för att komma till huvudmenyn");
                Console.WriteLine("=====>");
                ConsoleKeyInfo info = Console.ReadKey(); // Kollar om användaren trycker ner ENTER-knappen
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    inputCheck = false;
                    
                }
                else Console.WriteLine("Felaktig inmatning");
            } while (inputCheck == true);
        }


        // Metoden WithdrawMoney listar inloggad användares konto, sen kan man välja konto att ta ut pengar ifrån.
        static void WithdrawMoney(string[] accounts, double[] balances, string password) // ToDo: skicka in inloggad användares pinkod i metoden
        {
            Console.Clear();
            Console.WriteLine($"Välj ett konto mellan 1 och {accounts.Length} ta ut pengar ifrån: ");
            int validAccount = accounts.Length;

            for (int index = 0; index < accounts.Length; index++) // itererar igenom alla konton kopplade till inloggad användare
            {
                Console.WriteLine($"{index + 1} {accounts[index]} {balances[index] } kr.");

            }
            

            Console.Write("===> ");
            string userInput = Console.ReadLine();
            int userChoice = int.Parse(userInput) - 1;
            
            Console.WriteLine($"Ange en summa mellan 1 och {balances[userChoice]}");
            Console.Write("===> ");
            userInput = Console.ReadLine();
            double userAmount = double.Parse(userInput);

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
            else if (userAmount <= 0)
            {
                Console.WriteLine($"Värdet måste vara större än noll (0) kr");
                return;
            }else
            {
                balances[userChoice] -= userAmount;
                Console.WriteLine($"Saldo {FormatMoney(balances[userChoice])} kr");
            }
            bool inputCheck = true;
            do
            {
                Console.WriteLine("\nTryck [Enter] för att komma till huvudmenyn");
                Console.Write("=====>");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)
                {
                    inputCheck = false;
                }
                else Console.WriteLine("Felaktig inmatning");
            } while (inputCheck == true);
            return;
        }


        //DepositMoney är en metod som ger användaren möjlighet att sätta in pengar på något av sina konton.
        static void DepositMoney(string[] accounts, double[] balances, string password)
        {
            Console.Clear();
            Console.WriteLine($"Välj ett konto mellan 1 och {accounts.Length} att sätta in pengar på: ");

            for (int index = 0; index < accounts.Length; index++) // itererar igenom in
            {
                Console.WriteLine($"{index + 1} {accounts[index]} {balances[index]} kr.");
            }
            double maxDeposit = 15000;
            Console.Write("===> ");
            string userInput = Console.ReadLine();
            Console.WriteLine($"Du valde {userInput}");

            int userChoice = int.Parse(userInput) - 1;
            Console.WriteLine($"Ange en summa att sätta in: ");
            Console.Write("===> ");
            userInput = Console.ReadLine();
            double userAmount = double.Parse(userInput);

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
            if (userAmount > 15000)
            {
                Console.WriteLine($"Du får sätta in maximum {maxDeposit} kr.");
            }
            else if (userAmount <= 0)
            {
                Console.WriteLine($"Värdet måste vara större än noll (0) kr.");
                return;
            }
            else
            {
                balances[userChoice] += userAmount;
                Console.WriteLine($"Saldo {FormatMoney(balances[userChoice])} kr.");
            }
            bool inputCheck = true;
            do
            {
                Console.WriteLine("\nTryck [Enter] för att komma till huvudmenyn.");
                Console.Write("=====>");

                //Kod som kollar att du trycker på Enter-knappen
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)
                {
                    inputCheck = false;
                }
                else Console.WriteLine("Felaktig inmatning.");
            } while (inputCheck == true);
            return;
        }


        // En metod som tar två parametrar, innehållandes konto och saldo, ...
        // för att sedan kunna flytta pengar mellan användarens egna konton
        static void TransferMoney(string[] accounts, double[] balances)
        {

            Console.Clear();
            Console.WriteLine($"Välj ett konto mellan 1 och {accounts.Length} ta ut pengar ifrån: ");
            for (int index = 0; index < accounts.Length; index++)
            {
                Console.WriteLine($"{index + 1} {accounts[index]} {balances[index]} kr");
            }
            Console.Write("===> ");
            string amountFrom = Console.ReadLine();
            int userChoiceFrom = int.Parse(amountFrom) - 1;
            Console.WriteLine($"Du valde {amountFrom}");

            
            Console.WriteLine($"Välj ett konto att sätta in pengar till: ");
            for (int index = 0; index < accounts.Length; index++)
            {
                if (index != userChoiceFrom)
                {
                    Console.WriteLine($"{index + 1} {accounts[index]} {balances[index]}");
                }
                
            }
            Console.Write("===> ");
            string userInput = Console.ReadLine();
            Console.WriteLine($"Du valde {userInput}");

            int userChoiceTo = int.Parse(userInput) - 1;
            Console.WriteLine($"Ange en summa mellan 1 och {balances[userChoiceFrom]}");
            
            Console.Write("===> ");
            userInput = Console.ReadLine();
            double userAmount = double.Parse(userInput);
            Console.WriteLine("Formatting test:" + FormatMoney(userAmount));

            if (userAmount > balances[userChoiceFrom])
            {
                Console.WriteLine("Du har inte så mycket pengar");
            }
            else
            {

                balances[userChoiceFrom] -= userAmount; // Tar bort saldo från valt konto
                balances[userChoiceTo] += userAmount; // adderar saldo till valt konto
                Console.WriteLine($"{accounts[userChoiceFrom]} {FormatMoney(balances[userChoiceFrom])}");
                Console.WriteLine($"{accounts[userChoiceTo]} {FormatMoney(balances[userChoiceTo])}");
            }
            Console.WriteLine("Tryck [Enter] för att komma till huvudmenyn");
            Console.ReadKey();

        }

        // Metoden MainLogin tar in all inloggningsinformation och testar dessa med användarens input
        // Skickar sedan vidare vid lyckad inloggning
        static void MainLogin(string[] userArray, string[] passwordArray, string[][] accountArray, double[][] balanceArray)
        {
            bool mainMenu = true;
            int loginAttempts = 3;
           
            while (mainMenu)
            {
                
                Console.Write("Skriv in ditt användarnamn: ");
                string inputFromUser = Console.ReadLine();
                string user = inputFromUser.ToLower();

                Console.Write("Skriv in din pinkod: ");
                string pass = Console.ReadLine();
                int logtest = 0;


                for (int i = 0; i < passwordArray.Length; i++)
                {

                    if (user == userArray[i] && pass == passwordArray[i])
                    {
                        loginAttempts = 3;
                        logtest = 1;

                        LoggedIn(i, userArray, passwordArray, accountArray, balanceArray);
                        Console.Clear();
                        break;
                    }
                }
                if (logtest == 0)
                {
                    loginAttempts--;
                    if (loginAttempts > 0)
                    {
                        Console.WriteLine("Invalid input. Försök igen. Försök kvar innan programmet stängs ner:" + loginAttempts);
                    }
                }

                if (loginAttempts == 0)
                {
                    Console.WriteLine("Dina inloggningsförsök har tagit slut.Programmet stängs ner!");
                    mainMenu = false;
                    System.Environment.Exit(0);
                    Console.ReadLine();
                    Console.WriteLine("Exit check");
                }              
            }
        }

        // Metoden LoggedIn ger efter lyckad inloggning användaren möjlighet att navigera inuti bankprogrammet
         static void LoggedIn(int currentUserIndex, string[] userArray, string[] passwordArray, string[][] accountArray, double[][] balanceArray)

        {
            Console.Clear();
            DateTime aDay = DateTime.Now;

            Console.WriteLine($"Du loggade in som {userArray[currentUserIndex]} || Tid {aDay}");
            bool loggedIn = true;
            while (loggedIn)
            {

                Console.WriteLine("Gör ditt val med knapparna 1-5");
                Console.WriteLine("1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Sätt in pengar\n5. Logga ut");

                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("========================\nSe dina konton och saldo\n========================");
                        ShowAccounts(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;

                    case "2":
                        Console.WriteLine("========================\nÖverföring mellan konton\n========================");
                        TransferMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex]);
                        break;
                    case "3":
                        Console.WriteLine("========================\nTa ut pengar\n========================");
                        WithdrawMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex], passwordArray[currentUserIndex]);
                        break;

                    case "4":
                        Console.WriteLine("========================\nSätt in pengar\n========================");
                        DepositMoney(accountArray[currentUserIndex], balanceArray[currentUserIndex], passwordArray[currentUserIndex]);
                        break;

                    case "5":
                        Console.WriteLine("========================\nLogga ut\n========================");
                        Console.Clear();
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt menyval. Gör val med siffra 1-5");
                        Console.ReadLine();
                        break;
                }
            }
            
        }

    } 
}
