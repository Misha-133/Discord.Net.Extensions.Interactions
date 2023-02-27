using Discord.Interactions;

namespace Discord.Net.Extensions.Interactions;

/// <summary>
///     An attribute used to determine to which guilds the <see cref="InteractionModuleBase {T}"/> module 
///     will be registered to with <see cref="InteractionServiceExtensions.RegisterCommandsAsync"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class GuildModuleAttribute : Attribute
{
    /// <summary>
    ///     Gets ids of guilds to register a module to.
    /// </summary>
    public ulong[] GuildsIds { get; }

    public GuildModuleAttribute(params ulong[] guildIds)
    {
        GuildsIds = guildIds;
    }
}