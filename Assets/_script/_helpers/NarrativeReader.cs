using System;
using System.IO;
using UnityEngine;

namespace NarrativeReader{

	public class NarrativeReader{
		private System.IO.StreamReader file;
		private int scenes;
		
		public NarrativeReader(string fileLocation){
			try{
				file = new System.IO.StreamReader(fileLocation);
			}
			catch(Exception e){
				Debug.Log("File not readable: ");
				Debug.Log(e);
			}
			// Parse number of scenes
			String parse = file.ReadLine();
			int parseScenes = 0;
			if(!(Int32.TryParse(parse, out scenes))){
				throw(new System.IO.IOException());
			}
			scenes = parseScenes;
		}

		
	}
}
