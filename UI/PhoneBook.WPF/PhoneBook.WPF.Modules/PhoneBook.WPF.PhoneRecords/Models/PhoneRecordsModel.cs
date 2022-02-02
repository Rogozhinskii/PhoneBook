using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WPF.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PhoneBook.WPF.PhoneRecords.Models
{
    public class PhoneRecordsModel : IPhoneRecordModel
    {
        private readonly IRepository<PhoneRecordInfo> _repository;

        public ObservableCollection<PhoneRecordInfo> PhoreRecords { get; set; }=new ObservableCollection<PhoneRecordInfo>();

        public PhoneRecordsModel(IRepository<PhoneRecordInfo> repository)
        {
            _repository = repository;
        }

        public async Task LoadData()
        {
            PhoreRecords.Clear();
            var records=await _repository.GetAllAsync();
            PhoreRecords.AddRange(records);            
        }
    }
}
