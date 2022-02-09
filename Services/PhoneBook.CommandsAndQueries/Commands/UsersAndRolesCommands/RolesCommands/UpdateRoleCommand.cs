using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands
{
    public class UpdateRoleCommand:IRequest<bool>
    {
        public string Token { get; set; }
        public IdentityRole Role { get; set; }
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
