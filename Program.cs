using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
  class Program
  {






    static void Main(string[] args)
    {


      var tracker = new AccountManager();


      // var reader = new StreamReader("account.csv");
      // var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      // Accounts = csvReader.GetRecords<Account>().ToList();
      //tracker.LoadTransactions();
      //display balances
      // accounts.Add(new Account { Name = "checking", Balance = 500 });
      // accounts.Add(new Account { Name = "saving", Balance = 500 });


      var empty = true;
      while (empty)
      {
        Console.WriteLine("Welcome to the First Bank of Suncoast.");
        Console.WriteLine("Do you have an existing account with us? Yes(Y) or no(n)?");

        var createNew = Console.ReadLine().ToLower();
        if (createNew == "y")
        {
          Console.WriteLine("(CHECKING) or (SAVING)?");
          var name = Console.ReadLine().ToLower();
          if (name != "checking" && name != "saving")
          {
            Console.WriteLine("Firs Bank of Suncoast doesn't offer that type of account.");
            Console.WriteLine("Please select (CHECKING) or (SAVING)");
            name = Console.ReadLine().ToLower();
          }
          else
          {
            Console.WriteLine("How much?");
            var balance = int.Parse(Console.ReadLine());
            tracker.NewAccount(name, balance);
          }
        }
        else
        {
          Console.WriteLine("Looks like you have an existing account");
          empty = false;
        }
      }

      tracker.LoadAccounts();
      tracker.Display();

      var moneyInTheBank = true;
      while (moneyInTheBank)
      {
        tracker.LoadAccounts();
        //What do you want to do with your Money?
        Console.WriteLine("Do you want to deposit(D), withdraw(W) or transfer(T)");
        var input = Console.ReadLine().ToLower();
        if (input != "d" && input != "w" && input != "t" && input != "quit")
        {
          Console.WriteLine("Incorrect input. Please input (D), (W) or (T) ");
          input = Console.ReadLine().ToLower();
        }
        else if (input == "d")
        {
          Console.WriteLine("********************************************************");

          Console.WriteLine("What account do you want to deposit into?");
          var name = Console.ReadLine().ToLower();
          //var deposit = "deposit";
          if (name != "checking" && name != "saving")
          {
            Console.WriteLine("that account doesn't exist. Please type (CHECKING) or (SAVING) ");
            name = Console.ReadLine().ToLower();
          }
          Console.WriteLine("How much do you want to deposit?");
          var amount = int.Parse(Console.ReadLine());
          //tracker.AddNewTransaction(deposit, name, amount);
          tracker.Deposit(name, amount);
          Console.WriteLine("********************************************************");

        }
        else if (input == "w")
        {
          Console.WriteLine("********************************************************");

          Console.WriteLine("What account do you want to withdraw from?");
          //var withdraw = "withdraw";
          var withdrawName = Console.ReadLine().ToLower();
          if (withdrawName != "checking" && withdrawName != "saving")
          {
            Console.WriteLine("that account doesn't exist. Please type (CHECKING) or (SAVING) ");
            withdrawName = Console.ReadLine().ToLower();
          }
          Console.WriteLine("How much do you want to withdraw?");
          var withdrawAmount = int.Parse(Console.ReadLine());
          tracker.Withdraw(withdrawName, withdrawAmount);
          //tracker.AddNewTransaction(withdraw, withdrawName, withdrawAmount);
          Console.WriteLine("********************************************************");

        }
        else if (input == "t")
        {
          Console.WriteLine("********************************************************");

          Console.WriteLine("What account do you want to transfer from?");
          var from = Console.ReadLine().ToLower();
          //var transaction = "transaction";
          if (from != "checking" && from != "saving")
          {
            Console.WriteLine("that account doesn't exist. Please type (CHECKING) or (SAVING) ");
            from = Console.ReadLine().ToLower();
          }
          Console.WriteLine("How much do you want to transfer?");
          var tAmount = int.Parse(Console.ReadLine());
          tracker.Transfer(from, tAmount);
          //tracker.AddNewTransaction(transaction, from, tAmount);
          Console.WriteLine("********************************************************");

        }
        else if (input == "quit")
        {
          moneyInTheBank = false;
          tracker.Save();
        }
      }
    }
  }
}






