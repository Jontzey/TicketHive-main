using Newtonsoft.Json;
using TicketHive.Server.Enums;
using static TicketHive.Shared.Models.CurrencyModel;

namespace TicketHive.Client.Managers;

public static class CurrencyManager
{
    public static decimal RateGBP { get; set; }
    public static decimal RateEUR { get; set; }
    public static HttpClient HttpClient { get; set; } = new();

    public static async Task CurrencyApiCall()
    {
        string url = "https://api.apilayer.com/exchangerates_data/latest?symbols=EUR,GBP&base=SEK";

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        Console.WriteLine(requestMessage);

        requestMessage.Headers.Add("apikey", "SlU3Hqjihduz3zPkEUmc7HHHOq6GijSY");

        HttpResponseMessage responseMessage = await HttpClient.SendAsync(requestMessage);

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            Console.WriteLine(jsonResponse);

            Root? result = JsonConvert.DeserializeObject<Root>(jsonResponse);

            if (result != null)
            {
                RateEUR = result.Rates.EUR;
                RateGBP = result.Rates.GBP;
            }
        }
        else
        {
            Console.WriteLine(responseMessage.Content);
        }
    }

    public static decimal GetConvertedTicketPrice(Country customerCountry, decimal ticketPrice)
    {
        if (customerCountry.Equals(Country.Sweden))
        {
            return ticketPrice;
        }
        else if (customerCountry.Equals(Country.United_Kingdom))
        {
            return Math.Round(ticketPrice * RateGBP, 2);
        }
        else
        {
            return Math.Round(ticketPrice * RateEUR, 2);
        }
    }

    public static string GetCurrencyAbbreviation(Country customerCountry)
    {
        if (customerCountry.Equals(Country.Sweden))
        {
            return "SEK";
        }
        else if (customerCountry.Equals(Country.United_Kingdom))
        {
            return "£";
        }
        else
        {
            return "€";
        }
    }

}
