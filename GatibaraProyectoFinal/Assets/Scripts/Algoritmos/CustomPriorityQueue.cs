using UnityEngine;

public class CustomPriorityQueue<T> : PriorityQueue<T>
{
    public string GetAllTasks()
    {
        return ReadQueueToString();
    }
    public string ReadQueueToString(PriorityQueueNode<T> current = null, int depth = 0)
    {
        if(depth>= count)
        {
            return "";
        }
        if(current == null)
        {
            current = last;
        }
        if(current == null)
        {
            return "No hay más tareas.";
        }
        string currentTask = "Tarea " + (count - depth) + current.Value;
        return currentTask + ReadQueueToString(current.Next, depth +1);
    }
}
