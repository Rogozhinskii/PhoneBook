using PhoneBook.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Entities
{
    /// <summary>
    /// Сущность записи телефонной книги
    /// 
    /// </summary>
    public class PhoneRecord:Entity<Guid>
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Адресс
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
