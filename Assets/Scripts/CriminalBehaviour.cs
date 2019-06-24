using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;

public class CriminalBehaviour : MonoBehaviour
{
    [HideInInspector] public CriminalTemplate criminalTemplate;
    
    public bool judged = false;
    
    [HideInInspector] public float verdictCurrentTimer = 0f;

    private bool firstTime = true;

    [HideInInspector] public bool guilty = false;
    [HideInInspector] public float currentPoints = 0f;

    public SkeletonData sk;
    private void Start()
    {
        //GetComponent<Image>().sprite = criminalTemplate.sprite;
    }

    private void Update()
    {
        if (judged && firstTime)
        {
            firstTime = false;
            oneTimer = false;

            GameManager.Instance.GetVerdict(currentPoints, guilty);

            if (guilty)
            {
                GameManager.Instance.gavelAnim.Play();

                GameManager.Instance.slam.SetActive(true);
                GameManager.Instance.slam.GetComponent<UnityEngine.Animation>().Play();

                var na = 0;

                if (currentPoints < GameManager.Instance.rulesTemplate.guiltoMeterMaxSentence / 3) na = 0;
                else if (currentPoints >= GameManager.Instance.rulesTemplate.guiltoMeterMaxSentence / 3 && currentPoints < GameManager.Instance.rulesTemplate.guiltoMeterMaxSentence / 2) na = 1;
                else if (currentPoints >= GameManager.Instance.rulesTemplate.guiltoMeterMaxSentence / 2 && currentPoints < GameManager.Instance.rulesTemplate.guiltoMeterMaxSentence) na = 2;

                GameManager.Instance.guiltiness.GetComponent<Image>().sprite = GameManager.Instance.guilts[na];
                GameManager.Instance.guiltiness.SetActive(true);
                

                if (guiltyCO == null) guiltyCO = StartCoroutine(GuiltyCO());


                if (slamCO == null) slamCO = StartCoroutine(SlamCO());

                if (vibrateCO == null) vibrateCO = StartCoroutine(VibrateCO());
            }

            GameManager.Instance.UpdateCriminalTimer(0f);

            if (respawnCO == null) respawnCO = StartCoroutine(RespawnCO());
        }

        if (verdictCurrentTimer < GameManager.Instance.rulesTemplate.verdictTimer)
        {
            verdictCurrentTimer += Time.deltaTime;

            GameManager.Instance.UpdateCriminalTimer( 1 - (verdictCurrentTimer / GameManager.Instance.rulesTemplate.verdictTimer));

            if (verdictCurrentTimer > GameManager.Instance.rulesTemplate.verdictTimer * 0.5f && !oneTimer)
            {
                oneTimer = true;
                GameManager.Instance.audioManager.PlaySound(5);
            }
        }
        else
        {
            judged = true;
            GameManager.Instance.UnClick();
        }
    }
    bool oneTimer = false;

    Coroutine slamCO = null;
    IEnumerator SlamCO()
    {
        
        yield return new WaitForSeconds(.5f);
        
        GameManager.Instance.slam.SetActive(false);
        
    }

    Coroutine guiltyCO = null;
    IEnumerator GuiltyCO()
    {

        yield return new WaitForSeconds(.5f);

        GameManager.Instance.guiltiness.SetActive(false);

    }

    Coroutine vibrateCO = null;
    IEnumerator VibrateCO()
    {
        yield return new WaitForSeconds(GameManager.Instance.rulesTemplate.vibrationDelay);
        Handheld.Vibrate();
        GameManager.Instance.audioManager.PlaySound(3);
    }
    

    Coroutine respawnCO = null;
    IEnumerator RespawnCO()
    {
        GameManager.Instance.guiltMeter.gameObject.GetComponent<Button>().interactable = false;
        GameManager.Instance.guiltMeter.gameObject.GetComponent<Image>().raycastTarget = false;
        GameManager.Instance.notGuilty.interactable = false;

        yield return new WaitForSeconds(GameManager.Instance.rulesTemplate.respawnDelay);

        GameManager.Instance.SpawnCriminal();

        respawnCO = null;

        GameManager.Instance.UnClick();
        
        GameManager.Instance.notGuilty.interactable = true;
        GameManager.Instance.guiltMeter.gameObject.GetComponent<Button>().interactable = true;
        GameManager.Instance.guiltMeter.gameObject.GetComponent<Image>().raycastTarget = true;

        Destroy(gameObject);
    }
}
