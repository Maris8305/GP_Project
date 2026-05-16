using UnityEngine;
using System.Collections.Generic;
namespace AH2722
{

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    private List<string> heldKeys = new List<string>();

    void Awake() { Instance = this; }

    public void AddKey(string id) { heldKeys.Add(id); }
    public bool HasKey(string id) { return heldKeys.Contains(id); }
}
}