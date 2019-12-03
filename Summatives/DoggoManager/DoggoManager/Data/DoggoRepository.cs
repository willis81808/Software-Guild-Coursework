using DoggoManager.Models;
using System;
using System.Collections.Generic;

namespace DoggoManager.Data
{
    static class DoggoRepository
    {
        private static List<Doggo> doggos = new List<Doggo>();
        private static Dictionary<int, int> doggoIdMap = new Dictionary<int, int>();

        public static Doggo Add(Doggo doggo)
        {
            if (doggoIdMap.ContainsKey(doggo.id))
            {
                Console.WriteLine($"The ID '{doggo.id}' is already belongs to '{Read(doggo.id).name}'!");
                return null;
            }
            doggos.Add(doggo);
            doggoIdMap.Add(doggo.id, doggos.Count - 1);
            return doggo;
        }
        public static List<Doggo> ReadAll()
        {
            return doggos;
        }
        public static Doggo Read(int id, bool enableError = true)
        {
            if (doggoIdMap.ContainsKey(id))
            {
                return doggos[doggoIdMap[id]];
            }
            else if (enableError)
            {
                Console.WriteLine($"Invalid Doggo ID '{id}' given!");
            }
            return null;
        }
        public static void Update(int id, Doggo doggo)
        {
            if (doggoIdMap.ContainsKey(id))
            {
                Delete(id);
                Add(doggo);
            }
            else
            {
                Console.WriteLine($"Invalid Doggo ID '{id}' given!");
            }
        }
        public static void Delete(int id)
        {
            if (doggoIdMap.ContainsKey(id))
            {
                doggos.RemoveAt(doggoIdMap[id]);
                // rebuild id mappings
                doggoIdMap.Clear();
                for (int i = 0; i < doggos.Count; i++)
                {
                    doggoIdMap.Add(doggos[i].id, i);
                }
            }
            else
            {
                Console.WriteLine($"Invalid Doggo ID '{id}' given!");
            }
        }
    }
}
