using MediatR;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Domain;
using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.UsersQueries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string Token { get; set; }
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserManagementService _service;

        public GetUserByIdQueryHandler(IUserManagementService service) =>
            _service = service;

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUserById(request.Id,request.Token,cancellationToken).ConfigureAwait(false);
        }
    }
}
