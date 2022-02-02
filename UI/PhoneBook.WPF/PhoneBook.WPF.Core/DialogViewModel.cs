using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PhoneBook.WPF.Core
{
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public virtual string Title => "";

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequesClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual void RaiseRequesClose(ButtonResult buttonResult)
        {
            var dialogResult = new DialogResult(buttonResult);
            RaiseRequesClose(dialogResult);
        }

        public virtual bool CanCloseDialog() => true;

        public virtual void OnDialogClosed() { }

        public virtual void OnDialogOpened(IDialogParameters parameters) { }
    }
}
