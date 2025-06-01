using UnityEngine;

public class linhaeintem : MonoBehaviour
{
    int l = 0;
    int i = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        l = Random.Range(0, 3);
        i = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int item()
    {
        return i;
    }
    public int linha()
    {
        return l;
    }
    public void gerar()
    {
        l = Random.Range(0, 3);
        i = Random.Range(0, 2);
    }
}
