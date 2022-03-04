using Sheldier.Common.Pause;
using Sheldier.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestPause : MonoBehaviour
{
    [SerializeField] private Button button;
    private bool _isPaused;
    private PauseNotifier _pauseNotifier;



    [Inject]
    private void InjectDependencies(PauseNotifier pauseNotifier)
    {
        _pauseNotifier = pauseNotifier;
    }
    public void Pause()
    {
        if(_pauseNotifier.IsPaused)
            _pauseNotifier.Unpause();
        else
            _pauseNotifier.Pause();
    }

}
