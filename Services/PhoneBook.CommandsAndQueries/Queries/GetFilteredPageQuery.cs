using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries
{
    public class GetFilteredPageQuery:IRequest<IPage<PhoneRecordInfo>>
    {
        public Func<dynamic, bool> FilterExpression { get; set; }
    }

    public class GetFilteredPageQueryHandler : IRequestHandler<GetFilteredPageQuery, IPage<PhoneRecordInfo>>
    {
        private readonly IRepository<PhoneRecordInfo> _repository;
        public GetFilteredPageQueryHandler(IRepository<PhoneRecordInfo> repository)=>
            _repository = repository;
        public async Task<IPage<PhoneRecordInfo>> Handle(GetFilteredPageQuery request, CancellationToken cancellationToken)=>
            await _repository.GetPage(request.FilterExpression, cancellationToken);
        
    }
}
