using System;
using System.Collections.Generic;
using ConsoleApplication1.Enums;
using Microsoft.Build.Framework;

namespace ConsoleApplication1
{
    public class Ninja
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ServedInOniwaban { get; set; }
        public Clan Clan { get; set; }
        public int ClainId { get; set; }
        public List<NinjaEquipment> EquipmentOwned { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class NinjaEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        [Required]
        public Ninja Ninja { get; set; }
    }

    public class Clan
    {
        public int Id { get; set; }
        public string ClainName { get; set; }
        public List<Ninja> Ninjas { get; set; }
    }
}