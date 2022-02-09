using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands.UsersCommands
{
    public class UpdateUserCommand:IRequest<bool>
    {
        public IdentityUser User { get; set; }
        public string Token { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserManagementService _service;
        public UpdateUserCommandHandler(IUserManagementService service) =>
            _service = service;
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateUser(request.User, request.Token,cancellationToken).ConfigureAwait(false);
        }
    }
}
