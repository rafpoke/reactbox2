using UnityEngine;

public class MoveInfinite : MonoBehaviour
{
    public float moveSpeed = 100f;

    void Update()
    {
        // Movimento contínuo
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
}
}