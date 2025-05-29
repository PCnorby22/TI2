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
class Score
{
    public int[] pontuacao;
}
public class menu : MonoBehaviour
{
    [SerializeField]
    GameObject configuracao;
    [SerializeField]
    GameObject inicial, Pause, Despause, fundo;
    public TextMeshProUGUI dinheiroP;
    public AudioMixer mixer;
    public Slider musicSlider, effectsSlider, masterSlider;
    int dimdimP;
    int [] higscore;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name!="fase1"&& SceneManager.GetActiveScene().name != "menu fase"&& SceneManager.GetActiveScene().name != "faseinfinida") 
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
        else if(SceneManager.GetActiveScene().name == "faseinfinida")
        {
            string path = Application.persistentDataPath + "/Score.json";
            if (File.Exists(path))
            {
                Debug.Log("ja existe");
                loaddataScore();
            }
            else
            {
                Debug.Log("criar");
                SavedataScore();

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
        SceneManager.LoadScene("fase2");
    }
    public void Fase3()
    {
        SceneManager.LoadScene("fase3");
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
        fundo.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void depause()
    {
        Despause.SetActive(false);
        inicial.SetActive(false);
        Pause.SetActive(true);
        configuracao.SetActive(false);
        fundo.SetActive(false);
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
        loaddata();
        PlayerData data = new PlayerData
        {
            dimdim = d+dimdimP
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
    public void SavedataScore()
    {
        Score data = new Score
        {
            pontuacao = new int[5]
        };
        higscore = new int[data.pontuacao.Length];
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/Score.json", json);
        Debug.Log(Application.persistentDataPath + "/Score.json");
        Debug.Log(higscore);
    }
    public void SavedataScore(int d)
    {
        loaddataScore();
        higscore = LerloaddataScore();
        Debug.Log(higscore);
        for(int i=0; i < higscore.Length; i++)
        {
            if (higscore[i] < d)
            {
                higscore[i] = d;
                Debug.Log(higscore[i]);
                i = higscore.Length;
            }
        }
        Score data = new Score
        {
            pontuacao = higscore
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/Score.json", json);
        Debug.Log(Application.persistentDataPath + "/Score.json");
    }
    public void loaddataScore()
    {
        string path = Application.persistentDataPath + "/Score.json";
        Debug.Log(Application.persistentDataPath + "/Score.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Score data = JsonUtility.FromJson<Score>(json);
            higscore = new int[data.pontuacao.Length];
            higscore = data.pontuacao;
        }
        Debug.Log(higscore);
    }
    public int[] LerloaddataScore()
    {
        string path = Application.persistentDataPath + "/Score.json";
        Debug.Log(Application.persistentDataPath + "/Score.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Score data = JsonUtility.FromJson<Score>(json);
            higscore = new int[data.pontuacao.Length];
            Debug.Log(higscore);
            return data.pontuacao;
        }
        else
        {
            return null;
        }
    }
}
