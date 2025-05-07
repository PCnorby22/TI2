using UnityEngine;

public class criaitem : MonoBehaviour
{
    public GameObject[] item;
    public selecionalinha selecionalinha;
    private void Awake()
    {
        int i = selecionalinha.intem();
        int l = selecionalinha.linha();
        if (l == 0)
        {
            if (this.transform.position.x <= -14) {
                Debug.Log(this.transform.position);
                Instantiate(item[i], this.transform.position, this.transform.rotation);
            }
        }
        else if (l == 1)
        {
            if (this.transform.position.x == 0)
            {
                Debug.Log(this.transform.position);
                Instantiate(item[i], this.transform.position, this.transform.rotation);
            }
        }
        else
        {
            if (this.transform.position.x >= 14)
            {
                Debug.Log(this.transform.position);
                Instantiate(item[i], this.transform.position, this.transform.rotation);

            }
        }
    }
}
