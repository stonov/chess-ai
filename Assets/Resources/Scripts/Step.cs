using UnityEngine;

public class Step {
    Vector2Int start;
    Vector2Int end;

    public Step(Vector2Int _start, Vector2Int _end) {
        start = _start;
        end = _end;
    }

    public Vector2Int GetStart() {
        return new Vector2Int(start.x, start.y);
    }

    public Vector2Int GetEnd() {
        return new Vector2Int(end.x, end.y);
    }
}
