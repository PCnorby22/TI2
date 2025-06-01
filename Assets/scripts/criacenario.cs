using UnityEngine;

public class criacenario : MonoBehaviour
{
    public GameObject []cenario;
    public GameObject P;
    GameObject escolheLI;
    Vector3 p;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        escolheLI = GameObject.FindGameObjectWithTag("gerar");
    }
    private void OnTriggerEnter(Collider other)
    {
        int x = Random.Range(0, 3);
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("cenario :" + x);
            Instantiate(cenario[x], new Vector3(P.transform.position.x, P.transform.position.y, P.transform.position.z+140),P.transform.rotation);
            escolheLI.GetComponent<linhaeintem>().gerar();
        }
    }
}
