using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Background Pattern", menuName = "Create New Background Pattern")]
public class BackgroundPattern : ScriptableObject
{
    public PatternBehavoiur patternBehavoiur;

    public List<GameObject> Layers;

    public float PatternMovingDistance;
    public float MovingTime;
}

public enum PatternBehavoiur
{
    classic = 1,
    agressive = 2,
}

