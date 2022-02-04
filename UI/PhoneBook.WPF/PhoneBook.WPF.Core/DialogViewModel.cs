using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PhoneBook.WPF.Core
{
    /// <summary>
    /// базовая вью модель диалогового окна
    /// </summary>
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public virtual string Title => "";

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual void RaiseRequestClose(ButtonResult buttonResult)
        {
            var dialogResult = new DialogResult(buttonResult);
            RaiseRequestClose(dialogResult);
        }

        public virtual bool CanCloseDialog() => true;

        public virtual void OnDialogClosed() { }

        public virtual void OnDialogOpened(IDialogParameters parameters) { }
    }
}
