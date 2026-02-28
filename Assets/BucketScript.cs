using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BucketScript : MonoBehaviour
{

    public GameObject bucketPrefab;
    public Vector3 bucketTransform = new Vector3(2.148f, 1.0853f, 1.8135f);
    public bool bucketSummoned = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void summonBucket()
    {
        GameObject clone;

        if (bucketSummoned == false)
        {
            clone = Instantiate(bucketPrefab, bucketTransform, Quaternion.identity);
            //clone.transform.SetParent(interactParent.transform, true);
            //Instantiate(bucketPrefab, bucketTransform, Quaternion.identity);
            bucketSummoned = true;
        }
    }
}
