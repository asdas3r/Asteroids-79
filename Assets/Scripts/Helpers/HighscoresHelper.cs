using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class HighscoresHelper
{
    public static List<Highscore> LoadHighscores()
    {
        string path = Application.persistentDataPath + "/highscores.dat";

        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        var tempHighscores = (List<Highscore>)binaryFormatter.Deserialize(stream);
        return tempHighscores;
    }

    public static List<Highscore> LoadDefaultHighscores()
    {
        List<Highscore> tempHighscores = new List<Highscore>()
        {
            new Highscore("AAA", 1000),
            new Highscore("AAA", 2000),
            new Highscore("AAA", 3000),
            new Highscore("AAA", 4000),
            new Highscore("AAA", 5000),
            new Highscore("AAA", 6000),
            new Highscore("AAA", 7000),
            new Highscore("AAA", 8000),
            new Highscore("AAA", 9000),
            new Highscore("AAA", 10000)
        };

        return tempHighscores.OrderByDescending(e => e.Score).ToList();
    }

    public static void SaveScores(List<Highscore> highscores)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscores.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, highscores);
        stream.Close();
    }
}
