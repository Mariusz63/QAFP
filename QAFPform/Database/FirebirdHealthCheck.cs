using Microsoft.Extensions.Diagnostics.HealthChecks;
using FirebirdSql.Data.FirebirdClient;

public class FirebirdHealthCheck : IHealthCheck
{
    private readonly string _cs;

    public FirebirdHealthCheck(IConfiguration config)
    {
        _cs = config.GetConnectionString("FirebirdConnection");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var con = new FbConnection(_cs);
            await con.OpenAsync(cancellationToken);

            await using var cmd =
                new FbCommand("SELECT 1 FROM RDB$DATABASE", con);
            await cmd.ExecuteScalarAsync(cancellationToken);

            return HealthCheckResult.Healthy("Firebird OK");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Firebird DOWN", ex);
        }
    }
}
