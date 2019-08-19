using System;
using System.Collections.Generic;
using System.Text;
using RHEntities;

namespace RevatureHousingAPITestProject
{
    public class LocationDummyData
    {

        //list of Provider
        public List<Location> LocationsList;
        //consturcter
        public LocationDummyData()
        {
            LocationsList = new List<Location>();
            RandomNumbers r = new RandomNumbers();
            for (int i = 0; i < 9; i++)
            {
                Location location = new Location()
                {
                    ProviderID = (i * 6).ToString(),
                    Address = String_Arr[r.GenerateRandomId()],
                    City = String_Arr[r.GenerateRandomId()],
                    State = String_Arr[r.GenerateRandomId()],
                    ZipCode = String_Arr[r.GenerateRandomId()],
                    TrainingCenter = (i*7).ToString(),
                    LocationID = i * 5

                };
                LocationsList.Add(location);

            }



        }

        string[] String_Arr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        bool[] Bool_Arr = new bool[] { true, false };



    }
}
