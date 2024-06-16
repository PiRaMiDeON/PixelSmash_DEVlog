using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator), typeof(AudioSource), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [Header("Main values")]
    public UserData UserData;
    public EnemiesController EnemiesController;

    [SerializeField] protected int _healthCount;
    [SerializeField] protected int _damageCount;
    [SerializeField] protected int _protectionCount;
    private float _startHealthCount;

    public DamageType Weakness;
    public DamageType Resistance;

    public AnimationClip DeathAnimation;

    [Header("Take damage animation values")]
    public float TakeDamageMovingSpread;
    public float TakeDamageAnimationTime;
    public Color TakeDamageColor;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;

    [Header("Particles")]
    public ParticleSystem TakeDamageParticles;
    public ParticleSystem DeathParticles;
    public ParticleSystem PassiveParticles;

    [Header("Sounds")]
    public bool RandomizeSounds;
    public AudioClip[] TakeDamageSounds;
    public AudioClip[] DeathSounds;
    protected AudioSource _audioSource;
    private int _soundIndex;
    private int _lastSoundIndex = -1;

    [Header("Health Bar")]
    public EnemyHealthBar EnemyHealthBar;

    private Vector3 _startPosition;
    private Color _startColor;

    [HideInInspector] public bool _isDead;
    private bool _isAttacking;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startPosition = transform.localPosition;
        _startColor = _spriteRenderer.color;
        _startHealthCount = _healthCount;
    }
    public void OnMouseDown()
    {
        if (!_isDead && !_isAttacking)
        {
            StartCoroutine(Reloading());
            TakeDamage();
        }
    }
    protected virtual void TakeDamage()
    {
        AnimateTakeDamage();

        int resultProtection = (_protectionCount - UserData.ClickWeapon._protectDamage);
        if (resultProtection < 0)
        {
            resultProtection = 0;
        }

        int resultDamage = (UserData.ClickWeapon._clickDamage - resultProtection);

        if (Weakness.HasFlag(UserData.ClickWeapon.DamageType))
        {
            resultDamage *= 2;
        }

        if (Resistance.HasFlag(UserData.ClickWeapon.DamageType))
        {
            resultDamage /= 2;
        }

        if (resultDamage <= 0)
        {
            resultDamage = 1;
        }

        _healthCount -= resultDamage;
        EnemyHealthBar.HealthDecrease(resultDamage / _startHealthCount, TakeDamageAnimationTime);

        if (_healthCount <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    protected virtual void AnimateTakeDamage()
    {
        if (RandomizeSounds)
        {
            do
            {
                _soundIndex = Random.Range(0, TakeDamageSounds.Length);
            } while (_soundIndex == _lastSoundIndex);
            _lastSoundIndex = _soundIndex;

            _audioSource.PlayOneShot(TakeDamageSounds[_soundIndex], _audioSource.volume);
        }
        else
        {
            if (_soundIndex >= TakeDamageSounds.Length)
            {
                _soundIndex = 0;
                _audioSource.PlayOneShot(TakeDamageSounds[_soundIndex], _audioSource.volume);
            }
            else
            {
                _audioSource.PlayOneShot(TakeDamageSounds[_soundIndex], _audioSource.volume);
                _soundIndex++;
            }
        }

        TakeDamageParticles.Play();

        transform.DOLocalMove(CalculateMovingVector(), TakeDamageAnimationTime).SetEase(Ease.OutElastic);
        _spriteRenderer.DOColor(TakeDamageColor, TakeDamageAnimationTime / 20);

        transform.DOLocalMove(_startPosition, TakeDamageAnimationTime).SetEase(Ease.OutElastic);
        _spriteRenderer.DOColor(_startColor, TakeDamageAnimationTime * 3);
    }

    protected virtual IEnumerator Dead()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        PassiveParticles.Stop();

        yield return new WaitForSeconds(DeathAnimation.length);

        _spriteRenderer.enabled = false;
        DeathParticles.Play();

        yield return new WaitForSeconds(DeathParticles.main.startLifetime.constantMax);

        EnemiesController.SpawnNewEnemy(DeathAnimation.length);
        gameObject.SetActive(false);
    }

    protected Vector3 CalculateMovingVector()
    {
        Vector3 movingVector = new Vector3(Random.Range(transform.localPosition.x - TakeDamageMovingSpread, transform.localPosition.x + TakeDamageMovingSpread), Random.Range(transform.localPosition.y - TakeDamageMovingSpread, transform.localPosition.y + TakeDamageMovingSpread), transform.localPosition.z);
        return movingVector;
    }

    private IEnumerator Reloading()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(UserData.ClickWeapon._clickReloading);

        _isAttacking = false;
    }

}
