//ACTION_NAME, joy_change, mana_change, health_change, fatigue_change, money_change

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.LoadConfig("config.txt");
        game.LoadGame("save.txt");

        while (true)
        {
            Console.Clear();
            game.DisplayStatus();
            game.DisplayActions();
            Console.Write("Выберите действие: ");
            string action = Console.ReadLine();
            game.ExecuteAction(action);
            game.SaveGame("save.txt");
        }
    }
}

class Game
{
    private Dictionary<string, ActionEffect> actions;
    private Player player;

    public Game()
    {
        actions = new Dictionary<string, ActionEffect>();
        player = new Player();
    }

    public void LoadConfig(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',');
            if (parts.Length == 6)
            {
                var action = parts[0].Trim();
                var joyChange = int.Parse(parts[1]);
                var manaChange = int.Parse(parts[2]);
                var healthChange = int.Parse(parts[3]);
                var fatigueChange = int.Parse(parts[4]);
                var moneyChange = int.Parse(parts[5]);

                actions[action] = new ActionEffect(joyChange, manaChange, healthChange, fatigueChange, moneyChange);
            }
        }
    }

    public void LoadGame(string filePath)
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            player.Health = int.Parse(lines[0]);
            player.Mana = int.Parse(lines[1]);
            player.Joy = int.Parse(lines[2]);
            player.Fatigue = int.Parse(lines[3]);
            player.Money = int.Parse(lines[4]);
        }
    }

    public void SaveGame(string filePath)
    {
        File.WriteAllLines(filePath, new string[]
        {
            player.Health.ToString(),
            player.Mana.ToString(),
            player.Joy.ToString(),
            player.Fatigue.ToString(),
            player.Money.ToString()
        });
    }

    public void DisplayStatus()
    {
        Console.WriteLine("Статус Валеры:");
        Console.WriteLine($"Здоровье: {player.Health}");
        Console.WriteLine($"Мана (Алкоголь): {player.Mana}");
        Console.WriteLine($"Жизнерадостность: {player.Joy}");
        Console.WriteLine($"Усталость: {player.Fatigue}");
        Console.WriteLine($"Деньги: {player.Money}");
        Console.WriteLine();
    }

    public void DisplayActions()
    {
        Console.WriteLine("Доступные действия:");
        foreach (var action in actions.Keys)
        {
            Console.WriteLine(action);
        }
    }

    public void ExecuteAction(string action)
    {
        if (actions.TryGetValue(action.ToUpper(), out ActionEffect effect))
        {
            if (CanPerformAction(action))
            {
                player.Joy += effect.JoyChange;
                player.Mana += effect.ManaChange;
                player.Health += effect.HealthChange;
                player.Fatigue += effect.FatigueChange;

                if (action.Equals("SING", StringComparison.OrdinalIgnoreCase))
                {
                    player.Money += effect.MoneyChange; 
                    if (player.Mana > 40 && player.Mana < 70) 
                    {
                        player.Money += 50; 
                    }
                }
                else
                {
                    player.Money += effect.MoneyChange; 
                }

                // Ограничения
                player.Health = Math.Clamp(player.Health, 0, 100);
                player.Mana = Math.Clamp(player.Mana, 0, 100);
                player.Joy = Math.Clamp(player.Joy, -10, 10);
                player.Fatigue = Math.Clamp(player.Fatigue, 0, 100);
                player.Money = Math.Max(player.Money, 0);

                Console.WriteLine($"Вы выполнили действие: {action}");
            }
            else
            {
                Console.WriteLine($"Невозможно выполнить действие: {action}");
            }
        }
        else
        {
            Console.WriteLine("Некорректное действие.");
        }

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    private bool CanPerformAction(string action)
    {
        if (action.Equals("WORK", StringComparison.OrdinalIgnoreCase))
        {
            return player.Mana < 50 && player.Fatigue < 10;
        }
        return true;
    }
}

class Player
{
    public int Health { get; set; } = 100;
    public int Mana { get; set; } = 0;
    public int Joy { get; set; } = 0;
    public int Fatigue { get; set; } = 0;
    public int Money { get; set; } = 0;
}

class ActionEffect
{
    public int JoyChange { get; }
    public int ManaChange { get; }
    public int HealthChange { get; }
    public int FatigueChange { get; }
    public int MoneyChange { get; }

    public ActionEffect(int joyChange, int manaChange, int healthChange, int fatigueChange, int moneyChange)
    {
        JoyChange = joyChange;
        ManaChange = manaChange;
        HealthChange = healthChange;
        FatigueChange = fatigueChange;
        MoneyChange = moneyChange;
    }
}
