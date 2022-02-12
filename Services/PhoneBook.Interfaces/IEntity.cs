using System;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для хранимых в БД сущностей
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public Guid Id { get; set; }
    }
}
