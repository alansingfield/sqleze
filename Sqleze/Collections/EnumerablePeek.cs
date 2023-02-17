using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Sqleze.Collections
{
    public class EnumerablePeek<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> inner;
        private readonly EnumeratorShim<T> shim;

        bool shimUsed = false;
        bool? isEmpty = null;

        public EnumerablePeek(IEnumerable<T> inner)
        {
            this.inner = inner;
            this.shim = new EnumeratorShim<T>(inner);
        }

        public bool IsEmpty() 
        {
            if(isEmpty != null)
                return isEmpty.Value;

            isEmpty = this.shim.IsEmpty();

            // If there are no items to enumerate, we dispose the enumerator here. If the caller
            // tries to enumerate they will get an ObjectDisposedException
            if(isEmpty.Value)
                this.shim.Dispose();

            // If we return false, the caller is expected to enumerate the items at least once
            // so the Dispose is called on the enumerator by the foreach()

            return isEmpty.Value;
        }

        private IEnumerator<T> getEnumerator()
        {
            if(isEmpty == true)
                throw new ObjectDisposedException(nameof(EnumeratorShim<T>), $"{nameof(EnumerablePeek<T>)} disposes the enumerator if there are no items to enumerate.");

            // Upon the first call to GetEnumerator(), return the shim.
            // All subsequent calls get an enumerator straight from the
            // original object.
            if(!shimUsed)
            {
                shimUsed = true;
                return shim;        // The shim will be disposed by the caller.
            }
            else
            {
                return inner.GetEnumerator();
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return getEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return getEnumerator();
        }
    }

    public class EnumeratorShim<T> : IEnumerator<T>, IEnumerator, IDisposable
    {
        private IEnumerator<T>? enumerator;
        private readonly IEnumerable<T> innerEnumerable;

        private bool resultZero;    // Result of first call to MoveNext() 
        private int innerPos = -1;  // Iterator position within inner IEnumerator
        private int position = -1;  // Position we should present to the outside world

        public EnumeratorShim(IEnumerable<T> innerEnumerable)
        {
            this.innerEnumerable = innerEnumerable;
        }

        [MemberNotNull(nameof(enumerator))]
        private void openInner()
        {
            if(this.enumerator == null)
                enumerator = innerEnumerable.GetEnumerator();
        }

        public bool IsEmpty()
        {
            // This is the clever bit. We want to take a sneak peek at the result of
            // MoveNext() so we can determine if the collection is empty. We only need
            // to do this if we haven't started enumerating.
            if(innerPos == -1)
                innerMoveNext();

            // This is the result of the first call to MoveNext() so will tell us if
            // the collection has no elements.
            return !resultZero;
        }

        bool IEnumerator.MoveNext()
        {
            position++;

            // If we have called MoveNext() as a result of IsEmpty(), return the result
            // as it was then.
            if(position == 0 && innerPos == 0)
                return resultZero;

            return innerMoveNext();
        }

        private bool innerMoveNext()
        {
            openInner();

            innerPos++;

            bool result = enumerator.MoveNext();

            if(innerPos == 0)
                resultZero = result;

            return result;
        }

        T IEnumerator<T>.Current
        {
            get
            {
                openInner();
                throwIfMoveNextNotCalled();

                return this.enumerator.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                openInner();
                throwIfMoveNextNotCalled();

                return ((IEnumerator)this.enumerator).Current;
            }
        }

        private void throwIfMoveNextNotCalled()
        {
            // Guard against reading Current before first external MoveNext call
            // takes place.
            if(position < 0)
                throw new InvalidOperationException();
        }


        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if(disposedValue)
                return;

            // Pass Dispose() call through to the inner enumerator.
            if(disposing)
                this.enumerator?.Dispose();

            disposedValue = true;
        }

        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        public void Dispose() => Dispose(true);
    }
}
