using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {


    public GameController gameController;
    public GameObject mortarBundle;
    public WindSystem windSystem;
    public Camera scenicCamera;
    public AudioListener scenicAudio;
    public List<Camera> cameraList;
    public List<AudioListener> audioList;
    public List<GameObject> mortarSpawns;

    
    private AudioSource[] audioSourceList;
    private GameObject activeProjectile;
    private GameObject[] ragdollsList;
    private GameObject[] displaceablesList;
    private ConfigurableJoint[] jointsList;
    private List<ConfigurableJoint> jointsCounted;
    private Camera projectileCamera;
    private AudioListener projectileAudio;
    private RoundScores roundScores;
    private Vector3 projectileImpactPosition;
    private bool slowMotion = false;
    private bool roundScoringComplete = false;
    private bool newHighscore = false;
    private bool currentlyScoring = false;
    private float projectileHeight;
    private float projectileDistance;
    private int totalScore;
    private int currentHighscore;
    private string currentHighscoreName;
    private int roundBrokenJoints = 0;
    private float roundDisplacement = 0;
    private int activeCameraIndex = 0;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        currentHighscore = PlayerPrefs.GetInt("Highscore");
        currentHighscoreName = PlayerPrefs.GetString("HighscoreName");
       
        jointsCounted = new List<ConfigurableJoint>();
        audioSourceList = GameObject.FindObjectsOfType<AudioSource>();
        
        FindJoints();
        FindRagdolls();
        FindDisplaceables();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeProjectile != null)
        {
            TrackProjectile();
        }
    }

    // ----------- Scoring System ------------

    private void FindJoints()
    {
        jointsList = GameObject.FindObjectsOfType<ConfigurableJoint>();

    }

    private void FindRagdolls()
    {
        ragdollsList = GameObject.FindGameObjectsWithTag("RagdollMaster");
    }


    private void FindDisplaceables()
    {
        displaceablesList = GameObject.FindGameObjectsWithTag("Displaceable");
    }


    private void StartScoring(int delay)
    {
        StartCoroutine(CountChanges(delay));
        currentlyScoring = true;
    }


    IEnumerator CountChanges(int delay)
    {
        roundDisplacement = 0;
        roundBrokenJoints = 0;
        yield return new WaitForSeconds(delay);

        // All joints that can break.
        foreach (ConfigurableJoint cj in jointsList)
        {
            if (cj == null && !jointsCounted.Contains(cj))
            {
                jointsCounted.Add(cj);
                roundBrokenJoints++;
            }
        }

        // All objects that have animations.
        foreach (GameObject ragdoll in ragdollsList)
        {
            
            
            float displacement = Vector3.Distance(ragdoll.GetComponent<RagdollMaster>().GetTrackedObjectPosition(),
                ragdoll.GetComponent<RagdollMaster>().GetOriginPosition());
            if (displacement > 2f)
            {
                roundDisplacement += displacement * ragdoll.GetComponent<RagdollMaster>().GetScoringFactor();
            }
           
            ragdoll.GetComponent<RagdollMaster>().SetNewOriginPosition(ragdoll.transform.position);
        }


        // All resting objects that can be moved.
        foreach (GameObject displaceable in displaceablesList)
        {
            
            roundDisplacement += Vector3.Distance(displaceable.GetComponent<Displaceable>().GetTrackedObjectPosition(),
                displaceable.GetComponent<Displaceable>().GetOriginPosition())
                * displaceable.GetComponent<Displaceable>().GetScoringFactor();
            displaceable.GetComponent<Displaceable>().SetNewOriginPosition(displaceable.transform.position);
        }
        roundScores = new RoundScores(roundBrokenJoints, roundDisplacement, projectileHeight, projectileDistance);
        
      
        totalScore += roundScores.totalRoundScore;
        if (totalScore > currentHighscore)
        {
            newHighscore = true;
        }
        roundScoringComplete = true;
        currentlyScoring = false;
    }


    // ----------- Projectiles -------------

    private void TrackProjectile()
    {

        if (activeProjectile.GetComponent<Projectile>().HasImpacted())
        {
            projectileImpactPosition = activeProjectile.transform.position;
            StartScoring(5);
            RemoveProjectile();
           

        } else
        {
            if (projectileHeight < activeProjectile.transform.position.y)
            {
                projectileHeight = activeProjectile.transform.position.y;
            }

            Vector3 targetPosition = activeProjectile.transform.position;
            Vector3 originPosition = mortarBundle.transform.position;
            targetPosition.y = 0f;
            originPosition.y = 0f;
            projectileDistance = Vector3.Distance(targetPosition, originPosition);
        }
    }


    public void AddProjectile(GameObject projectile)
    {
        projectileHeight = 0f;
        projectileDistance = 0f;
        activeProjectile = projectile;
        AddProjectileCamera(projectile);
        
        
        roundScores = null;
    }

    private void RemoveProjectile()
    {
        
        
        if (projectileCamera.enabled)
        {
            ToggleCamera();
        }
        cameraList.Remove(projectileCamera);
        audioList.Remove(projectileAudio);
        Destroy(activeProjectile);
        Destroy(projectileCamera.gameObject);
    }


    // ----------- Cameras -------------
    public void ToggleCamera()
    {
        for (int i = 0; i < cameraList.Count; i++)
        {
            if (cameraList[i].enabled)
            {

                if (i == cameraList.Count - 1)
                {
                    
                    cameraList[i].enabled = false;
                    cameraList[0].enabled = true;
                    audioList[i].enabled = false;
                    audioList[0].enabled = true;
                    activeCameraIndex = 0;
                }
                else
                {
                    cameraList[i].enabled = false;
                    cameraList[i + 1].enabled = true;
                    audioList[i].enabled = false;
                    audioList[i + 1].enabled = true;
                    activeCameraIndex = i + 1;
                }
                break;
            }
        }
    }

    private void AddProjectileCamera(GameObject projectile)
    {
        projectileCamera = projectile.GetComponentInChildren<Camera>();
        projectileCamera.enabled = false;
        projectileAudio = projectile.GetComponentInChildren<AudioListener>();
        cameraList.Add(projectileCamera);
        audioList.Add(projectileAudio);
        projectile.GetComponent<Projectile>().DetachCamera();
    }

    public void ActivateScenicCamera()
    {
        cameraList[activeCameraIndex].enabled = false;
        audioList[activeCameraIndex].enabled = false;
        scenicCamera.enabled = true;
        scenicAudio.enabled = true;
        scenicCamera.GetComponent<ScenicCamera>().StartTracking(projectileImpactPosition);
    }

    public void DeactivateScenicCamera()
    {
        scenicCamera.GetComponent<ScenicCamera>().StopTracking();
        roundScoringComplete = false;
        scenicCamera.enabled = false;
        scenicAudio.enabled = false;
        cameraList[0].enabled = true;
        audioList[0].enabled = true;
        activeCameraIndex = 0;
    }


    // --------------- Spawn ---------------

    public void ChangeSpawnPoint(int spawnNumber)
    {
        mortarBundle.transform.position = mortarSpawns[spawnNumber - 1].transform.position;
        mortarBundle.transform.rotation = Quaternion.identity;
    }


    // --------------- Slow-Motion ---------------------
    public void ActivateSlowMotion()
    {
        slowMotion = true;

        Time.timeScale = 0.2f;
        audioSourceList = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource aSource in audioSourceList)
        {
            aSource.pitch = 0.2f;
        }

    }

    public void DeactivateSlowMotion()
    {
        slowMotion = false;

        Time.timeScale = 1f;
        audioSourceList = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource aSource in audioSourceList)
        {
            aSource.pitch = 1f;
        }

    }


    // --------------- Information passing --------------
    public int GetTotalScore()
    {
        return totalScore;
    }

    public float GetProjectileHeight()
    {
        return projectileHeight;
    }

    public float GetProjectileDistance()
    {
        return projectileDistance;
    }

    public int GetActiveCameraIndex()
    {
        return activeCameraIndex;
    }

    public bool HasActiveProjectile()
    {
        return (activeProjectile != null);
    }

    public bool HasFinishedScoring()
    {
        return roundScoringComplete;
    }

    public RoundScores GetRoundScores()
    {
        return roundScores;
    }

    public Vector3 GetWindDirection()
    {
        return windSystem.GetWindDirection();
    }

    public int GetCurrentHighscore()
    {
        return currentHighscore;
    }

    public string GetHighscoreName()
    {
        return currentHighscoreName;
    }

    public bool IsNewHighscore()
    {
        return newHighscore;
    }

    public bool IsSlowMotion()
    {
        return slowMotion;
    }

    public bool IsCurrentlyScoring()
    {
        return currentlyScoring;
    }
}
