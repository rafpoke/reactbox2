using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float timer = 0f; // Timer para controlar o tempo de troca de estado
    float switchTime = 5f; // Tempo entre transições

    public Transform[] controlPoints; // Pontos de controle para a curva Bézier
    public float speed = 0.1f; // Velocidade ao longo da curva
    public float rotationSpeed = 5f; // Velocidade de rotação suave
    private float t = 0f; // Progresso ao longo da curva
    private int currentCurveIndex = 0; // Índice da curva Bézier atual (de 1 em 1 ponto)

    void Start()
    {
        animator = GetComponent<Animator>();
        // Começa no estado Walking
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsDancing", false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Alterna os estados somente após o tempo definido
        if (timer >= switchTime)
        {
            bool isWalking = animator.GetBool("IsWalking");

            // Alterna entre Walking e Dancing
            animator.SetBool("IsWalking", !isWalking);
            animator.SetBool("IsDancing", isWalking);

            // Reseta o timer
            timer = 0f;
        }

        // Movimento ao longo da curva Bézier no estado Walking
        if (animator.GetBool("IsWalking"))
        {
            MoveAlongBezierCurve();
        }
    }

    void MoveAlongBezierCurve()
    {
        if (controlPoints.Length < 6) return; // Precisamos de ao menos 6 pontos para curvas contínuas

        // Calcula o índice inicial da curva atual
        int startIndex = currentCurveIndex;

        // Define os pontos da curva atual (fazendo loop para conectar o último ao primeiro)
        Vector3 p0 = controlPoints[startIndex % controlPoints.Length].position;
        Vector3 p1 = controlPoints[(startIndex + 1) % controlPoints.Length].position;
        Vector3 p2 = controlPoints[(startIndex + 2) % controlPoints.Length].position;
        Vector3 p3 = controlPoints[(startIndex + 3) % controlPoints.Length].position;

        // Calcula a posição ao longo da curva
        Vector3 position = CalculateBezierPoint(t, p0, p1, p2, p3);

        // Calcula a direção para o próximo ponto na curva
        Vector3 direction = (position - transform.position).normalized;

        // Faz o personagem girar suavemente na direção do movimento
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move o personagem para a posição calculada
        transform.position = position;

        // Atualiza o progresso na curva
        t += speed * Time.deltaTime;

        // Avança para a próxima curva quando t atinge 1
        if (t > 1f)
        {
            t = 0f; // Reinicia o progresso
            currentCurveIndex = (currentCurveIndex + 3) % controlPoints.Length; // Avança 3 pontos e reinicia no loop
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; // (1-t)^3 * p0
        point += 3 * uu * t * p1; // 3 * (1-t)^2 * t * p1
        point += 3 * u * tt * p2; // 3 * (1-t) * t^2 * p2
        point += ttt * p3; // t^3 * p3

        return point;
    }
}
