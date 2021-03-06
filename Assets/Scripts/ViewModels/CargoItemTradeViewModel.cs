using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CargoItemTradeViewModel : Model
{
	private const string ResourcePath = "resource_icons";

	private Resource Resource;
	private GameVars GameVars => Globals.GameVars;

	public TradeViewModel Parent { get; private set; }
	TradeAction TradeAction;

	public int AmountKg {
		get => Mathf.RoundToInt(Resource.amount_kg);
		set {
			Resource.amount_kg = value;
			Notify();
		}
	}

	// the resource object gives the amount_kg stored on the ship or on the settlement, depending on what the source of the Resource reference was
	public int Price => GameVars.Trade.GetPriceOfResource(Resource.name, GameVars.currentSettlement);
	public string PriceStr => Price + "d/kg";

	public int AveragePrice => GameVars.Trade.GetAvgPriceOfResource(Name);
	public string HintStr {
		get {
			var price = Price;
			var avg = AveragePrice;
			if (price < avg) {
				var str = (avg - price) + " under average";
				return TradeAction == TradeAction.Buy ? MakeGreen(str) : MakeRed(str);
			}
			else if (price > avg) {
				var str = (price - avg) + " over average";
				return TradeAction == TradeAction.Buy ? MakeRed(str) : MakeGreen(str);
			}
			else return "";
		}
	}

	public string MakeGreen(string str) => "<#008800>" + str + "</color>";
	public string MakeRed(string str) => "<#880000>" + str + "</color>";

	public string Name => Resource.name;
	public Sprite Icon { get; private set; }

	public bool IsSelected => Parent.Selected == this;

	public CargoItemTradeViewModel(TradeAction action, Resource resource, TradeViewModel parentModel) {
		Resource = resource;
		Parent = parentModel;
		TradeAction = action;

		var iconFilename = Globals.GameVars.masterResourceList.FirstOrDefault(r => r.name == Name).icon;
		Icon = Resources.Load<Sprite>(ResourcePath + "/" + iconFilename);
	}

	public void Select() {
		Parent.TradeAction = TradeAction;
		Parent.Selected = this;
	}
}
