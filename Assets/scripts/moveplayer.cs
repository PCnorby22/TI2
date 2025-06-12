using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class moveplayer : MonoBehaviour
{
    public AudioClip[] clips;
    public GameObject voltar, reiniciar, derrota, vitoria, inimigo, fundo, shoque, efeitoblink;
    public Slider distancia, poder;
    Rigidbody rb;
    public int velocidade, vida, proxdistancia = 300, T = 0;
    int dinheiroC = 0, acelera = 0, dano = 2;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private Vector2 startouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;
    public TextMeshProUGUI vidaTela, dindin, pontuacao;
    Vector3 inicio;
    bool semdano = false, jasalvo = false, deudano = false, jamostrou = false, mudafase = true;
    public menu menu;
    public float slowTimeScale = 0.2f;
    public float duration = 5f;
    private bool isActive = false, isActionD = false, isActionT = false, isActionB = false, isActionA = false;
    private float timer = 0f;
    public float doubleTapMaxDelay = 0.4f;
    public float TapMaxDelay = 0.7f;
    private float lastTapTime = 0f;
    public AudioSource finalaudio;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
        PlayerData ativar = menu.MandaPLayer();
        isActionA = ativar.ativoA;
        isActionT = ativar.ativoT;
        isActionB = ativar.ativoB;
        isActionD = ativar.ativoD;
    }
    private void OnEnable()
    {
        touchPressAction.performed += TouchStarted;
        touchPressAction.canceled += TouchEnded;
    }
    private void OnDisable()
    {
        touchPressAction.performed -= TouchStarted;
        touchPressAction.canceled -= TouchEnded;
    }
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        vidaTela.text = vida.ToString();
        inicio = this.transform.position;
        if (SceneManager.GetActiveScene().name != "faseinfinida")
        {
            dindin.text = "dindin\n" + dinheiroC;
            inimigo = GameObject.FindGameObjectWithTag("inimigo");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < -15)
        {
            this.transform.position = new Vector3(-15, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x > 15)
        {
            this.transform.position = new Vector3(15, this.transform.position.y, this.transform.position.z);
        }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 1 * velocidade * Time.timeScale);
        Vector3 dis = inimigo.transform.position - this.transform.position;
        distancia.value = ((int)dis.z);
        if (SceneManager.GetActiveScene().name != "faseinfinida")
        {
            if (distancia.value > distancia.maxValue)
            {
                distancia.value = distancia.maxValue;
            }
            //Debug.Log((int)dis.z);
            if (vida <= 0)
            {
                //finalaudio.ignoreListenerPause = true;
                finalaudio.clip = clips[9];
                finalaudio.gameObject.SetActive(true);
                derrota.SetActive(true);
                voltar.SetActive(true);
                reiniciar.SetActive(true);
                fundo.SetActive(true);
                Time.timeScale = 0;
            }
            else if (inimigo.GetComponent<moveEnemy>().Mostravida() <= 0f)
            {

                vitoria.SetActive(true);
                voltar.SetActive(true);
                reiniciar.SetActive(true);
                fundo.SetActive(true);
                if (!jasalvo)
                {
                    PlayerData conquista = menu.MandaPLayer();
                    if (conquista.conquistasA[1])
                    {
                        conquista.conquistasA[1] = true;
                    }
                    menu.Savedata(dinheiroC);
                    jasalvo = true;
                }
                //finalaudio.ignoreListenerPause = true;
                finalaudio.clip = clips[8];
                finalaudio.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            int touchCount = GetActiveTouchCount();

            if (touchCount == 5)
                semdano = true;
            else if (touchCount == 4)
            {
                if (SceneManager.GetActiveScene().name == "fase1")
                {
                    SceneManager.LoadScene("fase2");
                }
                else if (SceneManager.GetActiveScene().name == "fase2")
                {
                    SceneManager.LoadScene("fase3");
                }
                else
                {
                    SceneManager.LoadScene("inicio");
                }
            }

            if (poder.value >= poder.maxValue)
            {
                if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
                {
                    float timeSinceLastTap = Time.unscaledTime - lastTapTime;

                    if (timeSinceLastTap <= doubleTapMaxDelay)
                    {
                        if (isActionT)
                        {
                            ActivateTimeDelay();
                        }
                        else if (isActionD)
                        {
                            Dobrodedano();
                        }
                        else if (isActionB)
                        {
                            Blink();
                        }
                        else if (isActionA)
                        {
                            Atackimediato();
                        }
                        lastTapTime = 0f;
                    }
                    else
                    {
                        lastTapTime = Time.unscaledTime;
                    }
                }

                if (isActive)
                {
                    timer += Time.unscaledDeltaTime;

                    if (timer >= duration)
                    {
                        Time.timeScale = 1f;
                        Time.fixedDeltaTime = 0.02f;
                        isActive = false;
                        timer = 0f;
                        poder.value = 0;
                        dano = 2;
                    }
                }
            }

        }
        else
        {
            dindin.text = ((int)(inicio - this.transform.position).magnitude) + "M";
            if (vida <= 0)
            {
                derrota.SetActive(true);
                if (!jasalvo)
                {
                    menu.SavedataScore((int)(inicio - this.transform.position).magnitude);
                    int[] higscore = menu.LerloaddataScore();
                    PlayerData conquistas = menu.MandaPLayer();
                    if((int)(inicio - this.transform.position).magnitude >= 1000)
                    {
                        conquistas.conquistasA[0] = true;
                        menu.recebePLayer(conquistas);
                    }
                    for (int i = 0; i < higscore.Length; i++)
                    {
                        pontuacao.text += "Player" + i + " = " + higscore[i] + "\n";
                    }
                    jasalvo = true;
                }
                voltar.SetActive(true);
                reiniciar.SetActive(true);
                fundo.SetActive(true);
                Time.timeScale = 0;
            }
        }
        
    }
    private void FixedUpdate()
    {

        T++;
        if (SceneManager.GetActiveScene().name != "faseinfinida")
        {
            if (this.transform.position.z >= inimigo.transform.position.z)
            {
                T = 0;
                velocidade = 10 + acelera;
                deudano = true;
                Debug.Log("errou");
            }
            else if (T == 500)
            {
                velocidade += 10;
                T = 0;
            }
            else if ((inimigo.transform.position - this.transform.position).magnitude >= distancia.maxValue && deudano)
            {
                T = 0;
                deudano = false;
                //Debug.Log("voltaaaaaaaaaaaaaaaaaaaaa");
                acelera += 5;
                velocidade = 30 + acelera;
                inimigo.GetComponent<moveEnemy>().acelera(velocidade);
            }
        }
        else
        {
            if (T == 500)
            {
                velocidade += 10;
                T = 0;
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

    private void TouchStarted(InputAction.CallbackContext context)
    {
        startouchPosition = touchPositionAction.ReadValue<Vector2>();
        isSwiping = true;
    }
    private void TouchEnded(InputAction.CallbackContext context)
    {
        endTouchPosition = touchPositionAction.ReadValue<Vector2>();
        isSwiping = false;
        DetectSwipe();
    }
    private void DetectSwipe()
    {
        Vector2 swipeDirection = startouchPosition - endTouchPosition;
        if (swipeDirection.magnitude > 50)
        {
            //Debug.Log("swipe detected: " + swipeDirection.normalized);
            Vector3 startWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(startouchPosition.x, startouchPosition.y, 10));
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(endTouchPosition.x, endTouchPosition.y, 10));
            Debug.DrawLine(startWorldPosition, endWorldPosition, Color.red, 2.0f);
            if (swipeDirection.normalized.x > 0 && this.transform.position.x > -15)
            {
                rb.MovePosition(this.transform.position + new Vector3(-15, 0, 0));
            }
            else if (swipeDirection.normalized.x < 0 && this.transform.position.x < 15)
            {
                rb.MovePosition(this.transform.position + new Vector3(15, 0, 0));
            }
        }
    }
    public void ActivateTimeDelay()
    {
        if (isActive) return;
        GetComponent<AudioSource>().ignoreListenerPause = true;
        GetComponent<AudioSource>().clip = clips[4];
        GetComponent<AudioSource>().Play();
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isActive = true;
        timer = 0f;
    }

    public void Dobrodedano()
    {
        if (isActive) return;
        dano *= 2;
        isActive = true;
        GetComponent<AudioSource>().clip = clips[5];
        GetComponent<AudioSource>().Play();
    }
    public void Blink()
    {
        if (isActive) return;
        GetComponent<AudioSource>().clip = clips[6];
        GetComponent<AudioSource>().Play();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 20f);
        isActive = true;
        Instantiate(efeitoblink, this.gameObject.transform);
        timer = duration;
    }
    public void Atackimediato()
    {
        if (isActive) return;
        GetComponent<AudioSource>().clip = clips[7];
        GetComponent<AudioSource>().Play();
        inimigo.GetComponent<moveEnemy>().Dano(dano / 2);
        inimigo.GetComponent<AudioSource>().clip = clips[2];
        inimigo.GetComponent<AudioSource>().Play();
        isActive = true;
        timer = duration;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obistaculo")
        {
            if (!semdano)
            {
                vida--;
                vidaTela.text = vida.ToString();
                GetComponent<AudioSource>().clip = clips[10];
                GetComponent<AudioSource>().Play();
                Instantiate(shoque, this.gameObject.transform);
            }
            //this.GetComponent<Collider>().isTrigger = true;
        }
        if (collision.gameObject.tag == "inimigo")
        {
            GetComponent<AudioSource>().clip = clips[11];
            GetComponent<AudioSource>().Play();
            velocidade = 10 + acelera;
            inimigo.GetComponent<moveEnemy>().Dano(dano);
            inimigo.GetComponent<AudioSource>().clip = clips[2];
            inimigo.GetComponent<AudioSource>().Play();
            acelera += 5;
            deudano = true;
            Debug.Log("dano");
        }
        if (collision.gameObject.tag == "soco")
        {
            ;
            vida--;
            vidaTela.text = vida.ToString();
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<AudioSource>().clip = clips[3];
            collision.gameObject.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject, 1);
            GetComponent<AudioSource>().clip = clips[10];
            GetComponent<AudioSource>().Play();
            Instantiate(shoque, this.gameObject.transform);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "obistaculo")
        {
            //this.GetComponent<Collider>().isTrigger = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "obistaculo")
        {
            //this.GetComponent<Collider>().isTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "moeda")
        {
            //Debug.Log(other.gameObject.name);
            dinheiroC += 1;
            dindin.text = "dindin\n" + dinheiroC;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<AudioSource>().clip = clips[0];
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 1);
        }
        else if (other.gameObject.tag == "energia")
        {
            //Debug.Log(other.gameObject.name);
            if (poder.value <= poder.maxValue)
            {
                poder.value += 1;
            }
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<AudioSource>().clip = clips[1];
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 1);
        }
    }
}