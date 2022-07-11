using Microsoft.Extensions.DependencyInjection;
using Warships.Game.Board.Description;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Engines.Game;
using Warships.Game.Engines.Players;
using Warships.Game.Engines.Warships;
using Warships.Game.Interfaces.BoardRenderer;
using Warships.Game.Interfaces.InputOutput;
using Warships.Game.Runner;
using Warships.Game.Ships.Descriptions;
using Warships.Game.Ships.Descriptions.Interfaces;
using Warships.Game.Ships.Factory;
using Warships.Game.Ships.Factory.Interfaces;
using Warships.Infrastructure.BoardRenderer;
using Warships.Infrastructure.InputOutput;
using Warships.Infrastructure.Wrappers;

namespace Warships
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            return new ServiceCollection()
                .AddSingleton<IConsole, ConsoleWrapper>()
                .AddSingleton<IGameRunner, GameRunner>()
                .AddSingleton<IGameInputOutput, ConsoleGameInputOutput>()
                .AddSingleton<IGameEngine, GameEngine>()
                .AddSingleton<IPlayerEngine, PlayerEngine>()
                .AddSingleton<IWarshipFactory, WarshipFactory>()
                .AddSingleton<IWarshipDescription, DestroyerDescription>()
                .AddSingleton<IWarshipDescription, BattleshipDescription>()
                .AddSingleton<IWarshipDescription, DestroyerDescription>()
                .AddSingleton<IWarshipsEngine, WarshipsEngine>()
                .AddSingleton<IBoardDescription, DefaultBoardDescription>()
                .AddSingleton<IBoardRenderer, BoardRenderer>()
                .BuildServiceProvider();
        }
    }
}