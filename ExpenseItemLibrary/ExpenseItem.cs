using System;

namespace ExpenseItemLibrary
{
    public class ExpenseItem
    {
        public decimal MoneyItemExpense { get; private set; }
        public string Name { get; private set; }
        public ExpenseItem()
        {
            MoneyItemExpense = 0;
            Name = null;
        }
        public ExpenseItem(decimal sum, string name)
        {
            MoneyItemExpense = sum;
            Name = name;
        }
    }
}
