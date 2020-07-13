using System;
using System.Collections;
using UnityEngine;

namespace CatlikeCoding.Basics.Fractal
{
    public class Fractal : MonoBehaviour
    {
        static Vector3[] childDirections = { Vector3.up, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
        static Quaternion[] childOrientations = { Quaternion.identity, Quaternion.Euler(0f, 0f, -90f), Quaternion.Euler(0f, 0f, 90f), Quaternion.Euler(90f, 0f, 0f), Quaternion.Euler(-90f, 0f, 0f) };
        public Mesh[] meshes;
        public Material material;

        public int maxDepth;
        int depth;

        public float spawnProbability;

        public float maxRotationSpeed;
        float rotationSpeed;

        public float maxTwist;

        float childScale = .5f;
        Material[,] materials;

        // Start is called before the first frame update
        private void Start()
        {
            if (materials == null) InitializeMaterials();
            gameObject.AddComponent<MeshFilter>().mesh = meshes[UnityEngine.Random.Range(0, meshes.Length)];
            gameObject.AddComponent<MeshRenderer>().material = materials[depth, UnityEngine.Random.Range(0, 2)];
            if (depth < maxDepth) StartCoroutine(CreateChildren());

            rotationSpeed = UnityEngine.Random.Range(-maxRotationSpeed, maxRotationSpeed);
            transform.Rotate(UnityEngine.Random.Range(-maxTwist, maxTwist), 0f, 0f);
        }

        private void InitializeMaterials()
        {
            materials = new Material[maxDepth + 1, 2];
            for (int i = 0; i <= maxDepth; i++)
            {
                float t = i / (maxDepth - 1f);
                t *= t;
                materials[i, 0] = new Material(material);
                materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
                materials[i, 1] = new Material(material);
                materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
            }
            materials[maxDepth, 0].color = Color.magenta;
            materials[maxDepth, 1].color = Color.red;
        }

        private IEnumerator CreateChildren()
        {
            for (int i = 0; i < childDirections.Length; i++)
            {
                if (UnityEngine.Random.value < spawnProbability)
                {
                    yield return new WaitForSeconds(UnityEngine.Random.Range(.1f, .5f));
                    new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
                }
            }
        }

        private void Initialize(Fractal parent, int childIndex)
        {
            meshes = parent.meshes;
            materials = parent.materials;
            maxDepth = parent.maxDepth;
            depth = parent.depth + 1;
            childScale = parent.childScale;
            spawnProbability = parent.spawnProbability;
            maxRotationSpeed = parent.maxRotationSpeed;
            maxTwist = parent.maxTwist;

            transform.parent = parent.transform;
            transform.localScale = Vector3.one * childScale;
            transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
            transform.localRotation = childOrientations[childIndex];

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0f, Mathf.Sin(rotationSpeed + Time.time), 0f);
        }
    }

}