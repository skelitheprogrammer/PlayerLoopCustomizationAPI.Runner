using System.Collections.Generic;

namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public sealed class InitializeLoopItem : OneTimeLoopItem
    {
        private readonly IEnumerable<IInitialize> _items;

        public InitializeLoopItem(IEnumerable<IInitialize> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IInitialize initialize in _items)
            {
                initialize.Init();
            }
        }
    }
    
    public sealed class PostInitializeLoopItem : OneTimeLoopItem
    {
        private readonly IEnumerable<IPostInitialize> _items;

        public PostInitializeLoopItem(IEnumerable<IPostInitialize> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IPostInitialize postInitialize in _items)
            {
                postInitialize.PostInit();
            }
        }
    }
}