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

Player classed (Human and Computer) don't contain implementation of strategy. Declared separated interface IStrategy
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
At the beginning of the game will be created (by uer's choose or configuration) strategy class with relevant level.
The approach satisfies SOLID principles: [Open/Closed principle] (https://en.wikipedia.org/wiki/Open/closed_principle), [Single Responsibility principle](https://en.wikipedia.org/wiki/Single_responsibility_principle).

>4 The system should optimize memory usage. Pick up the one aspect of your design that has the best ratio in terms of memory saving (no >need to produce a fully optimized design).

**TBD**

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
