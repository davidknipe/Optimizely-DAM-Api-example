using EPiServer.Cms.WelcomeIntegration.Core.Internal;
using EPiServer.Framework;
using EPiServer.Framework.Cache;
using EPiServer.Framework.Initialization;
using Foundation.Features.OptimizelyDAM.REST;

namespace Foundation.Features.OptimizelyDAM;

[InitializableModule]
[ModuleDependency(typeof(InitializationModule))]
public class EnhanceFromDamInit : IInitializableModule
{
    private IContentEvents _contentEvents;
    private ServiceProviderHelper _serviceLocationHelper;
    private IContentLoader _contentLoader;

    public void Initialize(InitializationEngine initializationEngine)
    {
        _serviceLocationHelper = initializationEngine.Locate;
        _contentLoader = _serviceLocationHelper.Advanced.GetInstance<IContentLoader>();
        _contentEvents = _serviceLocationHelper.Advanced.GetInstance<IContentEvents>();
        _contentEvents.SavingContent += _contentEvents_SavingContent;
    }

    public void Uninitialize(InitializationEngine context)
    {
    }

    private void _contentEvents_SavingContent(object sender, ContentEventArgs e)
    {
        var nameOfImageReferenceProperty = "DamImage";
        if (e?.Content is StandardPage.StandardPage standardPage && standardPage.Property.Contains(nameOfImageReferenceProperty))
        {
            // Check there is a reference and also that its to the Optimizely DAM
            var imageReference = standardPage.Property[nameOfImageReferenceProperty].Value as ContentReference;
            if (imageReference != null && imageReference.ProviderName == "dam")
            {
                var imageContent = _contentLoader.Get<DAMImageAsset>(imageReference);
                if (imageContent.DAMUrl == null)
                {
                    return;
                }

                // The DAM url is a base64 encoded string of "/g=[DAM guid]" (no quotes)
                // so get the guid so the asset can be looked up in the DAM API
                var damAssetGuid = DecodeAssetGuid(imageContent.DAMUrl.Segments[1]);

                // Look up the asset in the DAM API
                var damClient = _serviceLocationHelper.Advanced.GetInstance<IDamClient>();
                var imageItem = damClient.GetAsset(damAssetGuid);

                // Update on page/block properties as required
                var imageHeight = imageItem.ImageResolution.Height;
                var imageWidth = imageItem.ImageResolution.Width;
                var description = imageItem.Description;

                // Update on page properties
                standardPage.ImageHeight = imageHeight;
                standardPage.ImageWidth = imageHeight;
                standardPage.ImageDescription = description;
            }
        }
    }

    private string DecodeAssetGuid(string base64EncodedData)
    {
        // Base 64 decode the URL path
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        var assetGuid = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        // Remove "g="
        if (assetGuid.Length >= 2)
        {
            assetGuid = assetGuid[2..];
        }

        // Return the Guid
        return assetGuid;
    }
}