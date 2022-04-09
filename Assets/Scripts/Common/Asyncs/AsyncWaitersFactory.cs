using System;

namespace Sheldier.Common.Asyncs
{
    public class AsyncWaitersFactory
    {
        private static TickHandler _tickHandler;
        
        public AsyncWaitersFactory(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }

        public static AsyncWaitUntil WaitUntil(Func<bool> condition) =>
            new AsyncWaitUntil(condition, _tickHandler);

    }

}