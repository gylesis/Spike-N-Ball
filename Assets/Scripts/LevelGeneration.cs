using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public static LevelGeneration Instance { get; private set; }
    [SerializeField] GameObject[] patterns;
    [SerializeField] int[] patternsToSpawn;
    private float triggerHeight = 0;
    private float offsetBetweenPatterns = 3f;
    private float PreviousPatternSize;
    public List<GameObject> PatternsOnScene = new List<GameObject>() { };
    List<int> usedNums = new List<int>() { };
    int localIndex;
    int index;

    private void Awake()
    {
        Instance = this;
        if (patternsToSpawn.Length != 0)
        {
            SpawnPattern(GetValueOfIndexOfPatternToSpawn(patternsToSpawn));
            SpawnPattern(GetValueOfIndexOfPatternToSpawn(patternsToSpawn));
        }
        else
        {
            SpawnPattern(RandomWithoutRepeating(patterns.Length));
            SpawnPattern(RandomWithoutRepeating(patterns.Length));
        }
    }

    void Update()
    {
        
        if (patternsToSpawn.Length != 0 && CameraControl.Instance.transform.position.y + 4 >= triggerHeight)
        {
            SpawnPattern(GetValueOfIndexOfPatternToSpawn(patternsToSpawn));
           
            DeleteOldPattern();
        }
        else if (patternsToSpawn.Length == 0 && CameraControl.Instance.transform.position.y + 4 >= triggerHeight)
        {
            SpawnPattern(RandomWithoutRepeating(patterns.Length));
            
            DeleteOldPattern();
        }

        
    }

    private void DeleteOldPattern()
    {
        if (localIndex - 4 >= 0)
        {
            Destroy(PatternsOnScene[0]);
            PatternsOnScene.RemoveAt(0);
            localIndex--;
        } 
    }
    int RandomWithoutRepeating(int MaxValue)
    {
        int rnd = (int)(UnityEngine.Random.value * MaxValue);
        foreach (int num in usedNums)
        {
            if (rnd == num)
            {
                if (num > (float)MaxValue / 2) 
                    rnd--;
                else 
                    rnd++;
            }
        }
        if (usedNums.Count <= 1)
        {
            usedNums.Add(rnd);
        }
        else
        {
            usedNums.RemoveAt(0);
            usedNums.Add(rnd);
        }
        return rnd;
    }
    void SpawnPattern(int indexOfPattern)
    {
        triggerHeight += PreviousPatternSize + offsetBetweenPatterns;
        PatternsOnScene.Add(Instantiate(patterns[indexOfPattern], new Vector3(0, triggerHeight, 0), Quaternion.identity));

        int coef = 1;
        if (UnityEngine.Random.value > 0.5f) coef = -1;
        Vector3 scaleOfPattern = PatternsOnScene[localIndex].transform.localScale;
        PatternsOnScene[localIndex].transform.localScale = new Vector3(scaleOfPattern.x * coef, scaleOfPattern.y, scaleOfPattern.z);

        PreviousPatternSize = PatternsOnScene[localIndex].transform.GetChild(0).transform.localPosition.y;
        if (localIndex < 4) localIndex++;
        index++;
    }


    int GetValueOfIndexOfPatternToSpawn(int[] patternsToSpawn)
    {
        int i = index % patternsToSpawn.Length;
        return patternsToSpawn[i] - 1;
        
    }
}