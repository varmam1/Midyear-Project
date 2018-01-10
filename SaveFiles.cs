using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using System.IO;

public class SaveFiles : MonoBehaviour {

	GameObject player;

	#if UNITY_EDITOR
	[MenuItem("Tools/Write file")]
	#endif
	public void SaveToText(int saveNumber){
		string path = "Save" + saveNumber + ".txt";
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine(GlobalVariables.row);
		writer.WriteLine(GlobalVariables.col);
		writer.WriteLine(GlobalVariables.difficulty);
		writer.WriteLine(GlobalVariables.level);
		writer.WriteLine(GlobalVariables.time);
		writer.WriteLine(GlobalVariables.score);
		writer.WriteLine(GlobalVariables.startingPosition[0]);
		writer.WriteLine(GlobalVariables.startingPosition[1]);
		writer.WriteLine(GlobalVariables.endingPosition[0]);
		writer.WriteLine(GlobalVariables.endingPosition[1]);

		writer.WriteLine(player.GetComponent<PlayerHandler>().getPlayerLevel());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getExperienceRequired());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getCurrentExperience());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getMaxHealth());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getCurrentHealth());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getAttackPower());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getDefensePower());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getLuck());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getAccuracy());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getSpeed());
		writer.WriteLine(player.GetComponent<PlayerHandler>().getJumpPower());

		int[] inv = player.GetComponent<PlayerHandler> ().getInventory ();
		for (int i = 0; i < 11; i++) {
			writer.WriteLine(inv[i]);
		}

		writer.Close();
	}

	#if UNITY_EDITOR
	[MenuItem("Tools/Read file")]
	#endif
	public void LoadFromText(int saveNumber){
		string path = "Save" + saveNumber + ".txt";
		StreamReader reader = new StreamReader(path); 
		string a = reader.ReadToEnd();

		GlobalVariables.row = int.Parse (reader.ReadLine ());
		GlobalVariables.col = int.Parse (reader.ReadLine ());
		GlobalVariables.difficulty = int.Parse (reader.ReadLine ());
		GlobalVariables.level = int.Parse (reader.ReadLine ());
		GlobalVariables.time = int.Parse (reader.ReadLine ());
		GlobalVariables.score = int.Parse (reader.ReadLine ());
		GlobalVariables.startingPosition[0] = int.Parse (reader.ReadLine ());
		GlobalVariables.startingPosition[1] = int.Parse (reader.ReadLine ());
		GlobalVariables.endingPosition[0] = int.Parse (reader.ReadLine ());
		GlobalVariables.endingPosition[1] = int.Parse (reader.ReadLine ());

		player.GetComponent<PlayerHandler>().setPlayerLevel(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setExperienceRequired(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setCurrentExperience(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setMaxHealth(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setCurrentHealth(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setAttackPower(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setDefensePower(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setLuck(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setAccuracy(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setSpeed(int.Parse (reader.ReadLine ()));
		player.GetComponent<PlayerHandler>().setJumpPower(int.Parse (reader.ReadLine ()));

		for (int i = 0; i < 11; i++) {
			player.GetComponent<PlayerHandler> ().setInventory (i, int.Parse (reader.ReadLine ()));
		}

		reader.Close();
	}
		
}
