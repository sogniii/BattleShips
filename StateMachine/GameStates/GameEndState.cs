namespace StateMachines
{
    
    public class GameEndState : State {

        public GameEndState(StateMachine sm) : base(sm) => stateMachine = sm;

        public override void OnEnter()
        {
            Console.WriteLine("Game's over!");
            Console.WriteLine("Press 'e' to exit!");
            Console.WriteLine("Press 'r' to retry!");
        }

        public override void OnLeave()
        {
            //throw new NotImplementedException();
        }

        public override void InterceptInput(string cmd)
        {
            switch(cmd[0]) {
            
            case 'e':
                gameState.Exit();
                break;

            case 'r':
                gameState.Restart();
                break;
            }
        }
    }
}