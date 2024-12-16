using UnityEngine;

public class GemaRotacao : MonoBehaviour
{
    public float velocidadeRotacao = 50f; 

    void Update()
    {
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime, Space.World);
    }
}
