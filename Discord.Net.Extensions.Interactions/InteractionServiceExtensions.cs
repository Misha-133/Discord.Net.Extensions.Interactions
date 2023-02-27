using Discord.Interactions;

namespace Discord.Net.Extensions.Interactions;

public static class InteractionServiceExtensions
{
    /// <summary>
    ///    Registers <see cref="InteractionModuleBase{T}"/> modules globally and to guilds set
    ///    with <see cref="GuildModuleAttribute"/>.
    /// </summary>
    /// <param name="service"></param>
    /// <param name="deleteMissing"></param>
    /// <returns></returns>
    public static async Task RegisterCommandsAsync(this InteractionService service, bool deleteMissing = true)
    {
        await service.AddModulesGloballyAsync(deleteMissing, service.Modules.Where
        (
            x => !x.Attributes.Any(attr => attr is GuildModuleAttribute) &&
                 !x.Attributes.Any(attr => attr is DontAutoRegisterAttribute)
        ).ToArray());

        var moduleGroups = new Dictionary<ulong, List<ModuleInfo>>();

        foreach (var module in service.Modules.Where(x => x.Attributes.Any(attr => attr is GuildModuleAttribute) &&
                                                          !x.Attributes.Any(attr => attr is DontAutoRegisterAttribute)))
        {
            var attribute = (GuildModuleAttribute)module.Attributes.First(x => x is GuildModuleAttribute);

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