using System.Collections.Generic;
namespace NarrativeTypes{
	// Condition to trigger associated narrative
	public delegate bool condition();
	
	// Trigger narrative event
	public delegate void narrative();
	
	// Narrative Data
	public struct NarrativeEvent {
		List<string> lines;
		List<string> names;
		private int currentLine;

		public NarrativeEvent(List<string> namesIn, List<string> linesIn){
			lines = linesIn;
			names = namesIn;
			currentLine = 0;
		}

		public string nextLine(){
			string line = constructLine(currentLine);
			currentLine++;
			return line;
		}

		private string constructLine(int lineNum){
			// Fix return
			if(lineNum >= lines.Count){ return null; }
			return (names[lineNum] + ": " +lines[lineNum]);
		}

		public bool isEnd(){
			if(currentLine >= lines.Count){ return true; }
			return false;
		}
	}
}