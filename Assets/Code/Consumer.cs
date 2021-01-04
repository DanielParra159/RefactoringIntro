using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class Consumer : MonoBehaviour
    {
        private void Awake()
        {
            var performances = new[]
                               {
                                   new Performance("hamlet", 55),
                                   new Performance("as-like", 35),
                                   new Performance("othello", 40),
                               };
            var invoice = new Invoice("BigCo", performances);
            var statement = new Statement();
            var plays = new Dictionary<string, Play>
                        {
                            {"hamlet", new Play("Hamlet", "tragedy")},
                            {"as-like", new Play("As You Like It", "comedy")},
                            {"othello", new Play("Othello", "tragedy")},
                        };
            var result = statement.Generate(invoice, plays);

            Debug.Log(result);
        }
    }
}
