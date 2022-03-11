using Service;

namespace TheyAreComing
{
    public interface IGameStateObserver
    {
        public void OnGameStateChange(GameState gameState);
    }
}