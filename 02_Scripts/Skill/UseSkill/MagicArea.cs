using System.Collections;
using UnityEngine;
using System;

public class MagicArea : MonoBehaviour, SkillAction
{
    public Action onStart { get; set; }
    public Action onMiddle { get; set; }
    public Action onHit { get; set; }
    public Action onEnd { get; set; }
    public Action onEnemy { get; set; }
    public bool through { get; set; }
    public bool homing { get; set; }
    public float duration { get; set; }
    public ElementalType elemental { get; set; }
    public float damage { get; set; }
    public bool enemy { get; set; }
    public GameObject caster { get; set; }
    public string objName { get; set; }
    public float speed { get; set; }

    private Vector3 tempScale;
    private Vector3 randomOffset;


    [Header("Controller")]
    [SerializeField] private SkillSoundController soundController;
    [SerializeField] private SkillVFXController vfxController;


    public void Init(Vector3 pos)
    {
        onMiddle?.Invoke();
        GetComponent<Animator>().SetInteger("Elemental", (int)elemental);
        ChangeColor();

        FloorEffect floor = GetComponent<FloorEffect>(); // floor 설정
        floor.damage = damage;
        floor.delay = 0.5f;
        floor.elemental = elemental;
        floor.soundController = soundController;
        floor.vfxController = vfxController;
        floor.objName = objName;

        tempScale = transform.localScale;

        soundController.StartSound(elemental,objName);
        StartCoroutine(Deployment());
        StartCoroutine(VFXPlay());
    }

    private IEnumerator Deployment()
    {
        
        transform.localScale = new Vector3(0, 0, 0);

        while (transform.localScale.x < tempScale.x)
        {
            float x = transform.localScale.x + Time.deltaTime * tempScale.x;
            float y = transform.localScale.y + Time.deltaTime * tempScale.y;
            transform.localScale = new Vector3(x, y, 1f);
            yield return null;
        }
        yield return YieldCache.WaitForSeconds(duration);
        while (transform.localScale.x > 0)
        {
            float x = transform.localScale.x - Time.deltaTime * tempScale.x;
            float y = transform.localScale.y - Time.deltaTime * tempScale.y;
            transform.localScale = new Vector3(x, y, 1f);
            yield return null;
        }
        SoundManager.StopAllSFX();
        ObjectPoolManager.Instance.Return(this.gameObject, objName);

    }

    private IEnumerator VFXPlay()
    {
        while (true)
        {
            randomOffset = new Vector3(
                    UnityEngine.Random.Range(-tempScale.x / 2, tempScale.x / 2),  // X축 랜덤
                    UnityEngine.Random.Range(-tempScale.y / 2, tempScale.y / 2),  // Y축 랜덤
                    0f
                );

            vfxController.GetVFX(elemental, transform.rotation, gameObject.transform.position + randomOffset);

            yield return YieldCache.WaitForSeconds(0.25f);
        }
    }

    private void ChangeColor()
    {
        switch (elemental)
        {
            case ElementalType.Normal:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                break;
            case ElementalType.Fire:
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                break;
            case ElementalType.Water:
                GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 1);
                break;
            case ElementalType.Ice:
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                break;
            case ElementalType.Electric:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
                break;
            case ElementalType.Dark:
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
                break;
            case ElementalType.Light:
                GetComponent<SpriteRenderer>().color = new Color(1, 0.8f, 0);
                break;
        }
    }


    public void EnemyInit()
    {

    }

}
