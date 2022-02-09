using MediatR;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Queries
{
    public class GetPageQuery:IRequest<IPage<PhoneRecordInfo>>
    {
        public string SearchString { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }


    public class GetPageQueryHandler : IRequestHandler<GetPageQuery, IPage<PhoneRecordInfo>>
    {
        private readonly IWebRepository<PhoneRecordInfo> _repository;
        public GetPageQueryHandler(IWebRepository<PhoneRecordInfo> repository) =>
            _repository = repository;

        public async Task<IPage<PhoneRecordInfo>> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            if(request.SearchString is not null)
                return await _repository.GetPage(request.SearchString,cancellationToken).ConfigureAwait(false);
            return await _repository.GetPage(request.PageIndex, request.PageSize, cancellationToken);
        }
        
    }
}
