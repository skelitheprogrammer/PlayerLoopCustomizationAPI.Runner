using System.Collections.Generic;

namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public sealed class TickLoopItem : RepeatableLoopItem
    {
        private IEnumerable<ITick> _items;

        public TickLoopItem(IEnumerable<ITick> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (ITick tick in _items)
            {
                tick.Tick();
            }
        }

        public override void Dispose()
        {
            _items = null;
            base.Dispose();
        }
    }
    
    public sealed class PostTickLoopItem : RepeatableLoopItem
    {
        private readonly IEnumerable<IPostTick> _items;

        public PostTickLoopItem(IEnumerable<IPostTick> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IPostTick postTick in _items)
            {
                postTick.PostTick();
            }
        }
    }
}