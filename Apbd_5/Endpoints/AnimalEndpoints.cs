using Apbd_5.Database;
using Apbd_5.Models;

namespace Apbd_5;

public static class AnimalEndpoints
{
    public static void MapAnimalEndpoints(this WebApplication app)
    {
        app.MapGet("/animals",()=>
        {
//200 - ok
//400 - bad reequest
//481 - unauthorized
//483 - forbidden
//484 - not found
//500 - Internal Server

            var animals = StaticData.Animals;
            return Results.Ok();
        });

        app.MapGet("/animals/{id}",(int id)=>
        {
            return Results.Ok(id);
        });

        app.MapPost("/animals", (Animal animal ) =>
        {
            //201 - created
            return Results.Created("",animal);
        });
    }
}