using System.Collections.Generic;

namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public sealed class LateTickLoopItem : RepeatableLoopItem
    {
        private IEnumerable<ILateTick> _items;

        public LateTickLoopItem(IEnumerable<ILateTick> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (ILateTick tick in _items)
            {
                tick.LateTick();
            }
        }

        public override void Dispose()
        {
            _items = null;
            base.Dispose();
        }
    }

    public sealed class PostLateTickLoopItem : RepeatableLoopItem
    {
        private readonly IEnumerable<IPostLateTick> _items;

        public PostLateTickLoopItem(IEnumerable<IPostLateTick> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IPostLateTick postTick in _items)
            {
                postTick.PostLateTick();
            }
        }
    }
}