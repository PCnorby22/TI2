using UnityEngine;
using UnityEngine.SceneManagement;

public class criaitem : MonoBehaviour
{
    public GameObject[] item;
    public selecionalinha selecionalinha;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "faseinfinida")
        {
            int i = selecionalinha.intem();
            int l = selecionalinha.linha();
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
