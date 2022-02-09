using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    public interface IAuthorizedRequest
    {
        bool SetToken(string token);
        
    }
}
