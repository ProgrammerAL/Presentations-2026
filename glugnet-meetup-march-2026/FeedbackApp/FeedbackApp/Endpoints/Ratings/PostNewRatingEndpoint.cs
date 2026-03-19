using System.ComponentModel.DataAnnotations;

using FeedbackApp.Services;

using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Endpoints.Ratings;

public static class PostNewRatingEndpoint
{
    public static RouteHandlerBuilder RegisterApiEndpoint(RouteGroupBuilder group)
    {
        return group.MapPost("/new-rating",
            async ([FromBody, Required] PostNewRatingRequestBody requestBody,
            IRatingsService ratingsService) =>
            {
                var comments = requestBody.Comments ?? "";
                await ratingsService.StoreNewRatingAsync(requestBody.Rating, comments);

                return TypedResults.NoContent();
            })
            .WithSummary("Creates and persists a new rating");
    }

    public class PostNewRatingRequestBody
    {
        [Required, Range(1, 5), FromBody]
        public required int Rating { get; init; }

        [FromBody]
        public string? Comments { get; init; }
    }
}
