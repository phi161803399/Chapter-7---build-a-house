using System;

namespace Chapter_7___build_a_house
{
    class Opponent
    {
        private Location myLocation;
        private Random random;

        public Opponent(Location startingLocation)
        {
            myLocation = startingLocation;
            random = new Random();
        }

        public void Move()
        {
            bool hidden = false;
            while (!hidden)
            {
                if (myLocation is IHasExteriorDoor)
                {
                    if (random.Next(2) == 1)
                    {
                        IHasExteriorDoor location = myLocation as IHasExteriorDoor;
                        myLocation = location.DoorLocation;
                    }
                }
                int randomExitNumber = random.Next(myLocation.Exits.Length);
                myLocation = myLocation.Exits[random.Next(randomExitNumber)];
                if (myLocation is IHidingPlace)
                {
                    hidden = true;
                }
            }
        }

        public bool Check(Location locationToCheck)
        {
            if (myLocation == locationToCheck)
                return true;
            return false;
        }
    }
}
