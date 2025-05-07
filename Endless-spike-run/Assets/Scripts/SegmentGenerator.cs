using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segmentPrefab;
    public GameObject spikesPrefab;
    
    private static int numberOfSegments = 10;
    private List<GameObject> usableSegments = new List<GameObject>();
    private List<GameObject> poolOfSegments;

    public string uniqueID;

    private GameObject newSegment, newSpike;
    private Vector3 newSegmentPosition, spikeMove, spikePosition;

    void Start()
    {
        poolOfSegments = new List<GameObject>();
        for (int i = 0; i < numberOfSegments; i++)
        {
            int rSegment = UnityEngine.Random.Range(0, segmentPrefab.Length);
            
            // Tworzenie segmentu
            newSegmentPosition = new Vector3(transform.position.x - 10 * i, transform.position.y, transform.position.z);
            newSegment = Instantiate(segmentPrefab[rSegment], newSegmentPosition, transform.rotation);
            Segment segmentInfo = newSegment.AddComponent<Segment>();
            segmentInfo.uniqueID = Guid.NewGuid().ToString();
            poolOfSegments.Add(newSegment);

            // Tworzenie segmentu do użytku
            newSegmentPosition = new Vector3(transform.position.x - 10 * i, transform.position.y, transform.position.z + 20);
            newSegment = Instantiate(segmentPrefab[rSegment], newSegmentPosition, transform.rotation);
            segmentInfo = newSegment.AddComponent<Segment>();
            segmentInfo.uniqueID = Guid.NewGuid().ToString();
            poolOfSegments.Add(newSegment);
            usableSegments.Add(newSegment);
        }

        // Tworzenie kolca
        spikePosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        newSpike = Instantiate(spikesPrefab, spikePosition, transform.rotation);
        spikeMove = new Vector3(-5, 0, 0);
    }

    void Update()
    {
        MoveSpikeForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (usableSegments.Count > 0)
            {
                int rSegment = UnityEngine.Random.Range(0, usableSegments.Count);
                GameObject selectedSegment = usableSegments[rSegment];

                newSegmentPosition = new Vector3(transform.position.x - 10 * numberOfSegments, transform.position.y, transform.position.z);
                selectedSegment.transform.position = newSegmentPosition;

                usableSegments.RemoveAt(rSegment); // Usuwamy segment z listy po użyciu
            }
            else
            {
                Debug.LogWarning("Brak dostępnych segmentów do użycia!");
            }

            transform.position += new Vector3(-10, 0, 0);
        }
    }

    private void MoveSpikeForward()
    {
        newSpike.transform.position += spikeMove * Time.deltaTime;
    }

    public void addToUsable(string segmentID)
    {
        foreach(GameObject seg in poolOfSegments){
            Segment segtScript = seg.GetComponent<Segment>();
            if(segtScript.uniqueID == segmentID){
                usableSegments.Add(seg);
            }
        }
    }
}
