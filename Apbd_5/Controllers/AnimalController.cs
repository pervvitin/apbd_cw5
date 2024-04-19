using Apbd_5.Database;

namespace Apbd_5.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/animals-controller")]
//[Route("[controller]")]
public class AnimalController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAnimals()
    {
        var animals = new MockDb().Animals;
        return Ok(animals);
    }
    
    [HttpPost]
    public IActionResult AddAnimals()
    {
        return Created();
    }
}