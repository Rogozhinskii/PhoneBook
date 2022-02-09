using AutoMapper;
using PhoneBook.Common.Models;

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
