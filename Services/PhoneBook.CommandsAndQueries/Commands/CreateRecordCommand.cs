using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class CreateRecordCommand : IRequest<PhoneRecordInfo> 
    {
        public PhoneRecordInfo Record { get; set; }
        public string Token { get; set; }
    }

    public class CrateRecordCommandHandler : IRequestHandler<CreateRecordCommand, PhoneRecordInfo> 
    {
        private readonly IWebRepository<PhoneRecordInfo> _repository;

        public CrateRecordCommandHandler(IWebRepository<PhoneRecordInfo> repository)=>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(CreateRecordCommand request, CancellationToken cancellationToken)        
        => await _repository.AddAsync(request.Record,request.Token, cancellationToken).ConfigureAwait(false);


    }
}
