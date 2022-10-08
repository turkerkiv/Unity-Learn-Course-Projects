using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ballCounterTMP;
    [SerializeField] float _ballDestroyDelay = 20f;
    [SerializeField] float _restartDelay = 3f;

    int _ballCount;

    void Start()
    {
        _ballCounterTMP.text = "Ball Count: " + _ballCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        _ballCount++;
        _ballCounterTMP.text = "Ball Count: " + _ballCount;

        Destroy(other.gameObject, _ballDestroyDelay);

        if (other.gameObject.CompareTag("ClientMessenger"))
        {
            Invoke(nameof(Restart), _restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
