using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
  public class AccountManager
  {
    public List<Account> Accounts { get; set; } = new List<Account>();


    //Methods
    public void LoadAccounts()
    {
      var reader = new StreamReader("account.csv");
      var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      Accounts = csvReader.GetRecords<Account>().ToList();
    }
    public void Save()
    {
      var writer = new StreamWriter("account.csv");
      var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
      csvWriter.WriteRecords(Accounts);
      writer.Flush();
    }

    public void Display()
    {
      foreach (var b in Accounts)
      {
        Console.WriteLine($"{b.Name} has a balance of ${b.Balance} ");
      }
    }
    public void Deposit(string name, int amount)
    {
      var deposit = Accounts.First(account => account.Name == name).Balance;
      deposit += amount;
      Accounts.First(account => account.Name == name).Balance = deposit;
      Console.WriteLine($"Your {name} account has a new balance of {deposit}");
      Save();
    }
    public void Withdraw(string withdrawName, int witdrawAmount)
    {
      var withdraw = Accounts.First(account => account.Name == withdrawName).Balance;
      withdraw -= witdrawAmount;
      Accounts.First(account => account.Name == withdrawName).Balance = withdraw;
      Console.WriteLine($"Your {withdrawName} has a new balance of {withdraw}");
      Save();
    }
    public void Transfer(string from, int tAmount)
    {

      if (from == "checking")
      {
        var takerName = Accounts.First(account => account.Name == from);
        var takerBalance = Accounts.First(account => account.Name == from).Balance;
        var giverName = Accounts.First(to => to.Name == "saving");
        var giverBalance = Accounts.First(to => to.Name == "saving").Balance;
        takerBalance += tAmount;
        Accounts.First(account => account.Name == from).Balance = takerBalance;
        giverBalance -= tAmount;
        Accounts.First(to => to.Name == "saving").Balance = giverBalance;
        Console.WriteLine($"You transferred {tAmount} from {from} to saving");
        Console.WriteLine($"Your new {from} balance is {takerBalance}");
        Console.WriteLine($"Your new saving balance is {giverBalance}");
        Save();
      }
      else if (from == "saving")
      {
        var takerName = Accounts.First(account => account.Name == from);
        var takerBalance = Accounts.First(account => account.Name == from).Balance;
        var giverName = Accounts.First(to => to.Name == "checking");
        var giverBalance = Accounts.First(to => to.Name == "checking").Balance;
        takerBalance += tAmount;
        Accounts.First(account => account.Name == from).Balance = takerBalance;
        giverBalance -= tAmount;
        Accounts.First(to => to.Name == "checking").Balance = giverBalance;
        Console.WriteLine($"You transferred {tAmount} from {from} to saving");
        Console.WriteLine($"Your new {from} balance is {takerBalance}");
        Console.WriteLine($"Your new checking balance is {giverBalance}");
        Save();
      }


    }

  }
}





