namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public abstract class RepeatableLoopItem : LoopItem
    {
        public override bool MoveNext()
        {
            if (Disposed)
            {
                return false;
            }

            OnMoveNext();

            return !Disposed;
        }
    }
}