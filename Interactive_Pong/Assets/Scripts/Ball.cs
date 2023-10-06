using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    AudioManager audioManager;

    [SerializeField]
    public GameObject light;

    public float speed = 400.0f;

    float OriginalRadius = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        OriginalRadius = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius;
        Debug.Log(OriginalRadius);
    }

    private void Start()
    {
        ResetPosition();
        AddStartingForce();
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        Vector2 direction = new Vector2(x, y);
        rb.AddForce(direction * this.speed);
    }

    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void ResetPosition()
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = OriginalRadius;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            audioManager.Play("Wall Hit");
        }
        if (collision.gameObject.CompareTag("Paddle"))
        {
            audioManager.Play("Paddle Hit");           
            if (GameModeSetter.GameMode == "Hard" || GameModeSetter.GameMode == "FlashlightHard"){
                Vector2 CurrentDirection = rb.velocity;
                rb.AddForce(CurrentDirection * (this.speed * 0.08f));
            }

            if (GameModeSetter.GameMode == "Ultimate"){
                Vector2 CurrentDirection = rb.velocity;
                rb.AddForce(CurrentDirection * (this.speed * 0.05f));
                
                if((light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius) > 0.50f){
                    light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = (light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius * 0.90f);
                }
            }
            
        }
    }
}
