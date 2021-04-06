using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem 
{

   /// <summary>
   /// Saves a player objects properties to the local memory of the computer
   /// running the game
   /// </summary>
   /// <param name="playerMovement"></param>
    public static void SavePlayer(PlayerMovement playerMovement)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath ,"player.txt");
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
         
            PlayerData playerData = new PlayerData(playerMovement);
            formatter.Serialize(stream, playerData);
        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
        finally
        {
            stream.Close();
        }
    
    }

    public static void SaveTotalCherries(GlobalDataHolder globalDataHolder)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "totalcherries.txt");
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {

            TotalCherriesData totalCherriesData = new TotalCherriesData(globalDataHolder);
            formatter.Serialize(stream, totalCherriesData);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
        finally
        {
            stream.Close();
        }

    }

    public static void SaveLevelVisitData(GlobalDataHolder globalDataHolder)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "levelvisitdata.txt");
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {

            LevelVisitData levelVisitData = new LevelVisitData(globalDataHolder);
            formatter.Serialize(stream, levelVisitData);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
        finally
        {
            stream.Close();
        }
    }

    public static TotalCherriesData LoadTotalCherriesData()
    {
        string path = Path.Combine(Application.persistentDataPath, "totalcherries.txt");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                TotalCherriesData totalCherriesData = formatter.Deserialize(stream) as TotalCherriesData;
                stream.Close();
                return totalCherriesData;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static LevelVisitData LoadLevelVisitData()
    {
        string path = Path.Combine(Application.persistentDataPath, "levelvisitdata.txt");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                LevelVisitData levelVisitData = formatter.Deserialize(stream) as LevelVisitData;
                stream.Close();
                return levelVisitData;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Loads the player's data from local memory
    /// </summary>
    /// <returns></returns>
    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.txt");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return playerData;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
