using UnityEngine;

public class movesoco : MonoBehaviour
{
    Rigidbody rb;
    public int velocidade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * velocidade * Time.timeScale;
        Destroy(this.gameObject, 6);
    }
}
