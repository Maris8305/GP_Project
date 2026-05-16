using UnityEngine;
using UnityEngine.UI; 

namespace AH2722
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private Camera playerCam;
        [SerializeField] private float range = 3f;
        [SerializeField] private LayerMask interactLayer;

        [Header("UI Elements")]
        [SerializeField] private Image crosshairVisual;

        void Update()
        {
           CheckInteraction();
        }

        private void CheckInteraction()
        {
            // Create a ray from the center of the viewport
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, interactLayer))
            {
                // POLYMORPHISM: Look for the Interface
                IInteractable target = hit.collider.GetComponentInParent<IInteractable>();

                if (target != null)
                {
                    // Visual Feedback: Change crosshair color when looking at interactable objects
                    if (crosshairVisual != null) crosshairVisual.color = Color.red;

                    // Execute interaction on Key Press
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        target.Interact();
                    }
                }
            }
            else
            {
                // Reset crosshair color when not looking at anything interactable
                if (crosshairVisual != null) crosshairVisual.color = Color.white;
            }
        }
    }
}