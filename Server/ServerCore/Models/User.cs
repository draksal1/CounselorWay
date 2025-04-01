using System;
using System.Collections.Generic;
using ServerCore.Models.Enums;

namespace ServerCore.Models
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }
        public Characteristics Characteristics { get; } = new Characteristics();
        public Dictionary<Guid, List<Guid>> CompletedMapChallenges { get; set; } = new();
        public Dictionary<Guid, bool> PersonalChallenges { get; set; } = new();

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
