using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int[] cartridges = { 10, 50, 100 };

        int[] payOutAmounts = { 30, 50, 60, 80, 140, 230, 370, 610, 980, 100 };

        foreach (int payout in payOutAmounts)
        {
            Console.WriteLine($"Payout: {payout} EUR");
            List<List<int>> combinations = GeneratePayouts(cartridges, payout);
            PrintCombinations(combinations);
            Console.WriteLine();
        }

        Console.ReadLine();
    }

    static List<List<int>> GeneratePayouts(int[] cartridges, int payout)
    {
        List<List<int>> combinations = new List<List<int>>();
        CalculateCombinations(cartridges, payout, new List<int>(), combinations);
        return combinations;
    }

    static void CalculateCombinations(int[] cartridges, int payout, List<int> currentCombination, List<List<int>> combinations)
    {
        if (payout == 0)
        {
            combinations.Add(currentCombination);
            return;
        }

        if (payout < 0)
            return;

        for (int i = 0; i < cartridges.Length; i++)
        {
            int cartridge = cartridges[i];
            if (currentCombination.Count > 0 && cartridge < currentCombination[currentCombination.Count - 1])
                continue;

            List<int> newCombination = new List<int>(currentCombination);
            newCombination.Add(cartridge);
            CalculateCombinations(cartridges, payout - cartridge, newCombination, combinations);
        }
    }

    static void PrintCombinations(List<List<int>> combinations)
    {
        if (combinations.Count == 0)
        {
            return;
        }

        Console.WriteLine("Payout Possibilities");

        foreach (List<int> combination in combinations)
        {
            int index = 0;
            Dictionary<int, int> cartridgeCounts = new Dictionary<int, int>();
            foreach (int cartridge in combination)
            {
                if (cartridgeCounts.ContainsKey(cartridge))
                    cartridgeCounts[cartridge]++;
                else
                    cartridgeCounts.Add(cartridge, 1);
            }

            foreach (var item in cartridgeCounts)
            {
                Console.Write($"{item.Value} x {item.Key} EUR");
                if (index != (cartridgeCounts.Count - 1))
                {
                    Console.Write(" + ");
                }
                index++;
            }

            Console.WriteLine();
        }
    }
}
