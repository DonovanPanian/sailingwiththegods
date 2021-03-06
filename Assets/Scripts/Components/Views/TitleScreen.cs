using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : ViewBehaviour<GameViewModel>
{
	[SerializeField] Button title_newgame_button = null;
	[SerializeField] Button title_loadgame_button = null;
	[SerializeField] Button title_quitgame_button = null;

	[SerializeField] Button title_credits_button = null;
	[SerializeField] Button title_credits_exit = null;
	[SerializeField] Text title_credits_text = null;
	[SerializeField] GameObject title_credits_screen = null;

	private void Start() {
		Subscribe(title_newgame_button.onClick, () => Model.GUI_startNewGame(GameViewModel.Difficulty.Normal));
		Subscribe(title_loadgame_button.onClick, () => Model.GUI_loadGame(GameViewModel.Difficulty.Normal));
		Subscribe(title_quitgame_button.onClick, Application.Quit);

		Subscribe(title_credits_button.onClick, () => GUI_showCredits());
		Subscribe(title_credits_exit.onClick, () => GUI_hideCredits());
	}

	override protected void OnEnable() {
		base.OnEnable();

		title_credits_text.text = (Resources.Load("game_credits_message") as TextAsset).text;
	}

	public void GUI_showCredits() {
		title_credits_screen.SetActive(true);
	}
	public void GUI_hideCredits() {
		title_credits_screen.SetActive(false);
	}
}
