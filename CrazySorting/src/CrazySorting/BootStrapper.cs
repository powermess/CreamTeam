namespace CrazySorting
{
    public class BootStrapper : AbstractBootstrapper
    {
        public override void Configure(IIoCContainer container)
        {
            container.RegisterSingleton<IGame, Game>();
        }
    }
}