using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Domain;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands.UsersCommands
{
    public class AddToRoleCommand : IRequest<bool>
    {
        public User User { get; set; }
        public string NewRoleName { get; set; }
        public string Token { get; set; }
    }

    public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommand, bool>
    {
        private readonly IUserManagementService _service;
        public AddToRoleCommandHandler(IUserManagementService service) =>
            _service = service;

        public async Task<bool> Handle(AddToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.AddToRole(request.User, request.NewRoleName, request.Token).ConfigureAwait(false);
        }
    }
}
