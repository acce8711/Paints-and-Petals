using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketScript : MonoBehaviour
{

    public float redFlowers = 0;
    public float yellowFlowers = 0;
    public float blueFlowers = 0;
    public float whiteFlowers = 0;
    public float blackFlowers = 0;

    public TextMeshProUGUI redText;
    public TextMeshProUGUI yellowText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI whiteText;
    public TextMeshProUGUI blackText;

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

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("FlowerRed"))
        {
            redFlowers++;

            Destroy(other.transform.root.gameObject);
        }
        else if (other.CompareTag("FlowerBlue"))
        {
            blueFlowers++;

            Destroy(other.transform.root.gameObject);
        }
        else if (other.CompareTag("FlowerYellow"))
        {
            yellowFlowers++;

            Destroy(other.transform.root.gameObject);
        }
        else if (other.CompareTag("FlowerWhite"))
        {
            whiteFlowers++;

            Destroy(other.transform.root.gameObject);
        }
        else if (other.CompareTag("FlowerBlack"))
        {
            blackFlowers++;

            Destroy(other.transform.root.gameObject);
        }

    }
}
