using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MageSkillController : PlayerMovement
{

    public GameObject projectile;
    public GameObject ultProjectile;
    public GameObject constantProjectile;
    public GameObject iceProjectile;
    public GameObject fallingProjectile;

    public float horizontalMove = 0f;
    public float runSpeed = 40f;

    [SerializeField]
    private Image imageCooldownS1;
    [SerializeField]
    private TMP_Text textCooldownS1;
     [SerializeField]
    private Image imageCooldownS2;
    [SerializeField]
    private TMP_Text textCooldownS2;
    [SerializeField]
    private Image imageCooldownS3;
    [SerializeField]
    private TMP_Text textCooldownS3;
    [SerializeField]
    private Image imageCooldownSM;
    [SerializeField]
    private TMP_Text textCooldownSM;
    [SerializeField]
    private Image imageCooldownSU;
    [SerializeField]
    private TMP_Text textCooldownSU;
    [SerializeField]
    private TMP_Text healthPotionText;





    public float cooldownTimeMovement = 2;
   private float nextFireTimeMovement = 0;

   public float cooldownTimeSkill1 = 2;
   private float nextFireTimeSkill1 = 0;

   public float cooldownTimeSkill2 = 2;
   private float nextFireTimeSkill2 = 0;

   public float cooldownTimeSkill3 = 2;
   private float nextFireTimeSkill3 = 0;

   public float cooldownTimeSkillUlt = 2;
   private float nextFireTimeSkillUlt = 0;


    public Transform attackPos;
    public Transform ultimatePos;
    public Transform constantPos;
    public Transform teleportPos;

    public Transform attack2PositionA;
    public Transform attack2PositionB;
    public Transform attack2PositionC;
    public Transform attack2PositionD;
    public Transform attack2PositionE;
    public Transform attack2PositionF;

    public Animator animator;

    private float cooldownTimerS1 = 0.0f;
    private float cooldownTimerS2 = 0.0f;
    private float cooldownTimerS3 = 0.0f;
    private float cooldownTimerSM = 0.0f;
    private float cooldownTimerSU = 0.0f;


    public AudioSource audiosource;
    public AudioClip magicMisslesSoundEffect;
    public AudioClip flashSoundEffect;
    public AudioClip iceSoundEffect;
    public AudioClip ultSoundEffect;
    public AudioClip teleportJumpSoundEffect;






    public bool cooldown;

        void Start() {
        
        maxHealth = level * 100;
        healthPotionValue = level * 10;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

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

    }

    void Update()
    {

        healthPotionText.text = healthPotions.ToString();
        experienceBar.setMaxExp(maxExp);
        levelUI.text = "" + level;
        maxHealth = level * 100;
        healthPotionValue = level * 10;


        maxExp = level * 23;


        //set the health bar to the current health of the player
           healthBar.setMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        experienceBar.SetExperience(currentExp);



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
        moveCharacter();


        GetFirstSkillInput();
        GetSecondSkillInput();
        Get3rdSkill();
        GetMovementSkillInput();
        GetUltSkillInput();
        LevelUp();

        //Animate
        animate();

ApplyCooldownTracker();

    }


    private void FixedUpdate()
    {
        OnTriggerEnter2D(bc);
        //Move Player
        moveCharacter();
    }


    public void moveCharacter()
    {

        if (!isAirborne)
        {
            rb.velocity = new Vector3(moveDirection * moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        jumpCharacter();

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
      imageCooldownS3.fillAmount = cooldownTimerS3 / cooldownTimeSkill3;
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
  


    public void GetFirstSkillInput()
    {
     if (Time.time > nextFireTimeSkill1) {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("The first skill has been used");
           
           StartCoroutine(cooldownController());
                               nextFireTimeSkill1 = Time.time + cooldownTimeSkill1;
          
          textCooldownS1.gameObject.SetActive(true);
      cooldownTimerS1 = cooldownTimeSkill1;


    }
        }
    }


    public void GetSecondSkillInput()
    {
     if (Time.time > nextFireTimeSkill2) {

        if (Input.GetKey(KeyCode.S))
        {

                Instantiate(constantProjectile, constantPos.position, constantPos.rotation);
                audiosource.PlayOneShot(flashSoundEffect, 1);
                nextFireTimeSkill2 = Time.time + cooldownTimeSkill2;
   
                    textCooldownS2.gameObject.SetActive(true);
                    cooldownTimerS2 = cooldownTimeSkill2;
        }
        }
    }


    private void GetMovementSkillInput()
    {
             if (Time.time > nextFireTimeMovement) {

        // if the player taps the movement skill and is airborn then perform the movement skill 
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            Debug.Log("The movement skill has been used");

                if (!cooldown)
                {
                animator.SetBool("TeleportBegin", true);
                audiosource.PlayOneShot(teleportJumpSoundEffect, 0.7f);

                StartCoroutine(teleportDelay());
                StartCoroutine(teleportCooldown());

                       nextFireTimeMovement = Time.time + cooldownTimeMovement;
                               textCooldownSM.gameObject.SetActive(true);
                                cooldownTimerSM = cooldownTimeMovement; 
                }
            }
        }

    }


    public void Get3rdSkill()
    {
     if (Time.time > nextFireTimeSkill3) {

        if (Input.GetKeyDown(KeyCode.D))
        {
            

            
                Debug.Log("The 3rd skill has been used");
                StartCoroutine(adCooldownController());
                StartCoroutine(beCooldownController()); 
                StartCoroutine(cfCoolDownController());
              nextFireTimeSkill3 = Time.time + cooldownTimeSkill3;
    textCooldownS3.gameObject.SetActive(true);
      cooldownTimerS3 = cooldownTimeSkill3;
            
        }
     }
    }


    public void GetUltSkillInput()
    {
     if (Time.time > nextFireTimeSkillUlt) {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("The ultimate skill has been used");
            
                Instantiate(ultProjectile, ultimatePos.position, ultimatePos.rotation);
                audiosource.PlayOneShot(ultSoundEffect, 1);
                nextFireTimeSkillUlt = Time.time + cooldownTimeSkillUlt;
                           textCooldownSU.gameObject.SetActive(true);
      cooldownTimerSU = cooldownTimeSkillUlt;
            
        }
        }
    }


   



    IEnumerator cooldownController()
    {
        Instantiate(projectile, attackPos.position, attackPos.rotation);
        audiosource.PlayOneShot(magicMisslesSoundEffect, 0.7f);
        yield return new WaitForSeconds(0.5f);
        cooldown = false;

    }


    IEnumerator adCooldownController()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(iceProjectile, attack2PositionB.position, attack2PositionB.rotation);
        audiosource.PlayOneShot(iceSoundEffect, 0.3f);


    }

    IEnumerator beCooldownController()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(iceProjectile, attack2PositionA.position, attack2PositionA.rotation);
        audiosource.PlayOneShot(iceSoundEffect, 0.3f);
    }

    IEnumerator cfCoolDownController()
    {
        yield return new WaitForSeconds(1.3f);
        Instantiate(iceProjectile, attack2PositionC.position, attack2PositionC.rotation);
        audiosource.PlayOneShot(iceSoundEffect, 0.3f);



    }


    IEnumerator cooldownControllerConstant()
    {

        cooldown = true;
        Instantiate(constantProjectile, constantPos.position, constantPos.rotation);
        yield return new WaitForSeconds(0.5f);
        cooldown = false;        

    }

    IEnumerator teleportCooldown() 
    {
        yield return new WaitForSeconds(2);

        animator.SetBool("TeleportEnd", false);
        cooldown = false;
    }

    IEnumerator teleportDelay()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.5f);

        
        transform.position = teleportPos.position;
        animator.SetBool("TeleportBegin", false);
        animator.SetBool("TeleportEnd", true);

        
        isAirborne = true;
    }
}
