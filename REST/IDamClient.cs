using Foundation.Features.OptimizelyDAM.REST.Media;

namespace Foundation.Features.OptimizelyDAM.REST
{
    public interface IDamClient
    {
        ImageItem GetAsset(string id);
    }
}