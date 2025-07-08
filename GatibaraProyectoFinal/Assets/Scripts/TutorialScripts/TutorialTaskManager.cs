using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TutorialTaskManager : MonoBehaviour
{
    [Header("Cola de prioridad")]
    [SerializeField] private CustomPriorityQueue<Task> taskQueue= new CustomPriorityQueue<Task>();
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI taskText;
    void Start()
    {
        AssinTask();
    }
    private void OnEnable()
    {
        PlayerAttackCollider.OnMoving += CheckTaskMove;
        SelectorController.OnSelectElement += CheckSelectElement;
        Coin.OnCoinsCollection += CheckCoinsCollection;
        PopUpOptionsSlide.OnInventoryOpened += CheckInventoryOpened;
        InventoryUI.OnPurchase += CheckPurchase;
    }
    private void OnDisable()
    {
        PlayerAttackCollider.OnMoving -= CheckTaskMove;
        SelectorController.OnSelectElement -= CheckSelectElement;
        Coin.OnCoinsCollection -= CheckCoinsCollection;
        PopUpOptionsSlide.OnInventoryOpened -= CheckInventoryOpened;
        InventoryUI.OnPurchase -= CheckPurchase;
    }
    private void AssinTask()
    {
        taskQueue.Clear();

        taskQueue.Enqueue(new Task(Task.TaskType.Moverse), 1);
        taskQueue.Enqueue(new Task(Task.TaskType.Atacar), 2);
        taskQueue.Enqueue(new Task(Task.TaskType.SeleccionarElemento1), 3);
        taskQueue.Enqueue(new Task(Task.TaskType.SeleccionarElemento2), 4);
        taskQueue.Enqueue(new Task(Task.TaskType.ConseguirMonedas), 5);
        taskQueue.Enqueue(new Task(Task.TaskType.PresionarI), 6);
        taskQueue.Enqueue(new Task(Task.TaskType.Comprar), 7);
        taskQueue.Enqueue(new Task(Task.TaskType.Nivel2), 8);
        taskQueue.Enqueue(new Task(Task.TaskType.TierraAire), 9);
        taskQueue.Enqueue(new Task(Task.TaskType.Habilidad), 10);
        taskQueue.Enqueue(new Task(Task.TaskType.Ganar), 11);

        UpdateTaskText();
        Debug.Log("Lista de tareas iniciada.");
    }
    public void AddTask(Task.TaskType type, int priority)
    {
        taskQueue.Enqueue(new Task(type), priority);
        UpdateTaskText();
    }
    public void CompleteCurrentTask()
    {
        if(taskQueue.Count <= 0)
        {
            Debug.Log("No hay tareas para completar");
            return;
        }
        Task completedTask = taskQueue.Dequeue();
        UpdateTaskText();
    }
    public void UpdateTaskText()
    {
        if(taskQueue.Count > 0)
        {
            taskQueue.TryPeek(out var currentTaskNode, out int _);
            taskText.text ="Tarea actual: " + currentTaskNode.Value.GetTaskMessage();
        }
        else
        {
            taskText.text = "Has completado todas las tareas";
        }
    }
    //public bool IsTaskQueueEmpty()
    //{
    //    return taskQueue.Count == 0;
    //}
    private void CheckTaskMove()
    {
        CompleteCurrentTask();
    }
    private void CheckSelectElement()
    {
        CompleteCurrentTask();
    }
    private void CheckCoinsCollection(Vector3 empty)
    {
        CompleteCurrentTask();
    }
    private void CheckInventoryOpened()
    {
        CompleteCurrentTask();
    }
    private void CheckPurchase()
    {
        CompleteCurrentTask();
    }
}
