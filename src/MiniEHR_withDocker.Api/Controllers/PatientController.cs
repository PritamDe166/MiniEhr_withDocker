using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniEHR_withDocker.Api.Data;
using MiniEHR_withDocker.Api.Models;

namespace MiniEHR_withDocker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly MiniEhrDbContext _dbContext;

    public PatientController(MiniEhrDbContext dbContext, ILogger<PatientController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // GET api/patient
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
    {
        return await _dbContext.Patients.AsNoTracking().OrderBy(p => p.Id).ToListAsync();
    }

    // GET api/patient/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Patient>> GetPatientById(int id)
    {
        var patient = await _dbContext.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return patient is null ? NotFound() : patient;
    }
}
