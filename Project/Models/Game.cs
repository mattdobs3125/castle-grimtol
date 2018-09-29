using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        Player player;
        bool playing = false;
        public void Setup()
        {
            playing = true;
            // rooms
            Room CourtYard = new Room("CourtYard", "Your wake up in a court yard on a bench ");
            Room Barracks = new Room("Barracks", "the lots of armor on the wall");
            Room GuardRoom = new Room("Guard Room", "theres stuff");
            Room CaptainsQuarters = new Room("Guard Room", "theres stuff");
            Room SquireTower = new Room("Guard Room", "theres stuff");
            Room ThroneRoom = new Room("Guard Room", "theres stuff");



            //The relationships
            CourtYard.Exits.Add("east", Barracks);
            CourtYard.Exits.Add("north", ThroneRoom);
            CourtYard.Exits.Add("south", CaptainsQuarters);
            CourtYard.Exits.Add("west", GuardRoom);
            //start rooms
            CurrentRoom = CourtYard;

            Console.Clear();
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            player = new Player(name);
        }


                   








        
        public Room CurrentRoom { get; set ; }
        public Player CurrentPlayer { get ; set ; }

        public void GetUserInput()
        {
            CurrentRoom.GetRoomDescripition();
            Console.WriteLine("where would you like to go? : ");
            string answer = Console.ReadLine();
            answer = answer.ToLower();
            switch(answer)
            {
                case "go":
                Go(answer);
                 break;   
                
            }
            
            
        }

        public void Go(string direction)
        {
            Console.Clear();
            Room newRoom = CurrentRoom.Advance(direction);


            if (!CurrentRoom.Exits.ContainsKey(direction)){
               System.Console.WriteLine("Thats not a vaild direction");
               Console.ReadLine();
            return;
           }
      CurrentRoom = newRoom;
        }
            

        public void Help()
        {
            throw new System.NotImplementedException();
        }

        public void Inventory()
        {
            throw new System.NotImplementedException();
        }

        public void Look()
        {
            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
            Setup();
            while(playing)
            {
                GetUserInput();
            }

        }

        public void TakeItem(string itemName)
        {
            throw new System.NotImplementedException();
        }

        public void UseItem(string itemName)
        {
            throw new System.NotImplementedException();
        }
    }
}