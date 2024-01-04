using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHive.Shared.Models;
public class CurrencyModel
{
    public class Root
    {
        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class Rates
    {
        [JsonProperty("EUR")]
        public decimal EUR { get; set; }

        [JsonProperty("GBP")]
        public decimal GBP { get; set; }
    }
}
