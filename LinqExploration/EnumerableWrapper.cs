using System.Collections;
using System.Collections.Generic;

namespace LinqExploration
{
    public class EnumerableWrapper<T> : IEnumerable<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public EnumerableWrapper(IEnumerable<T> sequence)
        {
            _enumerator = sequence.GetEnumerator();
            NumCallsToMoveNext = 0;
            NumCallsToCurrent = 0;
            NumCallsToReset = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int NumCallsToMoveNext { get; private set; }
        public int NumCallsToCurrent { get; private set; }
        public int NumCallsToReset { get; private set; }

        public void ResetCallCounts()
        {
            NumCallsToMoveNext = 0;
            NumCallsToCurrent = 0;
            NumCallsToReset = 0;
        }

        public bool MoveNext()
        {
            NumCallsToMoveNext++;
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            NumCallsToReset++;
            _enumerator.Reset();
        }

        public T Current
        {
            get
            {
                NumCallsToCurrent++;
                return _enumerator.Current;
            }
        }

        public class Enumerator : IEnumerator<T>
        {
            private readonly EnumerableWrapper<T> _enumerableWrapper;

            public Enumerator(EnumerableWrapper<T> enumerableWrapper)
            {
                _enumerableWrapper = enumerableWrapper;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return _enumerableWrapper.MoveNext();
            }

            public void Reset()
            {
                _enumerableWrapper.Reset();
            }

            public T Current
            {
                get { return _enumerableWrapper.Current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
