using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] public Enemy enemy;
    private GameObject skillEndPrefab;

    private void Start()
    {
        skillEndPrefab = enemy.enemySkill.skillEndEffect;
    }

    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * enemy.enemySkill.skillSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Castle"))
        {
            SoundManager.PlaySound(enemy.enemySkill.sound);
            var explosionPosition = col.ClosestPoint(transform.position);
            var explosion = Instantiate(skillEndPrefab, explosionPosition, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosion, 1.5f);
        }
    }
}