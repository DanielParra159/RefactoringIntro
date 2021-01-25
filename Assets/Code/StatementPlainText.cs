using System.Collections.Generic;

namespace Code
{
    public class StatementPlainText
    {
        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementData = new CreateStatementData().Generate(invoice, plays);
            return RenderPlainText(statementData);
        }


        private string RenderPlainText(StatementData data)
        {
            var result = $"Statement for {data.Customer}\n";
            foreach (var perf in data.Performances)
            {
                // print line for this order
                result += $"  {perf.Play.Name}: {FormatUtilities.Usd(perf.Amount)} ({perf.Audience} seats)\n";
            }

            result += $"Amount owed is {FormatUtilities.Usd(data.TotalAmount)}\n";
            result += $"You earned {data.TotalVolumeCredits} credits\n";
            return result;
        }
    }
}
