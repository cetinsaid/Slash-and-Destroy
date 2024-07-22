using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float yPos = -0.43f;
    [SerializeField] private float xPos = 4;
    [SerializeField] private float endForce = 15;
    [SerializeField] private float startForce = 10;
    [SerializeField] private int hitScore = 3;
    [SerializeField] private float torque = 15;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(addForce(startForce , endForce) , ForceMode.Impulse);
        rb.AddTorque(addTorque(torque) , ForceMode.Impulse);
        transform.position = randomPosition(xPos , yPos , 1);
        audioSource.volume = 1f;
    }

    public Vector3 addForce(float startForce , float endForce)
    {
        return Vector3.up * Random.Range(startForce, endForce);
    }

    public Vector3 addTorque(float torque)
    {
        return new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque));
    }

    public Vector3 randomPosition(float x, float y, float z)
    {
        return new Vector3(Random.Range(-x, x), y, z);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < yPos)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bad"))
        {
            gameManager.decrementLives();
        }
    }

    public void DestroyTarget()
    {
        gameManager.changeScore(hitScore);
        audioSource.PlayOneShot(clip , 1f);
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
