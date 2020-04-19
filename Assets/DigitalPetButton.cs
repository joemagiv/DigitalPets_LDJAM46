using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalPetButton : MonoBehaviour
{
    private DigitalPet digitalPet;
    private Animator buttonAnimator;

    private AudioSource audioSource;

    public AudioClip audioClip;

    public int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        digitalPet = GetComponentInParent<DigitalPet>();
        buttonAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void ButtonPressed()
    {
        buttonAnimator.SetTrigger("Clicked");
        audioSource.clip = audioClip;
        audioSource.Play();

        if (buttonNumber == 1)
        {
            digitalPet.Button1ChangeScreen();
        }

        if (buttonNumber == 2)
        {
            digitalPet.Button2Confirmation();
        }

        if (buttonNumber == 3)
        {
            digitalPet.Button3Exit();
        }

    }

    void CastRay()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null)
        {


            if (hit.collider.gameObject == this.gameObject)
            {

                ButtonPressed();
            }
        }
    }
}
