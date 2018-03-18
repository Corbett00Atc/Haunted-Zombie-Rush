using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] float energyConsumptionRate = .3f;
    [SerializeField] float energyTickAmount = 3;
    [SerializeField] float energy = 100;
    [SerializeField] Player player;
    bool energyTick = false;
    RawImage healthBarRawImage;

    // Use this for initialization
    void Start()
    {
        healthBarRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // halfed because of pic contains health and death
        float xValue = -(energy / 2f) * 0.01f - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }

    void OnEnable()
    {
        energy = 100;
        StartCoroutine(ConsumeEnergy());
    }

    public void GameStart()
    {
        energy = 100;
        ContinueTicking();
    }

    public void EnergyChange(float amount)
    {
        // prevent overcapping energy
        energy = energy + amount > 100 ? 100 : energy + amount;

        // player is dead
        if (energy <= 0) 
        {
            player.PlayerDeath(150, 30);
            GameManager.instance.GameIsOver();
        }
    }

    IEnumerator ConsumeEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(energyConsumptionRate);

            if (energyTick)
                EnergyChange(-energyTickAmount);
        }
    }

    public void ContinueTicking() { energyTick = true; }
    public void Stop() { energyTick = false; }
}
