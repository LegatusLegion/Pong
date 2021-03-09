using UnityEngine;
using System.IO;


namespace Pong.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform highscoresHolderTransfom = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;

        [Header("Test")]
        [SerializeField] ScoreboardEntryData testEntryData = new ScoreboardEntryData();

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";

        public void StartUp()
        {
            ScoreboardSaveData savedScores = GetSavedScores();
            
            UpdateUI(savedScores);
            SaveScores(savedScores);
        }

        [ContextMenu("AddTestEntry")]
        public void TestEntry()
        {
            AddEntry(testEntryData);
        }

        //Auslesen der gespeicherten Highscores aus der Datei.
        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath)){

                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }

         }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            bool scoreAdded = false;


            //Vergleich der Highscores und ersetzen, falls der highscore höher ist als der alte.
            for (int i = 0; i< savedScores.highscores.Count; i++)
            {
                if(scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if(!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
            {
                savedScores.highscores.Add(scoreboardEntryData);
            }

            if(savedScores.highscores.Count > maxScoreboardEntries)
            {
                savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
            }


            UpdateUI(savedScores);
            SaveScores(savedScores);

        }

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach(Transform child in highscoresHolderTransfom)
            {
                Destroy(child.gameObject);
            }

            foreach(ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransfom).GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
        }


        //Hier werden die Highscores in der json datei gespeichert, bzw übergeben zum speichern.
        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }

    }

}
