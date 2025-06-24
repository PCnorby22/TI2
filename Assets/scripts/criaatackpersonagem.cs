using UnityEngine;

public class criaatackpersonagem : MonoBehaviour
{
    int t = 0;
    public GameObject soco, fumaca;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += 1;
        if (t >= 200 && Time.timeScale>0)
        {
            float x = Random.Range(0, 1f);
            if (x > 0.7)
            {
;                Instantiate(soco, this.transform.position, this.transform.rotation);
                Instantiate(fumaca,this.gameObject.transform);
                
            }
            t = 0;

        }
    }
}
