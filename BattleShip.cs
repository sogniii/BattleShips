using System.Numerics;
using System.Xml.Serialization;

namespace BattleShipGame;
public struct BattleShip {
    
    Vector2 Position;
    Vector2 Direction;
    int Length;
    List<Vector2> HitPositions;

    public BattleShip(Vector2 pos, Vector2 dir, int length) {
        
        Position = pos;
        Direction = dir;
        Length = Math.Max(1, length);

        HitPositions = new(); //{Position};
    }

    public void SetPos(Vector2 Pos) => Position = Pos; 
    public void SetDir(Vector2 Dir) => Direction = Dir;
    public void SetDir(int x, int y) => Direction = new(x, y);
    public Vector2 GetDir => Direction;
    public Vector2 GetPos => Position;
    public int Len() => Length;
    public bool IsAtPosition(int x, int y) {
        
        bool accum = false;
        Vector2 spot = Position;

        for (int i = 0; i < Length; i++)
        {
            spot += Direction;
            if(spot == new Vector2(x, y))
                return true;
        }

        return false;
    }
    public bool IsAtPosition(Vector2 vec) => IsAtPosition((int)vec.X, (int)vec.Y);
    public bool HitAtPos(int x, int y) => HitPositions.Contains(new(x, y));
    public bool HitAtPos(Vector2 vec) => HitAtPos((int)vec.X, (int)vec.Y);
    public bool CanBeShown(int player, int x, int y) => HitAtPos(x, y);
    public void Hit(Vector2 vec) => HitPositions.Add(vec);
    public void Hit(int x, int y) => HitPositions.Add(new(x, y));  

    public bool isDead() => HitPositions.Count == Length;
    public bool isAlive() => !isDead();
}