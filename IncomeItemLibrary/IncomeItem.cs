using System;

namespace IncomeItemLibrary
{
    public class IncomeItem
    {
        public decimal MoneyItemIncome { get; private set; }
        public string Name { get; private set; }
        public IncomeItem()
        {
            MoneyItemIncome = 0;
            Name = null;
        }
        public IncomeItem(decimal sum, string name)
        {
            MoneyItemIncome = sum;
            Name = name;
        }
    }
}
