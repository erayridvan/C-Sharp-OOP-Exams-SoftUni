using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotels;

        public HotelRepository()
        {
            hotels = new List<IHotel>();
        }

        public void AddNew(IHotel model)
        {
            hotels.Add(model);
        }

        public IReadOnlyCollection<IHotel> All() => hotels.AsReadOnly();

        public IHotel Select(string criteria)
        {
            return hotels.FirstOrDefault(x => x.FullName == criteria);
        }
    }
}
