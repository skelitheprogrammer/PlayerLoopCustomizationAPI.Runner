using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PlayerLoopCustomizationAPI.Addons.Runner
{
    public sealed class LoopRunner
    {
        private readonly Queue<ILoopItem> _runningQueue;
        private readonly Queue<ILoopItem> _waitingQueue;

        private readonly object _runningGate;
        private readonly object _waitingGate;

        private int _running;

        public LoopRunner()
        {
            _runningQueue = new();
            _waitingQueue = new();
            _runningGate = new();
            _waitingGate = new();
        }

        public void Dispatch(ILoopItem item)
        {
            if (Interlocked.CompareExchange(ref _running, 1, 1) == 1)
            {
                lock (_waitingGate)
                {
                    _waitingQueue.Enqueue(item);
                    return;
                }
            }

            lock (_runningGate)
            {
                _runningQueue.Enqueue(item);
            }
        }

        public void Run()
        {
            Interlocked.Exchange(ref _running, 1);

            lock (_runningGate)
            {
                lock (_waitingGate)
                {
                    while (_waitingQueue.Count > 0)
                    {
                        ILoopItem waitingItem = _waitingQueue.Dequeue();
                        _runningQueue.Enqueue(waitingItem);
                    }
                }
            }

            ILoopItem item;

            lock (_runningGate)
            {
                item = _runningQueue.Count > 0 ? _runningQueue.Dequeue() : null;
            }

            while (item != null)
            {
                bool continuous = false;

                try
                {
                    continuous = item.MoveNext();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }

                if (continuous)
                {
                    lock (_waitingGate)
                    {
                        _waitingQueue.Enqueue(item);
                    }
                }

                lock (_runningGate)
                {
                    item = _runningQueue.Count > 0 ? _runningQueue.Dequeue() : null;
                }
            }

            Interlocked.Exchange(ref _running, 0);
        }
    }
}