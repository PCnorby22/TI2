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
    int dinheiroC=0, acelera=0;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private Vector2 startouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;
    public  TextMeshProUGUI vidaTela, dindin;
    Vector3 inicio;
    bool semdano=false, jasalvo=false, deudano=false;
    public menu menu;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
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
        dindin.text = "dindin\n" + dinheiroC; 
        inicio = this.transform.position;
        inimigo = GameObject.FindGameObjectWithTag("inimigo");
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
        if (distancia.value > distancia.maxValue)
        {
            distancia.value = distancia.maxValue;
        }
        Debug.Log((int)dis.z);
        if (vida <= 0)
        {
            derrota.SetActive(true);
            voltar.SetActive(true);
            reiniciar.SetActive(true);
            fundo.SetActive(true);
            Time.timeScale = 0;
        }
        else if (inimigo.GetComponent<moveEnemy>().Mostravida()<=0f)
        {
            vitoria.SetActive(true);
            voltar.SetActive(true);
            reiniciar.SetActive(true);
            fundo.SetActive(true);
            if (!jasalvo)
            {
                menu.Savedata(dinheiroC);
                jasalvo =true;
            }
            Time.timeScale = 0;
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
        else if ((inimigo.transform.position- this.transform.position).magnitude>=distancia.maxValue && deudano)
        {
            T = 0;
            deudano = false;
            Debug.Log("voltaaaaaaaaaaaaaaaaaaaaa");
            acelera += 5;
            velocidade = 30 + acelera;
            inimigo.GetComponent<moveEnemy>().acelera(velocidade);
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
            Debug.Log("swipe detected: " + swipeDirection.normalized);
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
            poder.value += 1;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<AudioSource>().clip = clips[1];
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject,1);
        }
    }
}
