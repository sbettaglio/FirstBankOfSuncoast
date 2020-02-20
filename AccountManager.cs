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
      var deposit = Accounts.First(name => Accounts.Contains(name)).Balance;
      deposit += amount;
      Console.WriteLine($"Your {name} account has a new balance of {deposit}");
      Save();
    }
    public void Withdraw(string withdrawName, int witdrawAmount)
    {
      var withdraw = Accounts.First(withdrawName => Accounts.Contains(withdrawName)).Balance;
      withdraw -= witdrawAmount;
      Console.WriteLine($"Your {withdrawName} has a new balance of {withdraw}");
      Save();
    }
    public void Transfer(string from, int tAmount)
    {
      var takerBalance = Accounts.First(from => Accounts.Contains(from)).Balance;
      var giverBalance = Accounts.First(from => !Accounts.Contains(from)).Balance;
      var takerName = Accounts.First(from => Accounts.Contains(from));
      var giverName = Accounts.First(from => !Accounts.Contains(from));
      takerBalance += tAmount;
      giverBalance -= tAmount;
      Console.WriteLine($"You transferred {tAmount} from {giverName} to {takerName}");
      Console.WriteLine($"Your Balance in {takerName} is no {takerBalance}");
      Console.WriteLine($"Your Balance in {giverName} is no {takerName}");
      Save();

    }

  }


}
