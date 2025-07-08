using UnityEngine;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
public class TutorialTaskManager : MonoBehaviour
{
    [Header("Cola de prioridad")]
    [SerializeField] private CustomPriorityQueue<Task> taskQueue= new CustomPriorityQueue<Task>();
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI taskText;
    private RectTransform textTransform;

    private Coroutine animateRoutine;

    private bool animating;
    private bool taskReady;

    [Header("OriginalPositions and Scale")]
    private Vector3 originalPosition = new Vector3(49f, -190f, 0f);
    private Vector3 originalScale = new Vector3(5.8f, 5.7f, 1f);
    void Start()
    {
        textTransform = taskText.GetComponent<RectTransform>();
        animating = false;
        taskReady = false;
        textTransform.anchoredPosition = originalPosition;
        textTransform.localScale = originalScale;
        AssinTask();
    }
    private void OnEnable()
    {
        Move.OnMoving += CheckTaskMove;
        PlayerAttackCollider.OnAttack += CheckAttack;
        SelectorController.OnSelectElement1 += CheckSelectElement1;
        SelectorController.OnSelectElement2 += CheckSelectElement2;
        Coin.OnCoinsCollection += CheckCoinsCollection;
        PopUpOptionsSlide.OnInventoryOpened += CheckInventoryOpened;
        InventoryUI.OnPurchase += CheckPurchase;
        ElementAbilityManager.OnAbilityUsed += CheckAbilityUsed;
        AbilityListener.OnUnlockedList += CheckUnlockedList;
        PopUpController.OnWin += CheckWin;
    }
    private void OnDisable()
    {
        Move.OnMoving -= CheckTaskMove;
        PlayerAttackCollider.OnAttack -= CheckAttack;
        SelectorController.OnSelectElement1 -= CheckSelectElement1;
        SelectorController.OnSelectElement2 -= CheckSelectElement2;
        Coin.OnCoinsCollection -= CheckCoinsCollection;
        PopUpOptionsSlide.OnInventoryOpened -= CheckInventoryOpened;
        InventoryUI.OnPurchase -= CheckPurchase;
        ElementAbilityManager.OnAbilityUsed -= CheckAbilityUsed;
        AbilityListener.OnUnlockedList -= CheckUnlockedList;
        PopUpController.OnWin -= CheckWin;
    }
    private void AssinTask()
    {
        taskQueue.Clear();

        taskQueue.Enqueue(new Task(Task.TaskType.Moverse), 10);
        taskQueue.Enqueue(new Task(Task.TaskType.Atacar), 9);
        taskQueue.Enqueue(new Task(Task.TaskType.SeleccionarElemento1), 9);
        taskQueue.Enqueue(new Task(Task.TaskType.SeleccionarElemento2), 7);
        taskQueue.Enqueue(new Task(Task.TaskType.ConseguirMonedas), 6);
        taskQueue.Enqueue(new Task(Task.TaskType.PresionarI), 5);
        taskQueue.Enqueue(new Task(Task.TaskType.Comprar), 4);
        taskQueue.Enqueue(new Task(Task.TaskType.TierraAire), 3);
        taskQueue.Enqueue(new Task(Task.TaskType.UnlockedList), 2);
        taskQueue.Enqueue(new Task(Task.TaskType.Ganar), 1);

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
        if (animating || taskQueue.Count <= 0)
        {
            Debug.Log("No hay tareas para completar");
            return;
        }
        taskQueue.Dequeue();
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
        AnimateText();
    }
    private void AnimateText()
    {
        if (animateRoutine != null)
            StopCoroutine(animateRoutine);

        animateRoutine = StartCoroutine(AnimateTaskCoroutine());
    }
    private IEnumerator AnimateTaskCoroutine()
    {
        taskReady = false;
        animating = true;
        textTransform.anchoredPosition = originalPosition;
        textTransform.localScale = originalScale;
        yield return new WaitForSeconds(2f);
        var moveTween = textTransform.DOAnchorPos(new Vector2(-722f, 291f), 0.5f).SetEase(Ease.OutQuad);
        var scaleTween = textTransform.DOScale(new Vector3(2.2f, 2.4f, 1f), 0.5f).SetEase(Ease.OutBack);
        yield return moveTween.WaitForCompletion();
        yield return scaleTween.WaitForCompletion();
        animating = false;
        taskReady = true;
    }
    private bool IsCurrentPriority(int expectedPriority)
    {
        taskQueue.TryPeek(out var currentNode, out int priority);
        return currentNode != null && priority == expectedPriority;
    }
    //public bool IsTaskQueueEmpty()
    //{
    //    return taskQueue.Count == 0;
    //}
    private void CheckTaskMove()
    {
        if (IsCurrentPriority(10) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckAttack()
    {
        if (IsCurrentPriority(9) && taskReady)
        {
            CompleteCurrentTask();
}
    }
    private void CheckSelectElement1()
    {
        if (IsCurrentPriority(8) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckSelectElement2()
    {
        if (IsCurrentPriority(7) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckCoinsCollection(Vector3 empty)
    {
        if (IsCurrentPriority(6) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckInventoryOpened()
    {
        if (IsCurrentPriority(5) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckPurchase()
    {
        if (IsCurrentPriority(4) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckAbilityUsed()
    {
        if (IsCurrentPriority(3) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    public void CheckUnlockedList()
    {
        if (IsCurrentPriority(2) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
    private void CheckWin()
    {
        if (IsCurrentPriority(1) && taskReady)
        {
            CompleteCurrentTask();
        }
    }
}
