using AutoMapper;
using PhoneBook.Domain;
using PhoneBook.Models;

namespace PhoneBook.Automapper
{

    public class ApplicationRoleMap:Profile
    {
        public ApplicationRoleMap()
        {
            CreateMap<ApplicationRoleViewModel,ApplicationRole>()
                .ReverseMap();
        }
    }
}
