using System.Collections.Generic;
using UnityEngine;

public class AsteroidEntity : BaseGameEntity
{
    public AsteroidForm asteroidForm = AsteroidForm.None;

    public int levelNumber;

    protected override void OnStart()
    {
        base.OnStart();

        if (asteroidForm == AsteroidForm.None)
        {
            List<AsteroidRules> rulesList = GetAsteroidRulesList(asteroidForm, movementDirection);
            SetupMovement(rulesList[0].direction, rulesList[0].speed);
            asteroidForm = rulesList[0].form;
            transform.position = new Vector3(Random.Range(-gameManager.xBorder, gameManager.xBorder), Random.Range(-gameManager.yBorder, gameManager.yBorder), 0);
            transform.localScale = new Vector3(rulesList[0].scale, rulesList[0].scale, 1);
        }

        //entityRadius = 4f * transform.localScale.x;
        SetAsteroindsPoints();
    }

    protected override void Death()
    {
        var lastPosition = transform.position;
        var lastRotation = transform.rotation;

        base.Death();

        List<AsteroidRules> rulesList = GetAsteroidRulesList(asteroidForm, movementDirection);

        foreach (var rule in rulesList)
        {
            int number = Random.Range(0, objectsManager.asteroidsPrefabs.Length - 1);
            GameObject childAsteroidGO = Instantiate(objectsManager.asteroidsPrefabs[number], lastPosition, lastRotation);
            var asteroidEntity = childAsteroidGO.GetComponent<AsteroidEntity>();
            asteroidEntity.SetupMovement(rule.direction, rule.speed);
            asteroidEntity.gameObject.transform.localScale = new Vector3(rule.scale, rule.scale, 1);
            asteroidEntity.asteroidForm = rule.form;
        }

        float pitch = Random.Range(0.5f, 1.5f);
        if (rulesList.Count > 0)
        {
            audioManager.Play("asteroid_explosion", pitch);
        }
        else
        {
            audioManager.Play("asteroid_explosion_empty", pitch);
        }
    }

    private List<AsteroidRules> GetAsteroidRulesList(AsteroidForm currentForm, Vector3 parentDirection)
    {
        List<AsteroidRules> generatedList = new List<AsteroidRules>();

        int childAmount = 0;
        switch (currentForm)
        {
            case AsteroidForm.None:
                childAmount = 1;
                break;
            case AsteroidForm.Large:
                childAmount = 2;
                break;
            case AsteroidForm.Medium:
                childAmount = 2;
                break;
            case AsteroidForm.Small:
                childAmount = 0;
                break;
        }

        for (int i = 0; i < childAmount; i++)
        {
            generatedList.Add(AsteroidRulesBuilder.GetAsteroidRules(currentForm, parentDirection, levelNumber));
        }

        return generatedList;
    }

    private void SetAsteroindsPoints()
    {
        switch (asteroidForm)
        {
            case AsteroidForm.None:
                gamePoints = 0;
                break;
            case AsteroidForm.Large:
                gamePoints = 20;
                break;
            case AsteroidForm.Medium:
                gamePoints = 50;
                break;
            case AsteroidForm.Small:
                gamePoints = 100;
                break;
        }
    }

}
