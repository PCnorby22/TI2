using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class moveplayer : MonoBehaviour
{
    public GameObject voltar, reiniciar, derrota, vitoria, inimigo;
    public Slider distancia, poder;
    Rigidbody rb;
    public int velocidade, vida;
    int dinheiroC=0;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private Vector2 startouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;
    public  TextMeshProUGUI vidaTela, dindin;
    Vector3 inicio;
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
        rb.linearVelocity =new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 1*velocidade*Time.fixedDeltaTime);
        Vector3 dis = this.transform.position - inicio;
        distancia.value = ((int)dis.magnitude);
        if (vida <= 0)
        {
            derrota.SetActive(true);
            voltar.SetActive(true);
            reiniciar.SetActive(true);
            Time.timeScale = 0;
        }
        else if (dis.magnitude>=500f)
        {
            vitoria.SetActive(true);
            voltar.SetActive(true);
            reiniciar.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void FixedUpdate()
    {
        Debug.Log(distancia.value);
        if (distancia.value == 200)
        {
            velocidade += 500;
            Esperar(2);
        }
        else if (distancia.value == 400)
        {
            velocidade += 500;
        }
        if (this.transform.position.z >= inimigo.transform.position.z)
        {
            velocidade = 500;
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
            if (swipeDirection.normalized.y <= -1)
            {
                Debug.Log("olaaaa");
                rb.AddForce(Vector3.left*30000000000000);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obistaculo")
        {
            vida--;
            vidaTela.text = vida.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "moeda")
        {
            //Debug.Log(other.gameObject.name);
            dinheiroC += 1;
            dindin.text = "dindin\n" + dinheiroC;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "energia")
        {
            //Debug.Log(other.gameObject.name);
            poder.value += 1;
            Destroy(other.gameObject);
        }
    }
    System.Collections.IEnumerator Esperar(int x)
    {
        yield return new WaitForSeconds(1);
    }
}
