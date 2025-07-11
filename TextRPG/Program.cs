using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int level = 1;
    static string name = "Kyo";
    static string job = "전사";
    static int baseAttack = 10;
    static int baseDefense = 5;
    static int hp = 100;
    static int gold = 800;

    static List<(string Name, bool Equipped, string EffectType, int EffectValue, string Description)> inventory =
        new List<(string, bool, string, int, string)>
        {
            ("무쇠갑옷", true, "방어력", 5, "무쇠로 만들어져 튼튼한 갑옷입니다."),
            ("스파르타의 창", true, "공격력", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다."),
            ("낡은 검", false, "공격력", 2, "쉽게 볼 수 있는 낡은 검 입니다.")
        };

    static List<(string Name, string EffectType, int EffectValue, string Description, int Price, bool Purchased)> shopItems =
        new List<(string, string, int, string, int, bool)>
        {
            ("수련자 갑옷", "방어력", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false),
            ("무쇠갑옷", "방어력", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, true),
            ("스파르타의 갑옷", "방어력", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false),
            ("낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600, true),
            ("청동 도끼", "공격력", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false),
            ("스파르타의 창", "공격력", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000, true)
        };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Status();
                    break;
                case "2":
                    Inventory();
                    break;
                case "3":
                    Shop();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                    Console.ReadKey();
                    break;
            }
            
        }
    }

    static void Status()
    {
        int bonusAttack = inventory.Where(i => i.Equipped && i.EffectType == "공격력").Sum(i => i.EffectValue);
        int bonusDefense = inventory.Where(i => i.Equipped && i.EffectType == "방어력").Sum(i => i.EffectValue);

        int totalAttack = baseAttack + bonusAttack;
        int totalDefense = baseDefense + bonusDefense;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {level:D2}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {totalAttack} {(bonusAttack > 0 ? $"(+{bonusAttack})" : "")}");
            Console.WriteLine($"방어력 : {totalDefense} {(bonusDefense > 0 ? $"(+{bonusDefense})" : "")}");
            Console.WriteLine($"체력 : {hp}");
            Console.WriteLine($"Gold : {gold}G");

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            else
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }
    }

    static void Inventory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            if (inventory.Count == 0)
            {
                Console.WriteLine(" (보유 중인 아이템이 없습니다.)");
            }
            else
            {
                foreach (var item in inventory)
                {
                    string prefix = item.Equipped ? "[E]" : "";
                    Console.WriteLine($"- {prefix}{item.Name}\t| {item.EffectType} +{item.EffectValue} | {item.Description}");
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "1") ManageEquip();
            else if (input == "0") break;
            else 
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }
    }

    static void ManageEquip()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            if (inventory.Count == 0)
            {
                Console.WriteLine(" (보유 중인 아이템이 없습니다.)");
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    var item = inventory[i];
                    string prefix = item.Equipped ? "[E]" : "";
                    Console.WriteLine($"- {i + 1} {prefix}{item.Name}\t| {item.EffectType} +{item.EffectValue} | {item.Description}");
                }
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            if (int.TryParse(input, out int index))
            {
                if (index >= 1 && index <= inventory.Count)
                {
                    var item = inventory[index - 1];
                    inventory[index - 1] = (item.Name, !item.Equipped, item.EffectType, item.EffectValue, item.Description);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }
    }

    static void Shop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{gold} G\n");

            Console.WriteLine("[아이템 목록]");
            foreach (var item in shopItems)
            {
                string status = item.Purchased ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"- {item.Name}\t| {item.EffectType} +{item.EffectValue}  | {item.Description} |  {status}");
            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            else if (input == "1") BuyItem();
            else
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }
    }

    static void BuyItem()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string status = item.Purchased ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"- {i + 1} {item.Name}\t| {item.EffectType} +{item.EffectValue}  | {item.Description} |  {status}");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            if (int.TryParse(input, out int index))
            {
                if (index >= 1 && index <= shopItems.Count)
                {
                    var item = shopItems[index - 1];

                    if (item.Purchased)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.\n");
                    }
                    else if (gold < item.Price)
                    {
                        Console.WriteLine("Gold가 부족합니다.\n");
                    }
                    else
                    {
                        gold -= item.Price;
                        shopItems[index - 1] = (item.Name, item.EffectType, item.EffectValue, item.Description, item.Price, true);
                        inventory.Add((item.Name, false, item.EffectType, item.EffectValue, item.Description));
                        Console.WriteLine("구매를 완료했습니다.\n");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }
    }
}