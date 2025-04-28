using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentRecycler : MonoBehaviour
{
    public SegmentGenerator SegGen;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room")){
            SegGen.addToUsable(other.gameObject);
        }
    }
}
