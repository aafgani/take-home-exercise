using System;
using System.Linq;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace API;

public static class BlobUploadEndpoint
{
    public static RouteGroupBuilder MapBlobEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/blob");

        group.MapPost("/fileUpload", async (HttpContext context, string location, BlobServiceClient blobService) =>
        {
            try
            {
                var form = await context.Request.ReadFormAsync();
                var files = form.Files;
                var file = files.Any() && files.Count > 0 ? files[0] : null;

                if (file == null)
                    return Results.BadRequest("file not found");

                var containerClient = blobService.GetBlobContainerClient("tes");
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
                var newBlobName = $"{Guid.NewGuid().ToString()}.jpg";
                await containerClient.UploadBlobAsync(newBlobName, file.OpenReadStream());

                var response = "created";
                return Results.Created("FileUrl", response);
            }
            catch (System.Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        });

        return group;
    }

}
