using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureObject : MonoBehaviour, IInteractable
{
    public StructureData data;//ItemData와 변수명 동일

    public string GetInteractPrompt()
    {
        string str = $"{data.structureName}\n{data.structureDescription}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.structureData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
