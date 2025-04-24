using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField]
    GameObject configuracao;
    [SerializeField]
    GameObject inicial, Pause, Despause;
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
        configuracao.SetActive(true);
    }
    public void voltar()
    {
        if (SceneManager.GetActiveScene().name == "inicio")
        {
            inicial.SetActive(true);
            configuracao.SetActive(false);
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
    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void pause()
    {
        Pause.SetActive(false);
        Despause.SetActive(true);
        inicial.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void depause()
    {
        Despause.SetActive(false);
        inicial.SetActive(false);
        Pause.SetActive(true);
        Time.timeScale = 1.0f;
    }
}
