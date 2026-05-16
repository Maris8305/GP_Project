using UnityEngine;

namespace AH2722
{
        public class KeyItem : InteractableBase
    {
        [Header("Key Settings")]
        [SerializeField] private string keyID = "Room1Key"; 

              public override void Interact()
        {
            // Add the key to the Singleton Inventory
            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.AddKey(keyID);

            // Deactivate the key object in the world
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Critical Error: PlayerInventory instance not found in scene!");
            }
        }
    }
}