using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    [SerializeField] List<GameObject> _stock;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("ClientMessenger"))
        {
            int random = Random.Range(0,_stock.Count);
            Instantiate(_stock[random], transform.position, Quaternion.identity);
        }
    }
}
