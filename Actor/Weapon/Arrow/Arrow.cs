using System.Collections;
using Harmony;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Arrow : Weapon
    {
        [SerializeField] private float despawnDelay = 5f;
        public Archer Archer { get; set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        private new Collider2D collider2D;
        private Animator animator;
        private Game game;

        private void Awake()
        {
            game = Finder.Game;
            collider2D = GetComponent<Collider2D>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            IsFriendly = true;
        }

        private void Update()
        {
            if (Rigidbody2D.velocity != Vector2.zero)
            {
                Vector2 vect = Rigidbody2D.velocity;
                var angle = Mathf.Atan2(vect.y, vect.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.Hit(Damage);
            }
            
            var ground = other.gameObject.GetComponentInParent<Ground>();
            if (ground != null)
            {
                StartCoroutine(ArrowHit());
            }
        }

        private IEnumerator ArrowHit()
        {
            animator.enabled = false;
            collider2D.enabled = false;
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.isKinematic = true;

            StartCoroutine(MoveWithGround());
            yield return new WaitForSeconds(despawnDelay);

            animator.enabled = true;
            collider2D.enabled = true;
            Rigidbody2D.isKinematic = false;
            Archer.RemoveArrow(this);
        }

        private IEnumerator MoveWithGround()
        {
            while (isActiveAndEnabled)
            {
                transform.Translate(Vector3.left * (game.EnvironmentMovementSpeed * Time.deltaTime), Space.World);
                yield return null;
            }
        }
    }
}