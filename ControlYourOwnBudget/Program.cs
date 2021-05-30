using System;
using System.Collections.Generic;
using AccountLibrary;
using IncomeItemLibrary;
using ExpenseItemLibrary;
using ExceptionsLibrary;
using AccountEventArgsLibrary;

namespace ControlOfYourOwnBudget
{
    class Program
    {
        static void Main(string[] args)
        {
            int caseSwitch,id;
            string answer;
            List<Account> accounts = new List<Account>();   
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-----------OWN BUDGET AND FINANCE MANAGEMENT-----------");
                Console.WriteLine("\n1. Transfer money from one account to another.\n2. Create an account.\n3. Add/delete an expense item.");
                Console.WriteLine("4. Add/delete income item.\n5. How much money was received/spent on the account/item ?\n6. Close an account.\n0. End the program.");
                Console.WriteLine("---------------------\nSelect number: ");
                Console.ResetColor();

                caseSwitch = Convert.ToInt32(Console.ReadLine());
                switch (caseSwitch)
                {

                    case 1: //transfer money
                        {
                            try
                            {
                                int id1, id2;
                                Console.WriteLine($"You have {accounts.Count} accounts.");
                                Console.WriteLine("From which account do you want to transfer money?");
                                id1 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("What account should the money be deposited into?");
                                id2 = Convert.ToInt32(Console.ReadLine());
                                
                                if (id1 > (accounts.Count) || (id1 <= 0) || (id2 > (accounts.Count)) || (id2 <= 0))
                                    throw new InvalidAccountID("One or two account IDs are valid.");
                                
                                Console.WriteLine("How much do you want to transfer? ");
                                decimal sum = Convert.ToDecimal(Console.ReadLine());
                                accounts[id1 - 1].TransferMoney(accounts[id2 - 1], sum);
                            }
                            catch(InvalidAccountID ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch(NoEnoughMoney ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (Exception ex)
                            {
                                ExceptionMessage(ex);
                            }
                            break;
                        }
                    case 2: //create an account
                        {
                            try
                            {
                                Console.WriteLine("Do you want to create default account ?(yes/no)");
                                answer = Console.ReadLine();
                                if (answer == "yes")
                                {
                                    Console.WriteLine("Input an ID of your Account: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    if (id != (accounts.Count + 1))
                                        throw new InvalidAccountID("You can`t create an account with this ID.");
                                  
                                    accounts.Add(new Account(id));
                                    accounts[id - 1].Notify += DisplayMessage;
                                    accounts[id - 1].AddIncomeItem(new IncomeItem(30000, "Work"));
                                    accounts[id - 1].AddExpenseItem(new ExpenseItem(5000, "Food"));
                                    accounts[id - 1].AddExpenseItem(new ExpenseItem(3000, "Public Service"));
                                    accounts[id - 1].AddExpenseItem(new ExpenseItem(2000, "Transport"));
                                    AccountInformation(id, accounts);
                                }
                                else if (answer == "no")
                                {
                                    Console.WriteLine("Input an ID of your Account: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    
                                    if (id != (accounts.Count + 1))
                                        throw new InvalidAccountID("You can`t create an account with this ID.");                                  
                                    accounts.Add(new Account(id));
                                    accounts[id - 1].Notify += DisplayMessage;
                                    AccountInformation(id, accounts);
                                }
                                else
                                {
                                    throw new InvalidArgument("You can only write yes or no.");
                                }                            
                            }
                            catch(InvalidArgument ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch(InvalidAccountID ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (Exception ex)
                            {
                                ExceptionMessage(ex);
                            }
                            break;
                        }
                    case 3:  //add/delete an expense item.
                        {
                            try
                            {
                                Console.WriteLine("Choose an ID of account: ");
                                id = Convert.ToInt32(Console.ReadLine());

                                if (id > (accounts.Count) || (id <= 0))
                                    throw new InvalidAccountID("There is no account with this ID.");

                                Console.WriteLine("Do you want delete or add an expense item ?(add/delete)");
                                answer = Console.ReadLine();
                                if (answer == "add")
                                {
                                    Console.WriteLine("Write the name of a new item: ");
                                    string name = Console.ReadLine();
                                    Console.WriteLine("Write the amount of expenses for this item: ");
                                    decimal sum = Convert.ToDecimal(Console.ReadLine());
                                    accounts[id - 1].AddExpenseItem(new ExpenseItem(sum, name));
                                   
                                }
                                else if (answer == "delete")
                                {
                                    Console.WriteLine("Write the index of item which you want to delete: ");
                                    int Del_Index = Convert.ToInt32(Console.ReadLine());
                                    accounts[id - 1].DeleteExpenseItem(Del_Index - 1);
                                   
                                }
                                else
                                {
                                    throw new InvalidArgument("You can only write add or delete.");
                                }
                            }
                            catch (ExpenseEsxceedIncome ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (InvalidArgument ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (InvalidAccountID ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (InvalidIndex ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (Exception ex)
                            {
                                ExceptionMessage(ex);
                            }
                            break;
                        }
                    case 4: // Add/delete income item
                        {
                            try
                            {
                                Console.WriteLine("Choose an ID of account: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                
                                if (id > (accounts.Count) || (id <= 0))
                                    throw new InvalidAccountID("There is no account with this ID.");

                                Console.WriteLine("Do you want delete or add an income item ?");
                                answer = Console.ReadLine();
                                if (answer == "add")
                                {
                                    Console.WriteLine("Write the name of a new item: ");
                                    string name = Console.ReadLine();
                                    Console.WriteLine("Write the income: ");
                                    decimal sum = Convert.ToDecimal(Console.ReadLine());
                                    accounts[id - 1].AddIncomeItem(new IncomeItem(sum, name));
                                 
                                }
                                else if (answer == "delete")
                                {
                                    Console.WriteLine("Write the index of item which you want to delete: ");
                                    int Del_Index = Convert.ToInt32(Console.ReadLine());                               
                                    accounts[id - 1].DeleteIncomeItem(Del_Index - 1);
                                    
                                }
                                else 
                                {
                                    throw new InvalidArgument("You can only write add or delete.");
                                }
                            }
                            catch (InvalidArgument ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (InvalidAccountID ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (InvalidIndex ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch(ExpenseEsxceedIncome ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (Exception ex)
                            {
                                ExceptionMessage(ex);
                            }
                            break;
                        }
                    case 5:   //money was received and spent on the account
                        {
                            try
                            {
                                Console.WriteLine("Choose an ID of account: ");
                                id = Convert.ToInt32(Console.ReadLine());

                                if (id > (accounts.Count) || (id <= 0))
                                    throw new InvalidAccountID("There is no account with this ID.");

                                AccountInformation(id, accounts);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"TOTAL INCOMES: {accounts[id - 1].TotalIncomes()}");
                                Console.WriteLine($"TOTAL EXPENSES: {accounts[id - 1].TotalExpenses()}");
                                Console.ResetColor(); 
                            }
                            catch (InvalidAccountID ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (Exception ex)
                            {
                                ExceptionMessage(ex);
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                Console.WriteLine("Choose an ID of account: ");
                                id = Convert.ToInt32(Console.ReadLine());

                                if (id > (accounts.Count) || (id <= 0))
                                    throw new InvalidAccountID("There is no account with this ID.");

                                accounts.RemoveAt(id-1);
                            }
                            catch (InvalidAccountID ex)
                            {
                                ExceptionMessage(ex);
                            }
                            catch (Exception ex)
                            {
                                ExceptionMessage(ex);
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Default case. Try again.");
                            break;
                        }
                }
            } while (caseSwitch != 0);
            Console.ReadLine();
        }
        static void OutPutExpenseItems(int id, List<Account> accounts)
        {
            Console.WriteLine("Expense Items: ");
            for (int i = 0; i < accounts[id - 1]._NumberExpItems; i++)
            {
                Console.WriteLine(accounts[id - 1].GetExpItems(i));
            }
        }
        static void OutPutIncomeItems(int id, List<Account> accounts)
        {
            Console.WriteLine("Income Items: ");
            for (int i = 0; i < accounts[id - 1]._NumberIncItems; i++)
            {
                Console.WriteLine(accounts[id - 1].GetIncItems(i));
            }
        }
        static void AccountInformation(int id, List<Account> accounts)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n-------------------------\n\tAccount {id}");
            OutPutExpenseItems(id, accounts);
            OutPutIncomeItems(id, accounts);
            Console.WriteLine("-------------------------");
            Console.ResetColor();
        }
        static void ExceptionMessage(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        static void DisplayMessage(AccountEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (e.Name!=null && e.Money!=0)
            {
                Console.WriteLine($"{e.Message} ({e.Name}, {e.Money})");
                Console.ResetColor(); 
            } 
            else
            {
                Console.WriteLine($"{e.Message})");
                Console.ResetColor();
            }
        }
    }
}
