using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private int count;
    public float speed;
    public float jump = 5;
    public bool floorDetected = false;
    private Rigidbody rb;
    public Text countText;
    public GameObject cameraReference;

    public GameObject parkourVisible;

    public GameObject loseMenu;
    public GameObject winMenu;

    public AudioSource pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Points: " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && floorDetected)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            floorDetected = false;
        }

        if (transform.position.y <= -30)
        {
            ChangeScene(loseMenu.tag);
        }
        MakeParkourVisible();
    }
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rb.AddForce(z * cameraReference.transform.forward * speed);
        rb.AddForce(x * cameraReference.transform.right * speed);

    }

    void OnCollisionEnter(Collision collision)
    {
        floorDetected = true;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag != "platform")
        {
            other.gameObject.SetActive(false);
            count++;
            countText.text = "Points: " + count.ToString();
            pickUpSound.Play();

            if (count == 15)
            {
                yield return new WaitForSeconds(1.0f);
                ChangeScene(winMenu.tag);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "platform")
        {
            Vector3 vector = new Vector3(0, 1, 0);
            transform.position = other.transform.position + vector;
        }
    }

    private void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void MakeParkourVisible()
    {
        if (count >= 10)
        {
            parkourVisible.SetActive(true);
        }
    }

    private void timerEnded()
    {
        ChangeScene(winMenu.tag);
    }

}
