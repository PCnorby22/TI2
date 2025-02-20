using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField]
    GameObject configuraçao;
    [SerializeField]
    GameObject inicial;
    // Start is called before the first frame update
    public void play()
    {
        SceneManager.LoadScene("jogo");
    }
    public void config()
    {
        inicial.SetActive(false);
        configuraçao.SetActive(true);
    }
    public void voltar()
    {
        inicial.SetActive(true);
        configuraçao.SetActive(false);
    }
}
