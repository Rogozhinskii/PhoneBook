using MediatR;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.PermissionCommands
{
    public class CanDeleteCommand:IRequest<bool>
    {
        public string Token { get; set; }
    }

    public class CanDeleteCommandHandler : IRequestHandler<CanDeleteCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public CanDeleteCommandHandler(IPermissionService permissionService) =>
            _permissionService = permissionService;

        public async Task<bool> Handle(CanDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.CanDelete(request.Token, cancellationToken);
        }
    }
}
