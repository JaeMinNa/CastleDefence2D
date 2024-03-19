public class PlayerStateContext
{
    public IPlayerState CurrentState
    {
        get; set;
    }

    private readonly PlayerController _enemyController;

    public PlayerStateContext(PlayerController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Transition()
    {
        CurrentState.Handle(_enemyController);
    }

    public void Transition(IPlayerState state)
    {
        CurrentState = state;
        CurrentState.Handle(_enemyController);
    }
}