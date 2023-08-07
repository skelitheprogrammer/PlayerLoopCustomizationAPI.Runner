namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public interface IInitialize
    {
        void Init();
    }

    public interface IPostInitialize
    {
        void PostInit();
    }

    public interface IStart
    {
        void Start();
    }

    public interface IPostStart
    {
        void PostStart();
    }

    public interface IFixedTick
    {
        void FixedTick();
    }

    public interface IPostFixedTick
    {
        void PostFixedTick();
    }

    public interface ITick
    {
        void Tick();
    }

    public interface IPostTick
    {
        void PostTick();
    }

    public interface ILateTick
    {
        void LateTick();
    }

    public interface IPostLateTick
    {
        void PostLateTick();
    }
}