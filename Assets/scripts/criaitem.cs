using UnityEngine;

public class criaitem : MonoBehaviour
{
    public GameObject[] item;
    private void Awake()
    {

        Instantiate(item[0], this.transform.position, this.transform.rotation);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
