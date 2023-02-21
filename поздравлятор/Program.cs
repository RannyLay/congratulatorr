using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.IO;

namespace поздравлятор
{
    class Program
    {
        static int hello()
        {
            int choice;
            string x;
            Console.WriteLine("\nЧто бы вы хотели сделать?");
            Console.WriteLine("1 - добавить день рождения");
            Console.WriteLine("2 - удалить день рождения");
            Console.WriteLine("3 - редактировать день рождения");
            x = Console.ReadLine();
            if (Int32.TryParse(x, out choice))
                choice = Convert.ToInt32(x);
            else
                choice = 0;
            return choice;
        }

        static string whichMonth(int x)
        {
            string res;
            switch (x)
            {
                case 1:
                    res = "января";
                    break;
                case 2:
                    res = "февраля";
                    break;
                case 3:
                    res = "марта";
                    break;
                case 4:
                    res = "апреля";
                    break;
                case 5:
                    res = "мая";
                    break;
                case 6:
                    res = "июня";
                    break;
                case 7:
                    res = "июля";
                    break;
                case 8:
                    res = "августа";
                    break;
                case 9:
                    res = "сентября";
                    break;
                case 10:
                    res = "октября";
                    break;
                case 11:
                    res = "ноября";
                    break;
                case 12:
                    res = "декабря";
                    break;
                default:
                    res = " ";
                    break;
            }
            return res;
        }

        static string howOld(int x)
        {
            string res = "";
            if (x > 4 && x < 21 || x % 10 > 4 || x % 10 == 0){
                res = "лет";
            }
            else if (x % 10 == 1){
                res = "год";
            }
            else
            {
                res = "года";
            }
            return res;
        }

        static void showBirthdays(List<Birthday> AllBirthdays, int i)
        {
            int n = 0;
            Console.WriteLine("\n\tСегодняшние дни рождения: ");
            for (int j = 0; j < i; j++)
            {
                if (DateTime.Now.Day == AllBirthdays[j].Day && DateTime.Now.Month == AllBirthdays[j].Month)
                {
                    Console.WriteLine("\t{0}.) {1}\t\t{2} {3} \t Исполняется {4} {5}", Convert.ToInt32(n + 1), AllBirthdays[j].Name, AllBirthdays[j].Day, whichMonth(AllBirthdays[j].Month), DateTime.Now.Year - AllBirthdays[j].Year, howOld(DateTime.Now.Year - AllBirthdays[j].Year));
                    n++;
                    AllBirthdays[j].IsTexted = true;
                }
            }
            if (n == 0)
            {
                Console.WriteLine("\tСегодня ни у кого нет дня рождения :(");
            }
            n = 0;

            Console.WriteLine("\n\tДни рождения ближайший месяц: ");
            for (int j = 0; j < i; j++)
            {
                if ((DateTime.Now.Day < AllBirthdays[j].Day && DateTime.Now.Month == AllBirthdays[j].Month) || (DateTime.Now.Day > AllBirthdays[j].Day && (AllBirthdays[j].Month - DateTime.Now.Month == 1 || AllBirthdays[j].Month - DateTime.Now.Month == -11)))
                {
                    Console.WriteLine("\t{0}.) {1}\t\t{2} {3} \t Исполняется {4} {5}", Convert.ToInt32(n + 1), AllBirthdays[j].Name, AllBirthdays[j].Day, whichMonth(AllBirthdays[j].Month), DateTime.Now.Year - AllBirthdays[j].Year, howOld(DateTime.Now.Year - AllBirthdays[j].Year));
                    n++;
                    AllBirthdays[j].IsTexted = true;
                }
            }
            if (n == 0)
            {
                Console.WriteLine("\tВ этом месяце ни у кого нет дня рождения :(");
            }
            n = 0;

            Console.WriteLine("\n\tВсе остальные дни рождения: ");
            for (int j = 0; j < i; j++)
            {
                if (AllBirthdays[j].IsTexted == false)
                {
                    Console.WriteLine("\t{0}.) {1}\t\t{2} {3} \t Исполняется {4} {5}", Convert.ToInt32(n + 1), AllBirthdays[j].Name, AllBirthdays[j].Day, whichMonth(AllBirthdays[j].Month), DateTime.Now.Year - AllBirthdays[j].Year, howOld(DateTime.Now.Year - AllBirthdays[j].Year));
                    n++;
                    AllBirthdays[j].IsTexted = true;
                }
            }
            if (n == 0)
            {
                Console.WriteLine("\tБольше нет дней рождений :(");
            }

            for (int j = 0; j < i; j++)
            {
                AllBirthdays[j].IsTexted = false;
            }
        }

        static void Main(string[] args)
        {
            string path = "C:\\Users\\Света\\source\\repos\\поздравлятор\\Birthdays.txt"; //Укажите пожалуйста свой путь к текстовому файлу
            List<Birthday> AllBirthdays = new List<Birthday>();
            int choice, i = 0, cont = 1, number, change;
            string name, date, number1, change1, cont1, text;
            DateTime cooldate;
            bool b = true, check = true;

            Console.WriteLine("Здравствуйте! Добро пожаловать в приложение \"Поздравлятор\"");
            using (StreamReader FileReader = new StreamReader(path))
            {
                while (!FileReader.EndOfStream)
                {
                    while ((text = FileReader.ReadLine()) != null)
                    {
                        string[] data = text.Split(',');
                        AllBirthdays.Add(new Birthday(data[0], Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3])));
                        i++;
                    }
                }
                FileReader.Close();
            }

            while (cont == 1)
            {
                if (i > 0)
                {
                    showBirthdays(AllBirthdays, i);
                }
                do
                {
                    choice = hello();
                    switch (choice)
                    {
                        case 1:  //ДОБАВЛЕНИЕ
                            b = false;
                            Console.Clear();
                            Console.WriteLine("\nЧей день рождения запишем?)");
                            name = Console.ReadLine();
                            check = true;
                            do
                            {
                                Console.WriteLine("\nКогда у этого человека день рождения?(формат дд.мм.гггг)");
                                date = Console.ReadLine();
                                if (DateTime.TryParse(date, out cooldate) && Convert.ToDateTime(date)<DateTime.Now)
                                {
                                    cooldate = Convert.ToDateTime(date);
                                    check = false;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Вы ввели недопустимое значение. Попробуйте снова.");
                                    check = true;
                                }
                            } while (check);
                            AllBirthdays.Add(new Birthday(name, cooldate.Day, cooldate.Month, cooldate.Year));
                            using (StreamWriter FileWriter = new StreamWriter(path, true))
                            {
                                FileWriter.WriteLine("{0}, {1}, {2}, {3}", AllBirthdays[i].Name, AllBirthdays[i].Day, AllBirthdays[i].Month, AllBirthdays[i].Year);
                                FileWriter.Close();
                            }
                            i++;
                            break;
                        case 2: //УДАЛЕНИЕ
                            b = false;
                            check = true;
                            Console.Clear();
                            if (i > 0)
                            {
                                do
                                {
                                    Console.WriteLine("\nАктуальный список дней рождения:\n");
                                    for (int j = 0; j < i; j++)
                                        Console.WriteLine("{0}.) {1}\t\t{2} {3} \t Исполняется {4} {5}", Convert.ToInt32(j + 1), AllBirthdays[j].Name, AllBirthdays[j].Day, whichMonth(AllBirthdays[j].Month), DateTime.Now.Year - AllBirthdays[j].Year, howOld(DateTime.Now.Year - AllBirthdays[j].Year));
                                    Console.WriteLine("\nЧей день рождения удалим? Введите номер");
                                    number1 = Console.ReadLine();
                                    if (Int32.TryParse(number1, out number) && Convert.ToInt32(number1) <= i && Convert.ToInt32(number1) > 0)
                                    {
                                        number = Convert.ToInt32(number1);
                                        AllBirthdays.Remove(AllBirthdays[number - 1]);
                                        check = false;
                                        i--;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Вы ввели недопустимое значение. Попробуйте снова.");
                                        check = true;
                                    }
                                } while (check);
                            }
                            else
                            {
                                Console.WriteLine("Пока что у нас нет ни одной записи, так что нам нечего удалять!");
                                b = true;
                            }
                            break;
                        case 3: //РЕДАКТИРОВАНИЕ
                            b = false;
                            check = true;
                            Console.Clear();
                            if (i > 0)
                            {
                                do
                                {
                                    Console.WriteLine("\nАктуальный список дней рождения:\n");
                                    for (int j = 0; j < i; j++)
                                        Console.WriteLine("{0}.) {1}\t\t{2} {3} \t Исполняется {4} {5}", Convert.ToInt32(j + 1), AllBirthdays[j].Name, AllBirthdays[j].Day, whichMonth(AllBirthdays[j].Month), DateTime.Now.Year - AllBirthdays[j].Year, howOld(DateTime.Now.Year - AllBirthdays[j].Year));
                                    Console.WriteLine("\nЧей день рождения редактируем? Введите номер");
                                    number1 = Console.ReadLine();
                                    if (Int32.TryParse(number1, out number) && Convert.ToInt32(number1) <= i && Convert.ToInt32(number1) > 0)
                                    {
                                        number = Convert.ToInt32(number1);
                                        Console.WriteLine("\nЧто вы хотите изменить? 1 - имя, 2 - дату рождения");
                                        change1 = Console.ReadLine();
                                        if (Int32.TryParse(change1, out change))
                                        {
                                            change = Convert.ToInt32(change1);
                                            if (change == 1)
                                            {
                                                check = false;
                                                Console.WriteLine("\nВведите новое имя:");
                                                name = Console.ReadLine();
                                                AllBirthdays[number - 1].Name = name;
                                            }
                                            else if (change == 2)
                                            {
                                                check = false;
                                                Console.WriteLine("\nВведите новую дату рождения:");
                                                date = Console.ReadLine();
                                                if (DateTime.TryParse(date, out cooldate) && Convert.ToDateTime(date) < DateTime.Now)
                                                {
                                                    cooldate = Convert.ToDateTime(date);
                                                    check = false;
                                                    AllBirthdays[number - 1].Day = cooldate.Day;
                                                    AllBirthdays[number - 1].Month = cooldate.Month;
                                                    AllBirthdays[number - 1].Year = cooldate.Year;
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Вы ввели недопустимое значение. Попробуйте снова.");
                                                    check = true;
                                                }
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Вы ввели недопустимое значение. Попробуйте снова.");
                                                check = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Вы ввели недопустимое значение. Попробуйте снова.");
                                        check = true;
                                    }
                                } while (check);
                            }
                            else
                            {
                                Console.WriteLine("Пока что у нас нет ни одной записи, так что нам нечего редактировать!");
                                b = true;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Вы ввели недопустимое значение. Попробуйте снова.");
                            b = true;
                            break;
                    }
                } while (b);
                Console.Clear();
                if (choice != 1)
                {
                    using (StreamWriter FileWriter = new StreamWriter(path, false))
                    {
                        for (int j = 0; j < i; j++) 
                        {
                            FileWriter.WriteLine("{0}, {1}, {2}, {3}", AllBirthdays[j].Name, AllBirthdays[j].Day, AllBirthdays[j].Month, AllBirthdays[j].Year);
                        }
                        FileWriter.Close();
                    }
                }
                if (i > 0)
                {
                    Console.WriteLine("\n\tГотово! Актуальный список дней рождения:\n");
                    showBirthdays(AllBirthdays, i);
                }
                else
                    Console.WriteLine("\nГотово! Сейчас нет ни одной записи.\n");
                Console.WriteLine("\n Вы хотели бы продолжить? Введите 1, если да, и любой другой символ, если нет.");
                cont1 = Console.ReadLine();
                if (Int32.TryParse(cont1, out change))
                {
                    cont = Convert.ToInt32(cont1);
                }
                else
                    cont = 0;
                Console.Clear();
            }
        }
    }
}



