using System;
using System.Collections.Generic;
using System.Text;
using RHEntities;

namespace RevatureHousingAPITestProject
{
    public class RandomNumbers
    {
        //Generates Random Integer to create unique Random Ids
        //Note: rand.Next() returns a unique id everytime
        public Random rand;
        public RandomNumbers()
        {
            rand = new Random();
        }
        public int GenerateRandomId()
        {

            return rand.Next(9);
        }
    }

    public class RoomDummyData
    {

        //list of Provider
        public List<Room> RoomsList;
        //consturcter
        public RoomDummyData()
        {
            RoomsList = new List<Room>();
            RandomNumbers r = new RandomNumbers();
            for (int i = 0; i < 9; i++)
            {
                bool ActiveBool = true;
                if ((i % 3) == 0)
                    ActiveBool = false;
                Room room = new Room()
                {
                    RoomID = i * 5,
                    Type = String_Arr[r.GenerateRandomId()],
                    MaxOccupancy = r.GenerateRandomId(),
                    RoomNumber = r.GenerateRandomId().ToString() + r.GenerateRandomId(),
                    Gender = Gender_Arr[r.GenerateRandomId() % 3],
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(r.GenerateRandomId()),
                    CurrentOccupancy = r.GenerateRandomId(),
                    IsActive = ActiveBool,
                    Description = String_Arr[r.GenerateRandomId()],
                    Location = null,
                    LocationID = i * 6

                };
                RoomsList.Add(room);

            }



        }

        string[] String_Arr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        bool[] Bool_Arr = new bool[] { true, false };
        string[] Gender_Arr = new string[] { "Male", "Female", "Alternative" };


    }
}
