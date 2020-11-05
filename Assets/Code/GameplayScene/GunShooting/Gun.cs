using UnityEngine;

public class Gun : MonoBehaviour
{
    //Consts
    const int MaxBullets = 6;

    [SerializeField] GameObject pf_Spark;
    [SerializeField] LayerMask enemyLayer;

    //Object references
    CursorManager cursor;
    UIManager ui;

    //Properties
    int bulletsLeft;

    //Constructor
    void Start()
    {
        //Reference
        ui = UIManager.instance;
        cursor = CursorManager.instance;

        //Initialize
        bulletsLeft = MaxBullets;
        UpdateBulletDisplay();
    }

    public void Reload()
    {
        //Set the bullets to maxBullets allowed, then update the ammo count in HUD.
        bulletsLeft = MaxBullets;
        UpdateBulletDisplay();
    }

    public void ShootBullet ()
    {
        //Only allow shooting when there are ammo left.
        if (bulletsLeft > 0)
        {
            //Decrease ammo count.
            --bulletsLeft;
            
            //Cursor embellishment
            cursor.FlashFiringRing();

            //Ui display of ammo remaining
            UpdateBulletDisplay();

            //Spawn particle effect
            SpawnSpark();

            //Check for target hit
            CheckIfHitsTarget();
        }
    }

    #region Private methods
    void UpdateBulletDisplay() => ui.UpdateBulletCount(bulletsLeft, MaxBullets);

    void CheckIfHitsTarget ()
    {
        //Use a raycast shooting forward to see if we clicked on a target
        RaycastHit2D hit = Physics2D.Raycast(cursor.Position, 
            Vector3.forward, 100f,enemyLayer);
        //Debug.DrawRay(cursor.Position, Vector3.forward * 100f, Color.red, 10f);

        if (hit.collider != null)
        {
            cursor.FlashHitCross();
            GameplaySceneManager.instance.IncreaseKillCount();
            Object.Destroy(hit.collider.gameObject);
        }
    }

    void SpawnSpark ()
    {
        //Spawn a small particle effect to help the player know where they clicked.
        //Give the particle a random rotation each time to ma
        Instantiate(pf_Spark, 
            cursor.Position + Vector3.forward, 
            Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f))));
    }
    #endregion
}
