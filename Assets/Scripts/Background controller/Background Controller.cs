using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundController : MonoBehaviour
{

    private List<GameObject> _layers;

    public BackgroundPattern BackgroundPattern;

    public void CreateLayers()
    {
        _layers = new List<GameObject>();

        for (int i = 0; i < BackgroundPattern.Layers.Count; i++)
        {
            _layers.Add(Instantiate(BackgroundPattern.Layers[i], BackgroundPattern.Layers[i].transform.localPosition, Quaternion.identity));
        }
    }

    private void Start()
    {
        switch (BackgroundPattern.patternBehavoiur)
        {
            case PatternBehavoiur.classic:

                for(int i = 0; i < _layers.Count; i++)
                {
                    _layers[i].transform.DOMoveX(BackgroundPattern.PatternMovingDistance, BackgroundPattern.MovingTime * (i + 1)).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
                }

                break;

            case PatternBehavoiur.agressive:

                break;
        }

    }

    public void ClearLayers()
    {
        for (int i = 0; i < _layers.Count; i++)
        {
            Destroy(_layers[i]);
        }

        _layers.Clear();
    }

}

