using System;
using System.Collections.Generic;

// Mediator Pattern
public interface IChatMediator
{
    void SendMessage(string message, User sender);
}

public class ChatRoom : IChatMediator
{
    private List<User> users = new List<User>();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in users)
        {
            if (user != sender)
                user.ReceiveMessage(message);
        }
    }
}

public class User
{
    private string name;
    private IChatMediator mediator;

    public User(string name, IChatMediator mediator)
    {
        this.name = name;
        this.mediator = mediator;
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{name}: {message}");
        mediator.SendMessage(message, this);
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"{name} received: {message}");
    }
}

// Observer Pattern
public interface INewsAgency
{
    void RegisterSubscriber(ISubscriber subscriber);
    void UnregisterSubscriber(ISubscriber subscriber);
    void PublishNews(string news);
}

public class NewsAgency : INewsAgency
{
    private List<ISubscriber> subscribers = new List<ISubscriber>();

    public void RegisterSubscriber(ISubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void UnregisterSubscriber(ISubscriber subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void PublishNews(string news)
    {
        foreach (var subscriber in subscribers)
        {
            subscriber.Update(news);
        }
    }
}

public interface ISubscriber
{
    void Update(string news);
}

public class Subscriber : ISubscriber
{
    private string name;

    public Subscriber(string name)
    {
        this.name = name;
    }

    public void Update(string news)
    {
        Console.WriteLine($"{name} received news: {news}");
    }
}

// Strategy Pattern
public interface ITravelStrategy
{
    void Travel();
}

public class CarTravelStrategy : ITravelStrategy
{
    public void Travel()
    {
        Console.WriteLine("Jadę samochodem");
    }
}

public class BikeTravelStrategy : ITravelStrategy
{
    public void Travel()
    {
        Console.WriteLine("Jadę rowerem");
    }
}

public class Traveler
{
    private ITravelStrategy strategy;

    public void SetStrategy(ITravelStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void Travel()
    {
        strategy.Travel();
    }
}

// State Pattern
public interface IVendingMachineState
{
    void InsertCoin(VendingMachine machine);
    void SelectProduct(VendingMachine machine);
    void Dispense(VendingMachine machine);
}

public class NoSelectionState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine)
    {
        Console.WriteLine("Wprowadź monety");
        machine.SetState(new HasMoneyState());
    }

    public void SelectProduct(VendingMachine machine)
    {
        Console.WriteLine("Wybierz produkt");
    }

    public void Dispense(VendingMachine machine)
    {
        Console.WriteLine("Nie można wydać produktu");
    }
}

public class HasMoneyState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine)
    {
        Console.WriteLine("Już wprowadziłeś monety");
    }

    public void SelectProduct(VendingMachine machine)
    {
        Console.WriteLine("Wybrano produkt");
        machine.SetState(new ProductSelectedState());
    }

    public void Dispense(VendingMachine machine)
    {
        Console.WriteLine("Nie można jeszcze wydać produktu");
    }
}

public class ProductSelectedState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine)
    {
        Console.WriteLine("Już wprowadziłeś monety");
    }

    public void SelectProduct(VendingMachine machine)
    {
        Console.WriteLine("Produkt już został wybrany");
    }

    public void Dispense(VendingMachine machine)
    {
        Console.WriteLine("Wydaję produkt");
        machine.SetState(new NoSelectionState());
    }
}

public class VendingMachine
{
    private IVendingMachineState currentState;

    public VendingMachine()
    {
        currentState = new NoSelectionState();
    }

    public void SetState(IVendingMachineState state)
    {
        currentState = state;
    }

    public void InsertCoin()
    {
        currentState.InsertCoin(this);
    }

    public void SelectProduct()
    {
        currentState.SelectProduct(this);
    }

    public void Dispense()
    {
        currentState.Dispense(this);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Mediator Pattern
        var chatRoom = new ChatRoom();
        var user1 = new User("Alice", chatRoom);
        var user2 = new User("Bob", chatRoom);

        chatRoom.AddUser(user1);
        chatRoom.AddUser(user2);

        user1.SendMessage("Hello!");
        user2.SendMessage("Hi Alice!");

        // Observer Pattern
        var agency = new NewsAgency();
        var subscriber1 = new Subscriber("John");
        var subscriber2 = new Subscriber("Jane");

        agency.RegisterSubscriber(subscriber1);
        agency.RegisterSubscriber(subscriber2);

        agency.PublishNews("Breaking News!");

        // Strategy Pattern
        var traveler = new Traveler();

        traveler.SetStrategy(new CarTravelStrategy());
        traveler.Travel(); // Jadę samochodem

        traveler.SetStrategy(new BikeTravelStrategy());
        traveler.Travel(); // Jadę rowerem

        // State Pattern
        var machine = new VendingMachine();

        machine.InsertCoin();
        machine.SelectProduct();
        machine.Dispense();
    }
}

