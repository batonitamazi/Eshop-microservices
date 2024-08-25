﻿
namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, List<string> Category,  string ImageFile, decimal Price);
    
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest req, ISender sender) =>
            {
                var command = req.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create a new Product");
        }
    }
}
