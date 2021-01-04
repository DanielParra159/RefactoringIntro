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
            foreach (var perf in invoice.Performances)
            {
                volumeCredits += VolumeCredits(perf);
                
                // print line for this order
                result += $"  {PlayFor(perf).Name}: {Usd(AmountFor(perf))} ({perf.Audience} seats)\n";
                totalAmount += AmountFor(perf);
            }

            result += $"Amount owed is {Usd(totalAmount)}\n";
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }

        private string Usd(float aNumber)
        {
            return (aNumber / 100f).ToString("C3", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private int VolumeCredits(Performance aPerformance)
        {
            var result = Mathf.Max(aPerformance.Audience - 30, 0);
            if (PlayFor(aPerformance).Type == "comedy") result += Mathf.FloorToInt(aPerformance.Audience / 5f);
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
