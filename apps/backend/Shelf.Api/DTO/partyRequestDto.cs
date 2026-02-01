using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Shelf.Core.Enum;

namespace Shelf.Api.DTO;

public sealed class CreatePartyRequest
{
    public required string Name { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required PartyType PartyType { get; init; }
}


public sealed class CreatePartyResponse
{
    public required Guid PartyId { get; init; }
    public required string PartyName { get; init; }
    public required PartyType PartyType { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
