using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class UpdateRecordCommand : IRequest<PhoneRecordInfo> 
    {
        public PhoneRecordInfo UpdatableRecord { get; set; }
    }


    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, PhoneRecordInfo>
    {
        private readonly IWebRepository<PhoneRecordInfo> _repository;

        public UpdateRecordCommandHandler(IWebRepository<PhoneRecordInfo> repository) =>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(UpdateRecordCommand request, CancellationToken cancellationToken) =>
             await _repository.UpdateAsync(request.UpdatableRecord, cancellationToken);

    }
}
