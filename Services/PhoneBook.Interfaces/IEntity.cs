using System;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для хранимых в БД сущностей
    /// </summary>
    public interface IEntity<T>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; set; }

    }
}
