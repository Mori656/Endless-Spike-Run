using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public float coinRotationSpeed = 20;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, coinRotationSpeed * Time.deltaTime);
    }
}
