using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segmentPrefab;
    private static int numberOfSegment = 5;
    private List<GameObject> poolOfSegments = new List<GameObject>();
    private List<GameObject> usableSegments = new List<GameObject>();
    
    private GameObject newSegment;
    private Vector3 newSegmentPosition;
    // Start is called before the first frame update
    void Start(){
        int i = 0;
        while(i < numberOfSegment){
            int rSegment = UnityEngine.Random.Range(0,segmentPrefab.Length);
            newSegmentPosition = new Vector3(transform.position.x + 10*i,transform.position.y,transform.position.z);
            newSegment = Instantiate(segmentPrefab[rSegment], newSegmentPosition, transform.rotation);
            poolOfSegments.Add(newSegment); 
            usableSegments.Add(newSegment);
            i++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(usableSegments.Count > 3){
            int rSegment = UnityEngine.Random.Range(0,usableSegments.Count); 
            newSegmentPosition = new Vector3(10*numberOfSegment,0,0);
            usableSegments[rSegment].transform.position += newSegmentPosition;
            usableSegments.RemoveAt(rSegment);
        }
        

        transform.position += new Vector3(10,0,0);
        Debug.Log("triger work");
    }
}
