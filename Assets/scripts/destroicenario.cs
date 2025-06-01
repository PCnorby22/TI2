using UnityEngine;

public class destroicenario : MonoBehaviour
{
    GameObject player;
    public GameObject cenario;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if ((this.transform.position - player.GetComponent<Transform>().position).normalized.z < 0.0f)
        {
            Destroy(cenario, 2);
        }
    }
}
