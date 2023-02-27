using Discord.Interactions;

namespace Discord.Net.Extensions.Interactions;

public static class InteractionServiceExtensions
{
    /// <summary>
    ///    Registers <see cref="InteractionModuleBase{T}"/> modules globally and to guilds set
    ///    with <see cref="GuildCommandAttribute"/>.
    /// </summary>
    /// <param name="service"></param>
    /// <param name="deleteMissing"></param>
    /// <returns></returns>
    public static async Task RegisterCommandsAsync(this InteractionService service, bool deleteMissing = true)
    {
        await service.AddModulesGloballyAsync(deleteMissing, service.Modules.Where
        (
            x => !x.Attributes.Any(attr => attr is GuildCommandAttribute)
        ).ToArray());

        var moduleGroups = new Dictionary<ulong, List<ModuleInfo>>();

        foreach (var module in service.Modules.Where(x => x.Attributes.Any(attr => attr is GuildCommandAttribute)))
        {
            var attribute = (GuildCommandAttribute)module.Attributes.First(x => x is GuildCommandAttribute);

            foreach (var guildId in attribute.GuildsIds)
            {
                if (moduleGroups.TryGetValue(guildId, out var guildIds))
                {
                    guildIds.Add(module);
                }
                else
                {
                    moduleGroups.Add(guildId, new() { module });
                }
            }
        }

        foreach (var group in moduleGroups)
        {
            await service.AddModulesToGuildAsync(group.Key, deleteMissing, group.Value.ToArray());
        }
    }
}