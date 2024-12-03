using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;         // Referência ao personagem
    public Vector3 offset = new Vector3(3.54f, 2.7f, 1.36f);  // Deslocamento fixo da câmera em relação ao personagem

    public float smoothSpeed = 0.125f;  // Velocidade da suavização do movimento da câmera

    void LateUpdate()
    {
        // Calcula a posição desejada com base na posição do personagem e no deslocamento
        Vector3 desiredPosition = player.position + offset;

        // Suaviza o movimento da câmera para que ela siga o personagem de forma mais fluida
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posição da câmera
        transform.position = desiredPosition;

        // A câmera sempre olha para o personagem
        transform.LookAt(player);
    }
}