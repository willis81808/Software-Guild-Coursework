using DoggoManager.Data;
using DoggoManager.Models;
using System;

namespace DoggoManager.View
{
    class DoggoView
    {
        public Doggo Result { get; }

        public DoggoView(Doggo doggo, bool copyToBuffer = true)
        {
            if (copyToBuffer)
            {
                Result = doggo.Copy();
            }
            else
            {
                Result = doggo;
            }
        }

        public void Display()
        {
            Console.WriteLine($"(ID: {Result.id}) '{Result.name}' - Age {Result.age} - {Result.personality.ToString()} personality - A {Result.score}/10 good dog");
        }
        public Doggo Edit()
        {
            bool editing = true;
            do
            {
                Display();
                Console.WriteLine("Select an attribute to edit:");
                Console.WriteLine(" 1) Name\n 2) Age\n 3) Score\n 4) Personality\n 5) Identifier\n 6) Accept Changes\n");
                switch (Utils.GetNumber("> ", Utils.RangePredicate(1, 6)))
                {
                    case 1:
                        Console.WriteLine($"Current Name: {Result.name}");
                        Console.Write("New Name: ");
                        Result.name = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine($"Current Age: {Result.age}");
                        Result.age = Utils.GetNumber("New Age: ");
                        break;
                    case 3:
                        Console.WriteLine($"Current Score: {Result.score}/10");
                        Result.score = Utils.GetNumber("> ", "They're good dogs, Brent.", num => { return num > 10; });
                        break;
                    case 4:
                        Console.WriteLine($"Current Personality: {Result.personality.ToString()}");
                        Console.WriteLine("Select a new personality:");
                        Console.WriteLine(" 1) CONFIDENT\n 2) TIMID\n 3) INDEPENDENT\n 4) HAPPY\n 5) ADAPTABLE");
                        Result.personality = (Personality)(Utils.GetNumber("> ", Utils.RangePredicate(1, 5)) - 1);
                        break;
                    case 5:
                        Console.WriteLine($"Current Identifier: {Result.id}");
                        Result.id = Utils.GetNumber("> ", "Invalid ID", num => num == Result.id || DoggoRepository.Read(num, false) == null);
                        break;
                    case 6:
                        editing = false;
                        break;
                }
            }
            while (editing);
            return Result;
        }

        public static DoggoView Build()
        {
            Console.WriteLine("Please supply the following information for this new Doggo...");

            int id = Utils.GetNumber("Identifier: ", "Invalid ID", num => DoggoRepository.Read(num, false) == null);
            Console.Write("Name: ");
            string name = Console.ReadLine();
            int age = Utils.GetNumber("Age: ");
            int score = Utils.GetNumber("Score X/10: ", "They're good dogs, Brent.", num => { return num > 10; });
            Console.WriteLine("Personality:\n 1) CONFIDENT\n 2) TIMID\n 3) INDEPENDENT\n 4) HAPPY\n 5) ADAPTABLE");
            Personality personality = (Personality) (Utils.GetNumber("> ", Utils.RangePredicate(1, 5)) - 1);

            DoggoView view = new DoggoView(new Doggo(id, age, score, name, personality), false);

            Console.WriteLine("Is this correct?");
            view.Display();
            
            while (Utils.GetNumber("[1 = Yes, 2 = No]: ", Utils.RangePredicate(1, 2)) == 2)
            {
                view.Edit();
                Console.WriteLine("Is this correct?");
                view.Display();
            }

            return view;
        }
    }
}
