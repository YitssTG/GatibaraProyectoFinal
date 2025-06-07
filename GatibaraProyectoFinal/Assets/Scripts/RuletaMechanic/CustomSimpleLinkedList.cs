using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSimpleLinkedList<T> : SimpleLinkedList<T>
{
    public int spellnumber;
    int count2 = 0;

    public Node<T> SeekNodeBeforeCustomVersion(T objective)
    {
        if (head == null || count == 0)
            return null;

        Node<T> current = head;
        for (int i = 0; i < count - 1; i++) // no llegamos al último nodo porque no tiene .Next
        {
            if (current.Next != null && current.Next.Value.Equals(objective))
            {
                Debug.Log("Elemento encontrado: " + current.Value);
                Debug.Log("Se encontró en la posición: " + i);
                return current;
            }
            current = current.Next;
        }

        Debug.Log("No se encontró el elemento");
        return null;
    }
    public void RemoveCustomVersion(T objective)
    {
        Node<T> node = Seek(objective); // Usa el Seek heredado (funciona bien)

        if (node == null)
        {
            Debug.Log("No existe el elemento.");
            return;
        }

        // Si el nodo es el único en la lista
        if (count == 1 && node == head)
        {
            RemoveAll(); // Método heredado, borra todo
            return;
        }

        // Si el nodo es el primero (pero no el único)
        if (node == head)
        {
            head = node.Next;
            count--;
            return;
        }

        // Si el nodo está en el medio o al final
        Node<T> nodeBefore = SeekNodeBeforeCustomVersion(objective);
        if (nodeBefore != null)
        {
            nodeBefore.SetNext(node.Next);

            // Si se eliminó el último nodo, actualiza `last`
            if (node == last)
            {
                last = nodeBefore;
            }

            count--;
            return;
        }

        Debug.Log("No se pudo eliminar el nodo (no se encontró su anterior).");
    }
    public void AddElement(T value)
    {
        if (count2 < spellnumber)
        {
            Add(value);
            count2++;
        }
        else
        {
            if (head != null)
            {
                RemoveCustomVersion(head.Value);
            }
            Add(value);
        }
    }
    public T GetValueAt(int index)
    {
        if(index<0 || index >= count)
        {
            return default;
        }
        Node<T> current = head;
        int currentIndex = 0;

        while (current != null)
        {
            if (currentIndex == index)
                return current.Value;

            current = current.Next;
            currentIndex++;
        }
        return default;
    }
    public virtual void ReadAllElements(Node<T> _head = null, int deep = 0)
    {
        if (head == null || deep >= count) return;

        if (_head == null)
        {
            _head = head;
        }
        Debug.Log("Elemento " + (deep + 1) + " casteado: " + _head.Value.ToString());
        ReadAllElements(_head.Next, deep + 1);
    }
}