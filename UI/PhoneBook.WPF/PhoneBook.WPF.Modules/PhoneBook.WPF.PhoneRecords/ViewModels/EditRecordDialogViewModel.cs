using PhoneBook.Common.Models;
using PhoneBook.WPF.Core;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PhoneBook.WPF.PhoneRecords.ViewModels
{
    internal class EditRecordDialogViewModel:DialogViewModel
    {

        private PhoneRecordInfo _originalRecord;
        protected readonly Dictionary<string, object> _values = new();

        public string FirstName { get => GetValue(_originalRecord?.FirstName); set => SetValue(value); }
        public string LastName { get => GetValue(_originalRecord?.LastName); set => SetValue(value); }
        public string Patronymic { get => GetValue(_originalRecord?.Patronymic); set => SetValue(value); }
        public string PhoneNumber { get => GetValue(_originalRecord?.PhoneNumber); set => SetValue(value); }
        public string Address { get => GetValue(_originalRecord?.Address); set => SetValue(value); }
        public string Description { get => GetValue(_originalRecord?.Description); set => SetValue(value); }



        private DelegateCommand _saveChangesCommand;
        /// <summary>
        /// Выполняет сохранение изменений
        /// </summary>
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(() =>
           {
               SaveChanges(_originalRecord);
               DialogResult result = new DialogResult(ButtonResult.OK);
               RaiseRequestClose(result);
           });

       
        private DelegateCommand _cancelCommand;
        /// <summary>
        /// Отменяет изменения и закрывает диалоговое окно
        /// </summary>
        public DelegateCommand CancelCommand =>
           _cancelCommand ??= _cancelCommand = new(() =>
           {
               CancelChanges();
               var result = new DialogResult(ButtonResult.Cancel);
               RaiseRequestClose(result);
           });

       
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _originalRecord = parameters.GetValue<PhoneRecordInfo>(DialogNames.EditableRecord);
            var type = this.GetType();
            var props = type.GetProperties();
            foreach (var item in props)
            {
                RaisePropertyChanged(item.Name);
            }
        }

        public override void OnDialogClosed()
        {
            _originalRecord = null;
        }






        /// <summary>
        /// Устанавливает значение в словарь свойств изменяемого объекта 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual bool SetValue(object value, [CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out var oldValue) && Equals(oldValue, value))
                return false;
            _values[property] = value;
            return true;
        }

        protected virtual T GetValue<T>(T Default, [CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out var value))
                return (T)value;
            return Default;
        }

        /// <summary>
        /// Сохраняет значения изменившихся свойств редактируемого объекта
        /// </summary>
        /// <param name="namedEntity"></param>
        protected void SaveChanges(PhoneRecordInfo record)
        {
            var type = record.GetType();
            foreach (var (propertyName, value) in _values)
            {
                var property = type.GetProperty(propertyName);
                if (property is null || !property.CanWrite) continue;
                property.SetValue(record, value);
            }
            _values.Clear();
        }

        /// <summary>
        /// Отменяет запланированные изменения
        /// </summary>
        protected void CancelChanges()
        {
            var properties = _values.Keys.ToArray();
            _values.Clear();
            foreach (var property in properties)
                RaisePropertyChanged(property);
        }


    }
}
