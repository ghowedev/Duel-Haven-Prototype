public enum Directions
{
    Down,
    DownRight,
    Right,
    UpRight,
    Up,
    UpLeft,
    Left,
    DownLeft
}

public enum PlayerState
{
    IDLE,
    CASTING,
    DISABLED
}

public enum CastType
{
    CAST,
    CHARGE,
    CHANNEL
}

public enum DisabledType
{
    STUN,
    KNOCKBACK,
    BREAK,
    ACTION
}

/*
public interface IState
{
    void Enter();
    void Exit();
    void Update();
    bool CanTransitionTo(PlayerState nextState);
}

public interface IStateManager
{
    bool ChangeState(PlayerState newState);
    PlayerState CurrentState { get; }
    bool IsInState(PlayerState state);
    bool CanEnterState(PlayerState state);
}
*/