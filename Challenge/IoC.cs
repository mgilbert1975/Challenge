using Challenge.Interfaces;
using Challenge.Services;

namespace Challenge
{
    public static class IoC
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IUserLogin, UserService>();
            services.AddTransient<IProvince, ProvinceService>();
            services.AddSingleton<Ilog4net, Log4NetService>();
        }
    }
}
