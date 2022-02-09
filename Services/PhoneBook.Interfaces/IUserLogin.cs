namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Интерфейс формы аутентификации
    /// </summary>
    public interface IUserLogin
    {
        /// <summary>
        /// логин
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// пароль
        /// </summary>
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
