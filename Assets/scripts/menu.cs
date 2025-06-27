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
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class PlayerData
{
    public int dimdim, bater;
    public bool ativoT, conpradoT, ativoD, conpradoD, ativoB, conpradoB, ativoA, conpradoA;
    public bool[] conquistasA;
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
    public TextMeshProUGUI dinheiroP, Nconquista;
    public AudioMixer mixer;
    public Slider musicSlider, effectsSlider, masterSlider;
    int dimdimP;
    int [] higscore;
    PlayerData Player;
    public Toggle[] poderesC = new Toggle[4], poderesA = new Toggle[4], conquistas = new Toggle[6]; 
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name== "inicio"|| SceneManager.GetActiveScene().name == "menu loja") 
        {
            dinheiroP.text = "dindin player:\n0000000";
            string path = Application.persistentDataPath + "/playerdata.json";
            if (File.Exists(path))
            {
                //Debug.Log("ja existe");
                loaddata();
            }
            else
            {
                //Debug.Log("criar");
                Savedata();
               
            }
            if(SceneManager.GetActiveScene().name == "menu loja")
            {
                poderesC[0].isOn = Player.conpradoT;
                poderesC[1].isOn = Player.conpradoD;
                poderesC[2].isOn = Player.conpradoB;
                poderesC[3].isOn = Player.conpradoA;
                poderesC[0].interactable = !Player.conpradoT;
                poderesC[1].interactable = !Player.conpradoD;
                poderesC[2].interactable = !Player.conpradoB;
                poderesC[3].interactable = !Player.conpradoA;
                for (int i = 0; i < poderesC.Length; i++)
                {
                    poderesA[i].isOn = false;

                    poderesA[i].gameObject.SetActive(poderesC[i].isOn);
                }
            }
            else
            {
                int x = 0;
                for (int i = 0; i < conquistas.Length; i++)
                {
                    conquistas[i].isOn = Player.conquistasA[i];
                    //Debug.Log(conquistas[i].isOn + "   " + Player.conquistasA[i]);
                    if (conquistas[i].isOn)
                    {
                        x++;
                    }
                }
                Nconquista.text = x.ToString();
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
            int touchCount = GetActiveTouchCount();
            if (touchCount == 4)
            {
                Savedata(100);
            }
        }
    }
    int GetActiveTouchCount()
    {
        int count = 0;
        foreach (var touch in Touchscreen.current.touches)
            if (touch.press.isPressed)
                count++;
        return count;
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
    public void FaseInfinita()
    {
        SceneManager.LoadScene("faseinfinida");
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
            fundo.SetActive(false);
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
    public void EXIT()
    {
        Application.Quit();
    }
    public void usarT()
    {
        loaddata();
        Player.ativoT = poderesA[0].isOn;
        Player.ativoA = false;
        Player.ativoB = false;
        Player.ativoD = false;
        classinteira();
    }
    public void usarD()
    {
        loaddata();
        Player.ativoD = poderesA[1].isOn;
        Player.ativoT = false;
        Player.ativoB = false;
        Player.ativoA = false;
        classinteira();
    }
    public void usarB()
    {
        loaddata();
        Player.ativoB = poderesA[2].isOn;
        Player.ativoA = false;
        Player.ativoT = false;
        Player.ativoD = false;
        classinteira();
    }
    public void usarA()
    {
        loaddata();
        Player.ativoA = poderesA[3].isOn;
        Player.ativoT = false;
        Player.ativoB = false;
        Player.ativoD = false;
        classinteira();
    }
    public void ComprerT()
    {
        loaddata();
        if (dimdimP >= 200 && !Player.conpradoT)
        {
            Player.conpradoT = true;
            poderesA[0].gameObject.SetActive(true);
            Player.dimdim = dimdimP - 200;
            classinteira();
        }
        else if (Player.conpradoT)
        {
            poderesC[0].isOn = true;
        }
        else
        {
            poderesC[0].isOn = false;
        }
    }
    public void ComprarD()
    {
        loaddata();
        if (dimdimP >= 200 && !Player.conpradoD)
        {
            Player.conpradoD = true;
            poderesA[1].gameObject.SetActive(true);
            Player.dimdim = dimdimP - 200;
            classinteira();
        }
        else if (Player.conpradoD)
        {
            poderesC[1].isOn = true;
        }
        else
        {
            poderesC[1].isOn = false;
        }
    }
    public void ComprarB()
    {
        loaddata();
        if (dimdimP >= 200 && !Player.conpradoB)
        {
            Player.conpradoB = true;
            poderesA[2].gameObject.SetActive(true);
            Player.dimdim = dimdimP - 200;
            classinteira();
        }
        else if (Player.conpradoB)
        {
            poderesC[2].isOn = true;
        }
        else
        {
            poderesC[2].isOn = false;
        }
    }
    public void ComprarA()
    {
        loaddata();
        if (dimdimP >= 200 && !Player.conpradoA)
        {
            Player.conpradoA = true;
            poderesA[3].gameObject.SetActive(true);
            Player.dimdim = dimdimP - 200;
            classinteira();
        }
        else if (Player.conpradoA)
        {
            poderesC[3].isOn = true;
        }
        else
        {
            poderesC[3].isOn = false;
        }
    }
    public void Abrirconquistas()
    {
        fundo.SetActive(true);
        inicial.SetActive(false);
    }
    public void Savedata()
    {
        PlayerData data = new PlayerData
        {
            dimdim = dimdimP, ativoA=false, ativoB = false, ativoD= false, ativoT=false, conpradoA = false, conpradoB = false, conpradoD = false, conpradoT = false, conquistasA = new bool[6], bater=0
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json",json);
        Debug.Log(Application.persistentDataPath + "/playerdata.json");
    }
    public void classinteira()
    {
        PlayerData data = new PlayerData();
        if(Player.conpradoA && Player.conpradoB && Player.conpradoD && Player.conpradoT)
        {
            Player.conquistasA[4] = true;
        }
        //Debug.Log(Player.conquistasA[4]);
        data = Player;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
        //Debug.Log(Application.persistentDataPath + "/playerdata.json");
    }
    public void Savedata(int d)
    {
        loaddata();
        Player.dimdim += d;
        if (Player.dimdim >= 1000)
        {
            Player.conquistasA[3] = true;
        }
        PlayerData data = Player;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
        Debug.Log(Application.persistentDataPath + "/playerdata.json");
    }
    public void loaddata()
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        //Debug.Log(Application.persistentDataPath + "/playerdata.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            dimdimP = data.dimdim;
            dinheiroP.text = "dindin player:\n" + dimdimP;
            Player = data;
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
    public PlayerData MandaPLayer()
    {
        loaddata();
        return Player;
    }
    public void recebePLayer(PlayerData P)
    {
        Player = P;
        classinteira();
    }
}
