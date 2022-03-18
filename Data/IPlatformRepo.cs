using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        Platform GetPlatformById(int platId);
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatrofmr(Platform platform);    
    }
}