using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{




    /*
    private Vector2 sunVelocity = Vector2.zero;
    private int numberOfPlanets = 5;


    ///private float radius = 20f;

    */
    private static float minPlanetMass = 5f;
    private static float maxPlanetMass = 25f;
    private static float spawnDelay = 15f;
    private static float maxPlanetSize = 0.2f;

    /*

    private List<GameObject> planets = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnPlanets());

        sun.GetComponent<Rigidbody2D>().velocity = sunVelocity;
    }

    private IEnumerator SpawnPlanets()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            GameObject planet = Instantiate(planetPrefab);
            float x = 5;
            float y = 5;


            //Add below back when fixed the limits on random spawn location
            ///float x = -radius / 2f + Random.value * radius;
            ///float y = -radius / 2f + Random.value * radius;
            ///


            Vector2 position = new Vector2(x, y);
            planet.transform.position = position;
            float scale = 1f + Random.value * maxPlanetSize;
            planet.transform.localScale = new Vector2(scale, scale);

            float m = minPlanetMass + Random.value * (maxPlanetMass - minPlanetMass);
            Rigidbody2D prb = planet.GetComponent<Rigidbody2D>();
            prb.mass = m;

            //Static setting of velocity
            prb.velocity = new Vector2(3, 0);

            planets.Add(planet);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    */
    static public Gravity SpawnNewPlanet(Vector2 position, GameObject prefab)
    {
        GameObject planet = Instantiate(prefab);
        Gravity g = planet.GetComponent<Gravity>();
        g.rigidbody = planet.GetComponent<Rigidbody2D>();
        planet.transform.position = position;
        g.transform.position = position;
        g.rigidbody.position = position;

        float m = minPlanetMass + Random.value * (maxPlanetMass - minPlanetMass);
        g.rigidbody.mass = m;

        float scale = 1f + Random.value * maxPlanetSize;
        planet.transform.localScale = new Vector2(scale, scale);

        Vector2 toCenter = GameManager.Instance.deltaV(g); 

        g.startVelocity =
            (Random.value * 8 + 5) * (Quaternion.Euler(0,0,90) * 
            toCenter).normalized;

        Debug.Log(g.startVelocity+" "+toCenter);

        planet.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        return g;
    }

    private void Update()
    {

    }
}
