using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    protected PriorityQueueNode<T> head = null;
    protected PriorityQueueNode<T> last = null;

    protected int count = 0;

    public PriorityQueueNode<T> Head => head;
    public PriorityQueueNode<T> Last => last;
    public int Count => count;

    public virtual void Enqueue(T item, int prio)
    {
        PriorityQueueNode<T> newNode = new PriorityQueueNode<T>(item, prio);
        count++;

        if (last == null && head == null)
        {
            head = newNode;
            last = newNode;
            return;
        }
        if (newNode.Priority <= last.Priority)
        {
            last.SetPrev(newNode);
            newNode.SetNext(last);

            last = newNode;
            return;
        }
        if (newNode.Priority >= head.Priority)
        {
            head.SetNext(newNode);
            newNode.SetPrev(head);

            head = newNode;
            return;
        }
        ReadAndAssign(newNode);
    }
    public void ReadAndAssign(PriorityQueueNode<T> _current, PriorityQueueNode<T> _last = null, int depth = 0)
    {
        if (depth >= count)
            return;
        if (_last == null)
        {
            _last = last;
        }

        if (_current.Priority <= _last.Priority)
        {
            _current.SetNext(_last);
            _current.SetPrev(_last.Prev);

            _last.Prev.SetNext(_current);
            _last.SetPrev(_current);

            return;
        }
        ReadAndAssign(_current, _last.Next, depth + 1);

    }
    public virtual T Dequeue()
    {
        if (count <= 0)
        {
            //Clear();
            count = 0;
            return default;
        }
        count--;
        PriorityQueueNode<T> popNode = new(head.Value, head.Priority);
        head = head.Prev;
        return popNode.Value;
    }
    public virtual void TryPeek(out PriorityQueueNode<T> _head, out int prio)
    {
        _head = head;
        prio = _head.Priority;
    }
    public virtual void Clear()
    {
        head = null;
        last = null;
        count = 0;
    }
    public void ReadQueue(PriorityQueueNode<T> _current = null, int depth = 0)
    {
        if (depth >= count)
            return;
        if (_current == null)
        {
            _current = last;
        }
        if (_current == null)
        {
            Debug.Log("Lista Completa ");
            return;
        }

        Debug.Log("Nodo en posicion " + (count - depth) + "De valor :" + _current.Value.ToString());
        ReadQueue(_current.Next, depth + 1);
    }
    /*
        public void AssignHead(QueueNode<T> _current = null , int depth = 0)
        {
            if(depth >= count)
                return;
            if(_current == null)
            {
                _current = last;
            }
            if(_current.Next == null)
            {
                head = _current;
                return;
            }
            AssignHead(_current.Next, depth +1);
        }

       */
}

