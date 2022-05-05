﻿using SocketComm;
using static SocketComm.Cmd;

if (args.Length < 1)
{
    return;
}
FPipe p = new FPipe(args[0]);
await p.ReadCmdAsync();
await p.WriteCmdAsync(Error);
await foreach (var cmd in p)
{
    Console.WriteLine($"Received: {cmd}");
    Console.Write($"Replying [{cmd}]..");
    await p.WriteCmdAsync(cmd);
    Console.WriteLine(".OK");
}