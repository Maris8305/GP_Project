using UnityEngine;

namespace AH2722
{
    public class FlashlightItem : InteractableBase
    {
        [Header("Flashlight References")]
        // Drag the Flashlight object that is attached to the Player's hand here
        [SerializeField] private GameObject playerFlashlightModel;

        public override void Interact()
        {
            if (playerFlashlightModel != null)
            {
                // Activate the flashlight on the player's hand
                playerFlashlightModel.SetActive(true);

                Debug.Log($"Polymorphism: flashlight picked up and equipped.");

                // Hide the flashlight on the table
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Player Flashlight Model reference is missing in the Inspector!");
            }
        }
    }
}