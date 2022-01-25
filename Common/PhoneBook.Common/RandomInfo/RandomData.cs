using System;
using System.Text;

namespace PhoneBook.Common.RandomInfo
{
    /// <summary>
    /// Хранит коллекции имен и фамилий
    /// </summary>
    public class RandomData
    {
        /// <summary>
        /// Массив имен 
        /// </summary>
        private readonly static string[] names;
        /// <summary>
        /// Массив фамилий 
        /// </summary>
        private readonly static string[] surnames;

        /// <summary>
        /// Массив отчеств 
        /// </summary>
        private readonly static string[] patronymics;

        /// <summary>
        /// Массив адрессов 
        /// </summary>
        private readonly static string[] addresses;

        /// <summary>
        /// Массив адрессов 
        /// </summary>
        private readonly static string[] descriptions;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        private static readonly Random rnd;

        static RandomData()
        {
            rnd = new Random();
            names = new string[]
            {
                "Диана",
                "Давид",
                "Даниил",
                "Дарья",
                "Дмитрий",
                "Ирина",
                "Иван",
                "Игорь",
                "Илья",
                "Инна",
                "Павел",
                "Петр",
                "Полина",
                "Прохор",
                "Ян",
                "Яков",
                "Ядвига",
                "Глория",
                "Гавриил",
                "Георгий",
                "Галина",
                "Герман",
                "Любовь",
                "Лиана",
                "Лидия",
                "Леонид",
                "Лариса",
                "Роман",
                "Роза",
                "Рудольф",
                "Регина",
                "Руслана",
            };
            surnames = new string[]
            {
                "Абакумов",
                "Абакшин",
                "Абалакин",
                "Абалаков",
                "Абалдуев",
                "Абалки",
                "Баборыко",
                "Бабулин",
                "Бабунин",
                "Бабурин",
                "Бабухин",
                "Багаев",
                "Бадаев",
                "Гагин",
                "Гаевский",
                "Галаев",
                "Галанкин",
                "Галашев",
                "Галигузов",
                "Радзинский",
                "Радилов",
                "Радяев",
                "Разгилдеев",
                "Хаин",
                "Хаит",
                "Хайкин",
                "Хайт",
                "Хайдуков",
                "Хаймин",
                "Якимычев",
                "Якиров",
                "Яковкин",
                "Яковцев",
                "Якобец",
                "Яковенко",
                "Якобсон"

            };
            patronymics = new string[]
            {
                "Абакумович",
                "Алексеевич",
                "Артемиевич",
                "Аскольдович",
                "Виталиевич",
                "Вуколович",
                "Гавриилович",
                "Давидович",
                "Дормидонтович",
                "Евтихиевич",
                "Ерофеевич",
                "Иакимович",
                "Иннокентиевич",
                "Исаакович",
                "Киприанович",
                "Лаврович",
                "Лукич",
                "Меркулович",
                "Мокиевич",
                "Наумович",
                "Нилович"
            };
            addresses = new string[]
            {
                "612 Kshlerin Fort Davonteborough, WA 47498",
                "73404 Donnelly Streets Port Ericborough, MI 37733",
                "97219 Green Roads Carterfort, NH 34908",
                "3461 Claude Green Apt. 068 East Sigrid, PA 26174",
                "11379 Welch Lodge Leschton, IL 80198",
                "38018 Kiera Flats Suite 720 Carmineburgh, NH 70408",
                "983 Abbott Street Lednerview, KY 50543",
                "713 Oswald Grove Streichbury, IN 83835",
                "956 Huels Causeway Suite 604 North Wendyview, WI 66986",
                "551 Zora Roads Apt. 549 South Matteo, TX 16087-1316",
                "729 Hickle Light Preciousborough, MS 23230-3475",
                "26121 Donnell Burg Apt. 296 South Anastasia, NE 84884",
                "85824 Tremaine Pines Ankundingside, WV 77112-5430",
                "188 Madonna Station Suite 147 Wademouth, SC 08886",
                "910 Wisoky Trace Suite 876 Francotown, VA 88583-9014",
                "742 Green Rapids Suite 653 Schuppeborough, SC 69115",
                "64415 Jay Mission Samirport, OR 18519-9075",
                "4879 Denesik Inlet Oralfurt, WA 88058",
                "8666 Rashad Ramp Apt. 849 South Yesenia, AK 03313",
                "72886 Frami Stream Hintzton, RI 59455",
                "216021, Мурманская область, город Дмитров, наб. Славы, 68",
                "381268, Самарская область, город Шатура, спуск Славы, 27",
                "976293, Ленинградская область, город Одинцово, спуск Бухарестская, 68",
                "581917, Курская область, город Луховицы, наб. Гагарина, 99",
                "003299, Челябинская область, город Талдом, пр. Сталина, 60"
            };
            descriptions = new string[]
            {
                "Camera Operator",
                "Pest Control Worker",
                "Compliance Officers",
                "Art Director",
                "Personal Financial Advisor",
                "Choreographer",
                "Producers and Director",
                "Loading Machine Operator",
                "Keyboard Instrument Repairer and Tuner",
                "Recreation Worker",
                "Textile Machine Operator",
                "Benefits Specialist",
                "Executive Secretary",
                "Gas Pumping Station Operator",
                "Physical Therapist",
                "Paving Equipment Operator",
                "Mechanical Engineering Technician",
                "Veterinarian",
                "Embalmer",
                "Bindery Worker",
                "Teller",
            };
        }



       
        /// <summary>
        /// Возвращает случайное имя 
        /// </summary>
        /// <returns></returns>
        public static string GetRandomName() =>
            rnd.NextItem<string>(names);



        /// <summary>
        /// Возвращает случайную фамилию 
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSurname() =>
            rnd.NextItem<string>(surnames);



        /// <summary>
        /// Возвращает случайную фамилию 
        /// </summary>
        /// <returns></returns>
        public static string GetRandomPatronymic() =>
            rnd.NextItem<string>(patronymics);

        /// <summary>
        /// Возвращает случайный адресс
        /// </summary>
        /// <returns></returns>
        public static string GetRandomAddress() =>
            rnd.NextItem<string>(addresses);
        
        
        /// <summary>
        /// Возвращает случайное описание
        /// </summary>
        /// <returns></returns>
        public static string GetRandomDescription() =>
            rnd.NextItem<string>(descriptions);


        /// <summary>
        /// Возвращает случайный номер телефона
        /// </summary>
        /// <returns></returns>
        public static string GetRandomPhoneNumber()
        {
            StringBuilder stringBuilder = new("+7");
            for (int i = 0; i < 10; i++)
                stringBuilder.Append(rnd.Next(9));
            return stringBuilder.ToString();
        }

    }
}
