using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueueNode<T>
{
    #region Properties
    private T value;
    private PriorityQueueNode<T> next;
    private PriorityQueueNode<T> prev;
    private int priority;
    #endregion

    #region Getters
    public T Value => value;
    public PriorityQueueNode<T> Next => next;
    public PriorityQueueNode<T> Prev => prev;
    public int Priority => priority;
    #endregion

    #region Constructors
    public PriorityQueueNode(T value, int priority)
    {
        this.value = value;
        this.priority = priority;
        this.next = null;
    }
    public void SetNext(PriorityQueueNode<T> _next)
    {
        next = _next;
    }
    public void SetPrev(PriorityQueueNode<T> _prev)
    {
        prev = _prev;
    }
    #endregion

}