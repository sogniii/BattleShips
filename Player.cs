using System.Numerics;

namespace BattleShipGame;

public class Player {
    
    GameState gameState;
    int playernum;
    public int PlayerNum => playernum;
    List<BattleShip> Ships;
    public Player(int num, GameState state) {
        
        playernum = num;
        gameState = state;

        HitSpots = new bool[(int)(gameState.Dimensions.X * gameState.Dimensions.X)];

        Ships = new();
    }

    public void PlaceShip(BattleShip ship) {
        
        Ships.Add(ship); 
    }

    public bool IsValidSpot(BattleShip ship, int x, int y) {

        Vector2 pos = new(x, y);
        for (int i = 0; i < ship.Len(); i++)
        {
            // TODO: GET DIMENSIONS NORMALLY!
            Vector2 spot = pos + ship.Len() * ship.GetDir;
            bool OOB = spot.X >= gameState.Dimensions.X || spot.X < 0 || spot.Y >= gameState.Dimensions.Y || spot.Y < 0;
            
            // Possibly save a little bit of time by going out early
            if(OOB)
                return !OOB;

            if(IsAnyAtPosition(spot))
                return false;
        } 

        return true;
    }

    // This is on the ENEMY board
    public bool[] HitSpots;
    public List<BattleShip> GetShips() => Ships;

    public bool CanPlaceShip(int x, int y , BattleShip ship) => IsValidSpot(ship, x, y);
    public bool CanPlaceShip(Vector2 vec, BattleShip ship) => CanPlaceShip((int)vec.X, (int)vec.Y, ship);

    public bool CanBeRevealed(int Player, int x, int y) => IsAnyAtPosition(x, y) && (PlayerNum == Player || AnyHitAtPosition(x, y)); 
    public bool CanBeRevealed(int Player, Vector2 vec) => CanBeRevealed(Player, (int)vec.X, (int)vec.Y);

    public bool IsAnyAtPosition(int x, int y) => GetShips().Any((ship) => ship.IsAtPosition(x, y));  
    public bool IsAnyAtPosition(Vector2 vec) => IsAnyAtPosition((int)vec.X, (int)vec.Y);

    public bool AnyHitAtPosition(int x, int y) => GetShips().Any((ship) => ship.HitAtPos(x, y));

    public bool CanAttackShip(Vector2 vec) => CanAttackShip((int)vec.X, (int)vec.Y);
    public bool CanAttackShip(int x, int y) => !HitSpots[x + (int)gameState.Dimensions.X * y];

    public void TryAttackShip(Vector2 vec) => TryAttackShip((int)vec.X, (int)vec.Y);
    public void TryAttackShip(int x, int y) {
        
        if(IsAnyAtPosition(x, y)) {
            GetShipAtPos(x, y).Hit(x, y);
        }
        
        HitSpots[x + (int)gameState.Dimensions.X * y] = true;
    }

    public BattleShip GetShipAtPos(int x, int y) {
        return GetShips().First((ship) => ship.IsAtPosition(x, y));
    }

    public bool HasAliveShipsLeft() => !GetShips().All((ship) => ship.isDead());
}