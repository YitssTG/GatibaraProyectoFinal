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
        Nivel2,
        TierraAire,
        Habilidad,
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
                return "Ataca con el click izquierdo.";
            case TaskType.SeleccionarElemento1:
                return "Presiona SPACE para iniciar la selección de elementos";
            case TaskType.SeleccionarElemento2:
                return "Presiona SPACE otra vez para seleccionar el elemento";
            case TaskType.ConseguirMonedas:
                return "Consigue monedas.";
            case TaskType.PresionarI:
                return "Presiona I para abrir el libro de elementos.";
            case TaskType.Comprar:
                return "Compra la mejora.";
            case TaskType.Nivel2:
                return "Desbloquear el nivel 2.";
            case TaskType.TierraAire:
                return "Crea la combinación Tierra + Aire.";
            case TaskType.Habilidad:
                return "Lanzar habilidad combinada con click.";
            case TaskType.Ganar:
                return "Completar el nivel.";
            default:
                return "Tarea desconocida";
        }
    }
}
