using UnityEngine;

public class CanvasPlacement : MonoBehaviour
{
    [SerializeField] private Transform attachedTransform;

    private void OnRectTransformDimensionsChange()
    {
        var pos = transform.position;
        pos.z = 0;
        attachedTransform.position = pos;
    }
}
