using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PhoneBook.WPF.Core.Extensions
{
    public static class SecureStringExtencions
    {
        /// <summary>
        /// Расширение для преобразования SecureString в string
        /// </summary>
        public static string GetPasswordAsString(this SecureString source)
        {
            if (source == null)
                throw new ArgumentNullException("secure string is null");
            var lenth = source.Length;
            string result = string.Empty;
            char[] chars = new char[lenth];
            IntPtr pointer = IntPtr.Zero;
            try
            {
                pointer = Marshal.SecureStringToBSTR(source);
                Marshal.Copy(pointer, chars, 0, lenth);
                result = string.Join(string.Empty, chars);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can`t get data from secure string", ex.InnerException);
            }
            finally
            {
                if (pointer != IntPtr.Zero)
                {
                    Marshal.FreeBSTR(pointer);
                }
            }

            return result;
        }
    }
}
