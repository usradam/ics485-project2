using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private AudioClip bulletSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(bulletSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = bulletPoint.position;
        bullets[FindBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
