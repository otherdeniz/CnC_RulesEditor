// Cyotek Color Picker Controls Library
// http://cyotek.com/blog/tag/colorpicker

// Copyright (c) 2013-2021 Cyotek Ltd.

// This work is licensed under the MIT License.
// See LICENSE.TXT for the full text

// Found this code useful?
// https://www.cyotek.com/contribute

using System.Collections.ObjectModel;

namespace Deniz.TiberiumSunEditor.Gui.Controls.ColorControls
{
  /// <summary>
  /// Represents a collection of colors
  /// </summary>
  /// <remarks>
  /// 	<para>ColorCollection allows duplicate elements.</para>
  /// 	<para>Elements in this collection can be accessed using an integer index. Indexes in this collection are zero-based.</para>
  /// </remarks>
  public class ColorCollection : Collection<System.Drawing.Color>, ICloneable, IEquatable<ColorCollection>
  {
      private readonly object _lock = new object();

    private IDictionary<int, int> _indexedLookup;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCollection"/> class.
    /// </summary>
    public ColorCollection()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCollection"/> class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new collection.</param>
    public ColorCollection(IEnumerable<System.Drawing.Color> collection)
      : this()
    {
      this.AddRange(collection);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCollection"/> class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new collection.</param>
    public ColorCollection(ColorCollection collection)
      : this()
    {
      for (int i = 0; i < collection.Count; i++)
      {
        this.Add(collection[i]);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCollection"/> class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new collection.</param>
    public ColorCollection(IEnumerable<int> collection)
      : this()
    {
      this.AddRange(collection.Select(System.Drawing.Color.FromArgb));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCollection"/> class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new collection.</param>
    public ColorCollection(System.Drawing.Imaging.ColorPalette collection)
      : this()
    {
      this.AddRange(collection.Entries);
    }

    /// <summary>
    /// Occurs when elements in the collection are added, removed or modified.
    /// </summary>
    public event EventHandler<ColorCollectionEventArgs> CollectionChanged;

    public event EventHandler<ColorCollectionEventArgs> ItemInserted;

    public event EventHandler<ColorCollectionEventArgs> ItemRemoved;

    public event EventHandler<ColorCollectionEventArgs> ItemReplaced;

    public event EventHandler<ColorCollectionEventArgs> ItemsCleared;

    /// <summary>
    /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    protected override void ClearItems()
    {
      ColorCollectionEventArgs e;

      base.ClearItems();

      _indexedLookup = null;

      e = new ColorCollectionEventArgs(-1, System.Drawing.Color.Empty);
      this.OnItemInserted(e);
      this.OnCollectionChanged(e);
    }

    /// <summary>
    /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
    /// <param name="item">The object to insert.</param>
    protected override void InsertItem(int index, System.Drawing.Color item)
    {
      ColorCollectionEventArgs e;
      int key;

      base.InsertItem(index, item);

      key = item.ToArgb();

      if (_indexedLookup != null && index == this.Count - 1 && !_indexedLookup.ContainsKey(key))
      {
        lock (_lock)
        {
          if (!_indexedLookup.ContainsKey(key))
          {
            _indexedLookup.Add(key, index);
          }
        }
      }
      else
      {
        _indexedLookup = null;
      }

      e = new ColorCollectionEventArgs(index, item);
      this.OnItemInserted(e);
      this.OnCollectionChanged(e);
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    protected override void RemoveItem(int index)
    {
      System.Drawing.Color item;
      ColorCollectionEventArgs e;
      int key;

      item = this[index];
      key = item.ToArgb();

      if (_indexedLookup != null && _indexedLookup.ContainsKey(key))
      {
        lock (_lock)
        {
          _indexedLookup.Remove(key);
        }
      }

      base.RemoveItem(index);

      e = new ColorCollectionEventArgs(index, item);
      this.OnItemRemoved(e);
      this.OnCollectionChanged(e);
    }

    /// <summary>
    /// Replaces the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to replace.</param>
    /// <param name="item">The new value for the element at the specified index.</param>
    protected override void SetItem(int index, System.Drawing.Color item)
    {
      System.Drawing.Color oldItem;

      oldItem = this[index];

      if (oldItem != item)
      {
        ColorCollectionEventArgs e;

        if (_indexedLookup != null)
        {
          int key;
          int oldKey;

          key = item.ToArgb();
          oldKey = oldItem.ToArgb();

          lock (_lock)
          {
            if (_indexedLookup.ContainsKey(oldKey))
            {
              _indexedLookup.Remove(oldKey);
            }
            if (!_indexedLookup.ContainsKey(key))
            {
              _indexedLookup.Add(key, index);
            }
          }
        }

        base.SetItem(index, item);

        e = new ColorCollectionEventArgs(index, item);
        this.OnItemReplaced(e);
        this.OnCollectionChanged(e);
      }
    }

    /// <summary>Adds the elements of the specified collection to the end of the <see cref="ColorCollection"/>.</summary>
    /// <param name="colors">The collection whose elements should be added to the end of the <see cref="ColorCollection"/>.</param>
    public void AddRange(IEnumerable<System.Drawing.Color> colors)
    {
      foreach (System.Drawing.Color color in colors)
      {
        this.Add(color);
      }
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public virtual ColorCollection Clone()
    {
      return new ColorCollection(this);
    }

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="ColorCollection"/>.
    /// </summary>
    /// <param name="item">The <see cref="Color"/> to locate in the <see cref="ColorCollection"/>.</param>
    /// <returns>The zero-based index of the first occurrence of <c>item</c> within the entire <see cref="ColorCollection"/>, if found; otherwise, –1.</returns>
    public int Find(System.Drawing.Color item)
    {
      return this.Find(item.ToArgb());
    }

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="ColorCollection" />.
    /// </summary>
    /// <param name="item">The <see cref="Color"/> to locate in the <see cref="ColorCollection" />.</param>
    /// <param name="ignoreAlphaChannel">If set to <c>true</c> only the red, green and blue channels of items in the <see cref="ColorCollection"/> will be compared.</param>
    /// <returns>The zero-based index of the first occurrence of <c>item</c> within the entire <see cref="ColorCollection" />, if found; otherwise, –1.</returns>
    public int Find(System.Drawing.Color item, bool ignoreAlphaChannel)
    {
      int result;

      if (!ignoreAlphaChannel)
      {
        result = this.Find(item);
      }
      else
      {
        // TODO: This is much much slower than the lookup based find

        result = -1;

        for (int i = 0; i < this.Count; i++)
        {
          System.Drawing.Color original;

          original = this[i];
          if (original.R == item.R && original.G == item.G && original.B == item.B)
          {
            result = i;
            break;
          }
        }
      }

      return result;
    }

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="ColorCollection"/>.
    /// </summary>
    /// <param name="item">The ARGB color to locate in the <see cref="ColorCollection"/>.</param>
    /// <returns>The zero-based index of the first occurrence of <c>item</c> within the entire <see cref="ColorCollection"/>, if found; otherwise, –1.</returns>
    public int Find(int item)
    {
      int result;

      if (_indexedLookup == null)
      {
        this.BuildIndexedLookup();
      }

      if (_indexedLookup == null || !_indexedLookup.TryGetValue(item, out result))
      {
        result = -1;
      }

      return result;
    }

    /// <summary>
    /// Raises the <see cref="CollectionChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnCollectionChanged(ColorCollectionEventArgs e)
    {
      EventHandler<ColorCollectionEventArgs> handler;

      handler = this.CollectionChanged;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the <see cref="ItemInserted" /> event.
    /// </summary>
    /// <param name="e">The <see cref="ColorCollectionEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemInserted(ColorCollectionEventArgs e)
    {
      EventHandler<ColorCollectionEventArgs> handler;

      handler = this.ItemInserted;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the <see cref="ItemRemoved" /> event.
    /// </summary>
    /// <param name="e">The <see cref="ColorCollectionEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemRemoved(ColorCollectionEventArgs e)
    {
      EventHandler<ColorCollectionEventArgs> handler;

      handler = this.ItemRemoved;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the <see cref="ItemReplaced" /> event.
    /// </summary>
    /// <param name="e">The <see cref="ColorCollectionEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemReplaced(ColorCollectionEventArgs e)
    {
      EventHandler<ColorCollectionEventArgs> handler;

      handler = this.ItemReplaced;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the <see cref="ItemsCleared" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemsCleared(ColorCollectionEventArgs e)
    {
      EventHandler<ColorCollectionEventArgs> handler;

      handler = this.ItemsCleared;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Builds an indexed lookup for quick searching.
    /// </summary>
    private void BuildIndexedLookup()
    {
      lock (_lock)
      {
        _indexedLookup = new Dictionary<int, int>();

        for (int i = 0; i < this.Count; i++)
        {
          System.Drawing.Color color;
          int key;

          color = this[i];
          key = color.ToArgb();

          if (!_indexedLookup.ContainsKey(key))
          {
            _indexedLookup.Add(key, i);
          }
        }
      }
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    object ICloneable.Clone()
    {
      return this.Clone();
    }

    /// <summary>
    /// Compares two <see cref="ColorCollection"/> objects. The result specifies whether the values of the two <see cref="ColorCollection"/> objects are equal.
    /// </summary>
    /// <param name="left">A <see cref="ColorCollection"/> to compare.</param>
    /// <param name="right">A <see cref="ColorCollection"/> to compare.</param>
    /// <returns><c>true</c> if the values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ColorCollection left, ColorCollection right)
    {
      return ReferenceEquals(left, right) || !((object)left == null || (object)right == null) && left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="ColorCollection"/> objects. The result specifies whether the values of the two <see cref="ColorCollection"/> objects are unequal.
    /// </summary>
    /// <param name="left">A <see cref="ColorCollection"/> to compare.</param>
    /// <param name="right">A <see cref="ColorCollection"/> to compare.</param>
    /// <returns><c>true</c> if the values of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ColorCollection left, ColorCollection right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Specifies whether this <see cref="ColorCollection"/> contains the same coordinates as the specified <see cref="T:System.Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
    /// <returns><c>true</c> if <paramref name="obj"/> is a <see cref="ColorCollection"/> and has the same values as this <see cref="ColorCollection"/>.</returns>
    public override bool Equals(object obj)
    {
      return obj is ColorCollection && this.Equals((ColorCollection)obj);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public bool Equals(ColorCollection other)
    {
      bool result;

      result = other != null && other.Count == this.Count;
      if (result)
      {
        // check colors - by value though, as Color.Cornflowerblue != Color.FromArgb(255, 100, 149, 237)
        for (int i = 0; i < this.Count; i++)
        {
          System.Drawing.Color expected;
          System.Drawing.Color actual;

          expected = other[i];
          actual = this[i];

          if (expected.ToArgb() != actual.ToArgb())
          {
            result = false;
            break;
          }
        }
      }

      return result;
    }

    /// <summary>
    /// Serves as a hash function for a particular type.
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    public override int GetHashCode()
    {
      // http://stackoverflow.com/a/10567511/148962

      return this.Aggregate(0, (current, value) => current ^ value.GetHashCode());
    }
  }
}
