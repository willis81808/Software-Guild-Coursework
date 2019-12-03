using DoggoManager.Data;
using DoggoManager.Models;
using DoggoManager.View;
using System;
using System.Linq;

namespace DoggoManager.Controllers
{
    static class DoggoController
    {
        public static void CreateDoggo()
        {
            var doggo = DoggoView.Build();
            DoggoRepository.Add(doggo.Result);
            Console.Clear();
            Console.WriteLine("Doggo added.");
        }
        public static void DisplayDoggos()
        {
            Console.Clear();
            var doggos = DoggoRepository.ReadAll();
            Console.WriteLine("Results:");
            doggos.ForEach(d => new DoggoView(d).Display() );
        }
        public static void FindDoggo()
        {
            Console.WriteLine("Find a Doggo by:\n 1) Identifier\n 2) Name");
            switch (Utils.GetNumber("> ", Utils.RangePredicate(1, 2)))
            {
                case 1:
                    int id = Utils.GetNumber("Identifier: ");
                    var doggo = DoggoRepository.Read(id);
                    if (doggo != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Results:");
                        new DoggoView(doggo).Display();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"No Doggo found with the ID: '{id}'");
                    }
                    break;
                case 2:
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    var result = from Doggo item in DoggoRepository.ReadAll()
                                 where item.name == name
                                 select item;
                    if (result.Count() > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Results:");
                        result.ToList().ForEach(d => new DoggoView(d).Display());
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"No Doggo found by the name '{name}'");
                    }
                    break;
            }
        }
        public static void EditDoggo()
        {
            Console.WriteLine("Enter the identifier of the Doggo you'd like to edit:");
            int id = Utils.GetNumber("> ");
            var doggo = DoggoRepository.Read(id);
            if (doggo != null)
            {
                var view = new DoggoView(doggo);
                DoggoRepository.Update(id, view.Edit());
            }
            Console.Clear();
            Console.WriteLine("Doggo updated.");
        }
        public static void RemoveDoggo()
        {
            Console.WriteLine("Enter the identifier of the Doggo you'd like to remove:");
            int id = Utils.GetNumber("> ");
            var doggo = DoggoRepository.Read(id);
            if (doggo != null)
            {
                new DoggoView(doggo).Display();
                Console.WriteLine("Are you sure you'd like to delete this Doggo?");
                if (Utils.GetNumber("[1 = Yes, 2 = No]: ", Utils.RangePredicate(1, 2)) == 1)
                {
                    DoggoRepository.Delete(id);
                    Console.Clear();
                    Console.WriteLine("Doggo removed.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No changes made.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No Doggo found with the ID: '{id}'");
            }
        }
    }
}
