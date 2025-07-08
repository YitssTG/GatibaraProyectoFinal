using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    private TaskType _type;
    public enum TaskType
    {
        Moverse,
        Atacar,
        SeleccionarElemento1,
        SeleccionarElemento2,
        ConseguirMonedas,
        PresionarI,
        Comprar,
        TierraAire,
        UnlockedList,
        Ganar
    }
    public TaskType Type
    {
        get
        {
            return _type;
        }
        private set
        {
            _type = value;
        }
    }
    public Task(TaskType type)
    {
        Type = type;
    }
    public string GetTaskMessage()
    {
        switch (Type)
        {
            case TaskType.Moverse:
                return "Usa AWSD para moverte.";
            case TaskType.Atacar:
                return "Presiona click izquierdo \nPara atacar";
            case TaskType.SeleccionarElemento1:
                return "Presiona SPACE \nPara iniciar la selección de elementos";
            case TaskType.SeleccionarElemento2:
                return "Presiona SPACE otra vez \nPara seleccionar el elemento";
            case TaskType.ConseguirMonedas:
                return "Consigue monedas.";
            case TaskType.PresionarI:
                return "Presiona I \nPara abrir el libro de elementos.";
            case TaskType.Comprar:
                return "Compra la mejora \nPara subir al nivel 2.";
            case TaskType.TierraAire:
                return "Crea la combinación \nTierra + Aire y luego castea la habilidad con click.";
            case TaskType.UnlockedList:
                return "Presiona F \nPara anotar todas las habilidades descubiertas";
            case TaskType.Ganar:
                return "Completar el nivel.";
            default:
                return "Tarea desconocida";
        }
    }
}
