using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platRepo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platRepo, IMapper mapper)
        {
            _platRepo = platRepo ?? throw new ArgumentNullException(nameof(platRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _platRepo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public ActionResult<PlatformReadDto> GetById(int platId)
        {
            var platform = _platRepo.GetPlatformById(platId);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
            
            _platRepo.CreatePlatrofmr(platform);
            _platRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            return CreatedAtRoute(nameof(GetById), new { id = platformReadDto.Id} , platformReadDto);
            
        }
    }
}