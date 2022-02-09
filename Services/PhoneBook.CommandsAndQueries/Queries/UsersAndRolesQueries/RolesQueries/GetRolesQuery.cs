using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands
{
    public class GetRolesQuery:IRequest<IEnumerable<IdentityRole>>
    {
        public string Token { get; set; }
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<IdentityRole>>
    {
        private readonly IUserManagementService _service;

        public GetRolesQueryHandler(IUserManagementService service)=>
            _service = service;
        
        public async Task<IEnumerable<IdentityRole>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetRoles(request.Token, cancellationToken).ConfigureAwait(false);
        }
    }
}
