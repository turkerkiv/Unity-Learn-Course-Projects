using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float realTime;
    [SerializeField] float fireDelay = 1f;

    // Update is called once per frame
    void Update()
    {
        realTime += Time.deltaTime;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && realTime > fireDelay)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            realTime = 0;
        }
    }
}
