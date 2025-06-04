using UnityEngine;

public class criaatackpersonagem : MonoBehaviour
{
    int t = 0;
    public GameObject soco, fumaça;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += 1;
        if (t >= 200)
        {
            float x = Random.Range(0, 1f);
            if (x > 0.7)
            {
                Debug.Log(x)
;                Instantiate(soco, this.transform.position, this.transform.rotation);
                Instantiate(fumaça,this.gameObject.transform);
                
            }
            t = 0;

        }
    }
}
