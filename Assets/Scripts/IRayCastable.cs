namespace RPG.Control
{
    public interface IRayCastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController _callController);
    }
}
