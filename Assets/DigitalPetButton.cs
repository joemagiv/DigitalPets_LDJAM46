using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalPetButton : MonoBehaviour
{
    private DigitalPet digitalPet;
    private Animator buttonAnimator;

    public int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        digitalPet = GetComponentInParent<DigitalPet>();
        buttonAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }
    }

    void ButtonPressed()
    {
        buttonAnimator.SetTrigger("Clicked");
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
            Debug.Log(hit.collider.name);

            if (hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("This was taht");

                ButtonPressed();
            }
        }
    }
}
