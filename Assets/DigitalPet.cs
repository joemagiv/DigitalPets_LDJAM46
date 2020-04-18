using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalPet : MonoBehaviour
{
    //UI Text Elements
    public Text HealthText;
    public Text HungerText;
    public Text PoopText;
    public Text HappinessText;
    public Text ScreenText;

    public Text LevelText;

    public Text Notifications;

    public float Health;
    public float Hunger;
    public int Poop;
    public float Happiness;

    public float HungerRateDecrease;

    private bool timerActive;

    public float timer;
    public float timeToHeartbeat;

    public int totalHeartbeats;

    public int screen;




    // Start is called before the first frame update
    void Start()
    {
        UpdateStrings();
        timerActive = true;
    }

    public void UpdateStrings()
    {
        HealthText.text = "Health: " + Health.ToString();
        HungerText.text = "Hunger: " + Hunger.ToString();
        PoopText.text = "Poop: " + Poop.ToString();
        HappinessText.text = "Happiness: " + Happiness.ToString();
        ScreenText.text = "Screen: " + screen.ToString();
        LevelText.text = "Level: " + totalHeartbeats.ToString();
    }

    public void Button1ChangeScreen()
    {
        //Screens are 0 = base screen, 1 = food, 2 = poop cleaning, 3 = play. Button 3 always resets to Screen 0.
        if (screen == 3)
        {
            screen = 0;
        }
        else
        {
            screen++;
        }

        ScreenText.text = "Screen: " + screen.ToString();
    }



    public void Button2Confirmation()
    {
        if (timerActive)
        {
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

        UpdateStrings();
    }

    private void Feed()
    {
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
        if (Poop >= 1)
        {
            Poop -= 1;
        }
    }

    private void Play()
    {
        if (Happiness < 100 && Happiness > 90)
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

        screen = 0;
        ScreenText.text = "Screen: " + screen.ToString();
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
            }
        }
    }

    private void CheckHappiness()
    {
        Happiness -= 4f;
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
            Notifications.text = "Dead";
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

        IncrementHeartbeats();

        UpdateStrings();

    }

    private void IncrementHeartbeats()
    {
        totalHeartbeats++;

        if (totalHeartbeats > 10 && totalHeartbeats < 15)
        {
            timeToHeartbeat = 4f;
        }
        if (totalHeartbeats >= 15 && totalHeartbeats < 30)
        {
            timeToHeartbeat = 3f;
        }
        if (totalHeartbeats >= 30)
        {
            timeToHeartbeat = 2.5f;
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
        }
        
    }
}
