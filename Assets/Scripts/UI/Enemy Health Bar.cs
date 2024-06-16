using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class EnemyHealthBar : MonoBehaviour
{
    public Sprite[] HealthBarSprites;
    private SpriteRenderer _spriteRenderer;

    public ParticleSystem DestroyParticles;

    [Header("Health data")]
    public Image Image;
    public SpriteRenderer GlowSpriteRenderer;
    public TMP_Text Text;
    public AudioClip HealthBarDestroy_Sound;
    private AudioSource _audioSource;

    [Header("Animation values")]
    public float TakeDamageMovingSpread;
    public float TakeDamageScaleSpread;
    public float TakeDamageRotationSpread;

    private Vector3 _startPosition;
    private Vector3 _startRotation;
    private Vector3 _startScale;

    private bool _enemyDead;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Image.fillAmount = 1;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = HealthBarSprites[Random.Range(0, HealthBarSprites.Length)];

        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation.eulerAngles;
        _startScale = transform.localScale;
    }

    public void HealthDecrease(float amount, float takeDamageAnimationTime)
    {
        if (!_enemyDead)
        {
            Image.fillAmount -= amount;

            transform.DOLocalMove(CalculateMovingVector(), takeDamageAnimationTime).SetEase(Ease.OutElastic);

            transform.DOLocalRotate(CalculateRotationVector(), takeDamageAnimationTime).SetEase(Ease.OutElastic);

            transform.DOScale(CalculateScaleVector(), takeDamageAnimationTime).SetEase(Ease.OutElastic);

            transform.DOLocalMove(_startPosition, takeDamageAnimationTime).SetEase(Ease.OutElastic);
            transform.DOLocalRotate(_startRotation, takeDamageAnimationTime).SetEase(Ease.OutElastic);
            transform.DOScale(_startScale, takeDamageAnimationTime).SetEase(Ease.OutElastic);

            if (Image.fillAmount <= 0.0001f)
            {
                _audioSource.PlayOneShot(HealthBarDestroy_Sound, _audioSource.volume);
                Image.fillAmount = 0;
                DestroyParticles.Play();
                Image.color = new Color(0,0,0,0);
                _spriteRenderer.sprite = null;
                GlowSpriteRenderer.sprite = null;
                Text.text = null;
                _enemyDead = true;
            }
        }

    }

    private Vector3 CalculateMovingVector()
    {
        Vector3 movingVector = new Vector3(Random.Range(transform.localPosition.x - TakeDamageMovingSpread, transform.localPosition.x + TakeDamageMovingSpread), Random.Range(transform.localPosition.y - TakeDamageMovingSpread, transform.localPosition.y + TakeDamageMovingSpread), transform.localPosition.z);
        return movingVector;
    }

    private Vector3 CalculateScaleVector()
    {
        Vector3 scaleVector = new Vector3(Random.Range(transform.localScale.x - TakeDamageScaleSpread, transform.localScale.x + TakeDamageScaleSpread), Random.Range(transform.localScale.y - TakeDamageScaleSpread, transform.localScale.y + TakeDamageScaleSpread), transform.localScale.z);
        return scaleVector;
    }

    private Vector3 CalculateRotationVector()
    {
        Vector3 rotationVector = new Vector3(transform.localRotation.x, transform.localRotation.y, Random.Range(transform.localRotation.z - TakeDamageRotationSpread, transform.localRotation.z + TakeDamageRotationSpread));
        return rotationVector;
    }
}
