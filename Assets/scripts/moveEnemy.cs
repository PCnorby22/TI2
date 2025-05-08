using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class moveEnemy : MonoBehaviour
{
    public float rayDistance = 20f;
    public LayerMask layerMask;
    Rigidbody rb;
    public int velocidade;
    public Slider vida;
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
                    transform.position = new Vector3(-15, this.transform.position.y, this.transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(15, this.transform.position.y, this.transform.position.z);
                }
            }
            else if(transform.position.x == -15)
            {
                transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    public void dano(int d)
    {
        vida.value -= d;
    }
}
