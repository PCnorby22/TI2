using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
        SceneManager.LoadScene("menu fase");
    }
    public void Fase1()
    {
        SceneManager.LoadScene("fase1");
    }
    public void Fase2()
    {
        SceneManager.LoadScene("menu fase");
    }
    public void Fase3()
    {
        SceneManager.LoadScene("menu fase");
    }
    public void loja()
    {
        SceneManager.LoadScene("menu loja");
    }
    public void config()
    {
        inicial.SetActive(false);
        configuraçao.SetActive(true);
    }
    public void voltar()
    {
        if (SceneManager.GetActiveScene().name == "inicio")
        {
            inicial.SetActive(true);
            configuraçao.SetActive(false);
        }
        else
        {
            voltarmenu();
        }
    }
    public void voltarmenu()
    {
        SceneManager.LoadScene("inicio");
    }
}
