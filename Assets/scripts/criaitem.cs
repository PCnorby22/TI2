using UnityEngine;

public class criaitem : MonoBehaviour
{
    public GameObject[] item;
    public selecionalinha selecionalinha;
    private void Awake()
    {
        int i = Random.Range(0, 1);
        int l = selecionalinha.linha();
        if (l == 0)
        {
            if (this.transform.position.x == -0.3005181) {
                Instantiate(item[i], this.transform.position, this.transform.rotation);
            }
        }
        else if (l == 1)
        {
            if (this.transform.position.x == 0)
            {
                Instantiate(item[i], this.transform.position, this.transform.rotation);
            }
        }
        else
        {
            if (this.transform.position.x == 0.3005181)
            {
                Instantiate(item[i], this.transform.position, this.transform.rotation);
            }
        }
    }
}
