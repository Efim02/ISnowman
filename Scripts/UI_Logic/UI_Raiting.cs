using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dropbox.Api;
using System.Threading.Tasks;
using System.Linq;
public class UI_Raiting : MonoBehaviour
{
    [SerializeField]
    Text T_NickName;
    [SerializeField]
    Text T_maxScore;
    [SerializeField]
    GameObject Image_UpdatingArrows;
    [SerializeField]
    GameObject Text_ForIfNotInternet;
                                    //пути до данных :  "/usersScoresTable1.dat"      "/PossibleForUploadValue1.dat"
                                     //                  "/usersScoresTable2.dat"    "/PossibleForUploadValue2.dat"
    [SerializeField]
    GameObject Object_UI_ScrollRaiting;
    UI_ScrollRaiting scrollRaiting;

    [SerializeField]
    int NumberLevelForFiles;

    string pathForTableFile;
    string pathForPossibleForUploadValue;

    static DropboxClient client = new DropboxClient("PkxF0kO2J8AAAAAAAAAAK7GLTND9JkDUk8uLBBcXgafgcqdVViV9jNTfisPcw1Og");

    List<ClassSaveDataUser> users;  

    bool isUpload=false;
    static bool isUploadID=false;
    bool isUpdating=false;
    private void Awake()
    {
        pathForTableFile = "/usersScoresTable" + NumberLevelForFiles.ToString() + ".dat";
        pathForPossibleForUploadValue = "/PossibleForUploadValue" + NumberLevelForFiles.ToString() + ".dat";
        //ResetFilesOnDropbox();
        if (File.Exists(Application.persistentDataPath + pathForTableFile))
        {
            using (var fs = File.Open(Application.persistentDataPath + pathForTableFile, FileMode.Open))
            {
                if (fs.Length > 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    users = (List<ClassSaveDataUser>)formatter.Deserialize(fs);
                }
                else
                {
                    users = new List<ClassSaveDataUser>();
                }
            }
        }
    }
    void Start()
    {
       scrollRaiting = Object_UI_ScrollRaiting.GetComponent<UI_ScrollRaiting>();
        SaveLoad.OnSaveSetting += ChangeValues;
        CanvasMod.charMov.eventDeath += ChangeValues;
        ChangeValues();
        if(users != null)
             scrollRaiting.UpdateList(users);

    }
    private void OnDestroy()
    {
        SaveLoad.OnSaveSetting -= ChangeValues;
        CanvasMod.charMov.eventDeath -= ChangeValues;
    }
    void Update()
    {
        if (isUpload)
            DownloadTableUsers();
        if (isUploadID)
            GetID(isUploadID);
    }
    void ChangeValues()
    {
        //Debug.Log("Chsange");
        T_NickName.text = SaveLoad.save.nickName;
        T_maxScore.text = (SaveLoad.GetPointsOnLevel().ToString());
    }
    public void B_RefreshTableRaiting()   //логическое условие что бы кликать нельзя было пока обновляется
    {
        if(!isUpdating)
            DownloadFiles();  


                            /* var fs = File.Create(Application.persistentDataPath + "/PossibleForUploadValue.dat");
                            BinaryFormatter formatter = new BinaryFormatter();
                            int i =(int) SaveLoad.save.points;
                            formatter.Serialize(fs, i);
                            fs.Close();
                            fs = File.Open(Application.persistentDataPath + "/PossibleForUploadValue.dat", FileMode.Open);
                            Task.Run(() => Upload("/ISnowmanStorage/PossibleForUploadValue.dat", fs));*/
    }
    public static async Task Download(string dropboxPath, Stream stream)
    {
        Debug.Log("Start task the Download");
        var args = new Dropbox.Api.Files.DownloadArg(dropboxPath);
        using (var response = await client.Files.DownloadAsync(args))
        {
            response.GetContentAsStreamAsync().Result.CopyTo(stream);
            Debug.Log("Finish task the Download");
        }
    }
    public static async Task Upload(string dropboxPath, Stream bodyFile) 
    {
       // Debug.Log("Start task the Upload");
        await client.Files.UploadAsync(dropboxPath, Dropbox.Api.Files.WriteMode.Overwrite.Instance , body: bodyFile);
       // Debug.Log("Finish task the Upload");
    }
    void DownloadFiles()
    {
        bool b1 = false, b2 = false ;
        isUpdating = true;
        Image_UpdatingArrows.SetActive(true);
        scrollRaiting.ClearList();
        Text_ForIfNotInternet.SetActive(false);

        var stream = File.Create(Application.persistentDataPath + pathForPossibleForUploadValue);
        Task task3 = Task.Run(() => Download("/ISnowmanStorage"+pathForPossibleForUploadValue, stream));
        task3.ContinueWith((task4) =>
        { 
            stream.Close();  b1 = true;
            if (b1 == true && b2 == true)
            {
                isUpload = true;
                //Debug.Log("Any downloads finished");
            }
        });

        var fs1 = File.Create(Application.persistentDataPath + pathForTableFile);
        Task task1 = Task.Run(() => Download("/ISnowmanStorage"+pathForTableFile, fs1));
        task1.ContinueWith((task4) => 
        {
            fs1.Close(); b2 = true;
            if (b1 == true && b2 == true)
            {
                isUpload = true;
               // Debug.Log("Any downloads finished");
            }
        });
        
    }

    void DownloadTableUsers()    
    {
        isUpload = false;
        //Debug.Log("Started method the rewrite dates");
        BinaryFormatter formatter = new BinaryFormatter();
        var stream = File.Open(Application.persistentDataPath + pathForTableFile, FileMode.Open);
        //if(stream.Length > 0 )
        if(SaveLoad.save.id == -1)
        {
            GetID(false);
            Image_UpdatingArrows.SetActive(false);
            isUpdating = false;
            if (stream.Length != 0)
            {
                stream.Close();
                return;
            }
        }
        if(stream.Length == 0)
        {
            stream.Close();
            Image_UpdatingArrows.SetActive(false);
            Text_ForIfNotInternet.SetActive(true);
            isUpdating = false;
            return;
        }
        users = (List<ClassSaveDataUser>)formatter.Deserialize(stream);
        stream.Close();
        var fs = File.Open(Application.persistentDataPath + pathForPossibleForUploadValue, FileMode.Open);
        int minvalue = (int)formatter.Deserialize(fs);
        fs.Close();
        bool bLocal = false;
        foreach(var u in users)
        {
            if(u.id == SaveLoad.save.id)
            {
                u.nickName = T_NickName.text;
                u.score = int.Parse(T_maxScore.text);
                bLocal = true;
                //Debug.Log("1 user have with id in list");
            }
        }
        if (users.Count < 10 && !bLocal)
        {
            users.Add(new ClassSaveDataUser(T_NickName.text, int.Parse(T_maxScore.text), SaveLoad.save.id));
            //Debug.Log("ADD -> users.Count<10");
        }
        else
        {
            if (minvalue < SaveLoad.GetPointsOnLevel() && !bLocal)   //скачиваем и меняем файл   
            {

                foreach(var u in users)
                {
                    if (u.score == minvalue)
                    {
                        u.nickName = T_NickName.text;
                        u.score = int.Parse(T_maxScore.text);
                        u.id = SaveLoad.save.id;
                        //Debug.Log("Now the not working");
                        break;  
                    }
                }
            }
        }

        IEnumerable<ClassSaveDataUser> listUsers = from u in users orderby u.score descending select u;
        List<ClassSaveDataUser> listLocal = new List<ClassSaveDataUser>();
        foreach(var u in listUsers)
        {
            listLocal.Add(u);
        }
        users = listLocal;
        /*foreach (var u in users)
        {
            Debug.Log(u.nickName + " - Nickname, " + u.id + "  - id,  " + u.score + "   u.score");
        }*/

        //Debug.Log("COUNT USERS -- " + users.Count());

        bool isUpd1 = false, isUpd2 = false;

        var fs1 = File.Create(Application.persistentDataPath + pathForTableFile);
        formatter.Serialize(fs1, users);
        fs1.Close();
        fs1 = File.Open(Application.persistentDataPath + pathForTableFile, FileMode.Open);
        Task task1 = Task.Run(() => Upload("/ISnowmanStorage"+pathForTableFile, fs1));
        task1.ContinueWith((task999) => { fs1.Close(); isUpd1 = true; if (isUpd1 == true && isUpd2 == true) isUpdating = false; });

        //Debug.Log("Started method the TransferListUsers");
        int minValue = 0;
        if (users.Count != 0)
            minValue = users.Last().score;

        var fs2 = File.Create(Application.persistentDataPath + pathForPossibleForUploadValue);    //меняем минимальное значение 
        formatter.Serialize(fs2, minValue); fs2.Close();
        fs2 = File.Open(Application.persistentDataPath + pathForPossibleForUploadValue, FileMode.Open);
        Task task2 = Task.Run(() => Upload("/ISnowmanStorage"+pathForPossibleForUploadValue, fs2));
        task2.ContinueWith((task999) => { fs2.Close(); isUpd2 = true; if (isUpd1 == true && isUpd2 == true) isUpdating = false; });

        Image_UpdatingArrows.SetActive(false);
        scrollRaiting.UpdateList(users);
    }
   /* void TransferListUsers()
    {
        Debug.Log("Started method the TransferListUsers");
        int minValue = users.Last().score;
        var fs = File.Create(Application.persistentDataPath + "/PossibleForUploadValue.dat");    //меняем минимальное значение 
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, minValue); fs.Close();
        fs = File.Open(Application.persistentDataPath + "/PossibleForUploadValue.dat", FileMode.Open);
        Task task1 = Task.Run(() => Upload("/ISnowmanStorage/PossibleForUploadValue.dat", fs));
        task1.ContinueWith((task) => fs.Close());

        scrollRaiting.UpdateList(users);
    }*/
    
    void ResetFilesOnDropbox()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        var fs2 = File.Create(Application.persistentDataPath + pathForPossibleForUploadValue);
        formatter.Serialize(fs2,(int)10);
        fs2.Close();
        fs2 = File.Open(Application.persistentDataPath + pathForPossibleForUploadValue, FileMode.Open);
        Task task2 = Task.Run(()=> Upload("/ISnowmanStorage/PossibleForUploadValue.dat", fs2));
        task2.ContinueWith((task) => fs2.Close());

        var fs1 = File.Create(Application.persistentDataPath + pathForTableFile);
        users = new List<ClassSaveDataUser>() { new ClassSaveDataUser("BOT", 10, 0)};
        formatter.Serialize(fs1, users);
        fs1.Close();
        fs1 = File.Open(Application.persistentDataPath + pathForTableFile, FileMode.Open);
        Task task1 = Task.Run(() => Upload(@"/ISnowmanStorage/usersScoresTable.dat", fs1));
        task1.ContinueWith((task) => fs1.Close());

        var fs3 = File.Create(Application.persistentDataPath + "/FileForGettingID.dat");
        formatter.Serialize(fs3, 1);
        fs3.Close();
        fs3 = File.Open(Application.persistentDataPath + "/FileForGettingID.dat", FileMode.Open);
        Task task3 = Task.Run(() => Upload(@"/ISnowmanStorage/FileForGettingID.dat", fs3));
        task3.ContinueWith((task) => fs3.Close());
    }
    
    public static void GetID(bool isContinue)
    {
        //path file for getting id name :   "/ISnowmanStorage/FileForGettingID.dat"
        //path file on the device       :  Application.persistentDataPath + "/FileForGettingID.dat"
        BinaryFormatter formatter = new BinaryFormatter();
        if (isContinue == false)
        {
            var fs = File.Create(Application.persistentDataPath + "/FileForGettingID.dat");
            Task task1 = Task.Run(() => Download("/ISnowmanStorage/FileForGettingID.dat", fs));
            task1.ContinueWith((task) => { fs.Close(); isUploadID = true;  Debug.Log("Finish method GetID "); });
        }
        else if (isContinue == true)
        {
            isUploadID = false;
            var fs = File.Open(Application.persistentDataPath + "/FileForGettingID.dat", FileMode.Open);
            if(fs.Length != 0)
            {
                SaveLoad.save.id = (int)formatter.Deserialize(fs);
                SaveLoad.savePoints.id = SaveLoad.save.id;
                fs.Close();
                fs = File.Create(Application.persistentDataPath + "/FileForGettingID.dat");
                formatter.Serialize(fs, SaveLoad.save.id + 1);
                fs.Close();
                fs = File.Open(Application.persistentDataPath + "/FileForGettingID.dat", FileMode.Open);
                Task task1 = Task.Run(() => Upload("/ISnowmanStorage/FileForGettingID.dat", fs));
                task1.ContinueWith((task) => { fs.Close();});
                //Debug.Log("New id for the user created  - "+SaveLoad.save.id);
                SaveLoad.Save();
            }
            else
                fs.Close();
        }
    }
    
}


[System.Serializable]
public class ClassSaveDataUser
{
    public string nickName;
    public int score;
    public int id;
    public ClassSaveDataUser(string nickName, int score, int id)
    {
        this.nickName = nickName;
        this.score = score;
        this.id = id;
    }
}

