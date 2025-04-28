using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentRecycler : MonoBehaviour
{
    private SegmentGenerator SegGen;
    void Start()
    {
        SegGen = FindObjectOfType<SegmentGenerator>();
        if (SegGen == null)
        {
            Debug.LogError("Nie znaleziono obiektu SegmentGenerator na scenie!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room"){
            Segment segmentScript = other.GetComponent<Segment>();
            SegGen.addToUsable(segmentScript.uniqueID);
        }
    }
}
