using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentRecycler : MonoBehaviour
{
    private SegmentGenerator SegGen;
    void Start()
    {
        GameObject tmp = GameObject.FindWithTag("SegmentGenerator");
        if (tmp == null) 
        {
            Debug.LogError("Nie znaleziono generatora");
        } else {
            SegGen = tmp.GetComponent<SegmentGenerator>();
        }
        if (SegGen == null)
        {
            Debug.LogError("Nie udało się załadować generatora!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rooms"){
            Segment segmentScript = other.GetComponent<Segment>();
            SegGen.addToUsable(segmentScript.uniqueID);
        }
    }
}
