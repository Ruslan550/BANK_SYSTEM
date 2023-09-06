using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;

public class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }

    public Transaction(decimal amount)
    {
        Amount = amount;
        Timestamp = DateTime.Now;
    }
}

internal class Card
{
    public string? BANKNAME { get; set; }
    public string? FULLNAME;
    public string? PIN;
    public string? PAN;
    public string? Date;
    public decimal Balance;
    public string? CVC;

    public List<Transaction> Transactions { get; set; }



    public string PANsystem
    {
        get { return PAN; }

        set
        {
            if (value.Length == 16)
            {
                value = PAN;
            }
            else
            {
                throw new Exception("длина панорамы должна составлять 16 символов!");
            }
        }
    }



    public string PINsystem
    {
        get { return PIN; }

        set
        {
            if (value.Length == 4)
                value = PIN;
            else
            {
                throw new Exception("\nПин должен быть длиной 4 слова!!");
            }

        }
    }
    public string DateSystem
    {
        get { return Date; }
        set
        {

            DateTime parsedDate;
            if (DateTime.TryParse(value, out parsedDate))
            {
                Date = value;
            }

        }
    }
    public decimal BALANCEsystem
    {
        get { return Balance; }
        set
        {
            if (!(value < 0))
                value = Balance;
            else
            {
                throw new Exception("\nувеличить баланс!\n!");
            }

        }

    }
    public string CVCsystem
    {
        get { return CVC; }
        set
        {
            string CVCToSt = value.ToString();

            if (CVCToSt.Length == 3 && CVCToSt.All(char.IsDigit))
                CVC = value;

            else
                throw new Exception(" \nНЕВЕРНЫЙ CVC-код карты!");

        }
    }



    public string FULLNAMEsystem
    {
        get { return FULLNAME; }
        set
        {
            if (value.All(char.IsLetter) && value.Length < 24)
            {
                value = FULLNAME;
            }
            else
            {
                throw new Exception("Имя неверно !!");
            }

        }
    }
    public bool ValidatePAN(string pan)
    {
        if (pan.Length == 16 && pan.All(char.IsDigit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
        }
        else
        {
            throw new Exception("Нет суммы на балансе.");
        }
    }
    public decimal GetBalance()
    {
        return Balance;
    }

    public void GeneratorPAN()
    {
        Random random = new Random();
        string pan = " ";

        for (int i = 0; i < 16; i++)
        {
            int digit = random.Next(0, 30);
            pan += digit;

            PAN = pan;
        }
        if (ValidatePAN(pan))
        {
            PAN = pan;
        }
        else
        {
            Console.WriteLine("НЕдействительный номер пан !!");
        }
    }

    public void GenerateBalance()
    {
        Random rand = new Random();
        decimal balance = (decimal)rand.Next(0, 1000000000);

    }
    public void GenerateRandomCardInfo()
    {
        GeneratorPAN();
        GenerateBalance();

    }

    public void Deposit(decimal amount)
    {
        Transactions.Add(new Transaction(amount));
    }


    public void Withdraw_(decimal amount)
    {
        Transactions.Add(new Transaction(-amount));
    }


    public List<Transaction> GetTransactionsWithinLastDays(int days)
    {
        DateTime startDate = DateTime.Now.AddDays(-days);
        return Transactions.Where(t => t.Timestamp >= startDate).ToList();
    }



    public override string ToString()
    {
        return $"Card Information: \n" +
               $" BAnk Name:{BANKNAME}\n" +
               $"FullName: {FULLNAMEsystem}\n" +
               $"PAN: {PANsystem}\n" +
               $"PIN: {PINsystem}\n" +
               $"CVC: {CVCsystem}\n" +
               $"Date:{DateSystem}\n" +
               $"Balence:{BALANCEsystem}\n";
    }

    public Card()
    {
        Transactions = new List<Transaction>();
    }
    public Card(string pan, string pin, string date, decimal balance)
    {
        PANsystem = pan;
        PINsystem = pin;
        BALANCEsystem = balance;
        DateSystem = date;

    }

}

class User
{
    private Guid id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int Age { get; set; }

    private Card? CreditCard;

    public string NAMEsystem
    {
        get { return Name; }
        set
        {
            if (value.All(char.IsLetter))
            {
                Name = value;
            }
            else
            {
                throw new Exception("Неверное имя !");
            }
        }
    }



    public string SurnameSystem
    {
        get { return Surname; }
        set
        {
            if (value.All(char.IsLetter))
                value = Surname;
            else
                throw new Exception(" Неверная фамилия!!");
        }
    }
    private bool CredycardIScheck(Card card)
    {
        if (card == null)
        {
            return false;
        }

        if (string.IsNullOrEmpty(card.PANsystem) || card.PAN.Length != 16)
        {
            return false;
        }

        if (string.IsNullOrEmpty(card.CVC) || card.CVC.Length != 3)
        {
            return false;
        }

        if (!DateTime.TryParse(card.Date, out DateTime date))
        {
            return false;
        }

        if (date <= DateTime.Now)
        {
            return false;
        }

        return true;
    }

    public Card CReditCardSystem
    {
        get { return CreditCard; }

        set
        {

            if (CredycardIScheck(value))

                CreditCard = value;
            else throw new Exception("НЕдействительная карта!!!");
        }

    }





}
internal class BAnk
{
    private List<User>? _Clients { get; set; }

    public BAnk()
    {
        _Clients = new List<User>();
    }

    public void AddUserCardInfo(Card card, User user)
    {
        user.CReditCardSystem = card;

    }
    public void AddUser(User user)
    {
        _Clients.Add(user);
    }

    public void ShowCardBalance(User user)
    {
        if (user.CReditCardSystem != null)
        {
            Console.WriteLine($"Пользователь: {user.Name} {user.Surname}");
            Console.WriteLine(user.CReditCardSystem.ToString());
        }
        else
        {
            Console.WriteLine($"Пользователь: {user.Name} {user.Surname} НЕдействительная карта .");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        BAnk bank = new BAnk();


        User user1 = new User { Name = "HESENAGA", Surname = "HESENLI" };
        Card card1 = new Card("1234567890123456", "1234", "12/25", 1000);

        User user2 = new User { Name = "HESEN", Surname = "HESENLI" };
        Card card2 = new Card("9876543210987654", "5678", "10/24", 2000);

        bank.AddUser(user1);
        bank.AddUserCardInfo(card1, user1);

        bank.AddUser(user2);
        bank.AddUserCardInfo(card2, user2);

        Console.WriteLine("Enter the Pin :");

        bool validInput = false;
        Card? selectedCard = null;
        int days = 10;

        while (!validInput)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.W)
            {
                selectedCard = card1;
                validInput = true;
            }
            else if (key.Key == ConsoleKey.S)
            {
                selectedCard = card2;
                validInput = true;
            }
        }

        if (selectedCard != null)
        {
            Console.WriteLine($"{user1.Name} {user1.Surname}Добро пожаловать. Пожалуйста, выберите один из следующих:");
            Console.WriteLine("1. Баланс Шоу");
            Console.WriteLine("2. Информация о карте ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Баланс: {selectedCard.BALANCEsystem}");
                    break;
                case 2:
                    Console.WriteLine(selectedCard.ToString());
                    break;
                case 3:
                    Console.Write("Укажите сумму, которую хотите снять: ");
                    decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                    try
                    {
                        selectedCard.Withdraw(withdrawAmount);
                        Console.WriteLine("Ваши деньги были выведены с помощью.");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    return;

                case 5:
                    if (selectedCard != null)
                    {
                        List<Transaction> transactionsWithinLastDays = selectedCard.GetTransactionsWithinLastDays(days);

                        Console.WriteLine($"Транзакции {days} за последние:");
                        foreach (Transaction transaction in transactionsWithinLastDays)
                        {
                            Console.WriteLine($"История: {transaction.Timestamp}, Количество: {transaction.Amount}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор карты Неверный выбор карты!");
                    }
                    break;

                case 6:
                    return;


                case 7:
                    Console.WriteLine("Виберіть картку, на яку хочете переказати гроші:");
                    Console.WriteLine("1.картка");
                    Console.WriteLine("2. картка");

                    int? fromCardChoice = int.Parse(Console.ReadLine());

                    Card fromCard = null;
                    if (fromCardChoice == 1)
                    {
                        fromCard = card1;
                    }
                    else if (fromCardChoice == 2)
                    {
                        fromCard = card2;
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор карты!");
                        break;
                    }

                    Console.WriteLine("Введите PIN-код:");
                    string enteredPIN = Console.ReadLine();

                    if (enteredPIN == fromCard.PINsystem)
                    {

                        Console.WriteLine("На какую карту вы хотите перевести деньги??");
                        Console.WriteLine("1.Карта ");
                        Console.WriteLine("2. Карта");

                        int toCardChoice = int.Parse(Console.ReadLine());

                        Card toCard = null;
                        if (toCardChoice == 1)
                        {
                            toCard = card1;
                        }
                        else if (toCardChoice == 2)
                        {
                            toCard = card2;
                        }
                        else
                        {
                            Console.WriteLine("Недійсний вибір картки!");
                            break;
                        }

                        Console.WriteLine("едоставить информацию о переводе денег.");
                        string transferInfo = Console.ReadLine();

                        Console.Write("Введите сумму, которую хотите перевести:\n ");
                        decimal transferAmount = decimal.Parse(Console.ReadLine());

                        try
                        {
                            fromCard.Withdraw(transferAmount);
                            toCard.Deposit(transferAmount);
                            Console.WriteLine("Денежный перевод прошел успешно.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Карта с этим ПИН-кодом не найдена.");
                    }
                    break;



            }

        }
        else
        {
            Console.WriteLine("Неверный PIN-код!");
        }

    }


}