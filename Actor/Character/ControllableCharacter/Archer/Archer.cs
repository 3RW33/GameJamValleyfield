using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Archer : ControllableCharacter
    {
        [Header("Arrows")] 
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private int arrowPoolSize = 25;
        [SerializeField] private string arrowPoolName = "Arrows";
        [SerializeField] private int arrowForce = 500;
        [SerializeField] private float shotDelay = 1f;

        private ArcherAim aim;
        private ObjectPool<Arrow> arrowPool;
        private List<Arrow> arrowsShot;
        private bool canShoot;
        private InputActions.ArcherActions archerInputs;
        private Animator animator;
        private new Audio audio;

        protected override void Awake()
        {
            base.Awake();
            archerInputs = Inputs.Actions.Archer;
            aim = GetComponent<ArcherAim>();
            animator = GetComponentInChildren<Animator>();
            audio = Finder.Audio;
            arrowsShot = new List<Arrow>();
            canShoot = true;
            
            InitializeArrowPool();
        }

        private void SubscribeToInputs()
        {
            archerInputs.Shoot.started += OnShoot;
            aim.SubscribeToInputs();
        }

        public override void StartControlling()
        {
            base.StartControlling();
            SubscribeToInputs();
            audio.MainSoundPlayer.Play(audio.GameClips.switchToArcher);
        }

        public override void StopControlling()
        {
            base.StopControlling();
            UnsubscribeToInputs();
        }

        private void UnsubscribeToInputs()
        {
            archerInputs.Shoot.started -= OnShoot;
            aim.UnsubscribeToInputs();
        }

        private void InitializeArrowPool()
        {
            var arrowsContainer = new GameObject(arrowPoolName);
            arrowsContainer.transform.parent = transform;
            arrowPrefab.SetActive(false);

            arrowPool = new ObjectPool<Arrow>(() => CreateArrow(arrowsContainer), arrowPoolSize);
        }

        private Arrow CreateArrow(GameObject containerObject)
        {
            //TODO: Changer position initial a arc
            var arrow = Instantiate(arrowPrefab, transform.position, transform.rotation, containerObject.transform);

            var script = arrow.GetComponent<Arrow>();
            script.Archer = this;
            return script;
        }

        private void ShootArrow()
        {
            animator.SetTrigger(AnimatorParameters.archerAttack);
            var arrow = arrowPool.GetObject();
            arrowsShot.Add(arrow);
            arrow.gameObject.SetActive(true);

            var arrowTransform = arrow.transform;
                
            //TODO: Positionner la fleche ou l'arc
            arrowTransform.position = aim.AimOrigin;
            arrowTransform.rotation = Quaternion.Euler(0, 0, aim.CalculateAimAngle());
            
            arrow.Rigidbody2D.AddForce(aim.AimDirection * arrowForce);
            audio.MainSoundPlayer.Play(audio.GameClips.bowShot);
        }

        private void OnShoot(InputAction.CallbackContext context)
        {
            if (canShoot && (aim.Angle > -90 && aim.Angle < 90))
            {
                ShootArrow();
                StartCoroutine(ShootDelay());
            }
        }

        public void RemoveArrow(Arrow arrow)
        {
            arrow.gameObject.SetActive(false);
            arrowPool.PutObject(arrow);
            arrowsShot.Remove(arrow);
        }

        public void ClearArrows()
        {
            if (arrowsShot != null)
            {
                foreach (var arrow in arrowsShot)
                {
                    arrowPool.PutObject(arrow);
                    arrow.gameObject.SetActive(false);
                }
                arrowsShot.Clear();
            }
        }

        private IEnumerator ShootDelay()
        {
            canShoot = false;

            yield return new WaitForSeconds(shotDelay);

            canShoot = true;
        }
    }
}