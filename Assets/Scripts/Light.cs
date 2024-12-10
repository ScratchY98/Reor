using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lrColor
{
    public string mirrorTag;
    public Color color;
}

[RequireComponent(typeof(LineRenderer))]
public class Light : MonoBehaviour
{
    [Header("Movement's Configuration")]
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float speed = 10f;
    private float originalSpeed;
    private Vector2 previousPosition;
    private Vector2 originalDirection;
    private GameObject lastCollision;

    [Header ("Scripts's Reference")]
    [SerializeField] private LightManager lightManager;

    [Header("Layers's Configuration")]
    [SerializeField] private LayerMask mirrorLayer;
    [SerializeField] private LayerMask finishPointLayer;

    [Header("Lr Colors's Configuration")]
    private LineRenderer lineRenderer;
    [SerializeField] private Color originalColor;
    [SerializeField] private List<lrColor> lrColors = new List<lrColor>();
    [SerializeField] private string simpleMirrorTag;
    [SerializeField] private string wallTag;

    // Others
    private Vector2 originalPos;
    private bool isFinish;

    private void Start()
    {
        originalSpeed = speed;
        originalPos = transform.position;
        originalDirection = direction;
        isFinish = false;

        previousPosition = transform.position;

        // Arrondir la direction initiale
        direction = QuantizeDirection(direction);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        lineRenderer.startColor = originalColor;
        lineRenderer.endColor = originalColor;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        DetectCollision();


        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
    }

    private void LateUpdate()
    {
        previousPosition = transform.position;
    }

    private void DetectCollision()
    {
        Vector2 currentPosition = transform.position;
        Vector2 moveDirection = currentPosition - previousPosition;
        float distance = moveDirection.magnitude;

            
        RaycastHit2D hit = Physics2D.Raycast(previousPosition, moveDirection, distance);

        if (hit.collider != null && lastCollision != hit.transform.gameObject)
            OnProjectileHit(hit);
    }

    private void OnProjectileHit(RaycastHit2D hit)
    {
        Vector2 newDirection = QuantizeDirection(Vector2.Reflect(direction, hit.normal).normalized);

        GameObject hitGO = hit.transform.gameObject;

        if (IsInLayerMask(hitGO, mirrorLayer) && newDirection != direction * -1)
        {
            if (!hitGO.CompareTag(simpleMirrorTag) && !hitGO.CompareTag(wallTag))
                ChangeLrColor(hitGO.tag);


            lastCollision = hitGO;
            direction = newDirection;
            previousPosition = hit.point;
        }
        else if (IsInLayerMask(hitGO, finishPointLayer) && lineRenderer.endColor == originalColor && !isFinish)
        {
            isFinish = true;
            speed = 0;
            lightManager.UpscaleFinishLightCount();
        }
        else
            speed = 0;
    }

    private void ChangeLrColor(string targetMirrorTag)
    {
        Color targetColor = lrColors.Find(colorConfig => colorConfig.mirrorTag == targetMirrorTag).color;

        lineRenderer.startColor = targetColor;
        lineRenderer.endColor = targetColor;
    }

    public void ResetLight()
    {
        if (isFinish)
            return;

        transform.position = originalPos;
        direction = originalDirection;
        speed = originalSpeed;
        previousPosition = transform.position;

        lastCollision = null;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    private Vector2 QuantizeDirection(Vector2 originalDirection)
    {
        float angle = Mathf.Atan2(originalDirection.y, originalDirection.x) * Mathf.Rad2Deg;

        angle = Mathf.Round(angle / 45f) * 45f;

        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask)  { return (mask.value & (1 << obj.layer)) != 0; }
}
