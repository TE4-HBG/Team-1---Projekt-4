using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MultiHashSet<T> : ISet<T>
{
    HashSet<T> m_Set = new HashSet<T>();
    Dictionary<T, ulong> amount = new Dictionary<T, ulong>();
    public int Count => m_Set.Count;
    
    public bool IsReadOnly => false;


    /// <summary>
    /// Adds one of a specific item
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if item was added to set, false if amount was simply increased</returns>
    public bool Add(T item)
    {
        bool itemAdded = m_Set.Add(item);
        // if the item was added properly
        if (itemAdded)
        {
            // simply set the amount to one
            amount.Add(item, 1);
            
        }
        else
        {
            // else increase the amount of the item
            amount[item]++;
        }

        return itemAdded;
    }
    /// <summary>
    /// Adds one of a specific item
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if item was added to set, false if amount was simply increased</returns>
    public bool Add(T item, out ulong amount)
    {
        bool itemAdded = m_Set.Add(item);
        // if the item was added properly
        if (itemAdded)
        {
            // simply set the amount to one
            this.amount.Add(item, 1);

        }
        else
        {
            // else increase the amount of the item
            this.amount[item]++;
        }
        amount = this.amount[item];

        return itemAdded;
    }
    /// <summary>
    /// Removes one of a specific item
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if item was completely removed from set, false if amount was simply decreased</returns>
    public bool Remove(T item)
    {
        bool fullyRemoved = --amount[item] == 0;

        if (fullyRemoved)
        {
            amount.Remove(item);
            m_Set.Remove(item);
        }

        return fullyRemoved;
    }
    public bool Remove(T item, out ulong amount)
    {
        bool fullyRemoved = --this.amount[item] == 0;

        if (fullyRemoved)
        {
            this.amount.Remove(item);
            m_Set.Remove(item);
            amount = 0;
        }
        else
        {
            amount = this.amount[item];
        }
        
        return fullyRemoved;
    }
    public void Clear()
    {
        m_Set.Clear();
        amount.Clear();
    }

    public ulong Amount(T item)
    {
        return amount[item];
    }

    #region GARBAGE
    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public void ExceptWith(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public void IntersectWith(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public bool Overlaps(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }



    public bool SetEquals(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    public void UnionWith(IEnumerable<T> other)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    void ICollection<T>.Add(T item)
    {
        throw new NotImplementedException();
    }
    #endregion
}