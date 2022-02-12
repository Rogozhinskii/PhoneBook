using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries
{
    public class GetPhoneRecordByIdQuery:IRequest<PhoneRecordInfo>
    {
        public Guid Id { get; set; }
    }

    public class GetPhoneRecordByIdQueryHandler : IRequestHandler<GetPhoneRecordByIdQuery, PhoneRecordInfo>
    {
        private readonly IWebRepository<PhoneRecordInfo> _repository;
        public GetPhoneRecordByIdQueryHandler(IWebRepository<PhoneRecordInfo> repository) =>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(GetPhoneRecordByIdQuery request, CancellationToken cancellationToken)=>
            await _repository.GetByIdAsync(request.Id, cancellationToken);
        
    }
}
