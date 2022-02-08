using MediatR;
using PhoneBook.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class UpdateRecordCommand : CommandBase<PhoneRecordInfo> { }


    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, PhoneRecordInfo>
    {
        public Task<PhoneRecordInfo> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
