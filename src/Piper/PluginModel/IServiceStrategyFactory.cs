namespace Piper.PluginModel
{
    public interface IServiceStrategyFactory
    {
        string StrategyName { get; }
        IServiceStrategy Create();
    }
}
