using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqleze.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using Shouldly;

namespace Sqleze.Tests.Collections
{
    [TestClass]
    public class EnumerablePeekTEnumeratorTests
    {
        [TestMethod]
        public void EnumerablePeekTEnumerator2()
        {
            var enumerable = Substitute.For<IEnumerable<int>>();

            var enumerator1 = Substitute.For<IEnumerator<int>>();

            enumerator1.MoveNext().Returns(true, true, false);
            enumerator1.Current.Returns(1, 2);

            var enumerator2 = Substitute.For<IEnumerator<int>>();
            enumerator2.MoveNext().Returns(true, true, false);
            enumerator2.Current.Returns(1, 2);

            enumerable.GetEnumerator().Returns(enumerator1, enumerator2);

            var peeker = new EnumerablePeek<int>(enumerable);

            bool empty = peeker.IsEmpty();
            var a1 = peeker.ToList();
            var a2 = peeker.ToList();

            enumerator1.Received(1).Dispose();
            enumerator2.Received(1).Dispose();

            empty.ShouldBe(false);
            a1.ShouldBe(new List<int>() { 1, 2 });
            a2.ShouldBe(new List<int>() { 1, 2 });

            // Should have dished out both enumerators because of the IsEmpty
            // then the SECOND ToList.
            enumerable.Received(2).GetEnumerator();

            // Both enumerators should be disposed once.
            enumerator1.Received(1).Dispose();
            enumerator2.Received(1).Dispose();
        }

        [TestMethod]
        public void EnumerablePeekTEnumerator3()
        {
            var enumerable = Substitute.For<IEnumerable<int>>();

            var enumerator1 = Substitute.For<IEnumerator<int>>();

            enumerator1.MoveNext().Returns(true, true, false);
            enumerator1.Current.Returns(1, 2);

            var enumerator2 = Substitute.For<IEnumerator<int>>();
            enumerator2.MoveNext().Returns(true, true, false);
            enumerator2.Current.Returns(1, 2);

            enumerable.GetEnumerator().Returns(enumerator1, enumerator2);

            var peeker = new EnumerablePeek<int>(enumerable);

            bool empty = peeker.IsEmpty();

            // At this point we will have done a MoveNext() and 
            // not yet disposed of the first enumerator
            enumerator1.Received(1).MoveNext();
            enumerator1.DidNotReceive().Dispose();

            empty.ShouldBe(false);

            // This is where we can miss out a Dispose() - we are expected
            // to enumerate now.
            enumerable.Received(1).GetEnumerator();
            enumerator1.DidNotReceive().Dispose();

            // Now enumerate
            var a = peeker.ToList();

            // Should now have disposed the first enumerator.
            enumerator1.Received(1).Dispose();

            // Did NOT ask for the second enumerator.
            enumerable.Received(1).GetEnumerator();
        }

        [TestMethod]
        public void EnumerablePeekTEnumerator4()
        {
            var enumerable = Substitute.For<IEnumerable<int>>();

            var enumerator1 = Substitute.For<IEnumerator<int>>();

            enumerator1.MoveNext().Returns(true, true, false);
            enumerator1.Current.Returns(1, 2);

            var enumerator2 = Substitute.For<IEnumerator<int>>();
            enumerator2.MoveNext().Returns(true, true, false);
            enumerator2.Current.Returns(1, 2);

            enumerable.GetEnumerator().Returns(enumerator1, enumerator2);

            var peeker = new EnumerablePeek<int>(enumerable);

            bool empty = peeker.IsEmpty();

            enumerator1.Received(1).MoveNext();
            enumerator1.DidNotReceive().Dispose();

            var a1 = peeker.ToList();

            // ToList() will cause the dispose to happen
            enumerator1.Received(1).Dispose();

            empty.ShouldBe(false);

            // Should have disposed the first enumerator but not given
            // out the second enumerator.
            enumerator1.Received(1).Dispose();
            enumerable.Received(1).GetEnumerator();
        }


        [TestMethod]
        public void EnumerablePeekTEnumeratorEmpty()
        {
            // Simulate an empty collection

            var enumerable = Substitute.For<IEnumerable<int>>();

            var enumerator1 = Substitute.For<IEnumerator<int>>();
            enumerator1.MoveNext().Returns(false);

            var enumerator2 = Substitute.For<IEnumerator<int>>();
            enumerator2.MoveNext().Returns(false);

            enumerable.GetEnumerator().Returns(enumerator1, enumerator2);

            var peeker = new EnumerablePeek<int>(enumerable);

            bool empty = peeker.IsEmpty();

            // IsEmpty should MoveNext() then Dispose()
            enumerator1.Received(1).MoveNext();
            enumerator1.Received(1).Dispose();

            empty.ShouldBe(true);

            // Should not have given out the second enumerator
            enumerable.Received(1).GetEnumerator();
        }


        [TestMethod]
        public void EnumerablePeekTEnumeratorEmptyToList()
        {
            // Simulate an empty collection

            var enumerable = Substitute.For<IEnumerable<int>>();

            var enumerator1 = Substitute.For<IEnumerator<int>>();
            enumerator1.MoveNext().Returns(false);

            var enumerator2 = Substitute.For<IEnumerator<int>>();
            enumerator2.MoveNext().Returns(false);

            enumerable.GetEnumerator().Returns(enumerator1, enumerator2);

            var peeker = new EnumerablePeek<int>(enumerable);

            bool empty = peeker.IsEmpty();
            enumerator1.Received(1).MoveNext();
            enumerator1.Received(1).Dispose();

            empty.ShouldBe(true);

            // Attempting to enumerate when IsEmpty() returned true should raise an error.
            Should.Throw(() => 
            {
                var a1 = peeker.ToList();
            }, typeof(ObjectDisposedException))
                .Message
                .ShouldBe("EnumerablePeek disposes the enumerator if there are no items to enumerate.\r\nObject name: 'EnumeratorShim'.");

            // Should not have given out the second enumerator
            enumerable.Received(1).GetEnumerator();
        }

        [TestMethod]
        public void EnumerablePeekTEnumeratorEmptyNotUsed()
        {
            // Simulate an empty collection

            var enumerable = Substitute.For<IEnumerable<int>>();

            var enumerator1 = Substitute.For<IEnumerator<int>>();
            enumerator1.MoveNext().Returns(false);

            enumerable.GetEnumerator().Returns(enumerator1);

            var peeker = new EnumerablePeek<int>(enumerable);

            // Don't do anything with the peeker.
            enumerator1.DidNotReceive().MoveNext();
            enumerator1.DidNotReceive().Dispose();

            // Should not have given out any enumerators
            enumerable.Received(0).GetEnumerator();
        }
    }
}
