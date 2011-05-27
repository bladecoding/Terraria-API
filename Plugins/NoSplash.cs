using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

public class NoSplashPlugin : TerrariaPlugin
{
	public override string Name
	{
		get { return "NoSplash"; }
	}

	public override Version Version
	{
		get { return new Version(1, 0); }
	}

	public override string Author
	{
		get { return "High"; }
	}

	public override string Description
	{
		get { return "Removes the splash"; }
	}

	public NoSplashPlugin(Main game)
		: base(game)
	{
		GameHooks.OnPreInitialize += GameHooks_OnPreInit;
	}
	
	void GameHooks_OnPreInit()
	{
		Terraria.Main.showSplash = false;
	}


	public override void Dispose()
	{
		GameHooks.OnPreInitialize -= GameHooks_OnPreInit;
		base.Dispose();
	}
}