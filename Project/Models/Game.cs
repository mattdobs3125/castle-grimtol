using System;
using System.Linq;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        
        bool playing = false;
        public void Setup()
        {
            playing = true;
            // rooms
            Room CourtYard = new Room("CourtYard", @"You start in a court yard on a bench theres a cold mist lingering to the east theres a door with coat of arms over the door,
 another door to west has a large blue and silver banner with a knights helmet over it    ");
            Room Barracks = new Room("Barracks", "you enter a room with large suits of armor on stands and the wall line with swords being held. You notice a set of stairs heading south down to smelly door");
            Room GuardRoom = new Room("Guard Room", "theres stuff");
            Room CaptainsQuarters = new Room("Guard Room", "theres stuff");
            Room MageTower = new Room("Mage Tower", "You enter the room in the middle of the room there a large blue glowing orb way to big for you to place in your inventory so dont try. There's a large spiral staircase to the north of you that winds to a door maybe check up there for the green key ");
            Room ThroneRoom = new Room("Guard Room", "theres stuff");
            Room Dungeon = new Room("Dungeon", "Your in a Dungeon and its wet and cold");
            Room CouncilRoom = new Room("Coucil Room", "You enter a room with a very large table with many scrolls and papers on it with a large war map scoring the back wall. You realise the a large glimmering key on the table   ");

            //The relationships
            CourtYard.Exits.Add("east", Barracks);
            CourtYard.Exits.Add("north", ThroneRoom);
            CourtYard.Exits.Add("south", CaptainsQuarters);
            CourtYard.Exits.Add("west", MageTower);
            MageTower.Exits.Add("east", CourtYard);
            MageTower.Exits.Add("north", CouncilRoom);
            Barracks.Exits.Add("west", CourtYard);
            Barracks.Exits.Add("south", Dungeon);

            //items
            Item greenkey = new Item("greenkey", "This key smells in ways one cant stand");
            CouncilRoom.Items.Add(greenkey);
            Item bluekey = new Item("bluekey", "This key glows blue like that door in the ");



            //start rooms
            CurrentRoom = CourtYard;

            Console.Clear();
            Console.WriteLine("Welcome to castle grimtol the console game");
            Console.WriteLine("For help during the game just enter 'help me' \r\nfor a run down on how the game works or try\r\n entering 'help rooms' to see which directions you can go in");
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            Console.WriteLine($"Hello {name}");
             Player p = new Player(name);
             CurrentPlayer = p;
        }
        public Game(string name)
        {
            

        }







        public Room CurrentRoom { get; set ; }
        public Player CurrentPlayer { get ; set ; }
        public string[] Answer { get; set; }
        public string Choice { get; set; }
        public string Option { get; set; }


        public void GetUserInput()
        {
            CurrentRoom.GetRoomDescripition();
            Console.WriteLine("where would you like to go? : ");
            Answer= Console.ReadLine().Split();
             Choice = Answer[0].ToLower();
            Option = Answer[1];
            switch(Choice)
            {
                case "go":Go(Option);
                break;
                case "help":Help(Option);
                break;
                case "take": TakeItem(Option);
                break;
                case"reset": Reset(Option);
                break;
                case "show":Inventory(Option);
                break;
                case"quit":Quit(Option);
                break;
                default:
                Console.WriteLine("cool... although thats not a command!!");
                break;
            }
            
            
        }

        public void Go(string direction)
        {
            Console.Clear();
            Room nextRoom = CurrentRoom.Advance(direction);

            if (!CurrentRoom.Exits.ContainsKey(direction)){
               Console.WriteLine("Thats not a vaild direction");
               Console.ReadLine();
            return;
            }else if(nextRoom.Name == "Dungeon") { enterDungeon(); }
            else{
                
            }
      CurrentRoom = nextRoom;
        }
        public void enterDungeon()
        {
            if(CurrentPlayer.Inventory.Count <1){
                Console.WriteLine("As you entered you hear a click. You run to the door although its to late your stuck");
                GameOver();
            }

        }

        private void GameOver()
        {
            Console.WriteLine("You have gotten locked in the dungeon and died. Would you like to play again? Enter yes if so or no if your done playing");
            string input = Console.ReadLine();
            if (input == "yes")
            {
                Reset("Game");
                StartGame();
            }
            Quit("game");

        }
        public void TakeItem(string itemName)
        {
            Item key = CurrentRoom.Items.Find(i => i.Name == itemName);
            if (key == null) Console.WriteLine("cant pick that up");
            else {
                CurrentPlayer.Inventory.Add(key);
                CurrentRoom.Items.Remove(key);
                Console.WriteLine("You got the key");
                return;
            }

        }
            public void Help(string option)
        {
            if (option == "rooms"){
                
            foreach (var exits in CurrentRoom.Exits)
            {
                    Console.WriteLine($"You could go {exits.Key}");
            }
            }if(option == "me"){
                Console.WriteLine("How the game works");
                Console.WriteLine("Enter go followed by the direction. Most rooms descriptions give a clue on the ways you can go ");

            }
            
        }

        public void Inventory(string i)
        {
            if (i =="inventory")
            {
                if (CurrentPlayer.Inventory.Count == 0)Console.WriteLine("You bag is empty");
                else{
                    
                foreach (var l in CurrentPlayer.Inventory)
                {
                    Console.WriteLine( "you have a !!");
                    Console.WriteLine(l.Name);
                    Console.WriteLine(l.Description);
                }
                }
            }





        }

        public void Look()
        {
            
        }

        public void Quit(string i)
        {
            if (i =="game")
            {
                playing = false;
            }

        }

        public void Reset(string i)
        {
                if(i == "game")
                {
                    
                CurrentPlayer.Inventory.Clear();
            StartGame();
                }  
        }

        public void StartGame()
        {
            Setup();
            while(playing)
            {
                GetUserInput();
            }

        }


        public void UseItem(string itemName)
        {
            throw new System.NotImplementedException();
        }
    }
}