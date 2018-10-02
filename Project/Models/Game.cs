using System;
using System.Linq;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set ; }
        public Player CurrentPlayer { get ; set ; }
        public string[] Answer { get; set; }
        public string Choice { get; set; }
        public string Option { get; set; }
        public string Input { get; set; }
        
        bool playing = false;
        public void Setup()
        {
            playing = true;
            // rooms
            Room CourtYard = new Room("CourtYard", @"You in a court yard on a bench theres a cold mist lingering to the east theres a door with coat of arms over the door,
 another door to west has a large blue and silver banner with a knights helmet over it ");
            Room Barracks = new Room("Barracks", "you enter a room with large suits of armor on stands and the wall line with swords being held. You notice a set of stairs heading south down to smelly door");

            Room Foyer = new Room("Foyer", "You enter a large foyer with a Door with two key holes one blue the other green maybe you need keys two keys to escape the castle");
            Room MageTower = new Room("Mage Tower", "You enter the room in the middle of the room there a large blue glowing orb way to big for you to place in your inventory so dont try. There's a large spiral staircase to the north of you that winds to a door maybe check up there for the green key ");
            Room Treasury = new Room("Treasury", "You enter a room with a mountain of gold coins and jewels. No key though you only really care about keys");
            Room Dungeon = new Room("Dungeon", "You enter the room and hear the door click good thing you have that stinky green key!  ");
            Room CouncilRoom = new Room("Coucil Room", "You enter a room with a very large table with many scrolls and papers on it with a large war map scoring the back wall. You realise the a large glimmering key on the table   ");

            //The relationships
            CourtYard.Exits.Add("east", Barracks);
            CourtYard.Exits.Add("north", Treasury);
            CourtYard.Exits.Add("south", Foyer);
            Foyer.Exits.Add("north", CourtYard);
            Dungeon.Exits.Add("north", Barracks);
            CourtYard.Exits.Add("west", MageTower);
            MageTower.Exits.Add("east", CourtYard);
            MageTower.Exits.Add("north", CouncilRoom);
            CouncilRoom.Exits.Add("south", MageTower);
            Barracks.Exits.Add("west", CourtYard);
            Barracks.Exits.Add("south", Dungeon);

            //items
            Item greenkey = new Item("greenkey", "This key smells in ways one cant stand");
            CouncilRoom.Items.Add(greenkey);
            Item bluekey = new Item("bluekey", "This key glows blue like that door in the ");
            Dungeon.Items.Add(bluekey);


            //start room
            CurrentRoom = CourtYard;


            Console.WriteLine("Welcome to castle grimtol the console game");
            Console.WriteLine("For help during the game just enter 'help me' \r\nfor a run down on how the game works or try\r\nentering 'help rooms' to see which directions you can go in");
            Console.WriteLine("What name would you like to use");
            var name = Console.ReadLine();
            Console.WriteLine($"Hello {name}");
             Player p = new Player(name);
             CurrentPlayer = p;
        }










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
            } 
            else if(nextRoom.Name == "Dungeon")  EnterDungeon(); 
            else if (nextRoom.Name == "Foyer") EnterFoyer();
            CurrentRoom = nextRoom;
        }


            



        public void EnterFoyer()
        {
            if(CurrentPlayer.Inventory.Count ==2)
            {
                Console.WriteLine("You enter the Foyer and the power of the keys pulls you threw the door and out of the castle!!!");
                GameWin();

            }
            
        }

        private void GameWin()
        {
            Console.WriteLine("You have escaped the castle with your life!! Good job!!!");
            Console.WriteLine("Would you like to play again?? Enter yes to play again enter no to close the game");
            Input = Console.ReadLine();
            if (Input== "yes")
            {
                Reset("game");
                StartGame();
                
            }
            Quit("game");

        }

        private void GameOver()
        {
            Console.WriteLine("You have gotten locked in a room and died. Would you like to play again? Enter yes if so or no if your done playing");
            Input = Console.ReadLine();
            if (Input == "yes")
            {
                Reset("game");
                StartGame();
            }
            Quit("game");

        }
        public void TakeItem(string itemName)
        {
            Item key = CurrentRoom.Items.Find(i => i.Name == itemName);
            if (key == null) Console.WriteLine("Cant pick that up. To pick up a key type take then the color with key like 'take yellowkey' ");
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
                Console.WriteLine("To see your inventory enter 'show inventory'");
                Console.WriteLine("To take a key you need to enter take follow by the color and the word key with no space like 'take purplekey'");
                Console.WriteLine("To reset the game enter 'reset game'");
                Console.WriteLine("To quit the game enter 'quite game'");

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

        public void EnterDungeon()
        {
            if(CurrentPlayer.Inventory.Count == 0)
            {
                Console.WriteLine("As you entered you hear a click. You run to the door although its to late your stuck");
                GameOver();

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