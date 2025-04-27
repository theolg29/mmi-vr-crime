using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectWeapon : MonoBehaviour
{
    [Header("Références")]
    public Transform rotatingPart; // ex: Ak47
    public GameObject selectionIndicator; // ex: SelectionIndicator (Quad ou Cylinder)

    [Header("Rotation")]
    public Vector3 rotationSpeed = new Vector3(0, 45, 0);

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool isSelected = false;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("Ce GameObject a besoin d’un XRBaseInteractable !");
            return;
        }

        interactable.selectEntered.AddListener(OnSelect);
        interactable.selectExited.AddListener(OnDeselect);

        // Masquer l’indicateur au début
        if (selectionIndicator != null)
            selectionIndicator.SetActive(false);
    }

    void Update()
    {
        if (!isSelected && rotatingPart != null)
            rotatingPart.Rotate(rotationSpeed * Time.deltaTime);
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        Debug.Log("Objet sélectionné !");
        isSelected = true;

        if (selectionIndicator != null)
            selectionIndicator.SetActive(true);
    }

    void OnDeselect(SelectExitEventArgs args)
    {
        Debug.Log("Objet désélectionné !");
        isSelected = false;

        if (selectionIndicator != null)
            selectionIndicator.SetActive(false);
    }
}
