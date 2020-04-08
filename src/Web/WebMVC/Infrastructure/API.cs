using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public static class API
    {
        public static class WhiskyRecord
        {
            public static string GetWhisky(string baseUri, string whiskyId)
            {
                return $"{baseUri}/whiskies/{whiskyId}";
            }

            public static string GetAllWhiskies(string baseUri)
            {
                return $"{baseUri}/whiskies/getallwhiskies";
            }

            public static string GetWhiskies(string baseUri, int page, int take, string name, string distillery, string bottler,
            string vintage, string bottled, int? statedAge, string caskType,
            string caskNumber, int? numberOfBottles, float? strength, int? size, string market)
            {
                var filterQs = "";

                if (!string.IsNullOrWhiteSpace(name))
                {
                    filterQs += $"&name={name}";
                }
                if (!string.IsNullOrWhiteSpace(distillery))
                {
                    filterQs += $"&distillery={distillery}";
                }
                if (!string.IsNullOrWhiteSpace(bottler))
                {
                    filterQs += $"&bottler={bottler}";
                }
                if (!string.IsNullOrWhiteSpace(vintage))
                {
                    filterQs += $"&vintage={vintage}";
                }
                if (!string.IsNullOrWhiteSpace(bottled))
                {
                    filterQs += $"&bottled={bottled}";
                }
                if (statedAge.HasValue)
                {
                    filterQs += $"&statedAge={statedAge.ToString()}";
                }
                if (!string.IsNullOrWhiteSpace(caskType))
                {
                    filterQs += $"&caskType={caskType}";
                }
                if (!string.IsNullOrWhiteSpace(caskNumber))
                {
                    filterQs += $"&caskNumber={caskNumber}";
                }
                if (numberOfBottles.HasValue)
                {
                    filterQs += $"&numberOfBottles={numberOfBottles.ToString()}";
                }
                if (strength.HasValue)
                {
                    filterQs += $"&strength={strength.ToString()}";
                }
                if (size.HasValue)
                {
                    filterQs += $"&size={size.ToString()}";
                }
                if (!string.IsNullOrWhiteSpace(market))
                {
                    filterQs += $"&market={market}";
                }

                return $"{baseUri}/whiskies/items?pageindex={page}&pageSize={take}{filterQs}";
            }


            public static string CreateWhiskyRecord(string baseUri)
            {
                return $"{baseUri}/whiskies/createwhiskyrecord";
            }

            public static string UpdateWhiskyRecord(string baseUri)
            {
                return $"{baseUri}/whiskies/updatewhiskyrecord";
            }

            public static string UpdateWhiskyPrice(string baseUri)
            {
                return $"{baseUri}/whiskies/updatewhiskyprice";
            }
            public static string UpdateWhiskyImage(string baseUri)
            {
                return $"{baseUri}/whiskies/updatewhiskyimage";
            }

            public static string GetAllDistilleries(string baseUri)
            {
                return $"{baseUri}/distilleries/getalldistilleries";
            }
        }


    }
}
