namespace System
{
    public static class RandomExtension
    {
        /// <summary>
        /// Вовзращает случайный элемент из массива элементов типа Т
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rnd"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T NextItem<T>(this Random rnd, params T[] items) =>
            items[rnd.Next(items.Length)];
    }
}
