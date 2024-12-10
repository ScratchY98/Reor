using UnityEngine;
using UnityEngine.InputSystem;

public class MirrorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mirrors;
    [SerializeField] private int mirrorId;
    [SerializeField] private GameObject mirrorUI;

    [SerializeField] private LightManager lightManager;
    [SerializeField] private PlayerInput playerInput;

    [HideInInspector] public bool canMove;

    private void Start()
    {
        canMove = true;
    }

    private void Update()
    {
        if (playerInput.actions["TurnOnLeft"].WasPerformedThisFrame())
            TurnMirror(-90f);

        if (playerInput.actions["TurnOnRight"].WasPerformedThisFrame())
            TurnMirror(90f);

        if (playerInput.actions["ChangeToNextMirror"].WasPerformedThisFrame())
            ChangeMirror(true);

        if (playerInput.actions["ChangeToBeforeMirror"].WasPerformedThisFrame())
            ChangeMirror(false);
    }

    public void TurnMirror(float amount)
    {
        if (!canMove)
            return;

        Vector3 rotation = mirrors[mirrorId].transform.rotation.eulerAngles;
        rotation.z += amount;
        mirrors[mirrorId].transform.rotation = Quaternion.Euler(rotation);

        lightManager.ResetAllLights();
    }

    public void ChangeMirror(bool isNext)
    {
        if (!canMove)
            return;

        if (isNext)
        {
            if (mirrorId < mirrors.Length - 1)
                mirrorId += 1;
            else
                mirrorId = 0;
        }
        else
        {
            if (mirrorId > 0)
                mirrorId -= 1;
            else
                mirrorId = mirrors.Length - 1;
        }

        mirrorUI.transform.position = mirrors[mirrorId].transform.position;
    }
}
