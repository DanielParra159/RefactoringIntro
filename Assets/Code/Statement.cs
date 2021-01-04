using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Code
{
    public class Statement
    {
        private Dictionary<string, Play> _plays;
        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            _plays = plays;
            
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            Func<float, string> format =
                value => value.ToString("C3",
                                        CultureInfo.CreateSpecificCulture("en-US")
                                       );
            foreach (var perf in invoice.Performances)
            {
                // add volume credits
                volumeCredits += Mathf.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (PlayFor(perf).Type == "comedy") volumeCredits += Mathf.FloorToInt(perf.Audience / 5f);

                // print line for this order
                result += $"  {PlayFor(perf).Name}: {format(AmountFor(perf) / 100f)} ({perf.Audience} seats)\n";
                totalAmount += AmountFor(perf);
            }

            result += $"Amount owed is {format(totalAmount / 100f)}\n";
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }

        private Play PlayFor(Performance perf)
        {
            var play = _plays[perf.PlayId];
            return play;
        }

        private int AmountFor(Performance aPerformance)
        {
            var result = 0;

            switch (PlayFor(aPerformance).Type)
            {
                case "tragedy":
                    result = 40000;
                    if (aPerformance.Audience > 30)
                    {
                        result += 1000 * (aPerformance.Audience - 30);
                    }

                    break;
                case "comedy":
                    result = 30000;
                    if (aPerformance.Audience > 20)
                    {
                        result += 10000 + 500 * (aPerformance.Audience - 20);
                    }

                    result += 300 * aPerformance.Audience;
                    break;
                default:
                    throw new Exception($"Unknown type: {PlayFor(aPerformance).Type}");
            }

            return result;
        }
    }
}
