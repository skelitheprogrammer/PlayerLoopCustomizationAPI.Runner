using System.Collections.Generic;

namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public sealed class StartLoopItem : OneTimeLoopItem
    {
        private readonly IEnumerable<IStart> _items;

        public StartLoopItem(IEnumerable<IStart> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IStart start in _items)
            {
                start.Start();
            }
        }
    }
    
    public sealed class PostStartLoopItem : OneTimeLoopItem
    {
        private readonly IEnumerable<IPostStart> _items;

        public PostStartLoopItem(IEnumerable<IPostStart> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IPostStart postStart in _items)
            {
                postStart.PostStart();
            }
        }
    }
}