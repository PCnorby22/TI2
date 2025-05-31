using UnityEngine;
using UnityEngine.SceneManagement;

public class criaitem : MonoBehaviour
{
    public GameObject[] item;
    public selecionalinha selecionalinha;
    private void Awake()
    {
        int l = selecionalinha.linha();
        int i = selecionalinha.intem();
        if (SceneManager.GetActiveScene().name != "faseinfinida")
        {
            
            if (l == 0)
            {
                if (this.transform.position.x <= -14)
                {
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
                if (this.transform.position.x >= 14)
                {
                    Instantiate(item[i], this.transform.position, this.transform.rotation);

                }
            }
        }
    }
}
