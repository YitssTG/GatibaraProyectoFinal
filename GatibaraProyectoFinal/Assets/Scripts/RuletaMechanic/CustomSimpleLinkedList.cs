using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSimpleLinkedList<T> : SimpleLinkedList<T>
{
    public int spellnumber = 1;

    public void AddElement(T value)
    {
        if (count < spellnumber)
        {
            Add(value);
        }
        else
        {
            if(head != null)
            {
                RemoveElement(head.Value);
            }
            Add(value);
        }
    }
    private void RemoveElement(T value)
    {
        if (head == null) return;

        if(count == 1)
        {
            RemoveAll();
            return;
        }
        head = head.Next;
        count--;
    }
    public List<T> GetOrderedElements()
    {
        List<T> elements = new List<T>();
        Node<T> current = head;

        while (current != null)
        {
            elements.Add(current.Value);
            current = current.Next;
        }

        return elements;
    }
}