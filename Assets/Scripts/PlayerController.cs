using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static string CLONE = "(Clone)";
    public float speed = 5f;
    public CharacterController characterController;
    public int health = 100;

    private Vector3 movement;
    private string currentScene;
    private RoomClearChecker roomClearChecker;
    private EventSystem eventSystem;
    private GameObject nextReward;
    private PlayerDataManager playerDataManager;

    private Animator animator;
    private int moveXParameterId;
    private int moveZParameterId;
    [SerializeField]
    private float animationSmoothTime = 0.1f;
    private Vector2 animationBlend;
    private Vector2 animationVelocity;

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
        animator = GetComponent<Animator>();
        playerDataManager = new PlayerDataManager();
        playerDataManager.Load();
        eventSystem = EventSystem.current;
        roomClearChecker = eventSystem.GetComponent<RoomClearChecker>();
        currentScene = SceneManager.GetActiveScene().name;
        movement = Vector3.zero;
        characterController = GetComponent<CharacterController>();
        playerDataManager.Load();
        moveXParameterId = Animator.StringToHash("MoveX");
        moveZParameterId = Animator.StringToHash("MoveZ");

        count = 0;
        SetCountText();
    }
                                                                    
    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movement *= speed;
        movement = transform.rotation * movement;

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animationBlend = Vector2.SmoothDamp(animationBlend, input, ref animationVelocity, animationSmoothTime);

        characterController.Move(movement * Time.deltaTime);

        animator.SetFloat(moveXParameterId, animationBlend.x);
        animator.SetFloat(moveZParameterId, animationBlend.y);

        // Player selects a reward and opens the door
        if (DoorCanOpen() && Input.GetKeyDown(KeyCode.F))
        {
            // Make sure the next reward is not null
            if (nextReward != null)
            {
                // Save the next reward to player data for use in the next scene.
                string nextRewardName = RemovePrefixAndSuffix(nextReward.name, RewardsHandler.PREVIEW, CLONE);
                Debug.Log("Setting next room reward to " + nextRewardName);
                playerDataManager.playerData.nextReward = RewardsHandler.getRewardEnum(nextRewardName);
                playerDataManager.Save();
            }

            // Choose the next scene randomly.
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
        if (roomClearChecker.IsRoomCleared() && inInteractRange)
        {
            canOpenDoor = true;
        } else
        {
            canOpenDoor = false;
        }
        return canOpenDoor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextReward")
        {
            nextReward = other.gameObject;
            Debug.Log("NextReward: " + nextReward.name);
        }
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
        if (other.tag == "NextReward")
        {
            nextReward = null;
        }
        if (other.tag == "Door")
        {
            inInteractRange = false;
        }
    }

    void Rotation()
    {
        if (Time.timeScale > 0f) {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }

    void SetCountText()
    {
        countText.text = "Food: " + count.ToString();
    }

    static string RemovePrefixAndSuffix(string str, string prefix, string suffix)
    {
        str = str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        return str.EndsWith(suffix) ? str.Substring(0, str.Length - suffix.Length) : str;
    }
}