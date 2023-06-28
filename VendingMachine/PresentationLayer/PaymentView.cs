using System;
using System.Net.NetworkInformation;
using System.Threading.Channels;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories;
using iQuest.VendingMachine.Repositories.Interfaces;


namespace iQuest.VendingMachine.PresentationLayer
{
    public class PaymentView:DisplayBase, IPaymentView
    {
        IEntityFrameworkRepository entityFramework = new EntityFrameworkRepository(new ApplicationDbContext());

        public string AskForPaymentMethod()
        {
            Display("How would you like to pay? ", ConsoleColor.White);
            Display("(cash / card)\n", ConsoleColor.DarkCyan);

            string read = Console.ReadLine();

            switch (read)
            {
                case "cash":
                    DisplayLine("\nYou selected cash.", ConsoleColor.Cyan);
                    return read;

                case "card":
                    DisplayLine("\nYou selected card.", ConsoleColor.Cyan);
                    return read;

                default:
                    DisplayLine("\nInvalid type!\n", ConsoleColor.Red);
                    return AskForPaymentMethod();
            }
        }

        public void PayWithCash(int id, double price, string name)
        {
            Display("\nThe price is ", ConsoleColor.White);
            Display($"{price}$", ConsoleColor.DarkGreen);
            DisplayLine(". Enter your money.", ConsoleColor.White);

            bool enteredMoneyType = double.TryParse(Console.ReadLine(), out double enteredMoney);

            if (enteredMoney == 0)
            {
                throw new CancelationException();
            }

            if (enteredMoneyType)
            {
                if (enteredMoney > 1000)
                {
                    throw new TooBigMoneyException();
                }

                Display($"\nThe price for ", ConsoleColor.White);
                Display($"{name}", ConsoleColor.Red);
                Display(" is ", ConsoleColor.White);
                Display($"{price}$.\n", ConsoleColor.DarkGreen);

                double sum = enteredMoney;

                while (sum < price)
                {
                    Display($"\nYou introduced {sum}$. You have to introduce ", ConsoleColor.White);
                    Display($"{price - sum}$ ", ConsoleColor.Red);
                    Display("more.\n", ConsoleColor.White);

                    bool worked = double.TryParse(Console.ReadLine(), out double amount);

                    if (worked)
                    {
                        if (amount > 1000)
                        {
                            throw new TooBigMoneyException();
                        }

                        sum += amount;
                    }

                    else
                    {
                        throw new InvalidTypeException();
                    }
                }

                ChangeForPayingWithCash(sum - price);
            }

            else
            {
                DisplayLine("NOT accepted type of money entered!", ConsoleColor.Red);
                PayWithCash(id, price, name);
                return;
            }
        }

        public void PrintYourCard()
        {
            DisplayLine("\nYour debit card details: ", ConsoleColor.White);

            DisplayLine("================================", ConsoleColor.Blue);
            Display("|", ConsoleColor.Blue);
            Display("  Alpha International Bank   ", ConsoleColor.Yellow);
            DisplayLine(" |", ConsoleColor.Blue);
            DisplayLine("|                              |", ConsoleColor.Blue);
            Display("|", ConsoleColor.Blue);
            Display(" XXXXXXXXXX", ConsoleColor.Red);
            DisplayLine("                   |", ConsoleColor.Blue);
            Display("|", ConsoleColor.Blue);
            Display(" Mare Maldini", ConsoleColor.Red);
            DisplayLine("                 |", ConsoleColor.Blue);
            Display("|", ConsoleColor.Blue);
            Display(" 1998", ConsoleColor.Red);
            DisplayLine("                         |", ConsoleColor.Blue);
            Display("|", ConsoleColor.Blue);
            DisplayLine("                              |", ConsoleColor.Blue);
            DisplayLine("================================", ConsoleColor.Blue);
        }

        public void PayWithCard(int id)
        {
            string surName = "Mare";
            string firstName = "Maldini";
            string pin = "1998";

            DisplayLine("\nEnter your debit card details.", ConsoleColor.Green);
            Display("\nSurname: ", ConsoleColor.White);
            string surNameRead = Console.ReadLine();
            Display("First name: ", ConsoleColor.White);
            string firstNameRead = Console.ReadLine();
            Display("Debit card number: ", ConsoleColor.White);
            string debitCardNumber = Console.ReadLine();
            Display("PIN: ", ConsoleColor.White);
            string pinRead = Console.ReadLine();

            if (CheckIfDebitCardIsValid(debitCardNumber) && surName==surNameRead && firstName==firstNameRead && pin==pinRead)
            {
                DisplayLine($"\nPayment successfully made!", ConsoleColor.Green); // 378282246310005 - valid card number
                return;
            }

            else
            {
                DisplayLine("\nNot valid card details! Enter Your details again!\n", ConsoleColor.Red);
                PayWithCard(id);
                return;
            }
        }

        public void ChangeForPayingWithCash(double change)
        {
            Display("\nYour change is ", ConsoleColor.White);
            Display($"{change}$", ConsoleColor.Green);
            Display(".\n", ConsoleColor.White);
        }

        public bool CheckIfDebitCardIsValid(string num) // Luhn alogithm
        {
            int sum = 0, d;
            int a = 0;

            if(!Int32.TryParse(num, out sum))
            {
                throw new InvalidTypeException();
            }

            for (int i = num.Length - 2; i >= 0; i--)
            {
                d = Convert.ToInt32(num.Substring(i, 1));
                if (a % 2 == 0)
                    d = d * 2;
                if (d > 9)
                    d -= 9;
                sum += d;
                a++;
            }

            if ((10 - (sum % 10) == Convert.ToInt32(num.Substring(num.Length - 1))))
                return true;
            else
                return false;
        }
    }
}