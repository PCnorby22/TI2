using UnityEngine;
using UnityEngine.SceneManagement;

public class criaitem : MonoBehaviour
{
    public GameObject[] item;
    GameObject escolheLI;
    int l = 0;
    int i = 0;
    bool escolhe = true;
    private void Start()
    {
        escolheLI = GameObject.FindGameObjectWithTag("gerar");
        if (escolhe)
        {
            l = escolheLI.GetComponent<linhaeintem>().linha();
            i = escolheLI.GetComponent<linhaeintem>().item();
            //Debug.Log("pqmano pq" + l);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "faseinfinida" && escolhe)
        {
            escolhe = false;
            if (l == 0)
            {
                if (this.transform.position.x <= -14)
                {
                    Instantiate(item[i], this.transform.position, this.transform.rotation);
                }
            }
            else if (l == 2)
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
