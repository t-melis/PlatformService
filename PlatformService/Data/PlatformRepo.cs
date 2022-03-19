using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly ApplicationDbContext _context;
        public PlatformRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreatePlatrofmr(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException();
            }

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int platId)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == platId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}