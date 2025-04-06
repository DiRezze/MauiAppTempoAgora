using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MauiAppTempoAgora.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<(Tempo? tempo, HttpStatusCode statusCode)> GetPrevisao(string cidade) 
        {
            Tempo? t = null;

            string apiKey = "ddffd8ff98d22a861d714810ef7d58ee";
            string url = $"https://api.openweathermap.org/data/2.5/weather?" + 
                         $"q={cidade}&units=metric&appid={apiKey}";

            try 
                {
                
                using(HttpClient client = new())
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();

                        var sketch = JObject.Parse(json);

                        DateTime baseTime = DateTime.UnixEpoch;

                        DateTime time = new();

                        DateTime sunrise = time.AddSeconds((double)sketch["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = time.AddSeconds((double)sketch["sys"]["sunrise"]).ToLocalTime();

                        t = new()
                        {
                            lat = (double)sketch["coord"]["lat"],
                            lon = (double)sketch["coord"]["lon"],
                            description = (string)sketch["weather"][0]["description"],
                            main = (string)sketch["weather"][0]["main"],
                            temp_max = (double)sketch["main"]["temp_max"],
                            temp_min = (double)sketch["main"]["temp_min"],
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),
                            speed = (double)sketch["wind"]["speed"],
                            visibility = (int)sketch["visibility"],
                        };

                        return (t, resp.StatusCode);

                    }
                    else
                    {
                        return (null, resp.StatusCode);
                    }

                } // fecha using

            } // fecha try
            catch (HttpRequestException)
            {
                return (null, 0);
            }
            catch (Exception)
            {
                return (null, HttpStatusCode.InternalServerError);
            }


        }

    }

}
