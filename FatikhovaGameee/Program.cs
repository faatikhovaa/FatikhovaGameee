using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatikhovaGameee
{

    
        class Program
    {
        static void Main(string[] args)
        {
            int health = 100;
            int gold = 50;
            int arrows = 20;
            List<string> inventary = new List<string>();

            string[] картаПодземелья = сгенирироватькарту();

            Console.WriteLine("Добро пожаловать в \"Числовой Квест PLUS\"!");

            for (int номерКомнаты = 0; номерКомнаты < картаПодземелья.Length; номерКомнаты++)
            {
                Console.WriteLine($"\nВы входите в комнату №{номерКомнаты + 1}...");
                string текущаяКомната = картаПодземелья[номерКомнаты];

                switch (текущаяКомната)
                {
                    case "Монстр":
                        (health, gold, arrows) = сражениесмонстром(health, gold, arrows);
                        break;
                    case "Ловушка":
                        health = попастьвловушку(health);
                        break;
                    case "Сундук":
                        (inventary, gold, arrows) = найтисундук(inventary, gold, arrows);
                        break;
                    case "Торговец":
                        gold = ВстретитьТорговца(gold, health);
                        break;
                    case "Пустая комната":
                        Console.WriteLine("Здесь абсолютно ничего нет. Тишина...");
                        break;
                    case "Босс":
                        (health, gold, arrows) = сражениесбоссом(health, gold, arrows);
                        break;
                }


                if (health <= 0)
                {
                    Console.WriteLine("\nВы пали в подземелье... Игра окончена.");
                    return;
                }

                if (номерКомнаты == картаПодземелья.Length - 1 && текущаяКомната == "Босс")
                {
                    Console.WriteLine("\nПоздравляем! Вы одолели Босса и прошли \"Числовой Квест PLUS\"!");
                    Console.WriteLine($"Ваши успехи: Здоровье: {health}, Золото: {gold}, Стрелы: {arrows}, Инвентарь: {string.Join(", ", inventary)}");
                    return;
                }


                Console.WriteLine($"Состояние: Здоровье: {health}, Золото: {gold}, Стрелы: {arrows}, Инвентарь: {string.Join(", ", inventary)}");
            }
        }


        static string[] сгенирироватькарту()
        {
            string[] картаПодземелья = new string[10];
            Random rnd = new Random();
            string[] события = { "Монстр", "Ловушка", "Сундук", "Торговец", "Пустая комната" };

            for (int i = 0; i < картаПодземелья.Length - 1; i++)
            {
                картаПодземелья[i] = события[rnd.Next(события.Length)];
            }

            картаПодземелья[9] = "Босс"; 
            return картаПодземелья;
        }

        static (int, int, int) сражениесмонстром(int health, int gold, int arrows)
        {
            Random rnd = new Random();
            int здоровьеМонстра = rnd.Next(25, 61); 

            Console.WriteLine("Внезапно! Перед вами возник злобный монстр!");

            while (health > 0 && здоровьеМонстра > 0)
            {
                Console.WriteLine($"Ваше здоровье: {health}, Здоровье монстра: {здоровьеМонстра}, Стрелы: {arrows}");
                Console.WriteLine("Выберите действие: (1) Атаковать мечом, (2) Стрелять из лука");

                if (arrows <= 0)
                {
                    Console.WriteLine("Стрелы закончились! Только меч!");
                }

                Console.Write("Ваш выбор: ");
                string выбор = Console.ReadLine();
                int уронИгрока = 0;

                switch (выбор)
                {
                    case "1": 
                        уронИгрока = rnd.Next(12, 23);
                        Console.WriteLine($"Вы рубите мечом и наносите {уронИгрока} урона!");
                        break;
                    case "2": 
                        if (arrows > 0)
                        {
                            уронИгрока = rnd.Next(7, 18);
                            arrows--;
                            Console.WriteLine($"Вы стреляете из лука, нанося {уронИгрока} урона! Осталось {arrows} стрел.");
                        }
                        else
                        {
                            Console.WriteLine("Стрел нет! Бесполезно.");
                        }
                        break;
                    default:
                        Console.WriteLine("Непонятно что вы сделали, пропускаете ход.");
                        break;
                }

                здоровьеМонстра -= уронИгрока;

                if (здоровьеМонстра <= 0)
                {
                    Console.WriteLine("Монстр повержен! Ура!");
                    int наградаЗолотом = rnd.Next(15, 36);
                    gold += наградаЗолотом;
                    Console.WriteLine($"Вы получаете {наградаЗолотом} золота!");
                    return (health, gold, arrows);
                }

                int уронМонстра = rnd.Next(7, 18);
                health -= уронМонстра;
                Console.WriteLine($"Монстр огрызается, нанося {уронМонстра} урона!");

                if (health <= 0)
                {
                    Console.WriteLine("Монстр вас одолел...");
                    return (health, gold, arrows);
                }
            }

            return (health, gold, arrows);
        }
        static (int, int, int) сражениесбоссом(int health, int gold, int arrows)
        {
            Random rnd = new Random();
            int здоровьеБосса = rnd.Next(70, 121);

            Console.WriteLine("Дрожите, путник! Перед вами сам БОСС!");

            while (health > 0 && здоровьеБосса > 0)
            {
                Console.WriteLine($"Ваше здоровье: {health}, Здоровье Босса: {здоровьеБосса}, Стрелы: {arrows}");
                Console.WriteLine("Выберите действие: (1) Атаковать мечом, (2) Стрелять из лука");

                if (arrows <= 0)
                {
                    Console.WriteLine("Стрелы? Забудьте. Только сталь!");
                }

                Console.Write("Ваш выбор: ");
                string выбор = Console.ReadLine();

                int уронИгрока = 0;

                switch (выбор)
                {
                    case "1": 
                        уронИгрока = rnd.Next(18, 29);
                        Console.WriteLine($"Вы яростно рубите мечом, нанося {уронИгрока} урона!");
                        break;
                    case "2": 
                        if (arrows > 0)
                        {
                            уронИгрока = rnd.Next(13, 24);
                            arrows--;
                            Console.WriteLine($"Стрела летит точно в цель, нанося {уронИгрока} урона! Осталось {arrows} стрел.");
                        }
                        else
                        {
                            Console.WriteLine("Зря на лук смотрите. Нет стрел.");
                        }
                        break;
                    default:
                        Console.WriteLine("Стоите как вкопанный, пропускаете ход.");
                        break;
                }

                здоровьеБосса -= уронИгрока;
                if (здоровьеБосса <= 0)
                {
                    Console.WriteLine("Босс повержен! Слава герою!");
                    int наградаЗолотом = rnd.Next(60, 121);
                    gold += наградаЗолотом;
                    Console.WriteLine($"Вы срываете с поверженного врага {наградаЗолотом} золота!");
                    return (health, gold, arrows);
                }

                int уронБосса = rnd.Next(13, 24);
                health -= уронБосса;
                Console.WriteLine($"Босс обрушивает на вас всю свою ярость, нанося {уронБосса} урона!");

                if (health <= 0)
                {
                    Console.WriteLine("Босс оказался сильнее...");
                    return (health, gold, arrows);
                }
            }

            return (health, gold, arrows);
        }
        static int попастьвловушку(int health)
        {
            Random rnd = new Random();
            int уронЛовушки = rnd.Next(12, 23);

            Console.WriteLine("О нет! Вы угодили в хитроумную ловушку!");
            health -= уронЛовушки;
            Console.WriteLine($"Ловушка отнимает {уронЛовушки} здоровья. Осталось {health}");

            return health;
        }
        static (List<string>, int, int) найтисундук(List<string> inventary, int health, int arrows)
        {
            Console.WriteLine("Чудесно! Перед вами древний сундук. Решите задачу, чтобы его открыть:");
            Random rnd = new Random();
            int число1 = rnd.Next(2, 13);
            int число2 = rnd.Next(2, 13);
            int действие = rnd.Next(1, 4);
            int верныйОтвет = 0;
            string знакДействия = "";

            switch (действие)
            {
                case 1:
                    знакДействия = "+";
                    верныйОтвет = число1 + число2;
                    break;
                case 2:
                    знакДействия = "-";
                    верныйОтвет = число1 - число2;
                    break;
                case 3:
                    знакДействия = "*";
                    верныйОтвет = число1 * число2;
                    break;
            }

            Console.Write($"Сколько будет: {число1} {знакДействия} {число2} = ");
            string ответ = Console.ReadLine();

            if (int.TryParse(ответ, out int ответИгрока) && ответИгрока == верныйОтвет)
            {
                Console.WriteLine("Браво! Замок открыт!");
                string[] награды = { "Зелье", "Золото", "Стрелы" };
                string награда = награды[rnd.Next(награды.Length)];

                switch (награда)
                {
                    case "Зелье":
                        if (inventary.Count < 5)
                        {
                            inventary.Add("Зелье");
                            Console.WriteLine("Найдено зелье! Отправляется в ваш рюкзак.");
                        }
                        else
                        {
                            Console.WriteLine("Зелье есть, но рюкзак полон.");
                        }
                        break;
                    case "Золото":
                        int наградаЗолотом = rnd.Next(25, 46);
                        gold += наградаЗолотом;
                        Console.WriteLine($"В сундуке оказалось {наградаЗолотом} золотых монет!");
                        break;
                    case "Стрелы":
                        int наградаСтрелами = rnd.Next(12, 24);
                        arrows += наградаСтрелами;
                        Console.WriteLine($"Получите {наградаСтрелами} стрел! Да пригодятся они вам...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Увы, неверно. Сундук заперт.");
            }
            return (inventary, gold, arrows);
        }
            static int ВстретитьТорговца(int золото, int health)
            {
                Console.WriteLine("Приветствую! Я - торговец. Что желаете?");
                Console.WriteLine($"У вас {золото} золота. Зелье исцеления - 30 золотых. Купите? (y/n)");
                string выбор = Console.ReadLine();

                if (выбор.ToLower() == "y")
                {
                    if (золото >= 30)
                    {
                        золото -= 30;
                        health += 35;
                        Console.WriteLine("Отлично! Зелье у вас. Здоровье восстановлено!");
                    }
                    else
                    {
                        Console.WriteLine("Не хватает золота, путник!");
                    }
                }
                else
                {
                    Console.WriteLine("Ну как знаете... Заходите еще!");
                }

                return золото;
            }
        }
}

