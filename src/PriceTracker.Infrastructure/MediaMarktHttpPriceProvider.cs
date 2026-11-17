using PriceTracker.Domain;
using System.Text.Json;

namespace PriceTracker.Infrastructure
{
    public class MediaMarktHttpPriceProvider : IPriceProvider
    {
        private readonly HttpClient _httpClient;
        public MediaMarktHttpPriceProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PriceData?> GetPriceAsync(string url)
        {
            string html;
            try
            {
                html = await _httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException)
            {
                return null;
            }
            var document = new AngleSharp.Html.Parser.HtmlParser().ParseDocument(html);
            var element = document.QuerySelector("script[type='application/ld+json']");
            if (element == null)
            {
                return null;
            }

            using var jsonDocument = JsonDocument.Parse(element.TextContent);

            if (!jsonDocument.RootElement.TryGetProperty("object", out var productElement))
            {
                return null;
            }
            if (!productElement.TryGetProperty("offers", out var offersElement))
            {
                return null;
            }
            if (!offersElement.TryGetProperty("price", out var priceElement))
            {
                return null;
            }

            decimal? crossedOutPrice = null;
            bool isOnPromotion = false;
            if(offersElement.TryGetProperty("priceSpecification", out var priceSpecificationElement))
            {
                foreach (var specification in priceSpecificationElement.EnumerateArray())
                {
                    if(specification.TryGetProperty("priceType", out var priceTypeElement)
                        && priceTypeElement.GetString() == "https://schema.org/StrikethroughPrice")
                    {
                        if (specification.TryGetProperty("price", out var crossedOutPriceElement))
                        {
                            crossedOutPrice = crossedOutPriceElement.GetDecimal();
                            isOnPromotion = true;
                            break;
                        }
                    }
                }
            }

            decimal? declaredLowestPrice = null;
            var lowestPriceSpan = document.QuerySelector("[data-test='mms-strike-price-type-lop'] .mms-ui-mBgaT");
            if(lowestPriceSpan != null)
            {
                declaredLowestPrice = decimal.Parse(lowestPriceSpan.TextContent.Replace("zł", "").Trim(), 
                    System.Globalization.CultureInfo.GetCultureInfo("pl-PL"));
            }

            return new PriceData
            {
                Price = priceElement.GetDecimal(),
                CrossedOutPrice = crossedOutPrice,
                DeclaredLowestPrice = declaredLowestPrice,
                IsOnPromotion = isOnPromotion,
            };
        }
    }
}
