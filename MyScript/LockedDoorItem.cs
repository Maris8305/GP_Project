using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace AH2722
{
        public class LockedDoorItem : DoorItem
    {
        [Header("Lock Settings")]
        [SerializeField] protected string requiredKeyID = "Room1Key";
        [SerializeField] private string lockedMessage = "The door is locked...";

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI notificationText; 

        // POLYMORPHISM: Overriding the Interact method to add key check logic
        public override void Interact()
        {
            if (PlayerInventory.Instance.HasKey(requiredKeyID))
            {
                // If player has the key, use the logic from the base class (DoorItem)
                base.Interact();
            }
            else
            {
                PlayLockedEffect();
            }
        }

        private void PlayLockedEffect()
        {
            // Sync animation with DoorItem's doorAnim
            if (doorAnim != null && doorAnim.GetClip("Door_Jam") != null)
            {
                doorAnim.Play("Door_Jam");
            }

            // Display the message on screen
            if (notificationText != null)
            {
                StopAllCoroutines(); 
                StartCoroutine(ShowMessageRoutine());
            }

            Debug.Log($"Interaction Feedback: {lockedMessage}");
        }

        private IEnumerator ShowMessageRoutine()
        {
            notificationText.text = lockedMessage;
            notificationText.gameObject.SetActive(true);

            yield return new WaitForSeconds(2.0f); 

            notificationText.gameObject.SetActive(false);
        }
    }
}