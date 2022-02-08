using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class CreateRecordCommand : CommandBase<PhoneRecordInfo> { }

    public class CrateRecordCommandHandler : IRequestHandler<CreateRecordCommand, PhoneRecordInfo> 
    {
        private readonly IRepository<PhoneRecordInfo> _repository;

        public CrateRecordCommandHandler(IRepository<PhoneRecordInfo> repository)=>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(CreateRecordCommand request, CancellationToken cancellationToken) =>
            await _repository.AddAsync(request.Record, cancellationToken).ConfigureAwait(false);

        
    }
}
