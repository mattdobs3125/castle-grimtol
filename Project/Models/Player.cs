using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
       public string Name { get; set; }
       public List<Item> Inventory { get; set; }
        public Player (string name)
        {
            Name = name;
            Inventory = new List<Item>();


        }
    }
}