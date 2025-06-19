using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class moveEnemy : MonoBehaviour
{
    public float rayDistance = 20f;
    public LayerMask layerMask;
    Rigidbody rb;
    public int velocidade;
    public Slider vida;
    public GameObject sanguen;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 1 * velocidade * Time.timeScale);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,rayDistance) && !hit.collider.isTrigger)
        {
            int x = UnityEngine.Random.Range(0, 2);
            //Debug.Log(hit.collider.gameObject.name);
            if (transform.position.x==0)
            {
                if (x==0) 
                {
                    gameObject.GetComponent<Animator>().SetBool("L", true);
                    transform.position = new Vector3(-15, this.transform.position.y, this.transform.position.z);
                }
                else
                {
                    gameObject.GetComponent<Animator>().SetBool("R", true);
                    transform.position = new Vector3(15, this.transform.position.y, this.transform.position.z);
                }
            }
            else if(transform.position.x == -15)
            {
                gameObject.GetComponent<Animator>().SetBool("R", true);
                transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("L", true);
                transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    public void Dano(int d)
    {
        gameObject.GetComponent<Animator>().SetBool("danoR", true);
        vida.value -= d;
        Instantiate(sanguen, this.gameObject.transform);
    }
    public float Mostravida()
    {
        return vida.value;
    }
    public void acelera(int a)
    {
        velocidade = a;
    }
    private void OnAnimatorMove()
    {
        gameObject.GetComponent<Animator>().SetBool("danoR", false);
        gameObject.GetComponent<Animator>().SetBool("L", false);
        gameObject.GetComponent<Animator>().SetBool("R", false);
    }

}
