using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Statement
    {
        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementData = new CreateStatementData().Generate(invoice, plays);
            return RenderPlainText(statementData);
        }
        
        public string GenerateHtml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementData = new CreateStatementData().Generate(invoice, plays);
            return RenderHtml(statementData);
        }

        private string RenderPlainText(StatementData data)
        {
            var result = $"Statement for {data.Customer}\n";
            foreach (var perf in data.Performances)
            {
                // print line for this order
                result += $"  {perf.Play.Name}: {Usd(perf.Amount)} ({perf.Audience} seats)\n";
            }
            
            result += $"Amount owed is {Usd(data.TotalAmount)}\n";
            result += $"You earned {data.TotalVolumeCredits} credits\n";
            return result;
        }
        
        private string RenderHtml(StatementData data)
        {
            var result = $"<h1>{data.Customer}</h1>\n";
            result += "<table>\n";
            result += "<tr><th>play</th><th>seats</th><th>cost</th>\n";
            foreach (var perf in data.Performances)
            {
                // print line for this order
                result += $"<tr><td>{perf.Play.Name}</td><td>{perf.Audience}</td><td>{Usd(perf.Amount)}</td>\n";
            }
            result += "</table>\n";
            result += $"<p>Amount owed is <em>{Usd(data.TotalAmount)}</em></p>\n";
            result += $"<p>You earned <em>{data.TotalVolumeCredits}</em> credits</p>\n";
            return result;
        }

        private string Usd(float aNumber)
        {
            return (aNumber / 100f).ToString("C3", CultureInfo.CreateSpecificCulture("en-US"));
        }
        
    }
}
