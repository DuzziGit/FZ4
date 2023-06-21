using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RogueSkillController: PlayerMovement  {

  public float MovementSkillForce;
  public float MovementSkillForceLeft;

  public GameObject projectile;
  public GameObject projectile2;
  public GameObject ProjectileUltimate;
  public Transform attackPos;
  




    public AudioSource audiosource;
    public AudioClip ThrowingStarSoundEffect;
    public AudioClip BigShurikenSoundEffect;
    public AudioClip RogueUltimateSoundEffect;
    public AudioClip FlashJumpSoundEffect;



    [HideInInspector]

    public Transform RogueUltPos;
    [HideInInspector]
    public Transform RogueUltPos1;
    [HideInInspector]
    public Transform RogueUltPos2;
    [HideInInspector]
    public Transform RogueUltPos3;
    [HideInInspector]
    public Transform RogueUltPos4;
    [HideInInspector]
    public Transform RogueUltPos5;
    [HideInInspector]
    public Transform RogueUltPos6;
    [HideInInspector]
    public Transform RogueUltPos7;
    [HideInInspector]
    public Transform RogueUltPos8;
    [HideInInspector]
    public Transform RogueUltPos9;
    [HideInInspector]
    public Transform RogueUltPos10;
    [HideInInspector]
    public Transform RogueUltPos11;
    [HideInInspector]
    public Transform RogueUltPos12;
    [HideInInspector]
    public Transform RogueUltPos13;
    [HideInInspector]
    public Transform RogueUltPos14;

    public float horizontalMove = 0f;
    public float runSpeed = 40f;


    [HideInInspector]

    [SerializeField]
    private Image imageCooldownS1;

    [HideInInspector]
    [SerializeField]
    private TMP_Text textCooldownS1;
    [HideInInspector]
    [SerializeField]
    private Image imageCooldownS2;
    [HideInInspector]
    [SerializeField]
    private TMP_Text textCooldownS2;
    [HideInInspector]
    [SerializeField]
    private Image imageCooldownS3;
    [HideInInspector]
    [SerializeField]
    private TMP_Text textCooldownS3;
    [SerializeField]
    [HideInInspector]
    private Image imageCooldownSM;
    [SerializeField]
    [HideInInspector]
    private TMP_Text textCooldownSM;
    [SerializeField]
    [HideInInspector]
    private Image imageCooldownSU;
    [SerializeField]
    [HideInInspector]
    private TMP_Text textCooldownSU;
    [SerializeField]
    [HideInInspector]
    private TMP_Text healthPotionText;



    public bool isInvincible;

   public float cooldownTimeMovement = 2;
   private float nextFireTimeMovement = 0;

   public float cooldownTimeSkill1 = 2;
   private float nextFireTimeSkill1 = 0;

   public float cooldownTimeSkill2 = 2;
   private float nextFireTimeSkill2 = 0;

   public static float cooldownTimeSkill3 = 23 ;
   private float nextFireTimeSkill3 = 0;

  public float cooldownTimeSkill3Upgraded;

   public float cooldownTimeSkillUlt = 2;
   private float nextFireTimeSkillUlt = 0;
    public Animator SwipeOne;
    public Animator SwipeTwo;
    public Animator MovementSkillOne;
    public Animator MovementSkillTwo;

    public Animator animator;
  Renderer rend;
  Color c;

    private float cooldownTimerS1 = 0.0f;
    private float cooldownTimerS2 = 0.0f;
    private float cooldownTimerS3 = 0.0f;
    private float cooldownTimerSM = 0.0f;
    private float cooldownTimerSU = 0.0f;
    private float cooldownTimer = 0.0f;

    // Start is called before the first frame update
    void Start() {
        HealthBar healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        textCooldownS1.gameObject.SetActive(false);
      imageCooldownS1.fillAmount = 0.0f;
       textCooldownS2.gameObject.SetActive(false);
      imageCooldownS2.fillAmount = 0.0f;
       textCooldownS3.gameObject.SetActive(false);
      imageCooldownS3.fillAmount = 0.0f;
       textCooldownSM.gameObject.SetActive(false);
      imageCooldownSM.fillAmount = 0.0f;
       textCooldownSU.gameObject.SetActive(false);
      imageCooldownSU.fillAmount = 0.0f;
        currentExp = 0;

      rend = GetComponent<Renderer> ();
      c = rend.material.color;

  }

  // Update is called once per frame
  void Update() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 2f;

        healthPotionText.text = healthPotions.ToString();
                experienceBar.setMaxExp(maxExp);

        levelUI.text = "" + level;
        maxHealth = level * 100;
        healthPotionValue = level * 10;


        maxExp = level * 60;




        experienceBar.SetExperience(currentExp);
        cooldownTimeSkill3Upgraded = cooldownTimeSkill3 - this.GetComponent<PlayerMovement>().skillThreeLevel;
       skillLevel1Text.text = skillOneLevel.ToString();
       skillLevel2Text.text = skillTwoLevel.ToString();
       skillLevel3Text.text = skillThreeLevel.ToString();
       skillUltText.text = ultSkillLevel.ToString();
       HealthDisplayText.text = "" + currentHealth.ToString() + " / " + maxHealth.ToString();
        coinCount.text = coins.ToString();



    //Get player inputs
    getPlayerInput();
    playerInteractInput();


        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    GetMovementSkillInput();
    GetFirstSkillInput();
    GetSecondSkillInput();
    GetThirdSkillInput();
    GetUltimateSkillInput();
        LevelUp();

    //Animate
    animate();
ApplyCooldownTracker();


  }

  private void FixedUpdate() {
    OnTriggerEnter2D(bc);
    //Move Player
    moveCharacter();
  }

  // Movement Skill

  private void GetMovementSkillInput() {
    // if the player taps the movement skill and is airborn then perform the movement skill 

     if (Time.time > nextFireTimeMovement) {
    if (Input.GetKeyDown(KeyCode.LeftControl) & isAirborne == true) {
      Debug.Log("The movement skill has been used");
               
                Debug.Log("M skill true");

                MovementSkill();
      //add the cooldown to the time that the skill is pressed, only allow the user to use the skill again once the cooldown is gone.
       nextFireTimeMovement = Time.time + cooldownTimeMovement;
              textCooldownSM.gameObject.SetActive(true);
                                cooldownTimerSM = cooldownTimeMovement; 
    }
     } 

  }

    public void moveCharacter()
    {
        {

            if (!isAirborne)
            {
                rb.velocity = new Vector3(moveDirection * moveSpeed, rb.velocity.y);
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }

            jumpCharacter();

        }
    }


    public void MovementSkill()
    {
        if (facingRight)
        {
            MovementSkillOne.SetBool("MovementSkillUsed", true);
            MovementSkillTwo.SetBool("MovementSkillUsed", true);
            audiosource.PlayOneShot(FlashJumpSoundEffect, 0.7f);

            rb.velocity = new Vector3(moveDirection * moveSpeed + MovementSkillForce, jumpForce);

            StartCoroutine(ResetMovementSkillAnimation());
        }
        else if (!facingRight)
        {
            MovementSkillOne.SetBool("MovementSkillUsed", true);
            MovementSkillTwo.SetBool("MovementSkillUsed", true);
            audiosource.PlayOneShot(FlashJumpSoundEffect, 0.7f);

            rb.velocity = new Vector3(moveDirection * moveSpeed + MovementSkillForceLeft, jumpForce);

            StartCoroutine(ResetMovementSkillAnimation());
        }
    }

    private IEnumerator ResetMovementSkillAnimation()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay duration as needed

        MovementSkillOne.SetBool("MovementSkillUsed", false);
        MovementSkillTwo.SetBool("MovementSkillUsed", false);

        Debug.Log("M skill false");
    }


    public void ApplyCooldownTracker()
  {
    cooldownTimerS1 -= Time.deltaTime;
    cooldownTimerS2 -= Time.deltaTime;
    cooldownTimerS3 -= Time.deltaTime;
    cooldownTimerSM -= Time.deltaTime;
    cooldownTimerSU -= Time.deltaTime;

    if (cooldownTimerS1 < 0.0f) {
      textCooldownS1.gameObject.SetActive(false);
      imageCooldownS1.fillAmount = 0.0f;

       
    } else {
 textCooldownS1.text = Mathf.RoundToInt(cooldownTimerS1).ToString();
      imageCooldownS1.fillAmount = cooldownTimerS1 / cooldownTimeSkill1;
    }
    
    
     if (cooldownTimerS2 < 0.0f) {
      textCooldownS2.gameObject.SetActive(false);
      imageCooldownS2.fillAmount = 0.0f;
    }  else {
 textCooldownS2.text = Mathf.RoundToInt(cooldownTimerS2).ToString();
      imageCooldownS2.fillAmount = cooldownTimerS2 / cooldownTimeSkill2;
    }
    
     if (cooldownTimerS3 < 0.0f) {
      textCooldownS3.gameObject.SetActive(false);
      imageCooldownS3.fillAmount = 0.0f;
    }  else {
 textCooldownS3.text = Mathf.RoundToInt(cooldownTimerS3).ToString();
      imageCooldownS3.fillAmount = cooldownTimerS3 / cooldownTimeSkill3Upgraded;
    }

     if (cooldownTimerSU < 0.0f) {
      textCooldownSU.gameObject.SetActive(false);
      imageCooldownSU.fillAmount = 0.0f;
    }  else {
 textCooldownSU.text = Mathf.RoundToInt(cooldownTimerSU).ToString();
      imageCooldownSU.fillAmount = cooldownTimerSU / cooldownTimeSkillUlt;
    }
     if (cooldownTimerSM < 0.0f) {
      textCooldownSM.gameObject.SetActive(false);
      imageCooldownSM.fillAmount = 0.0f;
    }  else {
 textCooldownSM.text = Mathf.RoundToInt(cooldownTimerSM).ToString();
      imageCooldownSM.fillAmount = cooldownTimerSM / cooldownTimeMovement;
    }
    
  }

  // First Skill

  public void GetFirstSkillInput() {
     if (Time.time > nextFireTimeSkill1) {

    if (Input.GetKeyDown(KeyCode.A)) {
                // Debug.Log("The first skill has been used");
                // start the timer and release the skill 
                  StartCoroutine(firstSkill());
                nextFireTimeSkill1 = Time.time + cooldownTimeSkill1;
       textCooldownS1.gameObject.SetActive(true);
      cooldownTimerS1 = cooldownTimeSkill1;
                SwipeOne.SetBool("SwipeOne", true);
                SwipeTwo.SetBool("SwipeTwo", true);


            }

        }
  }

  IEnumerator firstSkill() {
    Instantiate(projectile, attackPos.position, attackPos.rotation);
     audiosource.PlayOneShot(ThrowingStarSoundEffect, 0.4f);
        // create 3 prefabs and release them on a timer so they all "throw" seperatly 
        yield
    return new WaitForSeconds(0.05F);
    Instantiate(projectile, attackPos.position, attackPos.rotation);
       audiosource.PlayOneShot(ThrowingStarSoundEffect, 0.05f);
        SwipeOne.SetBool("SwipeOne", false);
        SwipeTwo.SetBool("SwipeTwo", false);
        yield
        return new WaitForSeconds(0.05F);
    Instantiate(projectile, attackPos.position, attackPos.rotation);
       audiosource.PlayOneShot(ThrowingStarSoundEffect, 0.05f);

    }

    // Second Skill

    public void GetSecondSkillInput() {
     if (Time.time > nextFireTimeSkill2) {

    if (Input.GetKeyDown(KeyCode.S)) {
      Debug.Log("The second skill has been used");

      secondSkill();
        nextFireTimeSkill2 = Time.time + cooldownTimeSkill2;
                  textCooldownS2.gameObject.SetActive(true);
      cooldownTimerS2 = cooldownTimeSkill2;
    }

    }
  }

  public void secondSkill() {
    //create the second skill prefab
    Instantiate(projectile2, attackPos.position, attackPos.rotation);
    audiosource.PlayOneShot(BigShurikenSoundEffect, 0.7f);
  }

  // Third Skill
  public void GetThirdSkillInput() {
     if (Time.time > nextFireTimeSkill3) {

    if (Input.GetKeyDown(KeyCode.D)) {
         c.a = 0.5f;
        rend.material.color = c;
          StartCoroutine(thirdSkillEnum());
      Debug.Log("The third skill has been used");
              nextFireTimeSkill3 = Time.time + cooldownTimeSkill3Upgraded;
              textCooldownS3.gameObject.SetActive(true);
      cooldownTimerS3 = cooldownTimeSkill3Upgraded;
    }
    }
  }

  IEnumerator thirdSkillEnum() {
   

  Physics2D.IgnoreLayerCollision(7, 11, true);        
    yield return new WaitForSeconds(2.5F);
    c.a = 0.5f;
        rend.material.color = c;
    //TODO:: Bug that doesnt renable the isinvincible after 2.5 seconds 
      Physics2D.IgnoreLayerCollision(7, 11, false);        
    c.a = 1f;
    rend.material.color = c;

  }



  //Ultimate Skill
  public void GetUltimateSkillInput() {
     if (Time.time > nextFireTimeSkillUlt) {

    if (Input.GetKeyDown(KeyCode.F)) {

      Debug.Log("The Ultimate skill has been used");
      StartCoroutine(ultimateSkillEnum());
      //   ultimateSkill();
              nextFireTimeSkillUlt= Time.time + cooldownTimeSkillUlt;
  textCooldownSU.gameObject.SetActive(true);
      cooldownTimerSU = cooldownTimeSkillUlt;

    }
    }
  }

  IEnumerator ultimateSkillEnum() {
    Instantiate(ProjectileUltimate, RogueUltPos.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos1.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos2.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos3.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos4.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos5.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos6.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos7.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos8.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos9.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos10.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos11.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos12.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos13.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos14.position, RogueUltPos.rotation);
        audiosource.PlayOneShot(RogueUltimateSoundEffect, 0.7f);

    yield
    return new WaitForSeconds(0.15F);
    Instantiate(ProjectileUltimate, RogueUltPos.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos1.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos2.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos3.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos4.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos5.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos6.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos7.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos8.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos9.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos10.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos11.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos12.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos13.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos14.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos2.position, RogueUltPos.rotation);
            audiosource.PlayOneShot(RogueUltimateSoundEffect, 0.7f);

    yield
    return new WaitForSeconds(0.15F);
    Instantiate(ProjectileUltimate, RogueUltPos.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos1.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos2.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos3.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos4.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos5.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos6.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos7.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos8.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos9.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos10.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos11.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos12.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos13.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos14.position, RogueUltPos.rotation);
    Instantiate(ProjectileUltimate, RogueUltPos2.position, RogueUltPos.rotation);
            audiosource.PlayOneShot(RogueUltimateSoundEffect, 0.7f);


  }
}