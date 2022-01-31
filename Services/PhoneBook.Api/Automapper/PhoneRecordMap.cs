using AutoMapper;
using PhoneBook.Common.Models;
using PhoneBook.Entities;

namespace PhoneBook.Api.Automapper
{
    public class PhoneRecordMap:Profile
    {
        public PhoneRecordMap()
        {
            CreateMap<PhoneRecordInfo, PhoneRecord>(MemberList.Source)
                .ReverseMap();
        }
    }
}
