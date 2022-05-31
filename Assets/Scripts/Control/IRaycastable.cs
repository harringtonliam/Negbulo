namespace RPG.Control
{
    public interface IRaycastable
    {
        public CursorType GetCursorType();
        public bool HandleRaycast(PlayerController playerController);
    }

}