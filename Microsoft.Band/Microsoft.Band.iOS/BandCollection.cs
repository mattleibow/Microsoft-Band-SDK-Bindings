using System;
using System.Collections;
using System.Collections.Generic;

using Foundation;

namespace Microsoft.Band
{
	internal class BandCollection<T> : IList<T>
		where T : NSObject
	{
		private readonly NSMutableArray children;
		private int version;

		public BandCollection (NSMutableArray childrenInternal)
		{
			children = childrenInternal;
		}

		public int IndexOf (T item)
		{
			nuint count = children.Count;
			for (nuint idx = 0; idx < count; idx++) {
				if (children.GetItem<T> (idx) == item) {
					return (int)idx;
				}
			}
			return -1;
		}

		public void Insert (int index, T item)
		{
			children.Insert (item, (nint)index);
			version++;
		}

		public void RemoveAt (int index)
		{
			children.RemoveObject ((nint)index);
			version++;
		}

		public T this [int index] {
			get {
				return children.GetItem<T> ((nuint)index);
			}
			set {
				children.ReplaceObject (index, value);
				version++;
			}
		}

		public void Add (T item)
		{
			children.Add (item);
			version++;
		}

		public void Clear ()
		{
			children.RemoveAllObjects ();
			version++;
		}

		public bool Contains (T item)
		{
			return IndexOf (item) != -1;
		}

		public void CopyTo (T[] array, int arrayIndex)
		{
			foreach (var item in this) {
				array [arrayIndex++] = item;
			}
		}

		public bool Remove (T item)
		{
			int index = IndexOf (item);
			if (index == -1) {
				return false;
			}
			children.RemoveObject ((nint)index);
			version++;
			return true;
		}

		public int Count {
			get {
				return (int)children.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return new BandCollection<T>.Enumerator (this);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		public struct Enumerator : IEnumerator<T>
		{
			private readonly BandCollection<T> collection;
			private int next;
			private readonly int ver;
			private T current;

			internal Enumerator (BandCollection<T> bandCollection)
			{
				this = default(BandCollection<T>.Enumerator);

				collection = bandCollection;
				ver = bandCollection.version;
			}

			public T Current {
				get {
					return current;
				}
			}

			object IEnumerator.Current {
				get {
					if (ver != collection.version) {
						throw new InvalidOperationException ("Collection was modified; enumeration operation may not execute.");
					}
					if (next <= 0) {
						throw new InvalidOperationException ();
					}
					return current;
				}
			}

			public void Dispose ()
			{
			}

			public bool MoveNext ()
			{
				BandCollection<T> list = collection;
				if (next < list.Count && ver == list.version) {
					current = list [next++];
					return true;
				}
				if (ver != collection.version) {
					throw new InvalidOperationException ("Collection was modified; enumeration operation may not execute.");
				}
				next = -1;
				return false;
			}

			void IEnumerator.Reset ()
			{
				if (ver != collection.version) {
					throw new InvalidOperationException ("Collection was modified; enumeration operation may not execute.");
				}
				next = 0;
				current = default(T);
			}
		}
	}
}
