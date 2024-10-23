using System;
using System.Collections.Generic;

namespace AirlineReservationSystem
{
    public class Customer
    {
        private int customerId;
        private string lastName;
        private string firstName;
        private string street;
        private string city;
        private string state;
        private string zipCode;
        private string phone;
        private string email;
        private string password;
        private List<Reservation> reservations;

        public int GetCustomerId() { return customerId; }
        public void SetCustomerId(int value) { customerId = value; }

        public string GetLastName() { return lastName; }
        public void SetLastName(string value) { lastName = value; }

        public string GetFirstName() { return firstName; }
        public void SetFirstName(string value) { firstName = value; }

        public string GetStreet() { return street; }
        public void SetStreet(string value) { street = value; }

        public string GetCity() { return city; }
        public void SetCity(string value) { city = value; }

        public string GetState() { return state; }
        public void SetState(string value) { state = value; }

        public string GetZipCode() { return zipCode; }
        public void SetZipCode(string value) { zipCode = value; }

        public string GetPhone() { return phone; }
        public void SetPhone(string value) { phone = value; }

        public string GetEmail() { return email; }
        public void SetEmail(string value) { email = value; }

        public string GetPassword() { return password; }
        public void SetPassword(string value) { password = value; }

        public List<Reservation> GetReservations() { return reservations; }
        public void SetReservations(List<Reservation> value) { reservations = value; }

        public Customer()
        {
            reservations = new List<Reservation>();
        }

        public void MakeReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }
    }

    public class RetailCustomer : Customer
    {
        private string creditCardType;
        private string creditCardNo;

        public string GetCreditCardType() { return creditCardType; }
        public void SetCreditCardType(string value) { creditCardType = value; }

        public string GetCreditCardNo() { return creditCardNo; }
        public void SetCreditCardNo(string value) { creditCardNo = value; }
    }

    public class CorporateCustomer : Customer
    {
        private string companyName;
        private int frequentFlyerPoints;
        private string billingAccountNo;

        public string GetCompanyName() { return companyName; }
        public void SetCompanyName(string value) { companyName = value; }

        public int GetFrequentFlyerPoints() { return frequentFlyerPoints; }
        public void SetFrequentFlyerPoints(int value) { frequentFlyerPoints = value; }

        public string GetBillingAccountNo() { return billingAccountNo; }
        public void SetBillingAccountNo(string value) { billingAccountNo = value; }
    }

    public class Reservation
    {
        private int reservationNo;
        private DateTime date;
        private List<Seat> seats;
        private Flight flight;

        public int GetReservationNo() { return reservationNo; }
        public void SetReservationNo(int value) { reservationNo = value; }

        public DateTime GetDate() { return date; }
        public void SetDate(DateTime value) { date = value; }

        public List<Seat> GetSeats() { return seats; }
        public void SetSeats(List<Seat> value) { seats = value; }

        public Flight GetFlight() { return flight; }
        public void SetFlight(Flight value) { flight = value; }

        public Reservation()
        {
            seats = new List<Seat>();
        }

        public void AddSeat(Seat seat)
        {
            seats.Add(seat);
        }
    }

    public class Seat
    {
        private int rowNo;
        private int seatNo;
        private decimal price;
        private string status;

        public int GetRowNo() { return rowNo; }
        public void SetRowNo(int value) { rowNo = value; }

        public int GetSeatNo() { return seatNo; }
        public void SetSeatNo(int value) { seatNo = value; }

        public decimal GetPrice() { return price; }
        public void SetPrice(decimal value) { price = value; }

        public string GetStatus() { return status; }
        public void SetStatus(string value) { status = value; }
    }

    public class Flight
    {
        private int flightId;
        private DateTime date;
        private string origin;
        private string destination;
        private TimeSpan departureTime;
        private TimeSpan arrivalTime;
        private int seatingCapacity;
        private List<Seat> seats;

        public int GetFlightId() { return flightId; }
        public void SetFlightId(int value) { flightId = value; }

        public DateTime GetDate() { return date; }
        public void SetDate(DateTime value) { date = value; }

        public string GetOrigin() { return origin; }
        public void SetOrigin(string value) { origin = value; }

        public string GetDestination() { return destination; }
        public void SetDestination(string value) { destination = value; }

        public TimeSpan GetDepartureTime() { return departureTime; }
        public void SetDepartureTime(TimeSpan value) { departureTime = value; }

        public TimeSpan GetArrivalTime() { return arrivalTime; }
        public void SetArrivalTime(TimeSpan value) { arrivalTime = value; }

        public int GetSeatingCapacity() { return seatingCapacity; }
        public void SetSeatingCapacity(int value) { seatingCapacity = value; }

        public List<Seat> GetSeats() { return seats; }
        public void SetSeats(List<Seat> value) { seats = value; }

        public Flight()
        {
            seats = new List<Seat>();
        }

        public void AddSeat(Seat seat)
        {
            if (seats.Count < seatingCapacity)
            {
                seats.Add(seat);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Flight flight = new Flight();
            flight.SetFlightId(101);
            flight.SetDate(DateTime.Now);
            flight.SetOrigin("New York");
            flight.SetDestination("Los Angeles");
            flight.SetDepartureTime(new TimeSpan(10, 30, 0));
            flight.SetArrivalTime(new TimeSpan(13, 45, 0));
            flight.SetSeatingCapacity(150);

            Seat seat1 = new Seat();
            seat1.SetRowNo(1);
            seat1.SetSeatNo(1);
            seat1.SetPrice(200);
            seat1.SetStatus("Available");

            Seat seat2 = new Seat();
            seat2.SetRowNo(1);
            seat2.SetSeatNo(2);
            seat2.SetPrice(200);
            seat2.SetStatus("Available");

            flight.AddSeat(seat1);
            flight.AddSeat(seat2);

            RetailCustomer customer = new RetailCustomer();
            customer.SetCustomerId(1);
            customer.SetFirstName("Wajahat");
            customer.SetLastName("ul Hasnain");
            customer.SetEmail("wajahat.doe@example.com");
            customer.SetCreditCardType("Visa");
            customer.SetCreditCardNo("1234-5678-9876-5432");

            Reservation reservation = new Reservation();
            reservation.SetReservationNo(1001);
            reservation.SetDate(DateTime.Now);
            reservation.SetFlight(flight);

            reservation.AddSeat(seat1);
            reservation.AddSeat(seat2);

            customer.MakeReservation(reservation);

            Console.WriteLine($"Customer {customer.GetFirstName()} {customer.GetLastName()} has made a reservation for Flight {flight.GetFlightId()}");
        }
    }
}

