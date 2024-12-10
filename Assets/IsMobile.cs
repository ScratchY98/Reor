using UnityEngine;

public class IsMobile : MonoBehaviour
{
    [SerializeField] private bool isWebGl;
    [SerializeField] private GameObject isMobileToggle;
    [HideInInspector] public static bool staticIsWebGL;
    [HideInInspector] public static bool isMobile = false;

    private void Start()
    {
        staticIsWebGL = isWebGl;
        isMobileToggle.SetActive(staticIsWebGL);
    }

    public void SetIsMobile(bool value)
    {
        isMobile = value;
    }
}
