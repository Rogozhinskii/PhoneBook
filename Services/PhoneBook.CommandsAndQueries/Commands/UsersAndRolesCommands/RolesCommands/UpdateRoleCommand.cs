using MediatR;
using PhoneBook.Domain;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands
{
    public class UpdateRoleCommand:IRequest<bool>
    {
        public string Token { get; set; }
        public ApplicationRole Role { get; set; }
    }


    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IUserManagementService _service;

        public UpdateRoleCommandHandler(IUserManagementService service) =>
            _service = service;

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateRole(request.Role,request.Token, cancellationToken).ConfigureAwait(false);
        }
    }

}
