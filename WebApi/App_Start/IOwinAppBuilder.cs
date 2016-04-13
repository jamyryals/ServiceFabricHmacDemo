namespace WebApiService
{
    using Owin;

    public interface IOwinAppBuilder
    {
        void Configuration(IAppBuilder appBuilder);
    }
}
