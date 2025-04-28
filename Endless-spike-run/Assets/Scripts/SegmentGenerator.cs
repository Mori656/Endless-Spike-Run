using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segmentPrefab;
    public GameObject spikesPrefab;
    private static int numberOfSegment = 5;
    private List<GameObject> poolOfSegments = new List<GameObject>();
    private List<GameObject> usableSegments = new List<GameObject>();
    
    private GameObject newSegment, newspike;
    private Vector3 newSegmentPosition, spikeMove, spikePosition;
    // Start is called before the first frame update

    void Start(){
        int i = 0;
        while(i < numberOfSegment){
            int rSegment = UnityEngine.Random.Range(0,segmentPrefab.Length);
            newSegmentPosition = new Vector3(transform.position.x - 10*i,transform.position.y,transform.position.z);
            newSegment = Instantiate(segmentPrefab[rSegment], newSegmentPosition, transform.rotation);
            poolOfSegments.Add(newSegment); 

            newSegmentPosition = new Vector3(transform.position.x - 10*i,transform.position.y,transform.position.z + 20);
            newSegment = Instantiate(segmentPrefab[rSegment], newSegmentPosition, transform.rotation);
            usableSegments.Add(newSegment);

            i++;
        }
        spikePosition = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        newspike = Instantiate(spikesPrefab,spikePosition,transform.rotation);
        spikeMove = new Vector3(-1,0,0);
    }

    void Update()
    { 
        moveSpikeForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(usableSegments.Count > 3){
            int rSegment = UnityEngine.Random.Range(0,usableSegments.Count); 
            newSegmentPosition = new Vector3(transform.position.x - 10*numberOfSegment,transform.position.y,transform.position.z);
            usableSegments[rSegment].transform.position = newSegmentPosition;
            usableSegments.RemoveAt(rSegment);
        }
        

        transform.position += new Vector3(-10,0,0);
        Debug.Log("triger work");
    }

    private void moveSpikeForward(){
        newspike.transform.position += spikeMove * Time.deltaTime;   
    }

    public void addToUsable(GameObject segment){
        usableSegments.Add(segment);
    }
}
