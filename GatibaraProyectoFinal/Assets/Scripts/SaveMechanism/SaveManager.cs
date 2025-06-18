using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class SaveManager
{
    public void SaveUnlockedCombinations(List<string> keys)
    {
        string saveData = "";
        for (int i=0;i<keys.Count; i++)
        {
            saveData += keys[i];
            if(i< keys.Count - 1)
            {
                saveData += ",";
            }
        }
        PlayerPrefs.SetString("HabilidadesDesbloqueadas", saveData);
        PlayerPrefs.Save();
        Debug.Log("Guardado " + saveData);
    }
    public List<string> LoadUnlockedCombinations()
    {
        string saved = PlayerPrefs.GetString("HabilidadesDesbloqueadas", "");
        List<string> combinationsList = new List<string>();
        if (string.IsNullOrEmpty(saved))
        {
            return new List<string>();
        }
        else
        {            
            string[] savedCombinations = saved.Split(',');
            for(int i=0; i < savedCombinations.Length; i++)
            {
                combinationsList.Add(savedCombinations[i]);
            }
            return combinationsList;
        }
    }
}