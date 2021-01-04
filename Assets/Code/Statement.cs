using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Code
{
    public class Statement
    {
        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            Func<float, string> format =
                value => value.ToString("C3",
                                        CultureInfo.CreateSpecificCulture("en-US")
                                       );
            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                var thisAmount = AmountFor(perf, play);

                // add volume credits
                volumeCredits += Mathf.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (play.Type == "comedy") volumeCredits += Mathf.FloorToInt(perf.Audience / 5f);

                // print line for this order
                result += $"  {play.Name}: {format(thisAmount / 100f)} ({perf.Audience} seats)\n";
                totalAmount += thisAmount;
            }

            result += $"Amount owed is {format(totalAmount / 100f)}\n";
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }

        private int AmountFor(Performance perf, Play play)
        {
            var thisAmount = 0;

            switch (play.Type)
            {
                case "tragedy":
                    thisAmount = 40000;
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }

                    break;
                case "comedy":
                    thisAmount = 30000;
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    thisAmount += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception($"Unknown type: {play.Type}");
            }

            return thisAmount;
        }
    }
}
