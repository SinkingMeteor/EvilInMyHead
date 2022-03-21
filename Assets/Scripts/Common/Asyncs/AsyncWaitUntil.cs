using System;
using System.Runtime.CompilerServices;

namespace Sheldier.Common.Asyncs
{
    public class AsyncWaitUntil
    {
        private Func<bool> _condition;
        private TickHandler _tickHandler;

        public AsyncWaitUntil(Func<bool> condition, TickHandler tickHandler)
        {
            _condition = condition;
            _tickHandler = tickHandler;
        }
        
        public AsyncWaitUntilAwaiter GetAwaiter()
        {
            return new AsyncWaitUntilAwaiter(_condition, _tickHandler);
        }

        public class AsyncWaitUntilAwaiter : INotifyCompletion, ITickListener
        {
            private readonly Func<bool> _condition;
            private TickHandler _tickHandler;
            private Action _continuation;

            public bool IsCompleted => _condition();
            public string GetResult() => "";
            
            public AsyncWaitUntilAwaiter(Func<bool> condition, TickHandler tickHandler)
            {
                _tickHandler = tickHandler;
                _tickHandler.AddListener(this);
                _condition = condition;
            }
            public void OnCompleted(Action continuation)
            {
                _continuation = continuation;
            }

            public void Tick()
            {
                if (_condition())
                {
                    _tickHandler.RemoveListener(this);
                    _continuation();
                }
            }
        }
    }
}

