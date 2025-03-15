using StackExchange.Redis;

namespace WatchBuddy.Services.Basket.API.Services;

public class RedisService(string host, int port)
{
    private ConnectionMultiplexer _connectionMultiplexer;

    public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{host}:{port}");
    public IDatabase GetDatabase(int db = 1) => _connectionMultiplexer.GetDatabase(db);
}