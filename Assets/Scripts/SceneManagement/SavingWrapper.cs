using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;
using RPG.Core;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeTime = 0.2f;

        const string defaultSaveFile = "autosave";
        const string quickSaveFile = "quicksave";

        public event Action onSaveUpated;

        GameConsole gameConsole;
        string currentSaveFile;

        private void Start()
        {
            gameConsole = FindObjectOfType<GameConsole>();
            currentSaveFile = defaultSaveFile;
        }

        private IEnumerator LoadLastScene(string savedGame)
        {
            currentSaveFile = savedGame;
            yield return  GetComponent<SavingSystem>().LoadLastScene(savedGame);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeTime); ;
        }
    

        public void LoadSavedGame(string savedGame)
        {
            currentSaveFile = savedGame;
            StartCoroutine(LoadLastScene(savedGame));
            WriteToConsole("Game loaded from save: " + savedGame);
        }


        public void Load()
        {
            currentSaveFile = defaultSaveFile;
            GetComponent<SavingSystem>().Load(defaultSaveFile);

        }

        public void LoadSaveableEntity(SaveableEntity saveableEntity)
        {
            GetComponent<SavingSystem>().LoadSaveableEntity(currentSaveFile, saveableEntity);
        }

        public void Save(string fileName)
        {
            GetComponent<SavingSystem>().Save(fileName);
            currentSaveFile = fileName;
            WriteToConsole("Game saved: " + fileName);
            if (onSaveUpated!= null)
            {
                onSaveUpated();
            }
            
        }

        public void QuickSave()
        {
            GetComponent<SavingSystem>().Save(quickSaveFile);
            currentSaveFile = quickSaveFile;
            WriteToConsole("Game quicksaved: " + quickSaveFile);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }

        public void AutoSave()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }

        public void Delete(string filename)
        {
            GetComponent<SavingSystem>().Delete(filename);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }

        public void DeleteDefaultSaveFile()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }


        public Dictionary<string, DateTime> ListSaveFiles()
        {

            Dictionary<string, DateTime> allSaveFiles = GetComponent<SavingSystem>().ListAllSaveFiles();
            if (allSaveFiles.ContainsKey(defaultSaveFile))
            {
                allSaveFiles.Remove(defaultSaveFile);
            }

            return allSaveFiles;

        }


        private void WriteToConsole(string textToWrite)
        {
            if (gameConsole == null) return;
            gameConsole.AddNewLine(textToWrite);
        }
    }



}
