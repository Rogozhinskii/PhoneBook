using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands.UsersCommands
{
    public class AddNewUserCommand:IRequest<bool>
    {
        public string Token { get; set; }
        public UserInfo User { get; set; }
    }

    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, bool>
    {
        private readonly IUserManagementService _service;
        public AddNewUserCommandHandler(IUserManagementService service) =>
            _service = service;

        public async Task<bool> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateUser(request.User,request.Token);
        }
    }
}
