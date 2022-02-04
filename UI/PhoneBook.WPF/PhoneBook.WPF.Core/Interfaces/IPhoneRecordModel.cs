using PhoneBook.Common.Models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WPF.Core
{
    public interface IPhoneRecordModel
    {
        ObservableCollection<PhoneRecordInfo> PhoreRecords { get; }
        /// <summary>
        /// Выполняет загрузка записей из хранилища
        /// </summary>
        /// <returns></returns>
        Task LoadData();

        /// <summary>
        /// возвращает true если у пользователя есть права добавлять новую запись
        /// </summary>
        /// <returns></returns>
        public bool IsUserCanAddNewRecord();

        /// <summary>
        /// возвращает true если у пользователя есть права редактировать запись
        /// </summary>
        /// <returns></returns>
        public bool IsUserCanEditRecord();

        /// <summary>
        /// возвращает true если у пользователя есть права удалять запись
        /// </summary>
        /// <returns></returns>
        public bool IsUserCanDeleteRecord();
        Task AddNewRecord(PhoneRecordInfo newRecord,CancellationToken cancelation=default);
        Task UpdateRecord(PhoneRecordInfo newRecord,CancellationToken cancelation=default);
        Task DeleteRecord(PhoneRecordInfo newRecord,CancellationToken cancelation=default);

    }
}
