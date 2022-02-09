using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands.UsersCommands
{

    public class RemoveFromRoleCommand : IRequest<bool>
    {
        public IdentityUser User { get; set; }
        public string ExistingRole { get; set; }
        public string Token { get; set; }
    }

    public class RemoveFromRoleCommandHandler : IRequestHandler<RemoveFromRoleCommand, bool>
    {
        private readonly IUserManagementService _service;
        public RemoveFromRoleCommandHandler(IUserManagementService service) =>
            _service = service;

        public async Task<bool> Handle(RemoveFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.RemoveFromRole(request.User,request.ExistingRole,request.Token).ConfigureAwait(false);
        }
    }
}
