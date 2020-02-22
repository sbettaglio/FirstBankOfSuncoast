using System;
namespace FirstBankOfSuncoast
{
  public class Transaction
  {
    public string Type { get; set; }
    public string WhatAccount { get; set; }
    public int Amount { get; set; }
    public DateTime When { get; set; }
  }
}