using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;



public class BasketScript : MonoBehaviour
{

    public float redFlowers = 1;
    public float yellowFlowers = 1;
    public float blueFlowers = 1;
    public float whiteFlowers = 1;
    public float blackFlowers = 1;

    public TextMeshProUGUI redText;
    public TextMeshProUGUI yellowText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI whiteText;
    public TextMeshProUGUI blackText;

    public GameObject ImagePlane;

    public GameObject redFlowerPrefab;

    public AudioSource sound_source;
    public AudioClip grab_sound;
    public AudioClip flower_place_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        redText.text = redFlowers.ToString();
        yellowText.text = yellowFlowers.ToString();
        blueText.text = blueFlowers.ToString();
        whiteText.text = whiteFlowers.ToString();
        blackText.text = blackFlowers.ToString();

        // WIP

        /*if(ImagePlane.activeSelf == true)
        {
            Transform redFlower = ImagePlane.transform.Find("flower_red_prefab");

            if (ImagePlane.transform.Find("flower_red_prefab") == null)
            {
                if(redFlowers != 0)
                {
                    redFlowers--;
                    Instantiate(redFlowerPrefab, new Vector3(0.35f, -0.389f, 0f), Quaternion.Euler(0, 0, 0), ImagePlane.transform);
                }
            }
        }*/

    }

    public void toggleImagePlane()
    {
        if(ImagePlane.activeSelf == true)
        {
            ImagePlane.SetActive(false);
        }
        else
        {
            ImagePlane.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("FlowerRed"))
        {
            redFlowers++;

            Destroy(other.transform.root.gameObject);

            PlayFlowerPlaceSound();
        }
        else if (other.CompareTag("FlowerBlue"))
        {
            blueFlowers++;

            Destroy(other.transform.root.gameObject);

            PlayFlowerPlaceSound();
        }
        else if (other.CompareTag("FlowerYellow"))
        {
            yellowFlowers++;

            Destroy(other.transform.root.gameObject);

            PlayFlowerPlaceSound();
        }
        else if (other.CompareTag("FlowerWhite"))
        {
            whiteFlowers++;

            Destroy(other.transform.root.gameObject);

            PlayFlowerPlaceSound();
        }
        else if (other.CompareTag("FlowerBlack"))
        {
            blackFlowers++;

            Destroy(other.transform.root.gameObject);

            PlayFlowerPlaceSound();
        }

    }

    private void PlayFlowerPlaceSound()
    {
        sound_source.clip = flower_place_sound;
        sound_source.Play();
    }

    public void OnBasketGrab(SelectEnterEventArgs args)
    {
        sound_source.clip = grab_sound;
        sound_source.Play();
    }
}
