                    // Admin permission check
                    if (!await Permission.RequireAdministratorAsync(commandContext.Channel, commandContext.Member)) return;
