namespace Foundation.Features.OptimizelyDAM;

/// <summary>
/// Options class to configure the DAM Api options
/// </summary>
/// <example>
/// Use the following in appsettings.json to configure:
/// {
///    "EPiServer": {
///        //Other config
///        "OptimizelyDamOptions": {
///            "ClientId": "Client Id",
///            "ClientSecret": "Secret stuff"
///        }
///    }
///}
/// </example>
[Options]
public class OptimizelyDamOptions
{
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }
}