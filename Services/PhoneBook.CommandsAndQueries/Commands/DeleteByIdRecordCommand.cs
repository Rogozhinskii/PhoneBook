using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class DeleteByIdRecordCommand : IRequest<PhoneRecordInfo>
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }

    public class DeleteByIdRecordCommandHandler : IRequestHandler<DeleteByIdRecordCommand, PhoneRecordInfo> 
    {
        private readonly IWebRepository<PhoneRecordInfo> _repository;

        public DeleteByIdRecordCommandHandler(IWebRepository<PhoneRecordInfo> repository) =>
            _repository = repository;

        public async Task<PhoneRecordInfo> Handle(DeleteByIdRecordCommand request, CancellationToken cancellationToken) =>
             await _repository.DeleteByIdAsync(request.Id,request.Token, cancellationToken);


    }
}
