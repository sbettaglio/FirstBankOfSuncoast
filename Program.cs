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

      //accounts.Add(new Account { Name = "checking", Balance = 500 });
      //accounts.Add(new Account { Name = "saving", Balance = 500 });
      //Save(accounts);
      // var reader = new StreamReader("account.csv");
      // var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      // Accounts = csvReader.GetRecords<Account>().ToList();
      tracker.LoadAccounts();
      //display balances
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
          if (name != "checking" && name != "saving")
          {
            Console.WriteLine("that account doesn't exist. Please type (CHECKING) or (SAVING) ");
            name = Console.ReadLine().ToLower();
          }
          Console.WriteLine("How much do you want to deposit?");
          var amount = int.Parse(Console.ReadLine());
          tracker.Deposit(name, amount);
          Console.WriteLine("********************************************************");

        }
        else if (input == "w")
        {
          Console.WriteLine("********************************************************");

          Console.WriteLine("What account do you want to withdraw from?");
          var withdrawName = Console.ReadLine().ToLower();
          if (withdrawName != "checking" && withdrawName != "saving")
          {
            Console.WriteLine("that account doesn't exist. Please type (CHECKING) or (SAVING) ");
            withdrawName = Console.ReadLine().ToLower();
          }
          Console.WriteLine("How much do you want to withdraw?");
          var withdrawAmount = int.Parse(Console.ReadLine());
          tracker.Withdraw(withdrawName, withdrawAmount);
          Console.WriteLine("********************************************************");

        }
        else if (input == "t")
        {
          Console.WriteLine("********************************************************");

          Console.WriteLine("What account do you want to transfer from?");
          var from = Console.ReadLine().ToLower();
          if (from != "checking" && from != "saving")
          {
            Console.WriteLine("that account doesn't exist. Please type (CHECKING) or (SAVING) ");
            from = Console.ReadLine().ToLower();
          }
          Console.WriteLine("How much do you want to transfer?");
          var tAmount = int.Parse(Console.ReadLine());
          tracker.Transfer(from, tAmount);
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

