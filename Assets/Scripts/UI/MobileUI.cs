using UnityEngine;

public class MobileUI : MonoBehaviour
{
    [SerializeField] private GameObject mobileUI;
    private void Start()
    {
        bool isMobile = false;

        if (IsMobile.staticIsWebGL)
            isMobile = IsMobile.isMobile;
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            isMobile = true;

        mobileUI.SetActive(isMobile);
    }
}
