
using System.Text.Json;

var animals = new List<Animal>();
var visits = new List<Visit>();
var nextAnimalId = 1;
var nextVisitId = 1;

var app = WebApplication.Create(args);

app.MapGet("/api/animals", () => Results.Ok(JsonSerializer.Serialize(animals)));

app.MapGet("/api/animals/{id}", (int id) =>
{
    var animal = animals.FirstOrDefault(a => a.Id == id);
    if (animal != null)
        return Results.Ok(JsonSerializer.Serialize(animal));
    return Results.NotFound();
});

app.MapPost("/api/animals", async (HttpContext context) =>
{
    var animal = await JsonSerializer.DeserializeAsync<Animal>(context.Request.Body);
    animal.Id = nextAnimalId++;
    animals.Add(animal);
    return Results.Created($"/api/animals/{animal.Id}", JsonSerializer.Serialize(animal));
});

app.MapPut("/api/animals/{id}", async (int id, HttpContext context) =>
{
    var existingAnimal = animals.FirstOrDefault(a => a.Id == id);
    if (existingAnimal == null)
        return Results.NotFound();

    var updatedAnimal = await JsonSerializer.DeserializeAsync<Animal>(context.Request.Body);
    existingAnimal.Name = updatedAnimal.Name;
    existingAnimal.Category = updatedAnimal.Category;
    existingAnimal.Weight = updatedAnimal.Weight;
    existingAnimal.CoatColor = updatedAnimal.CoatColor;

    return Results.Ok(JsonSerializer.Serialize(existingAnimal));
});

app.MapDelete("/api/animals/{id}", (int id) =>
{
    var existingAnimal = animals.FirstOrDefault(a => a.Id == id);
    if (existingAnimal == null)
        return Results.NotFound();

    animals.Remove(existingAnimal);
    return Results.NoContent();
});

app.MapGet("/api/visits/{animalId}", (int animalId) =>
{
    var animalVisits = visits.Where(v => v.AnimalId == animalId).ToList();
    return Results.Ok(JsonSerializer.Serialize(animalVisits));
});

app.MapPost("/api/visits", async (HttpContext context) =>
{
    var visit = await JsonSerializer.DeserializeAsync<Visit>(context.Request.Body);
    visit.Id = nextVisitId++;
    visits.Add(visit);
    return Results.Created($"/api/visits/{visit.Id}", JsonSerializer.Serialize(visit));
});

app.Run();

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string CoatColor { get; set; }
}

public class Visit
{
    public int Id { get; set; }
    public DateTime VisitDate { get; set; }
    public int AnimalId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
