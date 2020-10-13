using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad 
{
    public static ClassSave save;
    public delegate void OnSaveSettingFun();
    public static event  OnSaveSettingFun OnSaveSetting;

    public static ClassSavePoints savePoints;
    public static ClassSaveCounts saveCounts;
    public static void Save()
    {   
        if (save != null)
        {
            Debug.Log("SAVE   " + Application.persistentDataPath + "/savedGames.gd");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/savedGames.gd");
            bf.Serialize(fs, save);
            fs.Close();
        }
        else
        {
            save = new ClassSave();
            if (save.id == -1)
                  UI_Raiting.GetID(false);
            Save();
        }
        OnSaveSetting?.Invoke();
    }
    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath +"/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            save = (ClassSave)bf.Deserialize(fs);
            fs.Close();
        }
        else
        {
            Save();
        }
    }
    public static void SavePoints()
    {
        if (savePoints != null)
        {
            Debug.Log("SAVE   " + Application.persistentDataPath + "/savedPoints.dat");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/savedPoints.dat");
            bf.Serialize(fs, savePoints);
            fs.Close();
        }
        else
        {
            savePoints = new ClassSavePoints();
            SavePoints();   
        }
    }
    public static void LoadPoints()
    {
        if (File.Exists(Application.persistentDataPath + "/savedPoints.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/savedPoints.dat", FileMode.Open);
            savePoints = (ClassSavePoints)bf.Deserialize(fs);
            fs.Close();
        }
        else
        {
            SavePoints();
        }
    }
    public static int GetPointsOnLevel()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                return savePoints.points1;
            case 2:
                return savePoints.points2;
            case 3:
                return savePoints.points3;
        }
        return 0;
    }
    public static void SaveEnergies()
    {
        ClassSaveEnergies energies = new ClassSaveEnergies(EnergyBar.Energies);
        //Debug.Log("SAVE   " + Application.persistentDataPath + "/private.gd");
        if (savePoints.points1 == 11)
        {
            energies.energies = 5;
            //Debug.Log("Save.Points - " + save.points+"   energies - "+EnergyBar.Energies); 
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/private.gd");
        bf.Serialize(fs, energies);
        fs.Close();
        
        
    }
    public static void LoadEnergies()
    {
        if (File.Exists(Application.persistentDataPath + "/private.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/private.gd", FileMode.Open);
            ClassSaveEnergies energies = (ClassSaveEnergies)bf.Deserialize(fs);
            CalculationMinutes(energies.time, energies.energies);
            fs.Close();
        }
        else
        {
            SaveEnergies();
            LoadEnergies();
        }
    }
    public static void CalculationMinutes(System.DateTime time, int e)
    {
        System.TimeSpan timeSpan = System.DateTime.Now.Subtract(time);

        if(EnergyBar.MinutesRestore * 5 < timeSpan.TotalMinutes && e != 5)
        { 
            EnergyBar.Energies = 5;
        }
        else 
        {
            int returnValue = (int)(timeSpan.TotalMinutes / EnergyBar.MinutesRestore);
            EnergyBar.nonMinutes =(float)timeSpan.TotalMinutes % EnergyBar.MinutesRestore;
            EnergyBar.Energies = e + returnValue;
            if (EnergyBar.Energies > 5)
                EnergyBar.Energies = 5;
        }
    }
    public static void SaveCountPlayedGames()
    {
        if (saveCounts != null)
        {
            Debug.Log("SAVE   " + Application.persistentDataPath + "/savedCounts.dat");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/savedCounts.dat");
            bf.Serialize(fs, saveCounts);
            fs.Close();
            //Debug.Log($"Counts save:  c1 = {saveCounts.counts1}, c2 = {saveCounts.counts2}, c3 = {saveCounts.counts3}");
        }
        else
        {
            saveCounts = new ClassSaveCounts();
            SaveCountPlayedGames();
        }
    }
    public static void LoadCountPlayedGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savedCounts.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/savedCounts.dat", FileMode.Open);
            saveCounts = (ClassSaveCounts)bf.Deserialize(fs);
            fs.Close();
        }
        else
        {
            SaveCountPlayedGames();
        }
    }
    public static void UpdateGameValueSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            try
            {
                save = (ClassSave)formatter.Deserialize(fs);
                Debug.Log("Updated version");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                fs.Position = 0;
                var save1 = (ClassSaveOld)formatter.Deserialize(fs);
                save = new ClassSave(0.5f, 0.5f, save1.nickName);
                save.id = save1.id;
                save.idSceneLastOpen = save1.idSceneLastOpen;
                Save();
                Debug.Log("Update settings properties");
            }
            fs.Close();
        }
    }
}
