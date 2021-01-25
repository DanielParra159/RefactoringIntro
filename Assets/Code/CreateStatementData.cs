using System;
using System.Collections.Generic;
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
                var performanceCalculator = new PerformanceCalculator(performance, PlayFor(performance));
                performancesData[i] = new PerformanceData(performance.PlayId,
                                                          performance.Audience,
                                                          performanceCalculator.Play,
                                                          performanceCalculator.AmountFor(),
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

        
    }
}
