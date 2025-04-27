using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BottleInteraction : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform bottleObject;  // Drag "Wine Bottle Red Broke 2"
    [SerializeField] private GameObject panel;        // Drag "Spatial Panel Manipulator Model"

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool returning = false;
    private float returnSpeed = 2.5f;

    private Vector3 panelOffset;

    void Awake()
    {
        if (bottleObject == null || panel == null)
        {
            Debug.LogError("Assigne les références dans l'inspector !");
            return;
        }

        // Position initiale
        initialPosition = bottleObject.position;
        initialRotation = bottleObject.rotation;

        // Calcul de l'offset entre la bouteille et le panneau
        panelOffset = panel.transform.position - bottleObject.position;

        // Référence Grab
        grabInteractable = bottleObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Masquer le panneau au début
        panel.SetActive(false);

        // Événements de grab
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        panel.SetActive(true);
        returning = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        panel.SetActive(false);
        returning = true;
    }

    void Update()
    {
        // Suivi du panneau avec offset
        if (panel.activeSelf)
        {
            panel.transform.position = bottleObject.position + panelOffset;
            panel.transform.rotation = bottleObject.rotation; // Facultatif si tu veux suivre l'orientation
        }

        // Retour fluide à la position initiale
        if (returning)
        {
            bottleObject.position = Vector3.Lerp(bottleObject.position, initialPosition, Time.deltaTime * returnSpeed);
            bottleObject.rotation = Quaternion.Lerp(bottleObject.rotation, initialRotation, Time.deltaTime * returnSpeed);

            if (Vector3.Distance(bottleObject.position, initialPosition) < 0.01f &&
                Quaternion.Angle(bottleObject.rotation, initialRotation) < 1f)
            {
                bottleObject.position = initialPosition;
                bottleObject.rotation = initialRotation;

                Rigidbody rb = bottleObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                returning = false;
            }
        }
    }
}
