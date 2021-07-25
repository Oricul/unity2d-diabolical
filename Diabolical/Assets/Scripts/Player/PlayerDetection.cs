using UnityEngine;
public class PlayerDetection : MonoBehaviour
{
    public RaycastHit2D[] DetectObject(Vector2 direction, float distance = 1f)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformDirection(direction), distance);
        return hits;
    }
}