using System.Collections;
using System.Collections.Generic;

namespace LinqExploration
{
    public class EnumerableWrapper<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _sequence;
        private readonly Dictionary<Enumerator, IEnumerator<T>> _enumerators = new Dictionary<Enumerator, IEnumerator<T>>();

        public EnumerableWrapper(IEnumerable<T> sequence)
        {
            _sequence = sequence;
            ResetCallCounts();
        }

        public IEnumerator<T> GetEnumerator()
        {
            NumCallsToGetEnumerator++;
            var wrappingEnumerator = new Enumerator(this);
            var realEnumerator = _sequence.GetEnumerator();
            _enumerators.Add(wrappingEnumerator, realEnumerator);
            return wrappingEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int NumCallsToGetEnumerator { get; private set; }
        public int NumCallsToMoveNext { get; private set; }
        public int NumCallsToCurrent { get; private set; }
        public int NumCallsToReset { get; private set; }
        public int NumCallsToDispose { get; private set; }

        public void ResetCallCounts()
        {
            NumCallsToGetEnumerator = 0;
            NumCallsToMoveNext = 0;
            NumCallsToCurrent = 0;
            NumCallsToReset = 0;
            NumCallsToDispose = 0;
        }

        private bool MoveNext(Enumerator wrappingEnumerator)
        {
            NumCallsToMoveNext++;
            return GetRealEnumerator(wrappingEnumerator).MoveNext();
        }

        private void Reset(Enumerator wrappingEnumerator)
        {
            NumCallsToReset++;
            GetRealEnumerator(wrappingEnumerator).Reset();
        }

        private T GetCurrent(Enumerator wrappingEnumerator)
        {
            NumCallsToCurrent++;
            return GetRealEnumerator(wrappingEnumerator).Current;
        }

        private void Dispose(Enumerator wrappingEnumerator)
        {
            NumCallsToDispose++;
            var realEnumerator = GetRealEnumerator(wrappingEnumerator);
            _enumerators.Remove(wrappingEnumerator);
            realEnumerator.Dispose();
        }

        private IEnumerator<T> GetRealEnumerator(Enumerator wrappingEnumerator)
        {
            return _enumerators[wrappingEnumerator];
        }

        private class Enumerator : IEnumerator<T>
        {
            private readonly EnumerableWrapper<T> _enumerableWrapper;

            public Enumerator(EnumerableWrapper<T> enumerableWrapper)
            {
                _enumerableWrapper = enumerableWrapper;
            }

            public bool MoveNext()
            {
                return _enumerableWrapper.MoveNext(this);
            }

            public void Reset()
            {
                _enumerableWrapper.Reset(this);
            }

            public T Current
            {
                get { return _enumerableWrapper.GetCurrent(this); }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _enumerableWrapper.Dispose(this);
            }
        }
    }
}
