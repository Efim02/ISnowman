[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}
[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}
[System.Serializable]
public class MasMassivSentences
{
    public MassivSentences[] masMassivs;
}
[System.Serializable]
public class MassivSentences
{
    public string[] mas;
}
[System.Serializable]
public class Sentence
{
    public string sentence;
}

