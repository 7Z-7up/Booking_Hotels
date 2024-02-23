using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

public class StartupFilterHelperService : IStartupFilter
{
    private readonly Action<IWebHostEnvironment> _configureAction;

    public StartupFilterHelperService(Action<IWebHostEnvironment> configureAction)
    {
        _configureAction = configureAction;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            // Perform any additional configuration before the next action
            _configureAction?.Invoke(builder.ApplicationServices.GetRequiredService<IWebHostEnvironment>());

            // Continue with the next action in the pipeline
            next(builder);
        };
    }
}
