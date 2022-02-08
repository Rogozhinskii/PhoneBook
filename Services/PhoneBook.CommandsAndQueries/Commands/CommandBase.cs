using MediatR;

namespace PhoneBook.CommandsAndQueries.Commands
{
    public class CommandBase<T>:IRequest<T>
    {
        public T Record { get; set; }  
    }

       
}
