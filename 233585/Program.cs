using System;
using System.Collections.Generic;

namespace RideSharingApp
{
    public abstract class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public abstract void Register();
        public abstract void Login();
        public void DisplayProfile()
        {
            Console.WriteLine($"User ID: {UserId}\nName: {Name}\nPhone Number: {PhoneNumber}");
        }
    }

    public class Rider : User
    {
        public List<Trip> RideHistory { get; private set; } = new List<Trip>();

        public override void Register()
        {
            Console.Write("Enter Rider ID: ");
            UserId = Console.ReadLine();
            Console.Write("Enter Name: ");
            Name = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine("Rider registered successfully.");
        }

        public override void Login()
        {
            Console.WriteLine($"Rider {Name} logged in.");
        }

        public void RequestRide(RideSharingSystem system)
        {
            system.RequestRide(this);
        }
    }

    public class Driver : User
    {
        public List<Trip> TripHistory { get; private set; } = new List<Trip>();
        public bool IsAvailable { get; set; } = true;

        public override void Register()
        {
            Console.Write("Please Enter Driver ID: ");
            UserId = Console.ReadLine();
            Console.Write("Please Enter Name: ");
            Name = Console.ReadLine();
            Console.Write("Please Enter Phone Number: ");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine("Driver registered successfully.");
        }

        public override void Login()
        {
            Console.WriteLine($"Driver {Name} logged in.");
        }

        public void AcceptRide(Trip trip)
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                trip.Driver = this;
                Console.WriteLine($"Driver {Name} accepted the ride.");
            }
            else
            {
                Console.WriteLine("Driver is not available for new rides.");
            }
        }

        public void CompleteTrip(Trip trip)
        {
            if (trip.Driver == this)
            {
                trip.EndTrip();
                TripHistory.Add(trip);
                IsAvailable = true;
                Console.WriteLine($"Trip completed. Fare: {trip.CalculateFare()}");
            }
            else
            {
                Console.WriteLine("This trip does not belong to the driver.");
            }
        }
    }

    public class Trip
    {
        public Rider Rider { get; set; }
        public Driver Driver { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public decimal Fare { get; private set; }
        public bool IsCompleted { get; private set; } = false;

        public void StartTrip()
        {
            Console.WriteLine("Trip started.");
        }

        public void EndTrip()
        {
            IsCompleted = true;
            Console.WriteLine("Trip ended.");
        }

        public decimal CalculateFare()
        {

            Fare = 10.00m;
            if (IsCompleted) return Fare;
            return 0;
        }
    }


    public class RideSharingSystem
    {
        public List<Rider> Riders { get; private set; } = new List<Rider>();
        public List<Driver> Drivers { get; private set; } = new List<Driver>();
        public List<Trip> Trips { get; private set; } = new List<Trip>();

        public void RegisterRider(Rider rider)
        {
            Riders.Add(rider);
        }

        public void RegisterDriver(Driver driver)
        {
            Drivers.Add(driver);
        }

        public void RequestRide(Rider rider)
        {
            Console.Write("Enter Start Location: ");
            string startLocation = Console.ReadLine();
            Console.Write("Enter End Location: ");
            string endLocation = Console.ReadLine();

            Trip trip = new Trip
            {
                Rider = rider,
                StartLocation = startLocation,
                EndLocation = endLocation
            };
            trip.StartTrip();
            Trips.Add(trip);


            Driver availableDriver = Drivers.Find(d => d.IsAvailable);
            if (availableDriver != null)
            {
                availableDriver.AcceptRide(trip);
            }
            else
            {
                Console.WriteLine("No drivers available at the moment.");
            }
        }

        public void DisplayAllTrips()
        {
            Console.WriteLine("All Trips:");
            foreach (var trip in Trips)
            {
                Console.WriteLine($"Rider: {trip.Rider.Name}, Driver: {(trip.Driver != null ? trip.Driver.Name : "Not Assigned")}, Start: {trip.StartLocation}, End: {trip.EndLocation}, Completed: {trip.IsCompleted}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RideSharingSystem system = new RideSharingSystem();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Register as Rider");
                Console.WriteLine("2. Register as Driver");
                Console.WriteLine("3. Request Ride");
                Console.WriteLine("4. Accept Ride (Driver)");
                Console.WriteLine("5. Complete Trip (Driver)");
                Console.WriteLine("6. View Ride History (Rider)");
                Console.WriteLine("7. View All Trips");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Rider rider = new Rider();
                        rider.Register();
                        system.RegisterRider(rider);
                        break;

                    case 2:
                        Driver driver = new Driver();
                        driver.Register();
                        system.RegisterDriver(driver);
                        break;

                    case 3:
                        Console.Write("Enter your Rider ID to request a ride: ");
                        string riderId = Console.ReadLine();
                        Rider foundRider = system.Riders.Find(r => r.UserId == riderId);
                        if (foundRider != null)
                        {
                            foundRider.RequestRide(system);
                        }
                        else
                        {
                            Console.WriteLine("Rider not found.");
                        }
                        break;

                    case 4:
                        Console.Write("Enter your Driver ID to accept a ride: ");
                        string driverId = Console.ReadLine();
                        Driver foundDriver = system.Drivers.Find(d => d.UserId == driverId);
                        if (foundDriver != null)
                        {
                            Trip latestTrip = system.Trips.Find(t => t.Driver == null && !t.IsCompleted);
                            if (latestTrip != null)
                            {
                                foundDriver.AcceptRide(latestTrip);
                            }
                            else
                            {
                                Console.WriteLine("No ride requests available.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Driver not found.");
                        }
                        break;

                    case 5:
                        Console.Write("Enter your Driver ID to complete a trip: ");
                        string completingDriverId = Console.ReadLine();
                        Driver completingDriver = system.Drivers.Find(d => d.UserId == completingDriverId);
                        if (completingDriver != null)
                        {
                            Trip tripToComplete = system.Trips.Find(t => t.Driver == completingDriver && !t.IsCompleted);
                            if (tripToComplete != null)
                            {
                                completingDriver.CompleteTrip(tripToComplete);
                            }
                            else
                            {
                                Console.WriteLine("No trips found .");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Driver not found.");
                        }
                        break;

                    case 6:
                        Console.WriteLine("View ride history .");
                        break;

                    case 7:
                        system.DisplayAllTrips();
                        break;

                    case 8:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}