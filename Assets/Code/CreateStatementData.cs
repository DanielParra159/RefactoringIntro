using System.Collections.Generic;
using System.Linq;

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
                var performanceCalculator = new PerformanceCalculatorFactory()
                   .Create(performance, PlayFor(performance));
                performancesData[i] = new PerformanceData(performance.PlayId,
                                                          performance.Audience,
                                                          performanceCalculator.Play,
                                                          performanceCalculator.AmountFor(),
                                                          performanceCalculator.VolumeCredits());
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

        private Play PlayFor(Performance perf)
        {
            var play = _plays[perf.PlayId];
            return play;
        }
    }
}
