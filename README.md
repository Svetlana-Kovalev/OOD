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

>3 It should be possible to add more types of computer players and the game might be configured to choose a particular level, for >example providing an easy, medium and strong computer player.

>4 The system should optimize memory usage. Pick up the one aspect of your design that has the best ratio in terms of memory saving (no >need to produce a fully optimized design).

>5 It should be possible inform each user when it is his/her turn (by email/sms...)

