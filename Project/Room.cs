using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        List<Item> Items {get; set;}
        Dictionary<string,IRoom> Exits{get;set;}
        public Room ( string name, string description)
	    {
            Name = name;
            Description = description;
            Item = new List<Item>();
            Exits = new Dictionary<string, IRoom>();



	    }
    }
}