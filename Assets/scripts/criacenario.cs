using UnityEngine;

public class criacenario : MonoBehaviour
{
    public GameObject cenario;
    GameObject player;
    Vector3 p;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(cenario, new Vector3(this.gameObject.GetComponentInParent<Transform>().position.x*0, this.gameObject.GetComponentInParent<Transform>().position.y - 23.3f, this.gameObject.GetComponentInParent<Transform>().position.z+ 32.5f), this.transform.rotation);
    }
}
