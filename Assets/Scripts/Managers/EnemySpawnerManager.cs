using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerManager : MonoBehaviour
{
    private float[] probabilities = new float[] {1f, 0f, 0f}; // normal zombie, fast zombie, tank zombie
    private EnemyFactory.eEnemy[] zombies = new EnemyFactory.eEnemy[] {EnemyFactory.eEnemy.Zombie, EnemyFactory.eEnemy.FastZombie, EnemyFactory.eEnemy.Tank};
    private int zombiesToCreate = 1;
    private int zombiesToAddNextWave = 1;
    private int currentWave = 1;
    private int scoreToNextWave = 1;
    [SerializeField] private Text waveText;
    [SerializeField] private Text bossText;
    [SerializeField] private List<Material> skyboxes;
    [SerializeField] private Light directionalLight;
    [SerializeField] private Light characterFlashlight;
    [SerializeField] private ParticleSystem rain;
    private SpawnPoint[] _spawnPoints;
    private int currentAngle = 30;
    private float timeBetweenWaves = 3.0f;
    private float finishedWaveTime = 0f;
    private float startWaveTime = 0f;
    private float maxWaveTime = 5f;
    private bool finishedWave = false;

    void Start()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>();
        directionalLight.transform.rotation = Quaternion.Euler(currentAngle, 0, 0);
        characterFlashlight.intensity = 0;
        startWaveTime = Time.time;
        rain.Stop();
        rain.GetComponent<AudioSource>().mute = true;
    }

    void Update()
    {
        int currentScore = GlobalData.instance.Score;
        float timeInWave = Time.time - startWaveTime;
        if((currentScore == scoreToNextWave  || (timeInWave > (maxWaveTime * zombiesToCreate))) && !finishedWave) { // mato a todos los zombies de la wave
            finishedWaveTime = Time.time;
            finishedWave = true;
        }
        if ((finishedWaveTime + timeBetweenWaves <= Time.time) && finishedWave){
            finishedWave = false;
            
            zombiesToCreate += zombiesToAddNextWave;
            // zombiesToAddNextWave++;
            Spawn();
            startWaveTime = Time.time;
            currentWave += 1;
            scoreToNextWave = currentScore + zombiesToCreate;
            bossText.text = "";
            if(currentWave % 5 == 0){
                if(probabilities[0] > 0f){
                    probabilities[0] -= 0.1f;
                    probabilities[1] += 0.075f;
                    probabilities[2] += 0.025f;
                } else if (probabilities[1] > 0f){
                    probabilities[1] -= 0.075f;
                    probabilities[2] += 0.075f;
                }
                SpawnBoss();
                bossText.text = "Boss";
                scoreToNextWave+=5;
            }
            
            waveText.text = $"Wave {currentWave}";
            currentAngle = (currentAngle + 30) % 360;
            RenderSettings.skybox = getSkybox(Random.value > 0.5);
            directionalLight.transform.rotation = Quaternion.Euler(currentAngle, 0, 0);
            if(currentAngle > 30 && currentAngle < 150 ){
                characterFlashlight.intensity = 0;
            } else {
                characterFlashlight.intensity = 2;
            }
        }
    }

    private void Spawn()
    {   
        for(int i=0; i<zombiesToCreate; i++){
            int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
            EnemyFactory.createEnemy(chooseZombie(Random.value), _spawnPoints[spawnPointIndex].transform);
        }
    }

    private void SpawnBoss()
    {   
        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
        EnemyFactory.createEnemy(EnemyFactory.eEnemy.Boss, _spawnPoints[spawnPointIndex].transform);
        EventManager.instance.ChangeBackgroundMusic(true);
    }

    private EnemyFactory.eEnemy chooseZombie(float rand){
        float accum = probabilities[0];
        for(int i=0; i<probabilities.Length; i++){
            if(rand < accum){
                return zombies[i];
            }
            accum += probabilities[i+1];
        }
        return zombies[probabilities.Length - 1];
    }

    private Material getSkybox(bool rainy){
        
        if (rainy) {
            var emission = rain.emission;
            if(rain.isPlaying){
                emission.rateOverTime = 1000f;
            } else {
                emission.rateOverTime = 100f;
                rain.Play();
                rain.GetComponent<AudioSource>().mute = false;
            }
            
            return skyboxes[5];
        } else {
            rain.Stop();
            rain.GetComponent<AudioSource>().mute = true;
        }
        switch (currentAngle)
        {
            case 30: case 60: return skyboxes[0];
            case 90: return skyboxes[1];
            case 120: return skyboxes[2];
            case 150: return skyboxes[3];
            default: return skyboxes[4];
        }
    }
}
