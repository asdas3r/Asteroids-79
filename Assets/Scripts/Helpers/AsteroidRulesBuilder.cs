using UnityEngine;

public static class AsteroidRulesBuilder
{
    public static AsteroidRules GetAsteroidRules(AsteroidForm currentForm, Vector3 parentDirection, int levelNumber)
    {
        AsteroidRules rules = new AsteroidRules();

        switch (currentForm)
        {
            case AsteroidForm.None:
                rules = GetRulesForNone(levelNumber);
                break;
            case AsteroidForm.Large:
                rules = GetRulesForLarge(parentDirection, levelNumber);
                break;
            case AsteroidForm.Medium:
                rules = GetRulesForMedium(parentDirection, levelNumber);
                break;
            case AsteroidForm.Small:
                rules = null;
                break;
        }

        return rules;
    }

    private static AsteroidRules GetRulesForMedium(Vector3 parentDirection, int levelNumber)
    {
        AsteroidRules rules = new AsteroidRules();

        rules.direction = Quaternion.Euler(0, 0, Random.Range(-90f, 90f)) * parentDirection;
        rules.form = AsteroidForm.Small;
        rules.scale = 0.3f;
        rules.speed = Random.Range(8, 15) * (1 + 0.1f * levelNumber);

        return rules;
    }

    private static AsteroidRules GetRulesForLarge(Vector3 parentDirection, int levelNumber)
    {
        AsteroidRules rules = new AsteroidRules();

        rules.direction = Quaternion.Euler(0, 0, Random.Range(-90f, 90f)) * parentDirection;
        rules.form = AsteroidForm.Medium;
        rules.scale = 0.6f;
        rules.speed = Random.Range(4, 7) * (1 + 0.1f * levelNumber);

        return rules;
    }

    private static AsteroidRules GetRulesForNone(int levelNumber)
    {
        AsteroidRules rules = new AsteroidRules();

        rules.direction = Quaternion.Euler(0, 0, Random.Range(0f, 360f)) * Vector3.up;
        rules.form = AsteroidForm.Large;
        rules.scale = 1;
        rules.speed = Random.Range(1, 3) * (1 + 0.1f * levelNumber);

        return rules;
    }
}
