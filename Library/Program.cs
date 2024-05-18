using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Numerics;
using System.Text;

//using LibraryApp;
using System.Security.Claims;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using LibraryApp;

Library library;

Console.WriteLine("Если библиотека ранее на этом комьютере не создавалась, нажмите 1." +
    "\nЕсли создавалась- любой другой символ.");
Byte.TryParse(Console.ReadLine(), out byte n);

if (n == 1)
{
    library = new Library(n);
}
else
{
    library = new Library();
    library.LateNotice();
}


Console.WriteLine("\n\n");
Console.WriteLine("Выберете действие:\n1. Зайти в меню главного библиотекаря.\n2. Работать в текущей библиотеке. (любой символ) ");
Byte.TryParse(Console.ReadLine(), out byte first);

if (first == 1)
{
    int pass;
    Console.WriteLine("введите пароль главного библиотекаря (подсказка: 12345 ):");
    Int32.TryParse(Console.ReadLine(), out pass);
    if (pass == library.libBOSSpassword)
    {
        LibraryBOSS();
    }
    else
        Console.WriteLine("пароль неверный. работайте с этой библиотекой!");
}

Console.WriteLine("\n\n");

byte second;

do
{
    Console.WriteLine("Выберете действие:\n1. Выдать книгу.\n2. Вернуть книну." +
        "\n3. Подобрать книгу. \n4. Посмотреть журнал.  \n5. Завершить программу. ");
    Byte.TryParse(Console.ReadLine(), out second);

    switch (second)
    {
        case 1:
            byte gBook;
            Console.WriteLine("впишите ID выбранныой книги:");
            if (Byte.TryParse(Console.ReadLine(), out gBook))
                library.ReturnBook(gBook);
            else
                Console.WriteLine("некорректный ID");
            Console.WriteLine();
            break;

        case 2:
            byte rBook;
            Console.WriteLine("впишите ID возвращаемой книги:");
            if (Byte.TryParse(Console.ReadLine(), out rBook))
                library.ReturnBook(rBook);
            else
                Console.WriteLine("некорректный ID");
            Console.WriteLine();
            break;

        case 3:
            LookBook();
            Console.WriteLine();
            break;
        case 4:
            library.ShowJournal();
            break;

        default: break;
    }
}
while (second != 5);



void AddSomeBooks()
{
    Author dostoevsky = new Author("Федор Михайлович Достоевский", 1836, Countries.Russia);
    Author pushkin = new Author("Александр Сергеевич Пушкин", 1798, Countries.Russia);
    Author shekspire = new("William Shekspire", 1679, Countries.GreatBritan);
    Author gogol = new("Николай Васильевич Гоголь", 1768, Countries.Ukraine);

    PublOffice eksmo = new PublOffice("Эксмо", "Москва");

    Book nochPeredRozd = new("Ночь перед рождеством", gogol, eksmo, 1795, 1600,
        new Genre[] { Genre.Fiction, Genre.Mystry, Genre.Fantasy, Genre.Comedy });

    Book korolLir = new Book("Король лир", shekspire, eksmo, 1700, 1800,
                   new Genre[] { Genre.Drama, Genre.Poetry }, Languages.English);

    Book idiot = new Book("Идиот", dostoevsky, eksmo, 1868,
        500, new Genre[] { Genre.Drama, Genre.Romance, Genre.Fiction }, Languages.Russian);

    Book karamazovy = new Book("Братья Карамазовы", dostoevsky, eksmo, 1878,
        700, new Genre[] { Genre.Drama, Genre.Romance });

    Book onegin = new Book("Евгений Онегин", pushkin, eksmo, 1831,
        1200, new Genre[] { Genre.Poetry, Genre.Drama, Genre.Romance });

    Book kapsDoughter = new Book("Капитанская Дочка", pushkin, eksmo, 1836,
        920, new Genre[] { Genre.Romance });

    library.AddBook(idiot);
    library.AddBook(karamazovy);
    library.AddBook(karamazovy);
    library.AddBook(karamazovy);
    library.AddBook(onegin);
    library.AddBook(kapsDoughter);
    library.AddBook(kapsDoughter);
    library.AddBook(nochPeredRozd);
    library.AddBook(korolLir);
}

void LookBook()
{
    do
    {
        byte third;
        Console.WriteLine(" 1. По автору. " +
            "\n 2. По жанру. " +
            "\n 3. Найти по названию. " +
            "\n 4. Показать всех аторов конкретной страны. " +
            "\n 5. Показать все книги издательства. " +
            "\n 6. Выйти из меню подбора книг. (любой символ)");
        if (!Byte.TryParse(Console.ReadLine(), out third))
            break;

        switch (third)
        {
            case 1:
                Console.WriteLine("Впишите ФИО автора полностью:");
                string au = Console.ReadLine().ToString();
                library.ShowAutor(au);
                Console.WriteLine();
                break;

            case 2:
                Console.WriteLine("Выберете жанр из списка: 1.Fiction, 2.Horror, 3.Drama, 4.Mystry, 5.Poetry, 6.Nonfiction, 7.Western, " +
                    "8.Fantasy, 9.Romance, 10.Comedy.");
                Genre gen = (Genre)Int32.Parse(Console.ReadLine());
                library.ShowGenge(gen);
                Console.WriteLine();
                break;

            case 3:
                Console.WriteLine("Впишите название книги полностью:");
                string bookName = Console.ReadLine().ToString();
                library.SearchByNameOfBook(bookName);
                Console.WriteLine();
                break;

            case 4:
                Console.WriteLine("Выберете страну из списка: 1.Russia, 2.GreatBritan, 3.France, 4.Germany, 5.Ukraine ");
                Countries con = (Countries)Int32.Parse(Console.ReadLine());
                library.ShowAutorsOfCountry(con);
                Console.WriteLine();
                break;

            case 5:
                Console.WriteLine("Впишите название издательства полностью:");
                string pOf = Console.ReadLine().ToString();
                library.ShowPubOffice(pOf);
                Console.WriteLine();
                break;

            case 6: return;

            default: return;
        }
    }
    while (true);
}

void LibraryBOSS()
{
    while (true)
    {
        Console.WriteLine("Выберете действие: \n1. Создание нового журнала и новой библиотеки." +
            " (если журнал и библиотека создавались ранее, все данные будут стерты!!)" +
            $"\n2. Добавление в библиотеку минимального набора книг. " +
            $"\n3. Изменение срока выдачи книг.(Сейчас {library.timeKeepingBook}) " +
            $"\n4. Настройка уведомлений о просрочке книг. " +
            $"\n5. Выйти из меню главного библиотекаря. (любой символ)");
        byte zero;
        if (!Byte.TryParse(Console.ReadLine(), out zero))
            break;
        switch (zero)
        {
            case 1: library.CreateLibrary(); Console.WriteLine("библиотека создана"); break;
            case 2: AddSomeBooks(); Console.WriteLine("добавлено"); break;
            case 3:
                Console.WriteLine("впишите новый срок в днях: ");

                Byte.TryParse(Console.ReadLine(), out byte days);
                library.timeKeepingBook = days;
                XDocument lib = XDocument.Load("Library.xml");
                lib.Root.Attribute("days").Value = days.ToString();
                lib.Save("Library.xml");
                break;
            case 4:
                AlarmSettings(); break;
            case 5: return;
            default: return;
        }
    }
}

void AlarmSettings()
{
    while (true)
    {
        Console.WriteLine("Выберете действие:" +
            "\n1. Не уведомлять о просрочке." +
            "\n2. Уведомлять только библиотекаря. " +
            "\n3. Уведомлять только читателя. " +
            "\n4. Уведомлять обоих. " +
            "\n5. Выйти из меню настройки уведомлений. (любой символ)");

        XDocument lib = XDocument.Load("Library.xml");
        byte al;
        Byte.TryParse(lib.Root.Attributes().FirstOrDefault(a => a.Name == "alarm").Value, out al);

        Console.WriteLine($"-----Сейчас настроен вариант {al}-----");
        byte zero;
        if (!Byte.TryParse(Console.ReadLine(), out zero))
            break;



        switch (zero)
        {
            case 1:
                switch (al)
                {
                    case 1: break;
                    case 2:
                        library.Alarm -= library.AlarmLibrary;
                        break;
                    case 3:
                        library.Alarm -= library.AlarmReaders;
                        break;
                    case 4:
                        library.Alarm -= library.AlarmLibrary;
                        library.Alarm -= library.AlarmReaders;
                        break;
                    default: break;
                }
                Console.WriteLine("Уведомления отключены.\n");
                lib.Root.Attribute("alarm").Value = "1";
                lib.Save("Library.xml");
                break;

            case 2:
                switch (al)
                {
                    case 1:
                        library.Alarm += library.AlarmLibrary;
                        break;
                    case 2: break;
                    case 3:
                        library.Alarm -= library.AlarmReaders;
                        break;
                    case 4:
                        library.Alarm -= library.AlarmReaders;
                        break;
                    default: break;
                }
                Console.WriteLine("Включены оповещения библиотекаря.");
                lib.Root.Attribute("alarm").Value = "2";
                lib.Save("Library.xml");
                break;
            case 3:

                switch (al)
                {
                    case 1:
                        library.Alarm += library.AlarmReaders;
                        break;
                    case 2:
                        library.Alarm += library.AlarmReaders;
                        library.Alarm -= library.AlarmLibrary;
                        break;
                    case 3: break;
                    case 4:
                        library.Alarm -= library.AlarmLibrary;
                        break;
                    default: break;
                }
                Console.WriteLine("Включены оповещения читателей.");
                lib.Root.Attribute("alarm").Value = "3";
                lib.Save("Library.xml");
                break;
            case 4:
                switch (al)
                {
                    case 1:
                        library.Alarm += library.AlarmReaders;
                        library.Alarm += library.AlarmLibrary;
                        break;
                    case 2:
                        library.Alarm += library.AlarmReaders;
                        break;
                    case 3:
                        library.Alarm += library.AlarmLibrary;
                        break;
                    case 4: break;
                    default: break;
                }
                Console.WriteLine("Включены уведомления всех.");
                lib.Root.Attribute("alarm").Value = "4";
                lib.Save("Library.xml");
                break;
            case 5: return;
            default: return;
        }
        Console.WriteLine("\n");
    }
}



    internal class Library
    {
        public int libBOSSpassword;
        public int timeKeepingBook;

        public delegate void BookHandler(List<XElement> lates);
        public event BookHandler? Alarm;
        public Library()
        {
            libBOSSpassword = 12345;
            XDocument lib = XDocument.Load("Library.xml");
            Int32.TryParse(lib.Root.Attributes().FirstOrDefault(a => a.Name == "days").Value, out timeKeepingBook);
            byte al;
            Byte.TryParse(lib.Root.Attributes().FirstOrDefault(a => a.Name == "alarm").Value, out al);
            switch (al)
            {
                case 1: Alarm += DoNothing; break;
                case 2: Alarm += AlarmLibrary; break;
                case 3: Alarm += AlarmReaders; break;
                case 4: Alarm += AlarmLibrary; Alarm += AlarmReaders; break;
                default: break;
            }
        }

        public Library(int n)
        {
            libBOSSpassword = 12345;
            CreateLibrary();
        }
        public void GiveBook(byte id)
        {
            XDocument xmlLibrary = XDocument.Load("Library.xml");
            XDocument xmlJournal = XDocument.Load("Journal.xml");

            XElement libRoot = xmlLibrary.Root;
            XElement? book = libRoot.Elements().
                                    FirstOrDefault(b => b.Element("ID").
                                    Value == id.ToString());
            if (book != null)
            {
                if (book.Attribute("isReturned").Value == "onHand")
                { Console.WriteLine("Book is on hand"); }
                else
                {
                    XElement record = new XElement("record");
                    XAttribute isReturned = new XAttribute("isReturned", "notReturned"); record.Add(isReturned);

                    book.Attribute("isReturned").Value = "onHand";

                    Regex rPhone = new Regex(@".*\d?.*\d{3}.*\d{3}.*\d{2}.*\d{2}.*");
                    Regex rName = new Regex(@"\w*\s\w*\s\w*");
                    string? readerName;
                    do
                    {
                        Console.WriteLine("Впишите ФИО читателя:");
                        readerName = Console.ReadLine();
                    }
                    while (!rName.IsMatch(readerName));

                    string? readerPhone;
                    do
                    {
                        Console.WriteLine("Впишите номер телефона читателя:");
                        readerPhone = Console.ReadLine();
                    }
                    while (!rPhone.IsMatch(readerPhone));

                    XElement phone = new XElement("phone", readerPhone); record.Add(phone);
                    XElement name = new XElement("name", readerName); record.Add(name);

                    XElement bookID = new("BookID", book.Element("ID").Value); record.Add(bookID);

                    XElement bookName = new("book", book.Element("name").Value); record.Add(bookName);
                    XElement bookAutor = new("autor", book.Element("author").Element("name").Value); record.Add(bookAutor);
                    XElement date = new("date", DateTime.Today.ToString()); record.Add(date);

                    xmlJournal.Root.Add(record);
                    xmlJournal.Save("Journal.xml");
                    xmlLibrary.Save("Library.xml");
                }
            }

            else
                Console.WriteLine("book not found");
        }
        public void ReturnBook(byte id)
        {
            XDocument xmlLibrary = XDocument.Load("Library.xml");
            XDocument xmlJournal = XDocument.Load("Journal.xml");
            XElement libRoot = xmlLibrary.Root;
            XElement jorRoot = xmlJournal.Root;

            XElement? book = libRoot.Elements().
                                    FirstOrDefault(b => b.Element("ID").
                                    Value == id.ToString());

            book.Attribute("isReturned").Value = "inLibrary";

            XElement? record = jorRoot.Elements().
                                    FirstOrDefault(r => r.Element("BookID").
                                    Value == id.ToString() && r.Attribute("isReturned").Value == "notReturned");
            if (record == null)
            { Console.WriteLine("Такая книга не выдавалась никому."); return; }

            record.Attribute("isReturned").Value = "Returned";
            XElement returnDate = new XElement("returnDate", DateTime.Today.ToString());
            record.Add(returnDate);
            xmlJournal.Save("Journal.xml");
            xmlLibrary.Save("Library.xml");
        }

        public void CreateLibrary()
        {
            XDocument library = new XDocument();
            XElement lib = new XElement("Library"); library.Add(lib);
            XAttribute days = new XAttribute("days", 0); lib.Add(days);
            XAttribute alarm = new XAttribute("alarm", 1); lib.Add(alarm);
            XAttribute amount = new XAttribute("amount", 0); lib.Add(amount);
            library.Save("Library.xml");

            XDocument journal = new();
            XElement journ = new XElement("Journal"); journal.Add(journ);
            journal.Save("Journal.xml");
        }

        public void AddBook(Book book)
        {

            XDocument xmlLibrary = XDocument.Load("Library.xml");

            XElement b = BookToXml(book);
            XAttribute isReturned = new("isReturned", "inLibrary");
            b.Add(isReturned);
            XElement ID = new("ID", xmlLibrary.Root.Attribute("amount").Value);
            b.Add(ID);
            xmlLibrary.Root.Add(b);

            int amount = Int32.Parse(xmlLibrary.Root.Attribute("amount").Value);
            amount++;
            xmlLibrary.Root.Attribute("amount").Value = amount.ToString();

            xmlLibrary.Save("Library.xml");
        }

        public XElement BookToXml(Book book)
        {
            XElement book1 = new("book");
            XElement name = new XElement("name", book.name); book1.Add(name);
            XElement autor = book.author.AuthorToXml(); book1.Add(autor);
            XElement pubOffice = new("pubOffice", book.publOffice); book1.Add(pubOffice);
            XElement year = new("year", book.year); book1.Add(year);
            XElement price = new("price", book.price); book1.Add(price);
            XElement language = new("language", book.language); book1.Add(language);
            int genr = 1;
            foreach (Genre g in book.Genres)
            {
                XAttribute genre = new XAttribute("genre" + genr, g);
                book1.Add(genre);
                genr++;
            }

            return book1;
        }

        public void ShowAutor(string author)//выводит все книги автора
        {
            XDocument xLib = XDocument.Load("library.xml");
            XElement? root = xLib.Root;
            var books = root.Elements()
                .Where(a => a.Element("author").Element("name").Value == author).ToList();

            if (books.Count == 0)
            { Console.WriteLine("book not found"); return; }

            for (int i = 0; i < books.Count; i++)
            {
                int c = 1;
                int av = 1;
                XElement count = new XElement("TotalCount", c);
                books[i].Add(count);
                XElement available = new XElement("availabe", av);
                books[i].Add(available);

                XElement IDs = new XElement("IDofAvailableBooks");
                books[i].Add(IDs);

                if (books[i].Attribute("isReturned").Value == "onHand")
                    av--;
                else
                    books[i].Element("IDofAvailableBooks").Value += $"{books[i].Element("ID").Value}; ";

                for (int j = i + 1; j < books.Count; j++)
                {
                    if (books[i].Element("name").Value == books[j].Element("name").Value
                         && books[i].Element("author").Element("name").Value == books[j].Element("author").Element("name").Value)
                    {
                        if (books[j].Attribute("isReturned").Value == "inLibrary")
                        {
                            av++;
                            books[i].Element("IDofAvailableBooks").Value += $"{books[j].Element("ID").Value}; ";
                        }

                        books.RemoveAt(j);
                        c++;
                        j--;
                    }
                }
                count.Value = c.ToString();
                available.Value = av.ToString();
            }

            foreach (XElement b in books)
            {
                XmlCout(b);
                Console.WriteLine("----------------------------------");
            }
        }

        public void ShowAutor(Author author)//выводит все книги автора
        {
            XDocument xLib = XDocument.Load("library.xml");
            XElement? root = xLib.Root;

            var books = root.Elements()
                .Where(a => a.Element("autor").Element("name").Value == author.name).ToList();

            if (books.Count == 0)
            { Console.WriteLine("book not found"); return; }

            for (int i = 0; i < books.Count; i++)
            {
                int c = 1;
                int av = 1;
                XElement count = new XElement("TotalCount", c);
                books[i].Add(count);
                XElement available = new XElement("availabe", av);
                books[i].Add(available);

                XElement IDs = new XElement("IDofAvailableBooks");
                books[i].Add(IDs);

                if (books[i].Attribute("isReturned").Value == "notReturned")
                    av--;
                else
                    books[i].Element("IDofAvailableBooks").Value += $"{books[i].Element("ID").Value}; ";

                for (int j = i + 1; j < books.Count; j++)
                {
                    if (books[i].Element("name").Value == books[j].Element("name").Value
                         && books[i].Element("author").Element("name").Value == books[j].Element("author").Element("name").Value)
                    {
                        if (books[j].Attribute("isReturned").Value == "inLibrary")
                        {
                            av++;
                            books[i].Element("IDofAvailableBooks").Value += $"{books[j].Element("ID").Value}; ";
                        }

                        books.RemoveAt(j);
                        c++;
                        j--;
                    }
                }
                count.Value = c.ToString();
                available.Value = av.ToString();
            }
            foreach (XElement b in books)
            {
                XmlCout(b);
                Console.WriteLine("----------------------------------");
            }
        }

        public void ShowGenge(Genre genre)
        {
            XDocument xLib = XDocument.Load("library.xml");
            XElement? root = xLib.Root;
            var books = root.Elements()
                .Where(a => a.Attributes().First().Value == genre.ToString()).ToList();

            if (books.Count == 0)
            { Console.WriteLine("book not found"); return; }

            for (int i = 0; i < books.Count; i++)
            {
                int c = 1;
                int av = 1;
                XElement count = new XElement("TotalCount", c);
                books[i].Add(count);
                XElement available = new XElement("availabe", av);
                books[i].Add(available);

                XElement IDs = new XElement("IDofAvailableBooks");
                books[i].Add(IDs);

                if (books[i].Attribute("isReturned").Value == "notReturned")
                    av--;
                else
                    books[i].Element("IDofAvailableBooks").Value += $"{books[i].Element("ID").Value}; ";

                for (int j = i + 1; j < books.Count; j++)
                {
                    if (books[i].Element("name").Value == books[j].Element("name").Value
                         && books[i].Element("author").Element("name").Value == books[j].Element("author").Element("name").Value)
                    {
                        if (books[j].Attribute("isReturned").Value == "inLibrary")
                        {
                            av++;
                            books[i].Element("IDofAvailableBooks").Value += $"{books[j].Element("ID").Value}; ";
                        }

                        books.RemoveAt(j);
                        c++;
                        j--;
                    }
                }
                count.Value = c.ToString();
                available.Value = av.ToString();
            }
            foreach (XElement b in books)
            {
                XmlCout(b);
                Console.WriteLine("----------------------------------");
            }

        }

        public void ShowAutorsOfCountry(Countries country)
        {
            XDocument xLib = XDocument.Load("library.xml");
            XElement? root = xLib.Root;
            var authorsOfCountry = root?.Elements()
                .Where(b => b?.Element("author")?.Element("country")?.Value == country.ToString())
                .Select(b => new { AuthorName = b.Element("author").Element("name").Value }).ToList();

            if (authorsOfCountry == null)
            { Console.WriteLine("authors not found"); return; }

            for (int i = 0; i < authorsOfCountry.Count; i++)
            {
                for (int j = i + 1; j < authorsOfCountry.Count; j++)
                    if (authorsOfCountry[i].AuthorName == authorsOfCountry[j].AuthorName)
                    { authorsOfCountry.RemoveAt(j); j--; }
            }

            foreach (var au in authorsOfCountry)
                Console.WriteLine(au.AuthorName);
        }

        public void SearchByNameOfBook(string bookName)
        {
            XDocument xLib = XDocument.Load("library.xml");
            XElement? root = xLib.Root;

            var books = root.Elements()
                .Where(a => a.Element("name").Value == bookName).ToList();

            if (books.Count == 0)
            { Console.WriteLine("book not found"); return; }

            for (int i = 0; i < books.Count; i++)
            {
                int c = 1;
                int av = 1;
                XElement count = new XElement("TotalCount", c);
                books[i].Add(count);
                XElement available = new XElement("availabe", av);
                books[i].Add(available);

                XElement IDs = new XElement("IDofAvailableBooks");
                books[i].Add(IDs);

                if (books[i].Attribute("isReturned").Value == "notReturned")
                    av--;
                else
                    books[i].Element("IDofAvailableBooks").Value += $"{books[i].Element("ID").Value}; ";

                for (int j = i + 1; j < books.Count; j++)
                {
                    if (books[i].Element("name").Value == books[j].Element("name").Value
                         && books[i].Element("author").Element("name").Value == books[j].Element("author").Element("name").Value)
                    {
                        if (books[j].Attribute("isReturned").Value == "inLibrary")
                        {
                            av++;
                            books[i].Element("IDofAvailableBooks").Value += $"{books[j].Element("ID").Value}; ";
                        }

                        books.RemoveAt(j);
                        c++;
                        j--;
                    }
                }
                count.Value = c.ToString();
                available.Value = av.ToString();
            }
            foreach (XElement b in books)
            {
                XmlCout(b);
                Console.WriteLine("----------------------------------");
            }
        }

        public void LateNotice()
        {
            XDocument xmlJournal = XDocument.Load("Journal.xml");
            XElement? root = xmlJournal.Root;

            DateTime checkDate = DateTime.Today.AddDays(-timeKeepingBook);

            var notReturnBooks = root?.Elements()
                                .Where(b => b.Attribute("isReturned")?.Value == "notReturned");

            var lateBooks = notReturnBooks
                            .Where(b => DateTime.Parse(b.Element("date").Value) <= checkDate).ToList();

            if (lateBooks.Count > 0)
                Alarm(lateBooks);

        }

        public static void XmlCout(XElement book)
        {
            foreach (XAttribute at in book.Attributes())
                Console.Write(at.Name + ":  " + at.Value + "|\n");
            foreach (XElement at in book.Elements())
            {
                if (at.HasElements)
                {
                    Console.WriteLine();
                    Console.WriteLine("\t" + at.Name + ":");
                    XmlCout(at);
                    Console.WriteLine();
                }
                else
                    Console.WriteLine("\t" + at.Name + ":  " + at.Value);
            }
            // Console.WriteLine("__________________");
        }

        public void ShowJournal()
        {
            XDocument xmlJounal = XDocument.Load("Journal.xml");
            XElement root = xmlJounal.Root;

            var records = root.Elements();
            if (records.Count() == 0)
            { Console.WriteLine(" журнал пуст "); return; }
            foreach (var record in records)
            {
                if (record.Attribute("isReturned").Value == "Returned")
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                XmlCout(record);
                Console.WriteLine("--------------");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void AlarmLibrary(List<XElement> lates)
        {
            foreach (var record in lates)
            {
                Console.WriteLine("Late book!!!!");
                Console.WriteLine("Читатель: " + record?.Element("name")?.Value);
                Console.WriteLine("Книга: " + record?.Element("book")?.Value);
                Console.WriteLine();
                Console.WriteLine("Напоминание библиотекарю: \nпозвонить должнику на номер: ");
                Console.WriteLine(record?.Element("phone")?.Value);
                Console.WriteLine("----------------------");
            }
        }

        public void AlarmReaders(List<XElement> lates)
        {
            foreach (var record in lates)
            {
                Console.WriteLine("Late book!!!!");
                Console.WriteLine("Читатель: " + record?.Element("name")?.Value);
                Console.WriteLine("Книга: " + record?.Element("book")?.Value);
                Console.WriteLine();
                Console.WriteLine("Отправка автоматического сообщения на: ");
                Console.WriteLine(record?.Element("phone")?.Value);
                Console.WriteLine("----------------------");
            }
        }

        public void DoNothing(List<XElement> lates)
        {
            Console.WriteLine("У нас есть должники, но мы никак на это не реагируем...");
        }


        public void ShowPubOffice(string pOffice)
        {
            XDocument xLib = XDocument.Load("library.xml");
            XElement? root = xLib.Root;

            var books = root.Elements()
                .Where(a => a.Element("pubOffice").Value == pOffice).ToList();

            if (books.Count == 0)
            { Console.WriteLine("book not found"); return; }

            for (int i = 0; i < books.Count; i++)
            {
                int c = 1;
                int av = 1;
                XElement count = new XElement("TotalCount", c);
                books[i].Add(count);
                XElement available = new XElement("availabe", av);
                books[i].Add(available);

                XElement IDs = new XElement("IDofAvailableBooks");
                books[i].Add(IDs);

                if (books[i].Attribute("isReturned").Value == "notReturned")
                    av--;
                else
                    books[i].Element("IDofAvailableBooks").Value += $"{books[i].Element("ID").Value}; ";

                for (int j = i + 1; j < books.Count; j++)
                {
                    if (books[i].Element("name").Value == books[j].Element("name").Value
                         && books[i].Element("author").Element("name").Value == books[j].Element("author").Element("name").Value)
                    {
                        if (books[j].Attribute("isReturned").Value == "inLibrary")
                        {
                            av++;
                            books[i].Element("IDofAvailableBooks").Value += $"{books[j].Element("ID").Value}; ";
                        }

                        books.RemoveAt(j);
                        c++;
                        j--;
                    }
                }
                count.Value = c.ToString();
                available.Value = av.ToString();
            }
            foreach (XElement b in books)
            {
                XmlCout(b);
                Console.WriteLine("----------------------------------");
            }
        }
}
﻿
enum Genre
{
    Fiction = 1,
    Horror,
    Drama,
    Mystry,
    Poetry,
    Nonfiction,
    Western,
    Fantasy,
    Romance,
    Comedy,
}

enum Languages
{
    Russian,
    English,
    Turkish,
    Spanish,
    French
}


internal class Book
{
    public List<Genre> Genres;
    internal string? name { set; get; }
    internal Author? author;
    internal PublOffice? publOffice;
    internal int? year;
    internal double? price;
    internal Languages language;

    public Book(string? name = null, Author? author = null, PublOffice? publOffice = null,
                int? year = null, double? price = null, Genre[] genres = null,
                Languages language = Languages.Russian)
    {
        this.name = name;
        this.author = author;
        this.publOffice = publOffice;
        this.year = year;
        this.price = price;
        this.Genres = new List<Genre>();
        Genres?.AddRange(genres);
        this.language = language;
    }

    public override string ToString()
    {
        return $"Name: {name}, Autor: {author?.name} ," +
            $" Publishing office: {publOffice?.name},\n " +
            $"{publOffice?.city}, Year: {year}, Price: {price}, Language: {language} ";
    }
}


enum Countries
{
    Russia = 1,
    GreatBritan,
    France,
    Germany,
    Ukraine,


}

namespace LibraryApp
{
    internal class Author
    {
        internal string? name { set; get; }
        int? year { set; get; }
        public Countries? country { set; get; }

        public Author(string? name = null, int? year = null, Countries? country = null)
        {
            this.name = name;
            this.year = year;
            this.country = country;
        }

        public override string ToString()
        {
            return $"{name} {year} {country}";
        }

        public static bool operator ==(Author left, Author right)
        {
            return left.name == right.name
                && left.country == right.country && left.year == right.year;
        }

        public static bool operator !=(Author left, Author right)
        {
            return !(left == right);
        }

        public XElement AuthorToXml()
        {
            XElement author = new XElement("author");
            XElement name = new XElement("name", this.name); author.Add(name);
            XElement year = new XElement("year", this.year); author.Add(year);
            XElement country = new XElement("country", this.country); author.Add(country);

            return author;
        }
    }
}

namespace LibraryApp
{
    internal class PublOffice
    {
        internal string? name { get; set; }
        internal string? city { get; set; }

        public PublOffice(string? name = null, string? city = null)
        {
            this.name = name;
            this.city = city;
        }

        public override string ToString()
        {
            return $"{name} {city}";
        }
    }

}
