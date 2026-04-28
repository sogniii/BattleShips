using System.Text;
using StateMachines;

namespace BattleShipGame
{
    public class Game()
    {
        public static void Main()
        {
            GameState gameState = new GameState();
            StateMachine stateMachine = new(gameState);
            stateMachine.Add(new PlacementState(stateMachine));
            
            while (true)
            {
                string cmd = Console.ReadLine();

                if(!string.IsNullOrWhiteSpace(cmd))
                    stateMachine.Get().InterceptInput(cmd);

                if(gameState.WantExit)
                    return;

                if(gameState.WantRestart) {
                    gameState = new();
                    stateMachine = new(gameState);
                    stateMachine.Add(new PlacementState(stateMachine));
                }
            }
        }
    }
}