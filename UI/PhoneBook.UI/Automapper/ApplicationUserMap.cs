using AutoMapper;
using PhoneBook.Common.Models;
using PhoneBook.Domain;

namespace PhoneBook.Automapper
{
    public class ApplicationUserMap:Profile
    {
        public ApplicationUserMap()
        {
            CreateMap<UserInfo, User>()
                .ReverseMap();
        }
    }
}
