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

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();


    //Methods

    public void LoadTransactions()
    {
      var reader = new StreamReader("transactions.csv");
      var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      Transactions = csvReader.GetRecords<Transaction>().ToList();
    }
    public void SaveTransactions()
    {
      var writer = new StreamWriter("transactions.csv");
      var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
      csvWriter.WriteRecords(Transactions);
      writer.Flush();
    }
    public void AddNewTransaction(string type, string whatAccount, int whatAmount)
    {
      var transaction = new Transaction
      {
        Type = type,
        WhatAccount = whatAccount,
        Amount = whatAmount,
        When = DateTime.Now

      };
      Transactions.Add(transaction);
      SaveTransactions();
    }
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


    public void NewAccount(string userName, int checkingBalance, int savingBalance)
    {
      var checkingAccount = new Account
      {
        UserName = userName,
        Name = "checking",
        Balance = checkingBalance
      };
      Accounts.Add(checkingAccount);
      var savingAccount = new Account
      {
        UserName = userName,
        Name = "saving",
        Balance = savingBalance
      };
      Accounts.Add(savingAccount);
      Save();
    }
    public void NameChecker(string userName)
    {
      var exist = Accounts.Any(account => account.UserName == userName);
      if (exist == true)
      {
        Console.WriteLine("That username already exists. Please type in a different one");
        userName = Console.ReadLine().ToLower();
      }
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






