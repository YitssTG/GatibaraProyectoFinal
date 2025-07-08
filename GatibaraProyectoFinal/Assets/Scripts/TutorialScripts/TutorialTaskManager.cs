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
        PlayerAttackCollider.OnAttack += CheckAttack;
        SelectorController.OnSelectElement1 += CheckSelectElement1;
        SelectorController.OnSelectElement2 += CheckSelectElement2;
        Coin.OnCoinsCollection += CheckCoinsCollection;
        PopUpOptionsSlide.OnInventoryOpened += CheckInventoryOpened;
        InventoryUI.OnPurchase += CheckPurchase;
        ElementAbilityManager.OnAbilityUsed += CheckAbilityUsed;
        PopUpController.OnWin += CheckWin;

    }
    private void OnDisable()
    {
        PlayerAttackCollider.OnMoving -= CheckTaskMove;
        PlayerAttackCollider.OnAttack -= CheckAttack;
        SelectorController.OnSelectElement1 -= CheckSelectElement1;
        SelectorController.OnSelectElement2 += CheckSelectElement2;
        Coin.OnCoinsCollection -= CheckCoinsCollection;
        PopUpOptionsSlide.OnInventoryOpened -= CheckInventoryOpened;
        InventoryUI.OnPurchase -= CheckPurchase;
        ElementAbilityManager.OnAbilityUsed -= CheckAbilityUsed;
        PopUpController.OnWin -= CheckWin;
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
        taskQueue.Enqueue(new Task(Task.TaskType.TierraAire), 8);
        taskQueue.Enqueue(new Task(Task.TaskType.Ganar), 9);

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
    private bool IsCurrentPriority(int expectedPriority)
    {
        taskQueue.TryPeek(out var currentNode, out int priority);

        if (currentNode != null)
        {
            return priority == expectedPriority;
        }

        return false;
    }
    //public bool IsTaskQueueEmpty()
    //{
    //    return taskQueue.Count == 0;
    //}
    private void CheckTaskMove()
    {
        if (IsCurrentPriority(1))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckAttack()
    {
        if (IsCurrentPriority(2))
        {
            CompleteCurrentTask();
}
    }
    private void CheckSelectElement1()
    {
        if (IsCurrentPriority(3))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckSelectElement2()
    {
        if (IsCurrentPriority(4))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckCoinsCollection(Vector3 empty)
    {
        if (IsCurrentPriority(5))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckInventoryOpened()
    {
        if (IsCurrentPriority(6))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckPurchase()
    {
        if (IsCurrentPriority(7))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckAbilityUsed()
    {
        if (IsCurrentPriority(8))
        {
            CompleteCurrentTask();
        }
    }
    private void CheckWin()
    {
        if (IsCurrentPriority(9))
        {
            CompleteCurrentTask();
        }
    }
}
