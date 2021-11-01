using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using For_UI;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGeneration : MonoBehaviour
{
    public static LevelGeneration Instance { get; private set; }
    [SerializeField] GameObject[] patterns;
    [FormerlySerializedAs("patternsToSpawn")] [SerializeField] int[] patternsToDebug;
    private float triggerHeight = 0;
    private float offsetBetweenPatterns = 2.4f;
    private float PreviousPatternSize;
    public List<GameObject> PatternsOnScene = new List<GameObject>() { };
    List<int> usedNums = new List<int>() { };
    int localIndex;
    int index;

    private int[] levelPool = { };
    [SerializeField] private int[] levelElementary;
    [SerializeField] private int[] levelIntermediate;
    [SerializeField] private int[] levelAdvanced;

    private void Start()
    {
        Instance = this;
        levelPool = levelPool.Concat(levelElementary).ToArray();
        Score.Instance.OnScoreChange = () => { levelPool = levelPool.Concat(levelIntermediate).ToArray(); Debug.Log("LVL2"); };
        Score.Instance.OnSignificantScoreChange = () => { levelPool = levelPool.Concat(levelAdvanced).ToArray();
            Debug.Log("LVL3");
        };
        
        if (patternsToDebug.Length != 0)
        {
            SpawnPattern(GetValueOfIndexOfPatternToSpawn(patternsToDebug));
            SpawnPattern(GetValueOfIndexOfPatternToSpawn(patternsToDebug));
        }
        else
        {
            SpawnPattern(levelPool[RandomWithoutRepeating(levelPool.Length)] - 1);
            SpawnPattern(levelPool[RandomWithoutRepeating(levelPool.Length)] - 1);
        }
    }

    void Update()
    {
        
        if (patternsToDebug.Length != 0 && CameraControl.Instance.transform.position.y + 1 >= triggerHeight)
        {
            SpawnPattern(GetValueOfIndexOfPatternToSpawn(patternsToDebug));
           
            DeleteOldPattern();
        }
        else if (patternsToDebug.Length == 0 && CameraControl.Instance.transform.position.y + 1 >= triggerHeight)
        {
            SpawnPattern(levelPool[RandomWithoutRepeating(levelPool.Length)] - 1);
            
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