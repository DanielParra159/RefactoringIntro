using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class CreateStatementData
    {
        private Dictionary<string, Play> _plays;
        public StatementData Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            _plays = plays;
            return GenerateStatementData(invoice);
        }
        
        private StatementData GenerateStatementData(Invoice invoice)
        {
            var performancesData = EnrichPerformances(invoice);
            var statementData = new StatementData(invoice.Customer,
                                                  performancesData,
                                                  TotalAmount(performancesData),
                                                  TotalVolumeCredits(performancesData));
            return statementData;
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
                                                          AmountFor(performance),
                                                          VolumeCredits(performance));
            }

            return performancesData;
        }

        private int TotalAmount(PerformanceData[] performance)
        {
            return performance.Sum(perf => perf.Amount);
        }

        private int TotalVolumeCredits(PerformanceData[] performance)
        {
            return performance.Sum(perf => perf.VolumeCredits);
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
