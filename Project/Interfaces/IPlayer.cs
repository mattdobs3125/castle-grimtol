 using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public interface IPlayer
  {
    string Name { get; set; }
    List<Item> Inventory { get; set; }
  }
}