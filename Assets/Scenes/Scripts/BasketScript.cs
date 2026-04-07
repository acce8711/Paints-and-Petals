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

    public GameObject redFlowerPreview;
    public GameObject redFlowerPrefab;

    public GameObject blueFlowerPreview;
    public GameObject blueFlowerPrefab;

    public GameObject yellowFlowerPreview;
    public GameObject yellowFlowerPrefab;

    public GameObject whiteFlowerPreview;
    public GameObject whiteFlowerPrefab;

    public GameObject blackFlowerPreview;
    public GameObject blackFlowerPrefab;

    public AudioSource sound_source;
    public AudioClip grab_sound;
    public AudioClip flower_place_sound;

    private GameObject newFlower;

    [SerializeField] private XRInteractionManager interaction_manager;

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

    }

    public void grabRedFlower(SelectEnterEventArgs args)
    {
        newFlower = Instantiate(redFlowerPrefab);
        interaction_manager.SelectEnter((IXRSelectInteractor)args.interactorObject, newFlower.GetComponent<XRGrabInteractable>());
        redFlowers--;
        if(redFlowers == 0)
        {
            redFlowerPreview.SetActive(false);
        }
    }
    public void grabBlueFlower(SelectEnterEventArgs args)
    {
        newFlower = Instantiate(blueFlowerPrefab);
        interaction_manager.SelectEnter((IXRSelectInteractor)args.interactorObject, newFlower.GetComponent<XRGrabInteractable>());
        blueFlowers--;
        if (blueFlowers == 0)
        {
            blueFlowerPreview.SetActive(false);
        }

    }
    public void grabYellowFlower(SelectEnterEventArgs args)
    {
        newFlower = Instantiate(yellowFlowerPrefab);
        interaction_manager.SelectEnter((IXRSelectInteractor)args.interactorObject, newFlower.GetComponent<XRGrabInteractable>());
        yellowFlowers--;
        if (yellowFlowers == 0)
        {
            yellowFlowerPreview.SetActive(false);
        }
    }
    public void grabWhiteFlower(SelectEnterEventArgs args)
    {
        newFlower = Instantiate(whiteFlowerPrefab);
        interaction_manager.SelectEnter((IXRSelectInteractor)args.interactorObject, newFlower.GetComponent<XRGrabInteractable>());
        whiteFlowers--;
        if (whiteFlowers == 0)
        {
            whiteFlowerPreview.SetActive(false);
        }

    }
    public void grabBlackFlower(SelectEnterEventArgs args)
    {
        newFlower = Instantiate(blackFlowerPrefab);
        interaction_manager.SelectEnter((IXRSelectInteractor)args.interactorObject, newFlower.GetComponent<XRGrabInteractable>());
        blackFlowers--;
        if (blackFlowers == 0)
        {
            blackFlowerPreview.SetActive(false);
        }

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

            if (redFlowers > 0)
            {
                redFlowerPreview.SetActive(true);
            }

            if (blueFlowers > 0)
            {
                blueFlowerPreview.SetActive(true);
            }


            if (yellowFlowers > 0)
            {
                yellowFlowerPreview.SetActive(true);
            }


            if (whiteFlowers > 0)
            {
                whiteFlowerPreview.SetActive(true);
            }


            if (blackFlowers > 0)
            {
                blackFlowerPreview.SetActive(true);
            }

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
