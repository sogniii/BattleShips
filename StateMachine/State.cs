using BattleShipGame;

namespace StateMachines;

public abstract class State {
    
    public StateMachine stateMachine;

    public State(StateMachine sm) => stateMachine = sm;
    public GameState gameState => stateMachine.GetGm();
    public abstract void OnEnter();
    public abstract void OnLeave();
    public abstract void InterceptInput(string cmd);
}