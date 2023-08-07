using System.Collections.Generic;

namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public sealed class FixedTickLoopItem : RepeatableLoopItem
    {
        private readonly IEnumerable<IFixedTick> _items;

        public FixedTickLoopItem(IEnumerable<IFixedTick> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IFixedTick fixedTick in _items)
            {
                fixedTick.FixedTick();
            }
        }
    }

    public sealed class PostFixedTickLoopItem : RepeatableLoopItem
    {
        private readonly IEnumerable<IPostFixedTick> _items;

        public PostFixedTickLoopItem(IEnumerable<IPostFixedTick> items)
        {
            _items = items;
        }

        protected override void OnMoveNext()
        {
            foreach (IPostFixedTick postFixedTick in _items)
            {
                postFixedTick.PostFixedTick();
            }
        }
    }
}