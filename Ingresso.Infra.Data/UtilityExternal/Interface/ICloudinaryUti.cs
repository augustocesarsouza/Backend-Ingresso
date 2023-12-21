using Ingresso.Infra.Data.CloudinaryConfigClass;

namespace Ingresso.Infra.Data.UtilityExternal.Interface
{
    public interface ICloudinaryUti
    {
        public Task<CloudinaryCreate> CreateImg(string url, int width, int height);
    }
}
