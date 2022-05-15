using System.ComponentModel;
using JetBrains.Annotations;
using SRDebugger.Internal;
using SRF.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void SROptionsPropertyChanged(object sender, string propertyName);

public partial class SROptions : INotifyPropertyChanged
{
    public static SROptions Current { get; } = new SROptions();

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
        add => InterfacePropertyChangedEventHandler += value;
        remove => InterfacePropertyChangedEventHandler -= value;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void OnStartup()
    {
        SRServiceManager.GetService<InternalOptionsRegistry>().AddOptionContainer(Current);
    }

    public event SROptionsPropertyChanged PropertyChanged;

#if UNITY_EDITOR
    [NotifyPropertyChangedInvocator]
#endif
    public void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, propertyName);

        if (InterfacePropertyChangedEventHandler != null)
            InterfacePropertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
    }

    private event PropertyChangedEventHandler InterfacePropertyChangedEventHandler;
}