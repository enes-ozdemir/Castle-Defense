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
            var explosionPosition = col.ClosestPoint(transform.position);
            Instantiate(skillEndPrefab, explosionPosition, Quaternion.identity);
            var particle = GetComponent<ParticleSystem>();
            Destroy(particle);
        }
    }
}