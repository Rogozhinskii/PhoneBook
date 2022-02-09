using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands
{
    public class GetRoleByIdQuery : IRequest<IdentityRole>
    {
        public string Token { get; set; }
        public string Id { get; set; }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, IdentityRole>
    {
        private readonly IUserManagementService _service;

        public GetRoleByIdQueryHandler(IUserManagementService service) =>
            _service = service;

        public async Task<IdentityRole> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetRoleById(request.Id,request.Token,cancellationToken).ConfigureAwait(false);
        }
    }
}
