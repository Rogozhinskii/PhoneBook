﻿using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries
{
    public class GetPhoneRecordByIdQuery:IRequest<PhoneRecordInfo>
    {
        public int Id { get; set; }
    }

    public class GetPhoneRecordByIdQueryHandler : IRequestHandler<GetPhoneRecordByIdQuery, PhoneRecordInfo>
    {
        private readonly IRepository<PhoneRecordInfo> _repository;
        public GetPhoneRecordByIdQueryHandler(IRepository<PhoneRecordInfo> repository) =>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(GetPhoneRecordByIdQuery request, CancellationToken cancellationToken)=>
            await _repository.GetByIdAsync(request.Id, cancellationToken);
        
    }
}
