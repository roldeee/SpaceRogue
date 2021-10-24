using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public CharacterController characterController;
    public int health = 100;

    private Vector3 movement;
    private string currentScene;
    [SerializeField] private bool canOpenDoor = false;
    [SerializeField] private bool inInteractRange = false;

    private int count;
    public TextMeshProUGUI countText;

    // TODO: Dynamically get all active scenes.
    List<string> scenes = new List<string>
    {
        "Scene2",
        "Scene3",
        "Scene4"
    };

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        movement = Vector3.zero;
        characterController = GetComponent<CharacterController>();

        count = 0;
        SetCountText();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movement *= speed;
        movement = transform.rotation * movement;

        characterController.Move(movement * Time.deltaTime);

        if (DoorCanOpen() && Input.GetKeyDown(KeyCode.F))
        {
            string nextScene = "";
            do
            {
                nextScene = scenes[Random.Range(0, scenes.Count)];
            } while (nextScene.Length == 0 || nextScene == currentScene);
            SceneManager.LoadScene(nextScene);
        }
        Rotation();
    }

    private bool DoorCanOpen()
    {
        // Check if all enemies killed and player next to door
        if (roomCleared() && inInteractRange)
        {
            canOpenDoor = true;
        } else
        {
            canOpenDoor = false;
        }
        return canOpenDoor;
    }

    private bool roomCleared()
    {
        // Check if room cleared.
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            inInteractRange = true;
        } else if (other.tag == "Enemy")
        {
            TakeDamage(10);
        }



        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            inInteractRange = false;
        }
    }

    void Rotation()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }

    void SetCountText()
    {
        countText.text = "Food: " + count.ToString();
    }
}