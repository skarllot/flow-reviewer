using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Ciandt.FlowTools.FlowReviewer.Flow.Models.v1;

namespace Ciandt.FlowTools.FlowReviewer.Flow.AmazonBedrock.Models.v1;

/// <param name="AllowedModels">The list of models to use for this request.</param>
/// <param name="Messages">The input messages.</param>
/// <param name="System">
/// <para>The system prompt for the request.</para>
/// <para>A system prompt is a way of providing context and instructions to Anthropic Claude, such as specifying a particular goal or role.</para>
/// </param>
/// <param name="AnthropicVersion">The anthropic version.</param>
/// <param name="Temperature">The amount of randomness injected into the response, between 0 and 1.</param>
/// <param name="MaxTokens">The maximum number of tokens to generate before stopping.</param>
public sealed record BedrockChatCompletionRequest(
    [property: JsonPropertyName("allowedModels")] ImmutableList<AllowedModel> AllowedModels,
    [property: JsonPropertyName("messages")] ImmutableList<Message> Messages,
    [property: JsonPropertyName("system")] string? System = null,
    [property: JsonPropertyName("anthropic_version")] string AnthropicVersion = "bedrock-2023-05-31",
    [property: JsonPropertyName("temperature")] float? Temperature = null,
    [property: JsonPropertyName("max_tokens")] int MaxTokens = 4096);