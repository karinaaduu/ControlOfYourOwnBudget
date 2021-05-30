using System;

namespace ExceptionsLibrary
{
        public class InvalidAccountID : Exception
        {
            public InvalidAccountID(string message) : base(message)
            { }
        }
        public class NoEnoughMoney : Exception
        {
            public NoEnoughMoney(string message) : base(message)
            { }
        }
        public class ExpenseEsxceedIncome : Exception
        {
            public ExpenseEsxceedIncome(string message) : base(message)
            { }
        }
        public class InvalidArgument : Exception
        {
            public InvalidArgument(string message) : base(message)
            { }
        }
       public class InvalidIndex : Exception
       {
        public InvalidIndex(string message) : base(message)
            { }
       }
}
