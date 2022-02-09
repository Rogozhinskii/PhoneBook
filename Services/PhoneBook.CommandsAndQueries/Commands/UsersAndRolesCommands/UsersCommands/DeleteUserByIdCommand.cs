using MediatR;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands.UsersCommands
{
    public class DeleteUserByIdCommand:IRequest<bool>
    {
        public string Id { get; set; }
        public string Token { get; set; }
    }

    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly IUserManagementService _service;
        public DeleteUserByIdCommandHandler(IUserManagementService service) =>
            _service = service;
        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteUserById(request.Id,request.Token, cancellationToken).ConfigureAwait(false);
        }
    }
}
