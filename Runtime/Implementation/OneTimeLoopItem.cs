namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public abstract class OneTimeLoopItem : LoopItem
    {
        public override bool MoveNext()
        {
            if (Disposed)
            {
                return false;
            }

            OnMoveNext();

            return false;
        }
    }
}