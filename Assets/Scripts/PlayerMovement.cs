using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{

    public int level;
    public int healthPotions;
    public int maxHealthPotions;
    public int healthPotionValue;

    public int expValue;
    public int currentExp;
    public int maxExp;

    public float moveSpeed;

    public float jumpForce;

    public bool playerIsNearPortal = false;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public int expToBeGained;
    public ExperienceController expObject;
    public string destination = "";
    public static bool facingRight = true;

    private bool isJumping = false;
    public bool isAirborne = false;
    public bool isPressingUp = false;

    //private bool isNearItem = false;
    private bool isPressingInteract = false;
    private bool isPressingDrop = false;

    private bool isHoldingObject = false;
    public bool isWalking = false;

    public TextMesh playerLevel;
    public Text levelUI;

    public TMP_Text skillLevel1Text;
    public TMP_Text skillLevel2Text;
    public TMP_Text skillLevel3Text;
    public TMP_Text skillUltText;

    public TMP_Text HealthDisplayText;

    public int skillOneLevel = 1;
    public int skillTwoLevel = 1;
    public int skillThreeLevel = 1;
    public int ultSkillLevel = 1;

    public GameObject upgradeButtons; 

    public float moveDirection;

    public bool playerHasDied = false;
    public int DamageRecieved = 0;

    private Vector3 portalDestinationPosition;
    private string portalToTeleportTo;

    public HealthBar healthBar;
    public int currentHealth;
    public int maxHealth;

    private bool IsNearShopKeeper = false;
    public GameObject ShopKeeperCanvas;

    private bool IsNearOptionsMenu = true;
    public GameObject OptionsMenuCanvas;

    public bool CanMove = false;

    public bool startup = true;

    public ExperienceBar experienceBar;
    public int coins;
    public TMP_Text coinCount;

    public int gainedExp;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }


    void Start()
    {
        playerLevel = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<TextMesh>();
        experienceBar.setMaxExp(maxExp);
        GainExperience(0);  // Use GainExperience method here instead of directly accessing gainedExp variable

    }
    public void GainExperience(int gainedExp)
    {
        currentExp += gainedExp;

        if (currentExp >= maxExp)
        {
            LevelUp();
            currentExp -= maxExp;
        }

        experienceBar.SetExperience(currentExp);
    }

    public void UpdateHealth(int mod)
	{
        currentHealth += mod;


        if (currentHealth > maxHealth)
		{
        currentHealth = maxHealth;
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            if (healthBar != null)
            {
                healthBar.SetMaxHealth(maxHealth);
            }

        }
        else if (currentHealth <= 0)
		{
            playerHasDied = true;
            playerDeath();

        }
	}



    // Update is called once per frame`
    void Update()
    {

        if (healthPotions > maxHealthPotions)
        {
            healthPotions = maxHealthPotions;

        }


        //Get player inputs
        getPlayerInput();

        playerInteractInput();
        //Animate
        animate();

        //EnterPortal();
        LevelUp();
    }

    private void FixedUpdate()
    {

        OnTriggerEnter2D(bc);
        //Move Player
        moveCharacter();
    }

    private void playerDeath()
    {

       //
       //Debug.Log("Player Died");
     //   scenemanager.loadscene("hub world");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 0.85f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(-2, -1, 0);

    }

    private void flipCharacter()
    {
        
        facingRight = !facingRight; //inverse bool
        transform.Rotate(0f, 180f, 0f);
    }

    public void getPlayerInput()
    {
        moveDirection = Input.GetAxis("Horizontal");
        //Debug.Log(moveDirection);

        if (!isAirborne)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                isAirborne = true;
                //Debug.Log("Airborne State Changed to true");

            }
        }
    }


    public void playerInteractInput()
    {
        // Check for if the up key is being pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("The up arrow is being pressed ");
            EnterPortal();
            OpenShopKeeperUI();
            //isPressingUp = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("Up Arrow key was released.");
            //isPressingUp = false;
        }

        // Check If interact Key is being pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("The Interact Key is being pressed ");
            if (healthPotions > 0)
            {
                UpdateHealth(+healthPotionValue);
                healthPotions--;
            }
            isPressingInteract = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("The Interact Key was released ");
            isPressingInteract = false;
        }

        // Check If drop Key is being pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("The Drop Key is being pressed ");
            isPressingDrop = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("The Drop Key was released ");
            isPressingDrop = false;
        }

        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("The Drop Key is being pressed ");
            OpenOptionMenuUI();
            isPressingDrop = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("The Drop Key was released ");
            isPressingDrop = false;
        }
    }


  public void LevelUp()
    {
        if (level < 60)
        {
            level++;
            StartCoroutine(LevelUpDelay());
            Debug.Log("Level Up! Player Level is now: " + level);
            maxExp = level * 23;
            currentHealth = maxHealth + 100;
            upgradeButtons.SetActive(true);
        }
    }
	

    IEnumerator LevelUpDelay()
    {
        playerLevel.text = "LEVEL UP !";
        yield return new WaitForSeconds(2);
        playerLevel.text = "";

    }



    public void animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            flipCharacter();
            playerLevel.transform.Rotate(0f, 180f, 0f);
        }
        else if (moveDirection < 0 && facingRight)
        {
            flipCharacter();
            playerLevel.transform.Rotate(0f, 180f, 0f);
        }
    }

     
    public void moveCharacter()
    {
        if (!isAirborne)
        {
            rb.velocity = new Vector3(moveDirection * moveSpeed, rb.velocity.y);
        }
        jumpCharacter();
    }
    


    public void jumpCharacter()
    {


        if (isJumping)
        {
            rb.velocity = new Vector3(moveDirection * moveSpeed, jumpForce);

            isJumping = false;

        }
    }


    public void EnterPortal()
	{
        if(playerIsNearPortal)
		{
			if (!facingRight)
			{
                flipCharacter();
                playerLevel.transform.Rotate(0f, 180f, 0f);
            }
            Input.ResetInputAxes();
            SceneManager.LoadScene(destination);
            Debug.Log("Should Enter Portal");
        }
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerHasDied)
        {
            PositionPlayer("HW-B");
            currentHealth = maxHealth / 4;
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            if (healthBar != null)
            {
                healthBar.SetMaxHealth(maxHealth);
            }

            playerHasDied = false;
        }
        else
        {
            PositionPlayer(portalToTeleportTo);
        }

    }

    private void PositionPlayer(string portalName)
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = GameObject.FindGameObjectWithTag(portalName).GetComponent<Transform>().position;
    }

    public void OpenShopKeeperUI()
    {
        if (!ShopKeeperCanvas.active && IsNearShopKeeper)
        {
            ShopKeeperCanvas.active = true;
        }
        else
        {
            ShopKeeperCanvas.active = false;
        }
    }


    public void OpenOptionMenuUI()
    {
        if (!OptionsMenuCanvas.active && IsNearOptionsMenu)
        {
            OptionsMenuCanvas.active = true;
        }
        else
        {
            OptionsMenuCanvas.active = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deathBox" ){
            playerHasDied = true;
                   playerDeath();
        }


        if (collision.gameObject.tag == "World" || collision.gameObject.tag == "Platform")
        {
            //Debug.Log("Touching the Ground");
            isAirborne = false;
        }



        //This activates shop keeper menu
        else if (collision.gameObject.tag == "ShopKeeperUI")
        {
            IsNearShopKeeper = true;

        }


        else if (collision.gameObject.tag == "OptionsMenuUI")
        {
            IsNearOptionsMenu = true;

        }

        //WORLD 2 TELEPORTER SCRIPTS






        if (collision.gameObject.tag == "Experience")
        {
            Debug.Log("Exp Gained: " + ExperienceController.experience);
            currentExp += ExperienceController.experience;
            Debug.Log("Current Exp: " + currentExp);
            experienceBar.slider.value = currentExp;
            Debug.Log("Max Exp: " + maxExp);
            Debug.Log("Slider value: " + experienceBar.slider.value );
            Debug.Log("Slider value: " + experienceBar.slider.maxValue );
        }
         if (collision.gameObject.tag == "Coin")
        {
            coins += coinController.coin;
           
        }
    }






    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "ShopKeeperUI")
        {
            IsNearShopKeeper = false;

        }
        else if (collision.gameObject.tag == "OptionsMenuUI")
        {
            IsNearOptionsMenu = false;

        }


    }

}
