abstract class GameSystem : MonoBehaviourSingleton<GameSystem>
{
    public bool active;

    public abstract void Init();
}