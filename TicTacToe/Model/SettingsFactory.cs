using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Model
{
    public static class SettingsFactory
    {
        public static IDisplaySettings GetDisplaySettings() => new DisplaySettingsProvider();
        public static IGameplaySettings GetGameplaySettings() => new GameplaySettingsProvider();
    }
}
