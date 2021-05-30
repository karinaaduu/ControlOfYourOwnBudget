using System;
using System.Collections.Generic;
using IncomeItemLibrary;
using ExpenseItemLibrary;
using ExceptionsLibrary;
using AccountEventArgsLibrary;

namespace AccountLibrary
{
    public class Account
    {
        public delegate void AccountHandler( AccountEventArgs e);
        public event AccountHandler Notify;
        private int _ID;
        private List<ExpenseItem> _ExpenseItems;
        private List<IncomeItem> _IncomeItems;
        public int _NumberExpItems { get; private set; }
        public int _NumberIncItems { get; private set; }
        public Account(int id)
        {
            _ID = id;
            _NumberExpItems = 0;
            _NumberIncItems = 0;
            _ExpenseItems = new List<ExpenseItem>();
            _IncomeItems = new List<IncomeItem>();
        }
        public void AddExpenseItem(ExpenseItem item)  
        {
            if (TotalIncomes() < TotalExpenses() + item.MoneyItemExpense)
            {
                throw new ExpenseEsxceedIncome("Not enough money in the account. Add an income item.");
            }
            _ExpenseItems.Add(item);
            _NumberExpItems++;
            Notify?.Invoke(new AccountEventArgs($"Added expense item on {_ID} account: ", item));
        }
        public void DeleteExpenseItem(int number)  
        {
            if (number+1 > _NumberExpItems || number+1 <= 0)
                throw new InvalidIndex("There is no expense item with this index.");
            else
            {
                Notify?.Invoke(new AccountEventArgs($"Deleted expense item on {_ID} account: ", _ExpenseItems[number]));
                _ExpenseItems.RemoveAt(number);
                _NumberExpItems = _NumberExpItems - 1;
            }
            
        }
        public void AddIncomeItem(IncomeItem item)  
        {
            _IncomeItems.Add(item);
            _NumberIncItems++;
            Notify?.Invoke(new AccountEventArgs($"Added income item on {_ID} account: ", item));
        }
        public void DeleteIncomeItem(int number)  
        {
            if (number + 1 > _NumberIncItems || number + 1 <= 0)
                throw new InvalidIndex("There is no expense item with this index.");
            else 
            {
                if (TotalIncomes() - _IncomeItems[number].MoneyItemIncome > TotalExpenses())
                {
                    Notify?.Invoke(new AccountEventArgs($"Deleted income item on {_ID} account: ", _IncomeItems[number]));
                    _IncomeItems.RemoveAt(number);
                    _NumberIncItems--;
                }
                else
                {
                    throw new ExpenseEsxceedIncome("Expense items esxeed income items.");
                }
            }
        }
        public decimal TotalExpenses()
        {
            decimal sum = 0;
            for (int i = 0; i < _NumberExpItems; i++)
            {
                sum += _ExpenseItems[i].MoneyItemExpense;
            }
            return sum;
        }
        public decimal TotalIncomes()
        {
            decimal sum = 0;
            for (int i = 0; i < _NumberIncItems; i++)
            {
                sum += _IncomeItems[i].MoneyItemIncome;
            }
            return sum;
        }
        public void TransferMoney(Account obj, decimal sum)
        {
            if ((TotalIncomes() - TotalExpenses()) >= sum)
            {
                obj.AddIncomeItem(new IncomeItem(sum, "Money received "));
                AddExpenseItem(new ExpenseItem(sum, "Withdrawn money"));
                Notify?.Invoke(new AccountEventArgs($"Money transfer completed successfully: {sum}"));
            }
            else
            {
                throw new NoEnoughMoney("There is not enough money in the account");
            }
        }
        public (decimal, string) GetExpItems(int index)
        {
            return (_ExpenseItems[index].MoneyItemExpense, _ExpenseItems[index].Name);
        }
        public (decimal, string) GetIncItems(int index)
        {
            return (_IncomeItems[index].MoneyItemIncome, _IncomeItems[index].Name);
        }
    }
}
