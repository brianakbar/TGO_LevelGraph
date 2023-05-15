namespace CSG {
    using UnityEngine;
    using Parabox.CSG;

    public class CSGTest : MonoBehaviour {
        [SerializeField] GameObject modelPrefab;

        public void TestPrimitiveSubstract() {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            sphere.transform.localScale = Vector3.one * 1.3f;

            Model result = CSG.Subtract(cube, sphere);

            var composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

            MoveX(cube, 2);
            MoveX(sphere, -2);
        }

        public void TestPrimitiveUnion() {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            MoveZ(cube, -2);
            MoveZ(sphere, -2);

            MoveX(cube, 2);
            MoveX(sphere, -2);
            
            sphere.transform.localScale = Vector3.one * 1.3f;

            Model result = CSG.Union(cube, sphere);

            var composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        }

        public void TestModelUnion() {
            GameObject model = Instantiate(modelPrefab);
            model.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            MoveZ(model, -6);

            GameObject one = model.transform.GetChild(0).gameObject;
            GameObject two = model.transform.GetChild(1).gameObject;

            Model result = CSG.Union(one, two);

            var composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

            MoveX(composite, 4);
        }

        void MoveX(GameObject gameObject, float x) {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x + x,
                gameObject.transform.position.y,
                gameObject.transform.position.z
            );
        }

        void MoveZ(GameObject gameObject, float z) {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z + z
            );
        }
    }
}