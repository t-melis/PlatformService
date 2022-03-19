using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platRepo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformRepo platRepo, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _platRepo = platRepo ?? throw new ArgumentNullException(nameof(platRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commandDataClient = commandDataClient ?? throw new ArgumentNullException(nameof(commandDataClient));
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
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
            
            _platRepo.CreatePlatrofmr(platform);
            _platRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"---> Could not send synchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetById), new { id = platformReadDto.Id} , platformReadDto);
            
        }
    }
}