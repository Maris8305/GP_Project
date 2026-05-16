using UnityEngine;
using System.Collections;

namespace AH2722
{
        public class JumpscareLockedDoor : LockedDoorItem
    {
        [Header("Jumpscare Visuals")]
        [SerializeField] private GameObject scareObject;
        [SerializeField] private AudioSource scareAudio;
        [SerializeField] private float displayDuration = 2.0f;

        private bool isScared = false;

               public override void Interact()
        {
              if (PlayerInventory.Instance != null && PlayerInventory.Instance.HasKey(requiredKeyID))
            {
                if (!isScared)
                {
                    StartCoroutine(HandleScareSequence());
                }
                else
                {
                    // If already scared, just function as a normal door
                    base.Interact();
                }
            }
            else
            {
                // No key-> Play the 'Jam' animation from LockedDoorItem
                base.Interact();
            }
        }

        private IEnumerator HandleScareSequence()
        {
            isScared = true;

            base.Interact();

            if (scareAudio != null) scareAudio.Play();
            if (scareObject != null) scareObject.SetActive(true);

            Debug.Log("Event: Jumpscare triggered successfully.");
            yield return new WaitForSeconds(displayDuration);

            // Make the ghost disappear 
            if (scareObject != null) scareObject.SetActive(false);
        }
    }
}