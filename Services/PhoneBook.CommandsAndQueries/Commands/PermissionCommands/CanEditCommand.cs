using MediatR;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.PermissionCommands
{
    public class CanEditCommand : IRequest<bool>
    {
        public string Token { get; set; }
    }

    public class CanEditCommandHandler : IRequestHandler<CanEditCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public CanEditCommandHandler(IPermissionService permissionService)=>
            _permissionService = permissionService;
        
        public async Task<bool> Handle(CanEditCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.CanEdit(request.Token,cancellationToken);
        }
    }
}
