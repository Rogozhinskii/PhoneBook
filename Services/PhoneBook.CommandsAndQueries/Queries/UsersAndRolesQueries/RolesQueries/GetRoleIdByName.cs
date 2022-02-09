using MediatR;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.RolesQueries
{
    public class GetRoleIdByName:IRequest<string>
    {
        public string RoleName { get; set; }
        public string Token { get; set; }
    }

    public class GetRoleIdByNameHandler : IRequestHandler<GetRoleIdByName, string>
    {
        private readonly IUserManagementService _service;
        public GetRoleIdByNameHandler(IUserManagementService service) =>
            _service = service;

        public async Task<string> Handle(GetRoleIdByName request, CancellationToken cancellationToken)
        {
            return await _service.GetRoleIdByName(request.RoleName,request.Token,cancellationToken);
        }


    }

}
