using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceCheck : MonoBehaviour
{
    [SerializeField] Transform player_position;
    [SerializeField] float distance_threshold;
    [SerializeField] GameObject flower_field;
    [SerializeField] Vector3 flower_field_position;
    [SerializeField] Constants.FIELD field_type;

    public GameObject prefabRef;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        //check if 0.5 seconds have passed
        if (timer > 0.5) {
            float distance_to_player = (player_position.position - transform.position).sqrMagnitude;
            Debug.Log(distance_to_player);
            if (distance_to_player <= distance_threshold)
            {
                if (flower_field.activeSelf)
                    return;

                flower_field.transform.position = flower_field_position;
                flower_field.SetActive(true);
                GameManager.Instance.active_field = field_type;

            } else if (GameManager.Instance.active_field == field_type)
            {
                flower_field.SetActive(false);
                Destroy(flower_field);
                flower_field = Instantiate(prefabRef, flower_field_position, Quaternion.identity);
                flower_field.SetActive(false);
                GameManager.Instance.active_field = Constants.FIELD.NONE;
            }
        }
    }

}
