using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
This is the primary game controller.
Will likely be attached to an empty object in game and keep track of certain game variables.
*/

public class GameController : MonoBehaviour
{
    public static Dictionary<Vector2, TileBehavior> positionMap = new Dictionary<Vector2, TileBehavior>();
    public static List<Vector2> currentPath = new List<Vector2>();
    //public static HeroPathing pathing = new HeroPathing();

    // global list of tags to mitigate spelling errors, because referencing by string values is very bad no good
    public static readonly string PROJECTILE_TAG = "Projectile";
    public static readonly string ENEMY_TAG = "Enemy";

    // list of enemies currently in the scene
    public static List<GameObject> enemies = new List<GameObject>();

    // gameplay globals
    private static int credits = 100;
    private static int baseHealth = 100;
    public static int waveNumber = 0;
    public static int enemiesRemaining = 0;

    public static UnityEvent creditsChangeEvent = new UnityEvent();
    public static UnityEvent baseHealthChangeEvent = new UnityEvent();

    public static float towerSellMultiplier = 0.4f;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = togglePause();
        }
    }

    void OnGUI()
    {
        if (paused)
        {
            GUILayout.Label("Game is paused!");
            if (GUILayout.Button("Click me to unpause"))
                paused = togglePause();
            if (GUILayout.Button("Quit to Main")) {
                SceneManager.LoadScene("StartScene");
            }
        }
    }

    /* We don't want a class to be able to say GameController.credits = [variable], because we can't
    *  guarantee that they will also call the score change event. This method ensures that. Same applies
    *  for all global vars that require event listening for UI updates.
    */
    public static void ChangeCreditsBy(int creditsToAdd) {
        credits += creditsToAdd;
        creditsChangeEvent.Invoke();
    }

    public static int GetCurrentCredits() {
        return credits;
    }

    public static void ChangeBaseHealthBy(int healthToAdd) {
        baseHealth += healthToAdd;
        baseHealthChangeEvent.Invoke();
    }
    public static int GetCurrentBaseHealth() {
        return baseHealth;
    }

    bool togglePause() {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return false;
        }
        else {
            Time.timeScale = 0f;
            return true;
        }  
    }

    public bool isPaused() {
        return paused;
    }
}
