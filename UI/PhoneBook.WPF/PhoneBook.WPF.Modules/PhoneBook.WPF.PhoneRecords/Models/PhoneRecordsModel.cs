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
        private readonly IWebRepository<PhoneRecordInfo> _repository;
        private readonly IAuthentificationService _authentificationService;
        private readonly ITokenHandler _tokenHandler;

        public ObservableCollection<PhoneRecordInfo> PhoreRecords { get; set; }=new ObservableCollection<PhoneRecordInfo>();

        public PhoneRecordsModel(IWebRepository<PhoneRecordInfo> repository, IAuthentificationService authentificationService,ITokenHandler tokenHandler)
        {
            _repository = repository;
            _authentificationService = authentificationService;
            _tokenHandler = tokenHandler;
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
            var result= await _repository.AddAsync(newRecord,_tokenHandler.Token, cancelation);
            PhoreRecords.Add(result);
        }

        public bool IsUserCanEditRecord() => _authentificationService.AuthenticatedUserRole == UserRoles.Administrator;

        public async Task UpdateRecord(PhoneRecordInfo newRecord, CancellationToken cancelation = default)
        {
            await _repository.UpdateAsync(newRecord,_tokenHandler.Token,cancelation);            
        }

        public async Task DeleteRecord(PhoneRecordInfo newRecord, CancellationToken cancelation = default)
        {
            await _repository.DeleteByIdAsync(newRecord.Id,_tokenHandler.Token,cancelation);
            PhoreRecords.Remove(newRecord);
        }

        public bool IsUserCanDeleteRecord()=>_authentificationService.AuthenticatedUserRole==UserRoles.Administrator;
    }
}
