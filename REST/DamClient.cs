using EPiServer.Framework.Cache;
using Foundation.Features.OptimizelyDAM.REST.Authorization;
using Foundation.Features.OptimizelyDAM.REST.Media;

namespace Foundation.Features.OptimizelyDAM.REST
{
    public class DamClient : IDamClient
    {
        private readonly ISynchronizedObjectInstanceCache _cache;
        private readonly SynchronousWebClient _httpClient;
        private readonly OptimizelyDamOptions _options;

        public DamClient(ISynchronizedObjectInstanceCache cache, OptimizelyDamOptions options)
        {
            _cache = cache;
            _options = options;
            _httpClient = new SynchronousWebClient();
        }

        public ImageItem GetAsset(string id)
        {
            var cacheKey = string.Format(OptimizelyDamConstants.GetImagesPath, id);
            var asset = this._cache.Get<ImageItem>(cacheKey, ReadStrategy.Immediate);
            if (asset == null)
            {
                _httpClient.SetAccessToken(GetToken());
                asset = _httpClient.Get<ImageItem>(FormatRequestUrl(string.Format(OptimizelyDamConstants.GetImagesPath, id)));
                var genericItem = _httpClient.Get<object>(FormatRequestUrl(string.Format(OptimizelyDamConstants.GetImagesPath, id)));
                if (asset != null)
                {
                    _cache.Insert(cacheKey, asset, new CacheEvictionPolicy(TimeSpan.FromMinutes(1), CacheTimeoutType.Absolute));
                }
            }
            return asset;
        }

        private string GetToken()
        {
            var responseToken = this._cache.Get<AuthorizationResponse>(OptimizelyDamConstants.TokenCacheKey, ReadStrategy.Immediate);
            if (responseToken != null)
            {
                return responseToken.AccessToken;
            }

            var authorizationRequest = new AuthorizationRequest(_options.ClientId, _options.ClientSecret);
            responseToken = _httpClient.Post<AuthorizationRequest, AuthorizationResponse>(FormatRequestUrl("o/oauth2/v1/token", OptimizelyDamConstants.AccountsBaseUrl), authorizationRequest);
            if (responseToken != null)
            {
                _cache.Insert(OptimizelyDamConstants.TokenCacheKey, responseToken, new CacheEvictionPolicy(TimeSpan.FromMinutes(50), CacheTimeoutType.Absolute));
                return responseToken.AccessToken;
            }

            return null;
        }

        private string FormatRequestUrl(string url, string baseUrl = OptimizelyDamConstants.APIBaseUrl) =>
            string.Concat(baseUrl, url);
    }
}