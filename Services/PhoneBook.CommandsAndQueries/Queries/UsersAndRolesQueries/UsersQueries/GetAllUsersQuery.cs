using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.UsersQueries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<IdentityUser>>
    {
        public string Token { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<IdentityUser>>
    {
        private readonly IUserManagementService _service;

        public GetAllUsersQueryHandler(IUserManagementService service) =>
            _service = service;

        public async Task<IEnumerable<IdentityUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllUsers(request.Token,cancellationToken).ConfigureAwait(false);
        }
    }
}
