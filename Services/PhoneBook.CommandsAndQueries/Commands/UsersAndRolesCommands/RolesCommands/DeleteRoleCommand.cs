using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands
{
    public class DeleteRoleCommand:IRequest<bool>
    {
        public string Token { get; set; }
        public IdentityRole Role { get; set; }
    }


    public class DeleteRoleByIdCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IUserManagementService _service;

        public DeleteRoleByIdCommandHandler(IUserManagementService service) =>
            _service = service;

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteRole(request.Role, request.Token, cancellationToken).ConfigureAwait(false);
        }
    }
}
