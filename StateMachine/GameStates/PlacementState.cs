using System.Numerics;
using BattleShipGame;

namespace StateMachines;

public class PlacementState : State {
    
    public PlacementState(StateMachine sm) : base(sm) => stateMachine = sm;
    int maxShips;
    int shipsLeft => ShipStack.Count;
    Stack<BattleShip> ShipStack = new();
    public override void OnEnter() {
        
        ShipStack = new();
        ShipStack.Push(new(new(0,0), new(1, 0), 2));
        ShipStack.Push(new(new(0,0), new(1, 0), 4));
        ShipStack.Push(new(new(0,0), new(1, 0), 3));
        ShipStack.Push(new(new(0,0), new(1, 0), 3));
        ShipStack.Push(new(new(0,0), new(1, 0), 2));

        maxShips = ShipStack.Count;

        gameState.PrintState(gameState.Player, gameState.Player);
        Console.WriteLine($"Pick a location for your {shipsLeft} ship");
    }

    public override void OnLeave() {
        
    }

    public override void InterceptInput(string cmd) {
        
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
                if(gameState.Player.CanPlaceShip(gameState.GetMarker(), ShipStack.Peek())) {
                    
                    
                    BattleShip Ship = ShipStack.Pop();
                    Ship.SetPos(gameState.GetMarker());
                    gameState.Player.PlaceShip(Ship);
                }
                    
                break;

            case 'r':
                BattleShip ship = ShipStack.Pop(); 
                ship.SetDir((int)ship.GetDir.Y, (int)ship.GetDir.X);
                ShipStack.Append(ship);
                break;
        }

        Placed();
    }

    void Placed() {

        gameState.PrintState(gameState.Player, gameState.Player);
        // GO TO NEXT STATE HERE!

        if(shipsLeft == 0) {
            
            GenerateEnemyShips();

            
            stateMachine.Add(new AttackState(stateMachine));
        }
    }

    void GenerateEnemyShips() {
        
        ShipStack = new();
        ShipStack.Push(new(new(0,0), new(1, 0), 2));
        ShipStack.Push(new(new(0,0), new(1, 0), 4));
        ShipStack.Push(new(new(0,0), new(1, 0), 3));
        ShipStack.Push(new(new(0,0), new(1, 0), 3));
        ShipStack.Push(new(new(0,0), new(1, 0), 2));


        while(true) {

            BattleShip ship = ShipStack.Peek();
            ship.SetPos(new(Random.Shared.NextInt64(8), Random.Shared.NextInt64(8)));

            if(gameState.Enemy.CanPlaceShip(ship.GetPos, ship)) {
                    
                BattleShip Actual = ShipStack.Pop();
                Actual.SetPos(ship.GetPos);
                gameState.Enemy.PlaceShip(Actual);

            }
            else {
                ship.SetPos(new(Random.Shared.NextInt64(8), Random.Shared.NextInt64(8)));
            }
            
            if(ShipStack.Count == 0)
                break;
        }
    }
}