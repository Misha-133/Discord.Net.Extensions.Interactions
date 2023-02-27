# Discord.Net.Extensions.Interactions

  <a href="https://www.nuget.org/packages/Discord.Net.Extensions.Interactions/">
    <img src="https://img.shields.io/nuget/vpre/Discord.Net.Extensions.Interactions.svg" alt="NuGet">
  </a>
  <a href="https://discord.gg/dnet">
    <img src="https://discord.com/api/guilds/848176216011046962/widget.png" alt="Discord">
  </a>

An extension that provides additional functionality to [Discord.Net](https://github.com/discord-net/Discord.Net).Interactions package.

## Features
- [guild-only command modules](https://github.com/Misha-133/Discord.Net.Extensions.Interactions#guild-only-commands)

## Examples
### Guild-only commands
Commands modules that inherit from `InteractionModuleBase` can be marked with a `[GuildModule]` attribute to register them only to guilds with provided ids, while all other modules will be registered globally.
```cs
[GuildModule(123456, 69696969, 1337)]
public class ExampleModule : InteractionModuleBase<SocketInteractionContext>
{

}
```

interactionService`RegisterCommandsAsync` extension method has be used to register commands. 
```cs
await interactionService.RegisterCommandsAsync();
```
