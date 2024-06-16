using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterPassiveAnimation : MonoBehaviour
{
    public float PassiveAnimationTime;
    public Vector3 PassiveAnimationMovingValue;
    public Vector3 PassiveAnimationRotateValue;
    public Vector3 PassiveAnimationScaleValue;

    private void Start()
    {
        StartAnimate();
    }

    private IEnumerator RotateTransform()
    {

        transform.DORotate(PassiveAnimationRotateValue, PassiveAnimationTime, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).SetLoops(1, LoopType.Yoyo);

        yield return new WaitForSeconds(PassiveAnimationTime * 2);

        PassiveAnimationRotateValue *= -1;
        StartCoroutine(RotateTransform());
    }

    public void StartAnimate()
    {
        transform.DOMove(PassiveAnimationMovingValue, PassiveAnimationTime).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        transform.DOScale(PassiveAnimationScaleValue, PassiveAnimationTime).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(RotateTransform());
    }
}
