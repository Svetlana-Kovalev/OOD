# OOD
**Exercise 3**

**Design**

>1	Players information (name, email etc) might be retrieved from some external component such as a social network

To support different players and types of players defined interfaces hierarchy:
base interface IPlayer
```c#
    public interface IPlayer
    {
        string Name { get; }
    }
```
and more specific IHumanPlayer and 
```c#
    public interface IHumanPlayer : IPlayer
    {
        string Email { get; }
    }
    public interface IComputerPlayer : IPlayer
    {
        
    }    
```
The interfaces contain relevant information only and relevant classes can be created by relevant factory. Different factory can retrieve player's information from external sources (different UI, social networks, etc.). The approach allows to add new possibilities without chnages of existing classes, but by implementation new classes ([Open/Closed Principle](https://en.wikipedia.org/wiki/Open/closed_principle)). So, when it's necessary to take player's information from new social netwotk, will be implemented new factory, that will take the information and create existing player class by the data.

>2 It may be required to display the board UI in various representations

Created interface and class for board engine, which contains all necessary data of board and present basic methods for board's management:
```c#
    public interface IBoard
    {
        IPlayer this[int row, int col] { get; set; }       

        int Rows { get; }
        int Columns { get; }
        
        void Clear();
        void PlaceDisk(IPlayer player, int column);

        BoardStatus Status { get; }
        bool IsColumnFull(int columnIndex);
        bool BoardIsFull();
        int FirstEmptyRow(int columnIndex);
        bool IsWin(int rowIndex, int columnIndex, IPlayer player);
    }
```
Also created interface for board's presentation:
```c#
    public interface IBoardViewer
    {
        void DisplayBoard();
    }
```
Implemented class 
```public class ConsoleBoardViewer : IBoardViewer```
which implements console board presentation. So, to change presentation it's enough to implement relevant class (from IBoardViewer).
The approach satisfies SOLID principles: [Open/Closed principle] (https://en.wikipedia.org/wiki/Open/closed_principle), [Single Responsibility principle](https://en.wikipedia.org/wiki/Single_responsibility_principle).

>3 It should be possible to add more types of computer players and the game might be configured to choose a particular level, for example providing an easy, medium and strong computer player.

Player classes (Human and Computer) don't contain implementation of strategy. Declared separated interface IStrategy
```c#
    public interface IStrategy
    {
        IGame Game { get; }
        IPlayer Player { get; }
        void MakeNextStep();
    }
```
and different implementations:
```c# public class HumanConsoleStrategy : IStrategy``` and ```c# public class ComputerStandardStrategy : IStrategy```
Current implemented class ComputerStandardStrategy contains "computer strategy" from provided sample. Now, easy can be implemented any other strategies: each new strategy in new "strategy class" (```c# : IStrategy ```).
At the beginning of the game will be created (by user's choose or configuration) strategy class with relevant level.
The approach satisfies SOLID principles: [Open/Closed principle] (https://en.wikipedia.org/wiki/Open/closed_principle), [Single Responsibility principle](https://en.wikipedia.org/wiki/Single_responsibility_principle).

>4 The system should optimize memory usage. Pick up the one aspect of your design that has the best ratio in terms of memory saving (no need to produce a fully optimized design).

Current implementation of class Board contains array[][] which stores status of each board's cell:
```c#
    public class Board : IBoard
    {
        private readonly IPlayer[][] m_board;
   }
```
To optimize memory usage it's possible store value of cell and count of the cell by next way:
```c#
    public interface ICell
    {
       IPlayer Player;
       int Counter;
    }
    public interface IBoardColumn
    {
        List<ICell> CellList { get; }
    }
    public class Board : IBoard
    {
        private readonly List<IBoardColumn> m_board;
   }
```
It allows to reduce memory usage, but increases the time of operations with board.

>5 It should be possible inform each user when it is his/her turn (by email/sms...)

For different notifications and transform changes from "game engine" declared interfaces
```c#
    public interface INotificationEvent
    {
        
    }
    public interface INotificationService
    {
        void RaiseEvent(INotificationEvent notificationEvent);
    }
```
and classes
```c#
    public class ChangedUserEvent : INotificationEvent
    {
        public IPlayer Player { get; set; }
        public ChangedUserEvent(IPlayer player)
        {
            Player = player;
        }
    }

    public class NotificationService : INotificationService
    {
        ...

        public NotificationService(...)
        {
           ...
        }

        #region INotificationService
        public void RaiseEvent(INotificationEvent notificationEvent)
        {
            ChangedUserEvent userEvent = notificationEvent as ChangedUserEvent;
            if (userEvent != null)
            {
                ...
            }
        }
        #endregion
    }
```

Game engine (class GameContainer) with help of INotificationService informs about changing a player's turn:
```c#
        private void ChangePlayer()
        {
            m_activeStrategy = m_activeStrategy == m_strategyPlayer1 ? m_strategyPlayer2 : m_strategyPlayer1;
            OnChanged(new ChangedUserEvent(m_activeStrategy.Player));
        }
        protected virtual void OnChanged(INotificationEvent notificationEvent)
        {
            if (m_notificationService != null)
            {
                m_notificationService.RaiseEvent(notificationEvent);
            }
        }
```
Class NotificationService implements relevant action (sending email or SMS) for the notification.

**Diagram of interfaces**
![Diagram of interfaces](https://github.com/Svetlana-Kovalev/OOD/blob/master/Problem3/Pictures/Dependencies%20Graph.png)

**Diagram of data types**
![Diagram of data types](https://github.com/Svetlana-Kovalev/OOD/blob/master/Problem3/Pictures/DataTypes%20Dependencies%20Graph.png)

**Unit testing**

Unit tests implemented in separated project **FourInLineTests** with help of several "test" frameworks:
* [NUnit, "unit-testing framework for all .Net languages. Initially ported from JUnit"] (http://nunit.org/)
* [Moq,  "The most popular and friendly mocking framework for .NET"] (https://github.com/Moq/moq4)

Moq allows to create required environment for unit testing without implementation "mock" classes or configure real classes.
In several cases, added possibility to define settings of real classes, which uses by tests only. For example, added additional constructor of class Board **public Board(int rows, int cols)**, which allows to create "small" actual board and use it for tests.

Test coverage report:

![Test coverage report](https://github.com/Svetlana-Kovalev/OOD/blob/master/Problem3/Pictures/TestCoverageReport.png)

**Aspects**

Dynamic proxy implemented in class **public class Wrapper<T> : DynamicObject**. 
The class with help of framework ImpromptuInterface create dynamic proxy.
Method **TryInvokeMember** catches calling of method **NextStep()** and stores to logger duration of the method.

```c#
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                bool nextStepMethod = binder.Name.ToLower() == "nextstep";

                Stopwatch stopWatch = new Stopwatch();
                if (nextStepMethod)
                {
                    stopWatch.Start();
                }

                //call _wrappedObject object
                result = m_wrappedObject.GetType().GetMethod(binder.Name).Invoke(m_wrappedObject, args);

                if (nextStepMethod)
                {
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    m_logger.Info("elapsed time: player={0}, duration = {1} ms", m_gameGameContainer.GetLastStep().Player.Name, ts.TotalMilliseconds);
                }
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
```

I use [Postsharp](https://www.postsharp.net/) framework to implement aspect, which produces warning when the total current turn is over 30 seconds (a value easily configurable). The value can be easy defined in atribute:
```c#
        [TotalCurrentTurn(30)]
        public void MakeNextStep()
        {
            m_gameConsole.Write("Player {0}, choose a column: ", m_player.Name);
            int column = Int32.Parse(m_gameConsole.ReadLine()); // no exception handling...
            Game.PlaceDisk(m_player, column);
        }
```

Attribute TotalCurrentTurn starts Task in **OnEntry** method and cancels it in **OnExit**.
If task not cancelled during defined time, it will output warning.
```c#
    [Serializable]
    public sealed class TotalCurrentTurnAttribute : OnMethodBoundaryAspect
    {
        const int S1 = 1000;
        private readonly int _maxSeconds;
        [NonSerialized]
        private CancellationTokenSource _cancelationTokenSource;
        
        ...

        // Constructor specifying the tracing category, invoked at build time.
        public TotalCurrentTurnAttribute(int maxSeconds)
        {
            _maxSeconds = maxSeconds;
        }

        // Invoked at runtime before that target method is invoked.
        public override void OnEntry(MethodExecutionArgs args)
        {
            _cancelationTokenSource = new CancellationTokenSource();

            var token = _cancelationTokenSource.Token;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(_maxSeconds*S1);
            }, token).ContinueWith(t =>
            {
                token.ThrowIfCancellationRequested();
                Console.WriteLine("warning: current turn is over {0} seconds", _maxSeconds);
            }, token);
        }

        // Invoked at runtime after the target method is invoked (in a finally block).
        public override void OnExit(MethodExecutionArgs args)
        {
            _cancelationTokenSource.Cancel();       
        }
    }
```
