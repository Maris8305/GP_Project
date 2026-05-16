using UnityEngine;

namespace AH2722
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        // ENCAPSULATION: Hide variables from other classes but keep them visible in Inspector.
        [Header("Interaction Settings")]
        [SerializeField] protected string objectName;
        [SerializeField] protected float interactionDelay = 0f;

        // INHERITANCE: This method is abstract, forcing child classes to implement their own logic.
        public abstract void Interact();
    }
}