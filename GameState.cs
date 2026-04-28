using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using StateMachines;

namespace BattleShipGame;

public class GameState {
    
    List<Player> Players;
    public Player Player => Players.First();
    public Player Enemy => Players.Last();
    Vector2 Marker;
    public Vector2 Dimensions = new(8, 8);
    public GameState() {
        
        Players = new() { new(0, this), new(1, this)};
        Marker = new();
    }

    public void MoveMarker(Vector2 amount) {
        
        Marker = Vector2.Clamp(Marker += amount, Vector2.Zero, Dimensions);
    }

    public Vector2 GetMarker() => Marker;

    public string PrintState(Player Perspective, Player Board) {
        
        string state = "";
        char[] Grid = new char[(int)(Dimensions.X * Dimensions.X)];

        for (int i = 0; i < Dimensions.X * Dimensions.Y; i++)
        {
            Grid[i] = '~';

        }

        foreach (var ship in Board.GetShips())
        {
            for (int i = 0; i < ship.Len(); i++)
            {   
                Vector2 Pos = ship.GetPos + ship.GetDir * i;

                if(ship.HitAtPos(Pos) || Perspective == Board)
                    Grid[(int)Pos.X + (int)Dimensions.X * (int)Pos.Y] = 'x';
            }
        }
        
        for (int i = 0; i < Dimensions.X * Dimensions.X; i++)
        {
            if(Perspective == Board && Perspective.HitSpots[i])
                Grid[i] = 'H';

            if(Board.AnyHitAtPosition(i % (int)Dimensions.X, (int)(i / Dimensions.X)))
                Grid[i] = '!';
        }

        Grid[(int)Marker.X + (int)Dimensions.X * (int)Marker.Y] = '@';

        var result = Grid.Chunk(8);

        foreach (var res in result)
        {
            state += String.Join("", res);
            state += '\n';
        }

        //state = String.Join("\n", Rows);
        
        Console.WriteLine(state);
        return state;
    }

    public bool IsGameOver() {
        
        return Players.Any((player) => !player.HasAliveShipsLeft());
    }

    public bool WantRestart;
    public void Restart() {
        WantRestart = true;
    }

    public bool WantExit;
    public void Exit() {
        
        WantExit = true;
    }
}