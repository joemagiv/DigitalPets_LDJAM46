using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalPet : MonoBehaviour
{
    //Deprecated UI Text Elements
    /*    public Text HealthText;
        public Text HungerText;
        public Text PoopText;
        public Text HappinessText;
        public Text ScreenText;
        public Text Notifications;
        public Text LevelText;*/

    public TextMesh AgeText;
    public GameObject MainCharacterGO;

    public GameObject StatusIndicators;


    //Transforms for Status bars

    public Transform HealthBar;
    public Transform FoodBar;
    public Transform HappinessBar;

    //SpriteRenderers

    public SpriteRenderer ScreenSelectorSpriteRenderer;
    public SpriteRenderer PoopsSpriteRenderer;

    //Sprites Needed for Screens

    public Sprite FoodScreenSprite;
    public Sprite CleanScreenSprite;
    public Sprite PlayScreenSprite;

    public Sprite Poop1Sprite;
    public Sprite Poop2Sprite;
    public Sprite Poop3Sprite;
    public Sprite Poop4Sprite;


    //Animators

    public Animator MainCharacterAnimator;
    public Animator WashingScreenAnimator;

    

    public float Health;
    public float Hunger;
    public int Poop;
    public float Happiness;

    public float HungerRateDecrease;

    public bool timerActive;

    public float timer;
    public float timeToHeartbeat;

    public int totalHeartbeats;

    public int screen;




    // Start is called before the first frame update
    void Start()
    {
        UpdateBars();
        timerActive = true;
    }

    public void UpdateBars()
    {
        HealthBar.localScale = new Vector3(Health / 100f, 1f, 1f);
        FoodBar.localScale = new Vector3(Hunger / 100f, 1f, 1f);
        HappinessBar.localScale = new Vector3(Happiness / 100f, 1f, 1f);
    }

    public void Button1ChangeScreen()
    {
        if (timerActive)
        {
            //Screens are 0 = base screen, 1 = food, 2 = poop cleaning, 3 = play. Button 3 always resets to Screen 0.
            if (screen == 3)
            {
                screen = 0;
                ScreenSelectorSpriteRenderer.sprite = null;
                MainCharacterGO.SetActive(true);
                UpdatePoopSprite();
            }
            else
            {
                screen++;
                MainCharacterGO.SetActive(false);
                PoopsSpriteRenderer.sprite = null;

                if (screen == 1)
                {
                    ScreenSelectorSpriteRenderer.sprite = FoodScreenSprite;
                }
                if (screen == 2)
                {
                    ScreenSelectorSpriteRenderer.sprite = CleanScreenSprite;
                }
                if (screen == 3)
                {
                    ScreenSelectorSpriteRenderer.sprite = PlayScreenSprite;
                }
            }
        }
    }



    public void Button2Confirmation()
    {
        if (timerActive)
        {
            MainCharacterGO.SetActive(true);

            if (screen == 1)
            {
                Feed();
            }
            if (screen == 2)
            {
                Clean();
            }
            if (screen == 3)
            {
                Play();
            }
        }
       

        screen = 0;
        ScreenSelectorSpriteRenderer.sprite = null;

        UpdatePoopSprite();
        UpdateBars();
    }

    private void Feed()
    {
        MainCharacterAnimator.SetTrigger("Feed");
        if (Hunger <= 100 && Hunger > 90)
        {
            Hunger = 100;
        } else
        {
            Hunger += 10;
        }
    }

    private void Clean()
    {
        WashingScreenAnimator.SetTrigger("Wash");
        if (Poop >= 1)
        {
            Poop -= 1;
            UpdatePoopSprite();
        }
    }

    private void Play()
    {
        MainCharacterAnimator.SetTrigger("Play");
        if (Happiness <= 100 && Happiness > 90)
        {
            Happiness = 100;
        } else
        {
            Happiness += 10;
        }

        
    }

    public void Button3Exit()
    {
        //reset to base screen
        if (timerActive)
        {
            screen = 0;
            ScreenSelectorSpriteRenderer.sprite = null;
            MainCharacterGO.SetActive(true);
            UpdatePoopSprite();
        }
        
    }

    private void CheckHunger()
    {

        if (Hunger <= 80f && Hunger > 60f)
        {
            Health -= 5f;
            Happiness -= 2f;
        }

        if (Hunger <= 60f && Hunger > 40f)
        {
            Health -= 10f;
            Happiness -= 4f;
        }

        if (Hunger <= 40f && Hunger > 20f)
        {
            Health -= 15f;
            Happiness -= 5f;
        }

        if (Hunger <= 20f)
        {
            Health -= 25f;
            Happiness -= 10f;
        }

    }

    private void UpdatePoopSprite()
    {
        if(screen == 0)
        {
            //only do this if you're on screen 0
            if (Poop == 0)
            {
                PoopsSpriteRenderer.sprite = null;
            }
            if (Poop == 1)
            {
                PoopsSpriteRenderer.sprite = Poop1Sprite;
            }
            if (Poop == 2)
            {
                PoopsSpriteRenderer.sprite = Poop2Sprite;
            }
            if (Poop == 3)
            {
                PoopsSpriteRenderer.sprite = Poop3Sprite;
            }
            if (Poop == 4)
            {
                PoopsSpriteRenderer.sprite = Poop4Sprite;
            }
        }
        

    }

    private void CheckPoop()
    {
        if (Poop == 1)
        {
            Happiness -= 5f;
        }
        if (Poop == 2)
        {
            Health -= 1f;
            Happiness -= 10f;
        }
        if (Poop == 3)
        {
            Health -= 2f;
            Happiness -= 12f;
        }
        if (Poop == 4)
        {
            Health -= 4f;
            Happiness -= 17f;
        }

    }

    private void RollForPoop()
    {
        if (Poop < 4)
        {
            int roll = Random.Range(0, 4);
            if (roll <= 2)
            {
                Poop++;
                UpdatePoopSprite();
            }
        }
    }

    private void CheckHappiness()
    {
        if (Happiness < 4)
        {
            Happiness = 0;
        }
        else
        {
            Happiness -= 4f;

        }
        if (Happiness <= 80f && Happiness > 60f)
        {
            Health -= 1f;
        }

        if (Happiness <= 60f && Happiness > 40f)
        {
            Health -= 3f;
        }

        if (Happiness <= 40f && Happiness > 20f)
        {
            Health -= 5f;
        }

        if (Happiness <= 20f)
        {
            Health -= 10f;
        }
    }

    private void CheckDeath()
    {
        if (Health <= 0)
        {
            Health = 0;
            timerActive = false;
            screen = 0;
            ScreenSelectorSpriteRenderer.sprite = null;
            MainCharacterGO.SetActive(true);
            MainCharacterAnimator.SetTrigger("Dead");
            StatusIndicators.SetActive(false);
            PoopsSpriteRenderer.sprite = null;
            AgeText.text = totalHeartbeats.ToString() + "\nDays";
        }
    }

    private void Heartbeat()
    {
        //every couple seconds (increasing with difficulty) check all the values

        //update hunger always

        Hunger -= HungerRateDecrease;

        //check hunger, affect health
        CheckHunger();

        //check poop
        CheckPoop();

        //roll for poop
        RollForPoop();

        //check for happiness
        CheckHappiness();

        //check for death
        CheckDeath();

        //Increment total Heartbeats
        if (timerActive)
        {
            IncrementHeartbeats();
        }

        UpdateBars();

    }

    private void IncrementHeartbeats()
    {
        totalHeartbeats++;

        if (totalHeartbeats > 10 && totalHeartbeats < 15)
        {
            timeToHeartbeat = 7f;
        }
        if (totalHeartbeats >= 15 && totalHeartbeats < 30)
        {
            timeToHeartbeat = 5f;
        }
        if (totalHeartbeats >= 30)
        {
            timeToHeartbeat = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;

            if (timer > timeToHeartbeat)
            {
                timer = 0f;
                Heartbeat();
            }
        } else
        {
            StatusIndicators.SetActive(false);
            PoopsSpriteRenderer.sprite = null;
            AgeText.text = totalHeartbeats.ToString() + "\nDays";
        }
        
    }
}
