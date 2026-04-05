using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DispenseButton : MonoBehaviour
{
    Vector3 socketPos = new Vector3(1.922f, 0.6927f, 0.973f);

    public GameObject dispenserParticles;
    public AudioSource audio_player;
    public AudioClip pour_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dispenseSolvent()
    {
        Sequence buttonClick = DOTween.Sequence();
        buttonClick.Append(this.transform.DOMove(new Vector3(1.7313f, 0.69f, 0.9679f), 0.15f));
        buttonClick.Append(this.transform.DOMove(new Vector3(1.7313f, 0.7113f, 0.9679f), 0.15f));
        buttonClick.Play();
        this.GetComponent<AudioSource>().Play();

        if (BucketManager.Instance.GetCurrentBucket().transform.position == socketPos)
        {
            //disable grab on bucket
            GameObject bucket = BucketManager.Instance.GetCurrentBucket();
            bucket.GetComponent<BucketScript>().DisableBucketInteraction();
            bucket.GetComponent<BucketScript>().has_binder_and_solvent = true;
            Sequence bucketFill = DOTween.Sequence();
            bucketFill.Append(bucket.transform.GetChild(2).gameObject.transform.DOScaleY(0f, 1.5f));
            bucketFill.Append(bucket.transform.GetChild(2).gameObject.transform.DOScaleY(1f, 3.5f));
            bucketFill.AppendCallback(() => bucket.GetComponent<BucketScript>().EnableBucketInteraction());
            bucketFill.Play();
            dispenserParticles.GetComponent<ParticleSystem>().Play();
            audio_player.clip = pour_sound;
            audio_player.Play();

        } 
    }
}
