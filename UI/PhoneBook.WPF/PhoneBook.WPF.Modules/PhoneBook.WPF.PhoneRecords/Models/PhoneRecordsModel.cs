using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WPF.Core;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WPF.PhoneRecords.Models
{
    public class PhoneRecordsModel : IPhoneRecordModel
    {
        private readonly IRepository<PhoneRecordInfo> _repository;
        private readonly IAuthentificationService _authentificationService;

        public ObservableCollection<PhoneRecordInfo> PhoreRecords { get; set; }=new ObservableCollection<PhoneRecordInfo>();

        public PhoneRecordsModel(IRepository<PhoneRecordInfo> repository, IAuthentificationService authentificationService)
        {
            _repository = repository;
            _authentificationService = authentificationService;
        }

        public async Task LoadData()
        {
            PhoreRecords.Clear();
            var records=await _repository.GetAllAsync();
            PhoreRecords.AddRange(records);            
        }

        public bool IsUserCanAddNewRecord()
        {
            if (_authentificationService.AuthenticatedUserRole == UserRoles.RegularUser || _authentificationService.AuthenticatedUserRole == UserRoles.Administrator)
                return true;
            else return false;
        }

        public async Task AddNewRecord(PhoneRecordInfo newRecord, CancellationToken cancelation = default)
        {
            var result= await _repository.AddAsync(newRecord, cancelation);
            PhoreRecords.Add(result);
        }

        public bool IsUserCanEditRecord() => _authentificationService.AuthenticatedUserRole == UserRoles.Administrator;

        public async Task UpdateRecord(PhoneRecordInfo newRecord, CancellationToken cancelation = default)
        {
            await _repository.UpdateAsync(newRecord,cancelation);            
        }

        public async Task DeleteRecord(PhoneRecordInfo newRecord, CancellationToken cancelation = default)
        {
            await _repository.DeleteAsync(newRecord,cancelation);
            PhoreRecords.Remove(newRecord);
        }

        public bool IsUserCanDeleteRecord()=>_authentificationService.AuthenticatedUserRole==UserRoles.Administrator;
    }
}
