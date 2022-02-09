using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.UsersQueries
{
    public class GetUserByIdQuery : IRequest<IdentityUser>
    {
        public string Token { get; set; }
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IdentityUser>
    {
        private readonly IUserManagementService _service;

        public GetUserByIdQueryHandler(IUserManagementService service) =>
            _service = service;

        public async Task<IdentityUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUserById(request.Id,request.Token,cancellationToken).ConfigureAwait(false);
        }
    }
}
