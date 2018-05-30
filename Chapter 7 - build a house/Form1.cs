using System;
using System.Windows.Forms;

namespace Chapter_7___build_a_house
{
    public partial class Form1 : Form
    {
        private int Moves;

        private Location currentLocation;

        private RoomWithDoor livingRoom;
        private RoomWithHidingPlace diningRoom;
        private RoomWithDoor kitchen;
        private Room stairs;
        private Room hallway;
        private RoomWithHidingPlace bathroom;
        private RoomWithHidingPlace masterBedroom;
        private RoomWithHidingPlace secondBedroom;
        
        private OutsideWithHidingPlace garden;
        private OutsideWithHidingPlace driveway;
        private OutsideWithDoor frontYard;
        private OutsideWithDoor backYard;

        private Opponent opponent;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            opponent = new Opponent(frontYard);
            ResetGame(false);
        }

        private void CreateObjects()
        {
            // RoomWithDoor inherits from RoomWithHidingPlace 
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "inside the closet", "an oak door with a brass knob");
            diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandelier", "in the tall armoire");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "in the cabinet", "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "in the shed");
            frontYard = new OutsideWithDoor("Front Yard", false, "a heavy-looking oak door");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            stairs = new Room("Stairs", "wooden bannister");
            hallway = new RoomWithHidingPlace("Upstairs Hallway", "picture of a dog", "in the closet");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink and a toilet", "in the shower");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed", "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed", "under the bed");
            driveway = new OutsideWithHidingPlace("Driveway", true, "in the garage");
            livingRoom.Exits = new Location[] {diningRoom, stairs};
            stairs.Exits = new Location[] {livingRoom, hallway };
            hallway.Exits = new Location[] {stairs, masterBedroom, secondBedroom, bathroom };
            masterBedroom.Exits = new Location[] { hallway };
            secondBedroom.Exits = new Location[] { hallway };
            bathroom.Exits = new Location[] { hallway };
            diningRoom.Exits = new Location[] {livingRoom, kitchen};
            kitchen.Exits = new Location[] {diningRoom};
            frontYard.Exits = new Location[] {garden, backYard, driveway};
            backYard.Exits = new Location[] { garden, frontYard, driveway };
            garden.Exits = new Location[] { frontYard, backYard };
            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;
            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;

            opponent = new Opponent(frontYard);
        }

        private void MoveToANewLocation(Location newLocation)
        {
            Moves++;
            currentLocation = newLocation;
            RedrawForm();
        }

        private void RedrawForm()
        {
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
            {
                exits.Items.Add(currentLocation.Exits[i].Name);
            }
            exits.SelectedIndex = 0;
            description.Text = $"{currentLocation.Description}\r\n(move # {Moves})";
            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace = currentLocation as IHidingPlace;
                check.Text = $"Check {hidingPlace.HidingPlaceName}";
                check.Visible = true;
            }
            else
            {
                check.Visible = false;
            }

            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else
            {
                goThroughTheDoor.Visible = false;
            }
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            int indexOfLocation = exits.SelectedIndex;
            MoveToANewLocation(currentLocation.Exits[indexOfLocation]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor location;
            if (currentLocation is IHasExteriorDoor)
            {
                location = currentLocation as IHasExteriorDoor;
                MoveToANewLocation(location.DoorLocation);
            }
            
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;
            for (int i = 1; i <= 10; i++)
            {
                opponent.Move();
                description.Text = $"{i}...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }

            description.Text = "Ready or not, here I come";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            goHere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(livingRoom);
        }

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show($"You found me in {Moves} moves!");
                IHidingPlace foundLocation = currentLocation as IHidingPlace;
                description.Text = $"You found your opponent in {Moves} moves.\r\nHe was hiding {foundLocation.HidingPlaceName}.";
            }
            Moves = 0;
            hide.Visible = true;
            check.Visible = false;
            goHere.Visible = false;
            exits.Visible = false;
            goThroughTheDoor.Visible = false;
        }

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currentLocation))
            {
                ResetGame(true);
            }
            else
            {
                RedrawForm();
            }
        }
    }
}
