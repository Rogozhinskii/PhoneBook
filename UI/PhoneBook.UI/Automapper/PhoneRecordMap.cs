using AutoMapper;
using PhoneBook.Entities;
using PhoneBook.Models;

namespace PhoneBook.Automapper
{
    /// <summary>
    /// Инициализация маппинга объекта PhoneRecordViewModel в PhoneRecord
    /// </summary>
    public class PhoneRecordMap:Profile
    {
        public PhoneRecordMap()
        {
            CreateMap<PhoneRecordViewModel,PhoneRecord>()
                .ReverseMap();
        }
    }
}
