using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using NarrativeTypes;

namespace NarrativeReader{

	public class Reader{
		private System.IO.StreamReader file;
		private int numScenes;
		private int currentScene;
		private List<NarrativeEvent> scenes;
		// Probably bad design but these need to be able to communicate
		private NarrativeManager manager;
		
		public Reader(string fileLocation, NarrativeManager man){
			try{
				file = new System.IO.StreamReader(fileLocation);
			}
			catch(Exception e){
				Debug.Log("File not readable: ");
				Debug.Log(e);
			}
			// Parse number of scenes
			String parse = file.ReadLine();
			string[] sceneSplit = parse.Split();
			int parseScenes = 0;
			if(!(Int32.TryParse(sceneSplit[1], out parseScenes))){
				throw(new System.IO.IOException("Slumscript syntax error in SCENE"));
			}
			numScenes = parseScenes;
			currentScene = 0;
			manager = man;
		}

		public KeyValuePair<condition, NarrativeEvent> readNextScene(){
			if(file.Peek() == -1){ 
				throw new System.IO.IOException("Slumscript syntax error in scene count");
			}
			string curr = file.ReadLine();
			curr.Trim();
			while(String.Compare(curr, "") == 0){
				curr = file.ReadLine();
			}
			if(String.Compare(curr, "BEGIN") != 0){
				throw(new System.IO.IOException("Slumscript syntax error in scene " + currentScene));	
			}
			condition c = parseConditition();
			// Read lines until end
			List<string> lines = new List<string>();
			curr = file.ReadLine();
			curr.Trim();
			while(String.Compare(curr, "END") != 0){
				lines.Add(curr);
				curr = file.ReadLine();
			}

			return new KeyValuePair<condition, NarrativeEvent>(c, parseScene(lines));			
		}

		public int getCurrentScene(){ return currentScene; }

		public int getNumScenes(){ return numScenes; }

		private NarrativeEvent parseScene(List<string> lines){
			List<string> names = new List<string>();
			List<string> dialogue = new List<string>();
			string[] curr;
			char[] separators = new char[]{ '|' };
			int i = 0;
			
			foreach(var s in lines){
				curr = s.Split(separators);
				if(curr.Length != 2){ 
					throw new System.IO.IOException("Slumscript syntax error in scene " + currentScene + " Line: " + i);
				}
				names.Add(String.Copy(curr[0]));
				dialogue.Add(String.Copy(curr[1]));
			}
			NarrativeEvent ret = new NarrativeEvent(names, dialogue); 
			return ret;
		}

		private condition parseConditition(){
			// syntax is CONDITION variable predicate comparisonvalue
			string[] parse = file.ReadLine().Split();
			if(parse.Length != 4 || 
			(String.Compare(parse[0].Trim(), "CONDITION") != 0)){
				throw new System.IO.IOException("Slumscript syntax error in scene " 
				+ currentScene + " condition");
			}
			int comparisonvalue;
			if(!Int32.TryParse(parse[3], out comparisonvalue)){
				throw new System.IO.IOException("Slumscript syntax error in scene "
				+ currentScene + "comparision value");
			}
			// Predicate application
			if(String.Compare(parse[2], ">=") == 0){
				return createGreaterThan(parse[1], comparisonvalue);
			}
			throw new System.Exception("Invalid predicate: " + parse[2]);
		}

		private condition createGreaterThan(string variable, int value){
			int compare = manager.getVariable(variable);
			return (() => compare >= value);
		}
		
	}
}
