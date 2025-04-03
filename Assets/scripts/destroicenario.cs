using UnityEngine;

public class destroicenario : MonoBehaviour
{
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnCollisionExit(Collision collision)
    {
        if ((this.transform.position - player.GetComponent<Transform>().position).normalized.z < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
