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
