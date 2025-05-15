using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
class PlayerData
{
    public int dimdim;
}
public class menu : MonoBehaviour
{
    [SerializeField]
    GameObject configuracao;
    [SerializeField]
    GameObject inicial, Pause, Despause;
    public TextMeshProUGUI dinheiroP;
    public AudioMixer mixer;
    public Slider musicSlider, effectsSlider, masterSlider;
    int dimdimP;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name!="fase1"|| SceneManager.GetActiveScene().name != "menu fase") 
        {
            dinheiroP.text = "dindin player:\n9999999999";
            string path = Application.persistentDataPath + "/playerdata.json";
            if (File.Exists(path))
            {
                Debug.Log("ja existe");
                loaddata();
            }
            else
            {
                Debug.Log("criar");
                Savedata();
               
            }
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "inicio")
        {
            mixer.SetFloat("master", ((masterSlider.value / 100) * 85) - 80);
            mixer.SetFloat("efeitos", ((effectsSlider.value / 100) * 85) - 80);
            mixer.SetFloat("musica", ((musicSlider.value / 100) * 85) - 80);
        }
    }
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
        configuracao.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void depause()
    {
        Despause.SetActive(false);
        inicial.SetActive(false);
        Pause.SetActive(true);
        configuracao.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Savedata()
    {
        PlayerData data = new PlayerData
        {
            dimdim = dimdimP
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json",json);
        Debug.Log(Application.persistentDataPath + "/playerdata.json");
    }
    public void Savedata(int d)
    {
        PlayerData data = new PlayerData
        {
            dimdim = d
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
        Debug.Log(Application.persistentDataPath + "/playerdata.json");
    }
    public void loaddata()
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        Debug.Log(Application.persistentDataPath + "/playerdata.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            dimdimP = data.dimdim;
            dinheiroP.text = "dindin player:\n" + dimdimP;
        }
    }
}
