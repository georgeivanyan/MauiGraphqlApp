using Microsoft.Extensions.Logging;
using MauiGraphqlApp.Data;

namespace MauiGraphqlApp;

public static class MauiProgram
{
    public static string GraphQLUrl = "api-crypto-workshop.chillicream.com/graphql";
    public static string baseHttp = "https://";
    public static string baseWss = "wss://";

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services
            .AddMauiGraphqlAppClient()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{baseHttp}{GraphQLUrl}"))
            .ConfigureWebSocketClient(c => c.Uri = new Uri($"{baseWss}{GraphQLUrl}"));
        builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
