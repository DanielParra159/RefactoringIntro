using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Code
{
    public class Statement
    {
        private class PerformanceData
        {
            public readonly string PlayId;
            public readonly int Audience;
            public readonly Play Play;
            public readonly int Amount;

            public PerformanceData(string playId, int audience, Play play, int amount)
            {
                PlayId = playId;
                Audience = audience;
                Play = play;
                Amount = amount;
            }
        }
        private class StatementData
        {
            public readonly string Customer;
            public readonly PerformanceData[] Performances;
            public readonly int TotalAmount;

            public StatementData(string customer, PerformanceData[] performances, int totalAmount)
            {
                Customer = customer;
                Performances = performances;
                TotalAmount = totalAmount;
            }
        }
        private Dictionary<string, Play> _plays;
        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            _plays = plays;

            var performancesData = EnrichPerformances(invoice);
            var statementData = new StatementData(invoice.Customer,
                                                  performancesData,
                                                  TotalAmount(performancesData));
            return RenderPlainText(statementData, invoice);
        }

        private PerformanceData[] EnrichPerformances(Invoice invoice)
        {
            var performancesData = new PerformanceData[invoice.Performances.Length];
            for (var i = 0; i < invoice.Performances.Length; i++)
            {
                var performance = invoice.Performances[i];
                performancesData[i] = new PerformanceData(performance.PlayId,
                                                          performance.Audience,
                                                          PlayFor(performance),
                                                          AmountFor(performance));
            }

            return performancesData;
        }

        private string RenderPlainText(StatementData data, Invoice invoice)
        {
            var result = $"Statement for {data.Customer}\n";
            foreach (var perf in data.Performances)
            {
                // print line for this order
                result += $"  {perf.Play.Name}: {Usd(perf.Amount)} ({perf.Audience} seats)\n";
            }
            
            result += $"Amount owed is {Usd(data.TotalAmount)}\n";
            result += $"You earned {TotalVolumeCredits(invoice)} credits\n";
            return result;
        }

        private int TotalAmount(PerformanceData[] performance)
        {
            var result = 0;
            foreach (var perf in performance)
            {
                result += perf.Amount;
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
