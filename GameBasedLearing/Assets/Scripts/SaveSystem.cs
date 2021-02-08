using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem 
{
    public static void SavePlayer(PlayerMovement playerMovement,GlobalDataHolder globalDataHolder)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath ,"player.txt");
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
         
            PlayerData playerData = new PlayerData(playerMovement,globalDataHolder);
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
    public static PlayerData LoadPLayer()
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
