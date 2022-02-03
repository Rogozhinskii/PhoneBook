using PhoneBook.Common.Models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WPF.Core
{
    public interface IPhoneRecordModel
    {
        ObservableCollection<PhoneRecordInfo> PhoreRecords { get; }

        Task LoadData();
        public bool IsUserCanAddNewRecord();
        public bool IsUserCanEditRecord();
        public bool IsUserCanDeleteRecord();
        Task AddNewRecord(PhoneRecordInfo newRecord,CancellationToken cancelation=default);
        Task UpdateRecord(PhoneRecordInfo newRecord,CancellationToken cancelation=default);
        Task DeleteRecord(PhoneRecordInfo newRecord,CancellationToken cancelation=default);

    }
}
