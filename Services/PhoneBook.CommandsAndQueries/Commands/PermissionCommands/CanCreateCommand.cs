using MediatR;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands.PermissionCommands
{
    public class CanCreateCommand:IRequest<bool>
    {
        public string Token { get; set; }
    }
    public class CanCreateCommandHandler : IRequestHandler<CanCreateCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public CanCreateCommandHandler(IPermissionService permissionService) =>
            _permissionService = permissionService;

        public async Task<bool> Handle(CanCreateCommand request, CancellationToken cancellationToken)=>
            await _permissionService.CanCreate(request.Token, cancellationToken);
        
    }
}
