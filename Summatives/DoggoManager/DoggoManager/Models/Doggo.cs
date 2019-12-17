using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoggoManager.Models
{
    public enum Personality { CONFIDENT, TIMID, INDEPENDENT, HAPPY, ADAPTABLE }
    
    class Doggo
    {
        public int id;
        public int age;
        public int score;
        public string name;
        public Personality personality;

        public Doggo(int id, int age, int score, string name, Personality personality)
        {
            this.id = id;
            this.age = age;
            this.score = score;
            this.name = name;
            this.personality = personality;
        }

        public Doggo Copy()
        {
            return new Doggo(id, age, score, name, personality);
        }
    }
}
