using System;
using System.Linq;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace API;

public static class BlobUploadEndpoint
{
    public static RouteGroupBuilder MapStorageEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/storage");

        group.MapPost("/blobUpload", async (HttpContext context, string location, BlobServiceClient? blobService) =>
        {
            try
            {
                if (blobService == null)
                    return Results.BadRequest("service unavailable");

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

        group.MapPost("/queue", async (HttpContext context, string message, QueueServiceClient? queueService) =>
       {
           try
           {
               if (queueService == null)
                   return Results.BadRequest("service unavailable");

               if (string.IsNullOrEmpty(message))
                   return Results.BadRequest("message can't be blank");

               var queueClient = queueService.GetQueueClient("tes");
               await queueClient.CreateIfNotExistsAsync();

               await queueClient.SendMessageAsync($"{message}");

               var response = "created";
               return Results.Created("", response);
           }
           catch (System.Exception ex)
           {
               return Results.Problem(ex.Message);
           }

       });

        return group;
    }

}
