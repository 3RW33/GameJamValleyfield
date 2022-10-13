using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class FlagController : MonoBehaviour
    {
        [SerializeField] private GameObject[] flagSpawns;

        private void Start()
        {
            var range = Random.Range(0,4);

            transform.position = flagSpawns[range].transform.position;
        }
    }
}