using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Console;
using System.Runtime.Serialization.Json;
using System.Diagnostics.Metrics;
using System.Runtime.Serialization;
using Microsoft.VisualBasic;
using System.Drawing;



/*Создайте приложение для работы с коллекцией стихов.
Необходимо хранить такую информацию:
 Название стиха
 ФИО автора
 Год написания
 Текст стиха
 Тема стиха
Приложение должно позволять:
 Добавлять стихи
 Удалять стихи
 Изменять информацию о стихах
 Искать стих по разным характеристикам
 Сохранять коллекцию стихов в файл
 Загружать коллекцию стихов из файла
*/
/*Добавьте к приложению из первого задания возможность генерировать отчёты. Отчёт может быть отображён на экран или сохранён в файл. Создайте такие отчёты:
 По названию стиха
 По ФИО автора
 По теме стиха*/

/*Добавьте к приложению из первого задания дополнительные отчёты:
 По слову в тексте стиха
 По году написания стиха
 По длине стиха*/


/*КОММЕНТАРИИ:::!!!
 ИСКАТЬ СТИХ ПО РАЗНЫМ ХАРАКТЕРИСТИКАМ- 
1 вариант по параметру , вторйо вариант поиск по найденому слову во всем стихе,включая имя , год , автора и т.д.

 */

namespace File_System_HW_Module_12
{

    [Serializable]
    public  class Poem
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
        public int Year { get; set; }
        public Poem() { }
        public Poem(string Name, string Author, string Text, string Theme, int year)
        {
            this.Name = Name;
            this.Author = Author;
            this.Text = Text;
            this.Theme = Theme;
            Year = year;
        }



        public void OutPutPoem()
        {
            Console.WriteLine("Name : " + Name);
            Console.WriteLine("Theme : " + Theme);
            Console.WriteLine("Text : " + Text);
            Console.WriteLine("Author : " + Author);
            Console.WriteLine("Year : " + Year);
            Console.WriteLine();

        }
        public void SetInfo(Tuple<string, string, string, string,int> PoemInfo_Name_Auth_Text_Theme_Year)
        {
            Name = PoemInfo_Name_Auth_Text_Theme_Year.Item1;
            Author = PoemInfo_Name_Auth_Text_Theme_Year.Item2;
            Text = PoemInfo_Name_Auth_Text_Theme_Year.Item3;
            Theme = PoemInfo_Name_Auth_Text_Theme_Year.Item4;
            Year = PoemInfo_Name_Auth_Text_Theme_Year.Item5;

        }
        public void SetInfo()
        {
            Console.WriteLine("Enter a name of Poem : ");

            this.Name = Console.ReadLine();
            Console.WriteLine("Enter an Author  of Poem : ");
            this.Author = Console.ReadLine();

            Console.WriteLine("Enter a Text of Poem : ");
            this.Text = Console.ReadLine();

            Console.WriteLine("Enter a Theme of Poem : ");
            this.Theme = Console.ReadLine();

            Console.WriteLine("Enter a Year of Poem : ");
            this.Year = int.Parse(Console.ReadLine());
        }
     


    }

    [Serializable]
    public  class CollectPoem
    {
        public List<Poem> poemList;
        private string path;

        public string NameCollect { get; set; }
        public CollectPoem(CollectPoem? collectPoem)
        {
            poemList = new List<Poem>();
        }
        public CollectPoem(List<Poem> list, string nameCollect)
        {
            this.poemList = list;
            NameCollect = nameCollect;
        }

        public CollectPoem(CollectPoem? collectPoem, string path) : this(collectPoem)
        {
            this.path = path;
        }
        public CollectPoem() { }
        public int GetSizePoemList() => poemList.Count;
        public void AddPoem(Poem poem)
        {
            if (poemList == null)
                poemList = new List<Poem>();

            poemList.Add(poem);
        }
        public void DelPoem()
        {
            OutputPoemList();
            Console.WriteLine("Choose a Poem : ");
            int choice = int.Parse(Console.ReadLine());
            poemList.RemoveAt(choice - 1);

        }
        public void OutputPoemList()
        {
            if (poemList == null)
            {
                Console.WriteLine("No Data ");
                return;
            }
            int count = 1;
            foreach (Poem poem in poemList)
            {
                Console.Write(count++ + " : ");
                poem.OutPutPoem();
            }
        }
        public void SearchHaracteristic()
        {
            Console.WriteLine("Choose a Parametr for searching : ");
            Console.WriteLine("\n1-Name\n2-Author\n3-WordInText\n4-Theme\n");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter a Name : ");
                        string key = Console.ReadLine();
                        var search = from poem in poemList
                                     where poem.Name == key
                                     select poem;
                        foreach (var poem in search)
                        {
                            poem.OutPutPoem();
                        }

                    }
                    break;
                case 2:

                    {
                        Console.WriteLine("Enter a Author : ");
                        string key = Console.ReadLine();
                        var search = from poem in poemList
                                     where poem.Author == key
                                     select poem;
                        foreach (var poem in search)
                        {
                            poem.OutPutPoem();
                        }

                    }

                    break;
                case 3:
                    {
                        Console.WriteLine("Enter a searching word : ");
                        string key = Console.ReadLine();
                        var search = from poem in poemList
                                     where poem.Text.Contains(key)
                                     select poem;
                        foreach (var poem in search)
                        {
                            poem.OutPutPoem();
                        }


                    }
                    break;
                case 4:
                    {
                        Console.WriteLine("Enter a Theme : ");
                        string key = Console.ReadLine();
                        var search = from poem in poemList
                                     where poem.Theme == key
                                     select poem;
                        foreach (var poem in search)
                        {
                            poem.OutPutPoem();
                        }


                    }
                    break;
            }
        }

        public void SearchNoHaracteristik()
        {

            Console.WriteLine("Enter a search word");
            string key = Console.ReadLine();
            var search = from poem in poemList
                         where poem.Author == key ||
                         poem.Text.Contains(key) ||
                         poem.Name == key ||
                         poem.Author == key
                         select poem;
            foreach (var poem in search)
                poem.OutPutPoem();
        }
        public void ChangePoemInfo()
        {


            int localChoice2 = 0;
            int localChoice = 0;
            do
            {
                Console.Clear();
                OutputPoemList();
                Console.WriteLine();
                Console.WriteLine("Choose a Poem  - 1,2,3 ... or push 0 for exit");
                localChoice = int.Parse(Console.ReadLine());
                Console.WriteLine("Choose an Option : ");
                Console.WriteLine("1-change name");
                Console.WriteLine("2-change author");
                Console.WriteLine("3-change text");
                Console.WriteLine("4-change theme");
                Console.WriteLine("0-Back");
                localChoice2 = int.Parse(Console.ReadLine());
                switch (localChoice2)
                {
                    case 1: { Console.WriteLine("Enter a name of Poem : "); poemList[localChoice - 1].Name = Console.ReadLine(); } break;
                    case 2: { Console.WriteLine("Enter a Author of Poem : "); poemList[localChoice - 1].Author = Console.ReadLine(); } break;
                    case 3: { Console.WriteLine("Enter a Text of Poem : "); poemList[localChoice - 1].Text = Console.ReadLine(); } break;
                    case 4: { Console.WriteLine("Enter a Theme of Poem : "); poemList[localChoice - 1].Theme = Console.ReadLine(); } break;
                    case 0: { } break;
                    default: { } break;
                        
                }

            } while (localChoice2 != 0 || localChoice!=0);
        }

        public void Report()
        {
            Console.WriteLine("Choose an option : ");
            Console.WriteLine("1-Report by name : ");
            Console.WriteLine("2-Report by author :");
            Console.WriteLine("3-Report by theme : ");
            Console.WriteLine("4-Report by word in text");
            Console.WriteLine("5-Report by year");
            Console.WriteLine("5-Report by Length of Poem");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: { int count = 0; foreach (var item in poemList) { Console.WriteLine(++count+" : "+item.Name); } } break;
                case 2: { int count = 0; foreach (var item in poemList) { Console.WriteLine(++count + " : "+item.Author); } } break;
                case 3: { int count = 0; foreach (var item in poemList) { Console.WriteLine(++count + " : "+item.Theme); } } break;
                case 4: 
                    {
                        Console.WriteLine("Enter a searching word :");
                        string KeyWord = Console.ReadLine();
                        var search = from poem in poemList
                                     where poem.Text.Contains(KeyWord)
                                     select poem;
                        foreach (var poem in search)
                        {
                            poem.OutPutPoem();
                        }

                    }break;
                case 5:
                    {
                        Console.WriteLine("Enter a searching year :");
                        int keyYear = int.Parse(Console.ReadLine());
                        var search = from poem in poemList
                                     where poem.Year == keyYear
                                     select poem;
                        foreach (var poem in search)
                        {
                            poem.OutPutPoem();
                        }
                    }break;
                case 6:
                    {
                        int count = 0;
                        Console.WriteLine("Report by year : ");
                        foreach (var poem in poemList)
                        {
                            Console.WriteLine(++count + " : " + poem.Name + "  " + poem.Text.Length+  "  symbols");
                        }
                    }
                    break;
                
                    }










                    foreach (var poem in poemList) { }
        }

    }



    [Serializable]
    class ClientInterface
    {

        CollectPoem collectPoem;
        ClientInterface(CollectPoem collectPoem)
        {
            this.collectPoem = collectPoem;
        }
        ClientInterface()
        {
            collectPoem = new CollectPoem();
        }
        public int MainMenu()
        {
            Console.WriteLine("Change an Option : ");
            Console.WriteLine("1-Show poem collection");
            Console.WriteLine("2-Add poem in collection");
            Console.WriteLine("3-Change poem info");
            Console.WriteLine("4-Search poem");
            Console.WriteLine("5-Save poem to file");
            Console.WriteLine("6-Download poem from file");
            Console.WriteLine("7-Reports");
            Console.WriteLine("0-Exit");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }
        public void MainMenu2()
        {
            int choice = 0;

            do
            {
                choice = MainMenu();
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            if (collectPoem.GetSizePoemList() == 0) { Console.WriteLine("Poem collection is Empty"); break; }
                            collectPoem.OutputPoemList();
                            Console.WriteLine();

                        }
                        break;

                    case 2:
                        {
                            Console.Clear();
                            if (collectPoem == null) { collectPoem = new CollectPoem(); }
                            collectPoem.AddPoem(new Poem());

                        }
                        break;
                    case 3: 
                        {
                            Console.Clear();
                            collectPoem.ChangePoemInfo();

                        } break;
                    case 4: 
                        {
                            Console.Clear();
                            Console.WriteLine("Choose an option : ");
                            Console.WriteLine("1-search with parametrs");
                            Console.WriteLine("2-search without parametrs (by word)");
                            int localChoice = int.Parse(Console.ReadLine());
                            switch(localChoice) 
                            {
                                case 1: collectPoem.SearchHaracteristic(); break;
                                case 2: collectPoem.SearchNoHaracteristik(); break;
                                default: Console.WriteLine("Incorrect choice");choice = 1; break;
                            }
                        } break;
                    case 5: 
                        {
                            Console.Clear();
                            Console.WriteLine("enter name of new file with  .Xml");
                            string path = Console.ReadLine();
                            FileStream stream = new FileStream(path, FileMode.Create);
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Poem>));
                            serializer.Serialize(stream, collectPoem.poemList);
                            Console.WriteLine("Serialized OK");
                            stream.Close();

                        } break;
                    case 6: 
                        {
                            Console.Clear();
                            Console.WriteLine("enter name of new file with  .Xml");
                            string path = Console.ReadLine();
                            FileStream stream = new FileStream(path, FileMode.Open);
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Poem>));

                            collectPoem = new CollectPoem((List<Poem>)serializer.Deserialize(stream) , "ResCollect");
                            
                            Console.WriteLine("DeSerialized OK");
                            stream.Close();
                        } break;
                    case 7:
                        {
                            Console.Clear();
                            collectPoem.Report();
                        }break;
                    case 0: { } break;
                    default: { choice = 1; } break;



                }

            } while (choice != 0);


        }

        internal class Program
        {



            static void Main(string[] args)
            {
                string text1 = " Some say the world will end in fire\n" +
                                "Some say in ice\n" +
                                "From what I’ve tasted of desire\n" +
                                "I hold with those who favor fire.\n" +
                                "But if it had to perish twice,\n" +
                                "I think I know enough of hate\n" +
                                "To say that for destruction ice\n" +
                                "Is also great\n" +
                                "And would suffice.\n";
                string text2 = " The way a crow\n" +
                                "Shook down on me\n" +
                                "The dust of snow\n" +
                                "From a hemlock tree\n" +
                                "Has given my heart\n" +
                                "A change of mood\n" +
                                "And saved some part\n" +
                                "Of a day I had rued.\n";

                string text3 = " I carry your heart with me\n" +
                               " (I carry it in my heart)\n" +
                               " I am never without it\n" +
                               " (Anywhere i go you go, my dear;\n" +
                               "And whatever is done by only me\n" +
                               " Is your doing, my darling)\n";







                Poem poem1 = new Poem("Love is", "Ejdelmann", text1, "Love is...",2003);
                Poem poem2 = new Poem("The way a crow", "Unknown Writer", text2, "About way...",2012);
                Poem poem3 = new Poem("text3", "Edward Estlin Cummings", text3, "Fire in heart...",2044);
                CollectPoem collectPoem = new CollectPoem();
                collectPoem.AddPoem(poem1);
                collectPoem.AddPoem(poem2);
                collectPoem.AddPoem(poem3);
              

                ClientInterface clientInterface = new ClientInterface(collectPoem);
                clientInterface.MainMenu2();










            }
        }
    }
}
