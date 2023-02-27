# Discord.Net.Extensions.Interactions

An extension that provides additional functionality to [Discord.Net](https://github.com/discord-net/Discord.Net).Interactions package.

## Features
- [guild-only command modules]()

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
