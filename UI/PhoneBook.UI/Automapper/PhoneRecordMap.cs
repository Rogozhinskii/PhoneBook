using AutoMapper;
using PhoneBook.Entities;
using PhoneBook.Models;

namespace PhoneBook.Automapper
{
    public class PhoneRecordMap:Profile
    {
        public PhoneRecordMap()
        {
            CreateMap<PhoneRecordViewModel,PhoneRecord>()
                .ReverseMap();
        }
    }
}
