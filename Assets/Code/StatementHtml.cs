using System.Collections.Generic;

namespace Code
{
    public class StatementHtml
    {
        public string GenerateHtml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementData = new CreateStatementData().Generate(invoice, plays);
            return RenderHtml(statementData);
        }

        private string RenderHtml(StatementData data)
        {
            var result = $"<h1>{data.Customer}</h1>\n";
            result += "<table>\n";
            result += "<tr><th>play</th><th>seats</th><th>cost</th>\n";
            foreach (var perf in data.Performances)
            {
                // print line for this order
                result += $"<tr><td>{perf.Play.Name}</td><td>{perf.Audience}</td><td>{FormatUtilities.Usd(perf.Amount)}</td>\n";
            }

            result += "</table>\n";
            result += $"<p>Amount owed is <em>{FormatUtilities.Usd(data.TotalAmount)}</em></p>\n";
            result += $"<p>You earned <em>{data.TotalVolumeCredits}</em> credits</p>\n";
            return result;
        }
    }
}
