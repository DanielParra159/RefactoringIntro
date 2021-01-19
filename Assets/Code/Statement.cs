using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Code
{
    public class Statement
    {
        private class StatementData
        {
            
        }
        private Dictionary<string, Play> _plays;
        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            _plays = plays;

            var statementData = new StatementData();
            return RenderPlainText(statementData, invoice);
        }

        private string RenderPlainText(StatementData data, Invoice invoice)
        {
            var result = $"Statement for {invoice.Customer}\n";
            foreach (var perf in invoice.Performances)
            {
                // print line for this order
                result += $"  {PlayFor(perf).Name}: {Usd(AmountFor(perf))} ({perf.Audience} seats)\n";
            }
            
            result += $"Amount owed is {Usd(TotalAmount(invoice))}\n";
            result += $"You earned {TotalVolumeCredits(invoice)} credits\n";
            return result;
        }

        private int TotalAmount(Invoice invoice)
        {
            var result = 0;
            foreach (var perf in invoice.Performances)
            {
                result += AmountFor(perf);
            }

            return result;
        }

        private int TotalVolumeCredits(Invoice invoice)
        {
            var volumeCredits = 0;
            foreach (var perf in invoice.Performances)
            {
                volumeCredits += VolumeCredits(perf);
            }

            return volumeCredits;
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
