using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAxe : MonoBehaviour
{
    [SerializeField] private int skillDamage;
    [SerializeField] private float skillAttackCooldown;
    [SerializeField] private GameObject axeIcon;

    private PlayerControlActions playerAction;
    private ISkillAnimator skillAnimator;
    private bool canUseAxe = false;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private IDirection direction;
    private PlayerBasicAttack allowedAttack;

    private void Awake()
    {
        allowedAttack = GetComponentInParent<PlayerBasicAttack>();
        axeIcon.SetActive(false);
        WeaponPickup.WeaponPickupEvent += WeaponActivated;
        rb = GetComponentInParent<Rigidbody2D>();
        sprite = this.GetComponentInParent<SpriteRenderer>();
        playerAction = new PlayerControlActions();
        skillAnimator = GetComponentInParent<ISkillAnimator>();
        direction = GetComponent<IDirection>();
    }

    private void SkillAttack(InputAction.CallbackContext skill)
    {      
        if (Keyboard.current.digit2Key.wasPressedThisFrame && canUseAxe && allowedAttack.canAttack)
        {
            direction.AttackDirection();
            axeIcon.SetActive(false);
            canUseAxe = false;
            skillAnimator.SkillAttackAnimation(2);
            KnockbackEffect();
            StartCoroutine(AxeCooldown());
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(CapsuleCollider2D) && collision.gameObject.layer == 7)
            collision.GetComponent<ITakeDamage>().TakeDamage(skillDamage);
    }

    IEnumerator AxeCooldown()
    {
        yield return new WaitForSeconds(skillAttackCooldown);
        axeIcon.SetActive(true);
        canUseAxe = true;
    }

    private void KnockbackEffect()
    {
        if (sprite.flipX == false)
        {
            rb.AddForce(Vector2.left * 80, ForceMode2D.Force);
            rb.AddForce(Vector2.up * 20, ForceMode2D.Force);
        }
        else if (sprite.flipX == true)
        {
            rb.AddForce(Vector2.right * 80, ForceMode2D.Force);
            rb.AddForce(Vector2.up * 20, ForceMode2D.Force);
        }      
    }

    private void WeaponActivated(WeaponPickup weapon)
    {
        if(weapon.name == "Axe(Clone)")
        {
            axeIcon.SetActive(true);
            canUseAxe = true;
        }
    }

    private void OnEnable()
    {
        playerAction.Player.Enable();
        playerAction.Player.Skills.started += SkillAttack;
    }

    private void OnDisable()
    {
        WeaponPickup.WeaponPickupEvent -= WeaponActivated;
        playerAction.Player.Skills.started -= SkillAttack;
        playerAction.Player.Disable();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(0.6019654f, 2.4026f));
    //}
}
    