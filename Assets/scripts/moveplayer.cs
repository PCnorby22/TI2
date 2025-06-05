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
    public GameObject voltar, reiniciar, derrota, vitoria, inimigo,fundo;
    public Slider distancia, poder;
    Rigidbody rb;
    public int velocidade, vida, proxdistancia=300, T=0;
    int dinheiroC=0, acelera=0, dano=2;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private Vector2 startouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;
    public  TextMeshProUGUI vidaTela, dindin, pontuacao;
    Vector3 inicio;
    bool semdano=false, jasalvo=false, deudano=false, jamostrou=false;
    public menu menu;
    public float slowTimeScale = 0.2f;
    public float duration = 5f;         
    private bool isActive = false, isActionD = false, isActionT = false, isActionB = false, isActionA = false;
    private float timer = 0f;
    public float doubleTapMaxDelay = 0.4f;
    private float lastTapTime = 0f;

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
        rb.linearVelocity =new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 1*velocidade*Time.timeScale);
        Vector3 dis = inimigo.transform.position-this.transform.position;
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

                    menu.Savedata(dinheiroC);
                    jasalvo = true;
                }
                Time.timeScale = 0;
            }
            if (poder.value >= poder.maxValue)
            {
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
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

                        }
                        else if (isActionA)
                        {

                        }
                            lastTapTime = 0f; // Reset para evitar múltiplas ativações
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
            if (Input.touchCount > 0)
        {
            int touchLimit = Mathf.Min(Input.touchCount, 5);
            for (int i = 0; i < touchLimit; i++)
            {
                Touch touch = Input.touches[i];
                Debug.Log($"Toque {i + 1}: Posição = {touch.position}, Fase = {touch.phase}");
                if (i == 4)
                {
                    semdano = true;
                }
                else if (i == 2)
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
        if(swipeDirection.magnitude > 50)
        {
           //Debug.Log("swipe detected: " + swipeDirection.normalized);
            Vector3 startWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(startouchPosition.x, startouchPosition.y, 10));
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(endTouchPosition.x, endTouchPosition.y, 10));
            Debug.DrawLine(startWorldPosition, endWorldPosition, Color.red, 2.0f);
            if (swipeDirection.normalized.x > 0 && this.transform.position.x > -15)
            {
                rb.MovePosition( this.transform.position + new Vector3(-15, 0, 0));
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

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  // Importante ajustar para física
        isActive = true;
        timer = 0f;
    }

    public void Dobrodedano()
    {
        if (isActive) return;
        dano *= 2;
        isActive = true;
    }
    public void Blink()
    {
        if (isActive) return;
        this.transform.position = new Vector3(this.transform.position.x+10f, this.transform.position.y);
        isActive = true;
        timer = duration;
    }
    public void Atackimediato()
    {
        if (isActive) return;
        inimigo.GetComponent<moveEnemy>().Dano(dano/2);
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
            }
            //this.GetComponent<Collider>().isTrigger = true;
        }
        if (collision.gameObject.tag == "inimigo")
        {
            velocidade = 10+acelera;
            inimigo.GetComponent<moveEnemy>().Dano(2);
            inimigo.GetComponent<AudioSource>().clip = clips[2];
            inimigo.GetComponent<AudioSource>().Play();
            acelera += 5;
            deudano = true;
            Debug.Log("dano");
        }
        if (collision.gameObject.tag == "soco")
        {;
            vida--;
            vidaTela.text = vida.ToString();
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<AudioSource>().clip = clips[3];
            collision.gameObject.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject, 1);
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
            Destroy(other.gameObject,1);
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
            Destroy(other.gameObject,1);
        }
    }
}
