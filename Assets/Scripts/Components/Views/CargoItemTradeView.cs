using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CargoItemTradeView : ViewBehaviour<CargoItemTradeViewModel>
{
	InteractableBehaviour Interactable;

	[SerializeField] StringView Name = null;
	[SerializeField] StringView Price = null;
	[SerializeField] StringView Amount = null;
	[SerializeField] StringView Hint = null;
	[SerializeField] ImageView Icon = null;

	[SerializeField] Image SelectedOverlay = null;

	DelegateHandle SelectedHandle;

	private void Start() {
		if (Interactable != null) {
			Subscribe(Interactable.PointerClick, Clicked);
		}
	}

	public override void Bind(CargoItemTradeViewModel model) {
		base.Bind(model);

		Interactable = GetComponent<InteractableBehaviour>();

		Amount?.Bind(new BoundModel<int>(Model, nameof(Model.AmountKg)).AsString());
		Name?.Bind(new BoundModel<string>(Model, nameof(Model.Name)));
		Icon?.Bind(new BoundModel<Sprite>(Model, nameof(Model.Icon)));
		Price?.Bind(new BoundModel<string>(Model, nameof(Model.PriceStr)));
		Hint?.Bind(new BoundModel<string>(Model, nameof(Model.HintStr)));

		if(SelectedHandle != null) {
			Unsubscribe(SelectedHandle);
		}
		SelectedHandle = Subscribe(() => model.Parent.PropertyChanged += OnSelectedChanged, () => model.Parent.PropertyChanged -= OnSelectedChanged);
		RefreshSelection();
	}

	void OnSelectedChanged(object sender, PropertyChangedEventArgs e) {
		RefreshSelection();
	}

	void RefreshSelection() {
		SelectedOverlay.gameObject.SetActive(Model.IsSelected);
	}

	void Clicked() {
		Model.Select();
	}
}
