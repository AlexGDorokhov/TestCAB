using System.Collections.Generic;
using System.IO;
using System.Threading;
using Defines.Constants;
using Defines.Enums;
using Events;
using Models;
using Newtonsoft.Json;
using Scripts;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class SavesController : BaseController
    {
        
        private ScoresData _scores = ScriptableObject.CreateInstance(typeof(ScoresData)) as ScoresData;
        private string jsonFilePath = Path.Combine(Application.persistentDataPath, "ScoresData.json");

        public override void Init()
        {

            base.Init();

            switch (ApplicationSettings.Instance.SaveMode)
            {
                
                case SaveModes.PlayerPrefs :
                    var savedScoresCount = PlayerPrefs.GetInt(PlayerPrefsKeys.ScoresCount);
                    for (int i = 0; i < savedScoresCount; i++)
                    {
                        var score = PlayerPrefs.GetInt(string.Concat(PlayerPrefsKeys.ScoresPrefix, i));
                        _scores.AddScore(score);
                    }
                    break;
                
                case SaveModes.File :
                    
                    if (File.Exists(jsonFilePath))
                    {
                        var jsonText = File.ReadAllText(jsonFilePath);
                        var list = JsonConvert.DeserializeObject<List<int>>(jsonText);
                        for (int i = 0; i < list.Count; i++)
                        {
                            _scores.AddScore(list[i]);
                        }
                    }
                    
                    break;
                
                
            }
            
        }

        public override void AddEventsHandlers()
        {
            base.AddEventsHandlers();
            
            AddListener<SaveScoreEvent>(SaveScore);
        }

        public override void RemoveEventsHandlers()
        {
            
            RemoveListener<SaveScoreEvent>();
            
            base.RemoveEventsHandlers();

        }

        private void SaveScore(SaveScoreEvent e)
        {
            _scores.AddScore(e.Points);

            switch (ApplicationSettings.Instance.SaveMode)
            {
                case SaveModes.PlayerPrefs :

                    var count = _scores.GetCount();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.ScoresCount, count + 1);
                    PlayerPrefs.SetInt(string.Concat(PlayerPrefsKeys.ScoresPrefix, count), e.Points);
                    _scores.AddScore(e.Points);
                    
                    break;
                
                
                case SaveModes.File :
                    
                    var json = JsonConvert.SerializeObject(_scores.GetScores());
                    var thread = new Thread(new ThreadStart(new ThreadedSave(json, jsonFilePath).Run));
                    thread.Start();
                    
                    /*
                    var saveJsonThread = new Thread(() => {
                        var json = JsonConvert.SerializeObject(_scores.GetScores());
                        File.WriteAllText(jsonFilePath, json);
                    });
                    saveJsonThread.Start();
                    */
                    
                    break;
            }
        }
        
        public List<int> GetScores()
        {
            return _scores.GetScores();
        }
        
    }
}