using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, Room> Exits { get ; set; }

        public Room ( string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, Room>();
        }
        public void GetRoomDescripition()
        {
            System.Console.WriteLine($"{Description}");
            return;
        }
        public Room Advance(string direction)
{
            if (this.Exits.ContainsKey(direction))
{
                return (Room)this.Exits[direction];
}
            return null;
}

        public void PrintChosenDirection()
        {
            foreach(var option in Exits)
            {
                System.Console.WriteLine(option.Key);
            }
            
        }




    }
}