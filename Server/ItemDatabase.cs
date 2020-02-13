using System.Collections.Generic;
using Server.Models;
using Server.Models.Intefaces;

namespace Server
{
    public static class ItemDatabase
    {
        public static Dictionary<string, IItem> Items = new Dictionary<string, IItem>
        {
            {"wood", new Item("Drewno", "wood")},
            {"woodplank", new Item("Drewniana deska", "woodplank")},
            {"rock", new Item("Kamień", "rock")},
            {"metal", new Item("Metal", "metal")},
            {"copper", new Item("Miedź", "copper")},
            {"gold", new Item("Złoto", "gold")}
        };
    }
}