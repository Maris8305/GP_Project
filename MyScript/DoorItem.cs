using UnityEngine;

namespace AH2722
{
    public class DoorItem : InteractableBase
    {
        [Header("Animation References")]
        [SerializeField] protected Animation doorAnim;

        protected bool isOpened = false;

        protected virtual void Start()
        {
            // Automatic fallback if you forgot to drag and drop in Inspector
            if (doorAnim == null)
            {
                doorAnim = GetComponent<Animation>();
                if (doorAnim == null) doorAnim = GetComponentInParent<Animation>();
            }

            // Debug check for development
            if (doorAnim == null)
            {
                Debug.LogWarning($"Animation component missing on {gameObject.name}. Please assign it in the Inspector.");
            }
        }

        public override void Interact()
        {
            if (doorAnim == null || doorAnim.isPlaying) return;

            isOpened = !isOpened;
            string animToPlay = isOpened ? "Door_Open" : "Door_Close";

            doorAnim.Play(animToPlay);
            Debug.Log($"Polymorphism: {objectName} playing {animToPlay}");
        }
    }
}