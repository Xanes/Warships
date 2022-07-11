// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Warships;
using Warships.Game.Runner;

var provider = Startup.ConfigureService();
IGameRunner gameRunner = provider.GetRequiredService<IGameRunner>();
gameRunner.StartGame();
