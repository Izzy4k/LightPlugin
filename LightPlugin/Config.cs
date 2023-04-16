using Exiled.API.Interfaces;

namespace LightPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get ; set; } = true;
    }
}
