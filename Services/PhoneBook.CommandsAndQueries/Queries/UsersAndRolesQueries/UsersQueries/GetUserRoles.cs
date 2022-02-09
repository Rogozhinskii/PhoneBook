using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.UsersQueries
{
    public class GetUserRoles: IRequest<IList<string>>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }


    public class GetUserRolesHandler : IRequestHandler<GetUserRoles, IList<string>>
    {
        private readonly IUserManagementService _service;

        public GetUserRolesHandler(IUserManagementService service) =>
            _service = service;

        public async Task<IList<string>> Handle(GetUserRoles request, CancellationToken cancellationToken)
        {
            return await _service.GetUserRoles(request.UserId,request.Token,cancellationToken).ConfigureAwait(false);
        }
    }
}
