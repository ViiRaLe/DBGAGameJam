using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GuiltOMeter : MonoBehaviour
{
    private GameRulesTemplate rulesTemplate;


    private bool phase1 = true, phase2 = false;

    private float currentPrechargeTimer = 0f;
    private float currentChargeTimer = 0f;

    private EventSystem eventSystem;
    public UIManager ui;
    public Image prechargeSlider;
    public Image chargeSlider;

    private void Start()
    {
        rulesTemplate = GameManager.Instance.rulesTemplate;
        eventSystem = EventSystem.current;
    }


    private void Update()
    {
        if (pressed)
        {
            PhaseCalc();
        }
    }


    public void UnClick()
    {
        pressed = false;

        ResetVar();
        
        eventSystem.SetSelectedGameObject(null);
    }


    bool pressed = false;
    bool goingToMax = true;
    public void OnHold()
    {
        GameManager.Instance.audioManager.PlaySound(1);
        pressed = true;
    }

    public void OnRelease()
    {
        if (pressed)
        {
            GameManager.Instance.audioManager.PlaySound(2);

            if (!phase1 && phase2)
            {
                
                GameManager.Instance.currentCriminal.guilty = true;
                GameManager.Instance.currentCriminal.currentPoints = currentChargeTimer;

                ui.Guilty();
            }            
        }

        eventSystem.SetSelectedGameObject(null);

        ResetVar();        
    }

    void ResetVar()
    {
        currentPrechargeTimer = 0f;
        currentChargeTimer = 0f;
        goingToMax = true;
        phase1 = true;
        phase2 = false;
        prechargeSlider.material.SetFloat("_Threshold", 0f);
        chargeSlider.material.SetFloat("_Threshold", 0f);
        pressed = false;        
    }

    void PhaseCalc()
    {
        if (phase1 && !phase2)
        {
            if (currentPrechargeTimer < rulesTemplate.guiltoMeterPrecharge)
            {                
                currentPrechargeTimer += Time.deltaTime;

                prechargeSlider.material.SetFloat("_Threshold", currentPrechargeTimer / rulesTemplate.guiltoMeterPrecharge);
            }
            else
            {
                phase1 = false;
                phase2 = true;
            }
        }
        else if (phase2 && !phase1)
        {
            if (currentChargeTimer < rulesTemplate.guiltoMeterMaxSentence && goingToMax)
            {
                currentChargeTimer += rulesTemplate.guiltMeterPerTick * Time.deltaTime * rulesTemplate.guiltMeterMult[CurrentMult()];

                chargeSlider.material.SetFloat("_Threshold", currentChargeTimer / rulesTemplate.guiltoMeterMaxSentence);

                if (currentChargeTimer >= rulesTemplate.guiltoMeterMaxSentence)
                {                    
                    goingToMax = false;
                }
            }
            else if (currentChargeTimer > 0f && !goingToMax)
            {
                currentChargeTimer -= rulesTemplate.guiltMeterPerTick * Time.deltaTime * rulesTemplate.guiltMeterMult[CurrentMult()];

                chargeSlider.material.SetFloat("_Threshold", currentChargeTimer / rulesTemplate.guiltoMeterMaxSentence);

                if (currentChargeTimer <= 0f)
                {                    
                    goingToMax = true;
                }
            }
        }
    }

    int CurrentMult()
    {
        var i = 0;
        var x = rulesTemplate.guiltoMeterMaxSentence / 4;

        if (currentChargeTimer >= 0f && currentChargeTimer < x)
        {
            i = 0;
        }
        else if (currentChargeTimer >= x && currentChargeTimer < x * 2)
        {
            i = 1;
        }
        else if (currentChargeTimer >= x * 2 && currentChargeTimer < x * 3)
        {
            i = 2;
        }
        else if (currentChargeTimer >= x * 3 && currentChargeTimer <= x * 4)
        {
            i = 3;
        }

        return i;
    }

    /*int ProviamoMult()
    {
        int i = 0;

        var b = rulesTemplate.guiltoMeterMaxSentence / rulesTemplate.guiltoMeterPerTick.Length;

        if (currentChargeTimer >= 0f && currentChargeTimer < b)
        {
            i =
        }

        return i;
    }*/
}
