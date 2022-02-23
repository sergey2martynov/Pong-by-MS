using UnityEngine;

public class SwitchingControlOfTheRightRacket : MonoBehaviour
{
    [SerializeField] private OnePlayerButtonController _onePlayerButtonController;

    [SerializeField] private TwoPlayersButtonController _twoPlayersButtonController;

    [SerializeField] private StartMenuDisabler _startMenuDisabler;

    public NumberOfPlayers NumberOfPlayers { get; private set; }

    private void Start()
    {
        _onePlayerButtonController.Clicked += SetOnePlayer;

        _twoPlayersButtonController.Clicked += SetTwoPlayers;
    }

    private void OnDestroy()
    {
        _onePlayerButtonController.Clicked -= SetOnePlayer;

        _twoPlayersButtonController.Clicked -= SetTwoPlayers;
    }

    private void SetOnePlayer()
    {
        NumberOfPlayers = NumberOfPlayers.OnePlayer;
        _startMenuDisabler.DisableStartMenu(false);
    }

    private void SetTwoPlayers()
    {
        NumberOfPlayers = NumberOfPlayers.TwoPlayers;
        _startMenuDisabler.DisableStartMenu(false);
    }
}