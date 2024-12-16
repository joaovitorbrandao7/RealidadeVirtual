using UnityEngine;

public class BolaControlador : MonoBehaviour
{
    public float velocidade = 5f; 
    public float limiteX = 10f;  
    public float raio = 0.5f;    

    private Vector3 direcao;

    void Start()
    {
       
        direcao = Vector3.right;
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime, Space.World);

        float deslocamento = velocidade * Time.deltaTime; 
        float anguloRotacao = (deslocamento / (2 * Mathf.PI * raio)) * 360f; 
        transform.Rotate(Vector3.forward, -anguloRotacao * Mathf.Sign(direcao.x));

        if (transform.position.x >= limiteX)
        {
            direcao = Vector3.left;
        }
        else if (transform.position.x <= -limiteX)
        {
            direcao = Vector3.right;
        }
    }
}
