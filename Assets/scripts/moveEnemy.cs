using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class moveEnemy : MonoBehaviour
{
    public GameObject voltar, reiniciar, derrota, vitoria;
    public Slider distancia;
    Rigidbody rb;
    public int velocidade, vida;
    public UnityEngine.Object Enemy;
    
    public TextMeshProUGUI vidaTela;
    Vector3 inicio;
    private void Awake()
    {
        
  
    }
    private void OnEnable()
    {
       
    }
    private void OnDisable()
    {
    }
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        inicio = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        if (vida <= 0)
        {
            vitoria.SetActive(true);

        }


        }
  


    private void EnemyMovement()
    {
        if (this.transform.position.x < -15)
        {
            this.transform.position = new Vector3(-15, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x > 15)
        {
            this.transform.position = new Vector3(15, this.transform.position.y, this.transform.position.z);
        }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 1 * velocidade * Time.fixedDeltaTime);
        Vector3 dis = this.transform.position - inicio;
        distancia.value = ((int)dis.magnitude);
        if(Time.deltaTime%20 == 0)
        {
            velocidade = velocidade/2;
        }
        if (Time.deltaTime%30 == 0)
        {
            velocidade = velocidade*2;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "player")
        {
            vida--;
           
        }
        if (collision.gameObject.tag == "obistaculo")
        {
            velocidade = -velocidade;
            while (Time.deltaTime % 30 != 0)
            {
                
            }
            velocidade = Math.Abs(velocidade);
            if (this.transform.position.x > -15 && this.transform.position.x <0)
            {
                this.transform.position = new Vector3(-15, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x < 15 && this.transform.position.x > 0)
            {
                this.transform.position = new Vector3(15, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
