using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands
{
    public class CreateRoleCommand : IRequest<bool>
    {
        public string Token { get; set; }
        public IdentityRole Role { get; set; }
    }


    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IUserManagementService _service;

        public CreateRoleCommandHandler(IUserManagementService service) =>
            _service = service;

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateRole(request.Role, request.Token, cancellationToken).ConfigureAwait(false);
        }
    }
}
