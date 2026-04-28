using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using BattleShipGame;

namespace StateMachines;

public class StateMachine {
    
    GameState gameState;
    Stack<State> States;
    public StateMachine(GameState state) {
        
        gameState = state;
        States = new();
    }

    public GameState GetGm() => gameState;
    public State Add(State state) {
        
        if(States.Count > 0)
            Get()?.OnLeave();

        state.OnEnter();
        States.Push(state);
        return state;
    } 

    public State Get() => States.Peek();
    public State Remove() => States.Pop();
}