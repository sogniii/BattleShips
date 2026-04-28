using System.Numerics;
using StateMachines;

namespace BattleShipGame
{
    public class AttackState : State
    {
        public AttackState(StateMachine sm) : base(sm) => stateMachine = sm;

        public override void OnEnter()
        {
            Console.WriteLine("Attack your enemy's ships");
            gameState.PrintState(gameState.Enemy, gameState.Enemy);
            //throw new NotImplementedException();
        }

        public override void OnLeave()
        {
            //throw new NotImplementedException();
        }

        public override void InterceptInput(string cmd)
        {
            switch(cmd[0]) {
            
                case 's':
                    gameState.MoveMarker(new(0, 1));
                    break;

                case 'w':
                    gameState.MoveMarker(new(0, -1));
                    break;

                case 'a':
                    gameState.MoveMarker(new(-1, 0));
                    break;

                case 'd':
                    gameState.MoveMarker(new(1, 0));
                    break;

                case 'x':
                    Vector2 Marker = gameState.GetMarker();
                    if(gameState.Player.CanAttackShip(Marker)) {
                        gameState.Enemy.TryAttackShip(Marker);   
                        gameState.Player.HitSpots[(int)Marker.X + (int)gameState.Dimensions.X * (int)Marker.Y] = true;

                        TurnEnd();
                    }
                    break;
            }

            gameState.PrintState(gameState.Player, gameState.Enemy);

        }

        void EnemyTurn() {
            
            while(true) {
                
                Vector2 RandomSpot = new(Random.Shared.NextInt64((int)gameState.Dimensions.X), Random.Shared.NextInt64((int)gameState.Dimensions.Y));
                if(gameState.Enemy.CanAttackShip(RandomSpot)) {
                    gameState.Player.TryAttackShip(RandomSpot);   
                    gameState.Enemy.HitSpots[(int)RandomSpot.X + (int)gameState.Dimensions.X * (int)RandomSpot.Y] = true;
                    break;
                }
            }
        }

        void TurnEnd() {

            if (gameState.IsGameOver()) {
                stateMachine.Add(new GameEndState(stateMachine));
                return;
            }
                
            Console.WriteLine("Enemy turn.");

            EnemyTurn();

            gameState.PrintState(gameState.Player, gameState.Player);

            if (gameState.IsGameOver()) {
                stateMachine.Add(new GameEndState(stateMachine));
                return;
            }
        }

        void CheckForGameOver() {
            

        }
    }

}