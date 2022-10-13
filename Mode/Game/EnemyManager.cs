using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [Findable(Tags.GameController)]
    public class EnemyManager : MonoBehaviour
    {
        [Header("Spawns")] 
        [SerializeField] private GameObject leftSpawn;
        [SerializeField] private GameObject rightSpawn;

        [Header("Enemies")]
        [SerializeField] private GameObject torchmanPrefab;
        [SerializeField] private GameObject magePrefab;
        [SerializeField] private GameObject mageProjectilePrefab;
        [SerializeField] private int enemyPoolSize = 50;
        [SerializeField] private string torchmanPoolName = "Torchman";
        [SerializeField] private string magePoolName = "Mage";
        [SerializeField] private float spawnDelay = 2.5f;
        [SerializeField] private float spawnDelayDecrease = 0.1f;
        [SerializeField] private int enemyAmountToSpawn = 6;
        [SerializeField] private int enemyIncrease = 2;
        [SerializeField] private float delayBetweenWave = 5f;
        
        private Inputs inputs;
        private ObjectPool<Torchman> torchmanPool;
        private ObjectPool<Mage> magePool;
        private new Audio audio;

        private void Awake()
        {
            inputs = Finder.Inputs;
            InitializeEnemyObjectPools();
            audio = Finder.Audio;
        }

        private void Start()
        {
            StartCoroutine(SpawmEnemies());
        }

        private void InitializeEnemyObjectPools()
        {
            var torchmanContainer = new GameObject(torchmanPoolName);
            torchmanContainer.transform.parent = transform;
            torchmanPrefab.SetActive(false);

            torchmanPool = new ObjectPool<Torchman>(() => CreateTorchman(torchmanContainer), enemyPoolSize);

            var mageContainer = new GameObject(magePoolName);
            mageContainer.transform.parent = transform;
            magePrefab.SetActive(false);

            magePool = new ObjectPool<Mage>(() => CreateMage(mageContainer), enemyPoolSize);
        }

        private Torchman CreateTorchman(GameObject torchmanContainer)
        {
            var torchman = Instantiate(torchmanPrefab, transform.position, transform.rotation, torchmanContainer.transform);

            return torchman.GetComponent<Torchman>();
        }

        private Mage CreateMage(GameObject mageContainer)
        {
            var mage = Instantiate(magePrefab, transform.position, transform.rotation, mageContainer.transform).GetComponent<Mage>();
            var fireball = Instantiate(mageProjectilePrefab, transform.position, transform.rotation, mage.transform).GetComponent<MageProjectile>();

            mage.MageProjectile = fireball;
            fireball.Mage = mage;
            
            return mage;
        }

        public void SpawnEnemy(GameObject spawnPoint)
        {
            var rand = Random.Range(0, 2);
            if (rand >= 1)
            {
                var torchman = torchmanPool.GetObject();

                var torchmanTransform = torchman.transform;

                torchmanTransform.position = spawnPoint.transform.position;
                
                torchman.gameObject.SetActive(true);
                if (torchman.Rigidbody2D.velocity == Vector2.zero)
                {
                    torchman.StartMovement();
                }
            }
            else
            {
                var mage = magePool.GetObject();

                var mageTransform = mage.transform;

                mageTransform.position = spawnPoint.transform.position;

                mage.gameObject.SetActive(true);
                if (mage.Rigidbody2D.velocity == Vector2.zero)
                {
                    mage.StartMovement();
                }
                audio.MainSoundPlayer.Play(audio.GameClips.mageSpawn);
            }
        }

        public void RemoveTorchman(Torchman torchman)
        {
            torchman.gameObject.SetActive(false);
            torchmanPool.PutObject(torchman);
        }

        public void RemoveMage(Mage mage)
        {
            mage.gameObject.SetActive(false);
            magePool.PutObject(mage);
        }

        private IEnumerator SpawmEnemies()
        {
            int enemyRemainingToSpawn = enemyAmountToSpawn;

            while (enemyRemainingToSpawn > 0)
            {
                enemyRemainingToSpawn--;
                SpawnEnemy(enemyRemainingToSpawn % 2 == 1 ? leftSpawn : rightSpawn);
                yield return new WaitForSeconds(spawnDelay);
            }

            enemyAmountToSpawn += enemyIncrease;
            if (spawnDelay - spawnDelayDecrease > 0)
            {
                spawnDelay -= spawnDelayDecrease;   
            }
            
            yield return new WaitForSeconds(delayBetweenWave);
            StartCoroutine(SpawmEnemies());
        }
    }
}