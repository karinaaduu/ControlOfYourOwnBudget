using System;
using ExpenseItemLibrary;
using IncomeItemLibrary;

namespace AccountEventArgsLibrary
{
    public class AccountEventArgs
    {
      
        public string Message { get; }
        public decimal Money { get; }
        public string Name { get; }
        public AccountEventArgs(string message, ExpenseItem item)
        {
            Message = message;
            Money=item.MoneyItemExpense;
            Name = item.Name;
        }
        public AccountEventArgs(string message, IncomeItem item)
        {
            Message = message;
            Money = item.MoneyItemIncome;
            Name = item.Name;
        }
        public AccountEventArgs(string message)
        {
            Message = message;
            Name = null;
            Money = 0;
        }
    }
}
