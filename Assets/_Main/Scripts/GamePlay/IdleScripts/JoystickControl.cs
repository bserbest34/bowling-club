using System.Collections;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    FloatingJoystick floatingJoystick;
    CharacterController characterController;
    public float movSpeed, rotSpeed;
    internal bool isRelase;
    float posY = 0.18f;
    public GameObject hoverBoard;
    internal bool isOnHoverBoard = false;
    BallCollector ballCollector;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ballCollector = FindObjectOfType<BallCollector>();
        UIManager.OnStart += GameStarted;
    }

    private void Start()
    {
        movSpeed = PlayerPrefs.GetFloat("Speed", 13);
        floatingJoystick = GameObject.FindGameObjectWithTag("FloatingJoystick").GetComponent<FloatingJoystick>();
        characterController = GetComponent<CharacterController>();
    }
   
    public void Update()
    {
        SetPosition();
        SetRotation();
        SetAnimation();
        ConfigureIsRelase();
    }

    void SetRotation()
    {
        if (floatingJoystick.Vertical == 0 && floatingJoystick.Horizontal == 0)
            return;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    void SetPosition()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        direction.y = 0;
        characterController.Move(direction * movSpeed * Time.deltaTime); 
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);

    }

    void SetAnimation()
    {
        if(isOnHoverBoard)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("onHoverboard", true);
            return;
        }
        if(floatingJoystick.Vertical == 0 && floatingJoystick.Horizontal == 0) {

            anim.SetBool("onHoverboard", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
        } 
        else
        {
            anim.SetBool("onHoverboard", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
        }
    }

    void GameStarted()
    {
        enabled = true;
    }
    void ConfigureIsRelase()
    {
        isRelase = floatingJoystick.Vertical == 0 && floatingJoystick.Horizontal == 0 ? true : false;

        if (ballCollector.lastOnotherTime && !isRelase)
        {
            ballCollector.lastOnotherTime = false;
        }
    }

    internal void SpeedUpgrade()
    {
        PlayerPrefs.SetFloat("Speed", movSpeed + 0.5f);
        movSpeed = PlayerPrefs.GetFloat("Speed");
    }

    internal void Clicked(GameObject hoverBoardAdsObject)
    {
        StartCoroutine(SetFalseHoverBoard(hoverBoardAdsObject));
    }

    IEnumerator SetFalseHoverBoard(GameObject hoverBoardAdsObject)
    {
        hoverBoardAdsObject.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
        hoverBoardAdsObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        isOnHoverBoard = true;
        hoverBoardAdsObject.SetActive(false);
        GetComponent<CharacterController>().height = 140;
        movSpeed += 10;
        rotSpeed += 200;
        posY = 1f;
        hoverBoard.SetActive(true);
        isOnHoverBoard = true;
        yield return new WaitForSeconds(20f);
        hoverBoard.SetActive(false);
        movSpeed -= 10;
        rotSpeed -= 200;
        posY = 0.18f;
        GetComponent<CharacterController>().height = 102;
        isOnHoverBoard = false;
        yield return new WaitForSeconds(30f);
        hoverBoardAdsObject.GetComponent<BoxCollider>().enabled = true;
        hoverBoardAdsObject.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
        hoverBoardAdsObject.SetActive(true);
    }
}