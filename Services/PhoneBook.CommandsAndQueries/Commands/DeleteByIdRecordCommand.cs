using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class DeleteByIdRecordCommand : CommandBase<PhoneRecordInfo> { }

    public class DeleteByIdRecordCommandHandler : IRequestHandler<DeleteByIdRecordCommand, PhoneRecordInfo> 
    {
        private readonly IRepository<PhoneRecordInfo> _repository;

        public DeleteByIdRecordCommandHandler(IRepository<PhoneRecordInfo> repository) =>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(DeleteByIdRecordCommand request, CancellationToken cancellationToken)=>
            await _repository.DeleteByIdAsync(request.Record.Id, cancellationToken).ConfigureAwait(false);
    }
}
