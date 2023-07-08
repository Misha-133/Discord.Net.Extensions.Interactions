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
        var registrableModules = service.Modules
            .Where(x => (x.IsSlashGroup && x.IsTopLevelGroup || !x.IsSubModule)
                        && !x.Attributes.Any(attr => attr is DontAutoRegisterAttribute)).ToArray();

        await service.AddModulesGloballyAsync(deleteMissing, registrableModules
            .Where(x => !x.Attributes.Any(attr => attr is GuildModuleAttribute)).ToArray());

        var moduleGroups = new Dictionary<ulong, List<ModuleInfo>>();

        foreach (var module in registrableModules.Where(x => x.Attributes.Any(attr => attr is GuildModuleAttribute))) 
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