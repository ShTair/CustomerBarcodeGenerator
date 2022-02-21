using Microsoft.Extensions.Configuration;

namespace CustomerBarcodeGenerator;

internal static class CommandLineConfigurationExtensions
{
    public static IConfigurationBuilder AddCommandLine(this IConfigurationBuilder builder, string[] args)
    {
        return builder.Add(new CommandLineConfigurationSource(args));
    }
}

internal class CommandLineConfigurationSource : IConfigurationSource
{
    private readonly string[] _args;

    public CommandLineConfigurationSource(string[] args)
    {
        _args = args;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new CommandLineConfigurationProvider(_args);
    }
}

internal class CommandLineConfigurationProvider : ConfigurationProvider
{
    private readonly string[] _args;

    public CommandLineConfigurationProvider(string[] args)
    {
        _args = args;
    }

    public override void Load()
    {
        if (_args.Length > 0) Data["PostCode"] = _args[0];
        if (_args.Length > 1) Data["Number"] = _args[1];
    }
}
