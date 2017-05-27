using System;
using System.IO;
using UnityEngine;

namespace NarrativeReader{

	public class NarrativeReader{
		private System.IO.StreamReader file;
		
		public NarrativeReader(string fileLocation){
			try{
				file = new System.IO.StreamReader(fileLocation);
			}
			catch(Exception e){
				Debug.Log("File not readable: ");
				Debug.Log(e);
			}
		}
	}
}
