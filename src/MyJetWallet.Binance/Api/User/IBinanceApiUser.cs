using System;
using MyJetWallet.Binance.Api;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance
{
    public interface IBinanceApiUser : IDisposable
    {
        /// <summary>
        /// Get the API key.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Get or set the API request rate limiter.
        /// </summary>
        IApiRateLimiter RateLimiter { get; set; }

        /// <summary>
        /// Sign HTTP request parameters (query string concatenated with the request body).
        /// </summary>
        /// <param name="totalParams"></param>
        /// <returns></returns>
        string Sign(string totalParams);
    }
}
