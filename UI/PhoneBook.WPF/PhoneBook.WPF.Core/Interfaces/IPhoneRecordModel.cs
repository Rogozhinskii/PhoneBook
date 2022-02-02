using PhoneBook.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PhoneBook.WPF.Core
{
    public interface IPhoneRecordModel
    {
        ObservableCollection<PhoneRecordInfo> PhoreRecords { get; }

        Task LoadData();


    }
}
