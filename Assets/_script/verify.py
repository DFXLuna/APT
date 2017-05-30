# Catches most common errors in slumscript
# V .5302017
# Supported predicates: ">="
import sys
from os import linesep
predicates = ['>=']

def main():
    if len(sys.argv) != 2 or len(sys.argv) != 3:
        print('Usage: verify [-r] path\to\slumscript.slum')

    with open(sys.argv[1]) as f:
        beginstack = []
        numScenes = 0
        scenesplit = f.readline().strip().split(' ')
        if len(scenesplit) != 2 or scenesplit[0] != 'SCENE' or int(scenesplit[1]) <= 0:
            print('Syntax error in line 1 SCENE statement')
            sys.exit(1)
        else:
            correctNumScenes = int(scenesplit[1])
        linenum = 2
        numScenes = 0
        foundCondition = False
        for line in f:
            parse = line.strip().split(' ')
            if parse[0] == 'BEGIN':
                foundCondition = False
                if len(beginstack) != 0:
                    print('Error in line ' + str(linenum))
                    print('Not every BEGIN is paired with an END')
                    sys.exit(2)
                beginstack.append(linenum)
                numScenes += 1
            elif parse[0] == 'CONDITION':
                foundCondition = True
                if len(parse) != 4: 
                    print('Error in line ' + str(linenum))
                    print('Invalid condition statement')
                    sys.exit(3)
                if not parse[2] in predicates: 
                    print('Error in line ' + str(linenum))
                    print('Invalid predicate')
                    sys.exit(4)
            elif parse[0] == 'END':
                if len(beginstack) != 1 : 
                    print('Error in line ' + str(linenum))
                    print('Not every BEGIN is paired with an END')
                    sys.exit(5)
                else : beginstack.pop()
                if foundCondition != True:
                    print('No condition in scene ' + str(numScenes))
                    sys.exit(6)
                else:
                    foundCondition = False
            else :
                if line.strip() != '':
                    if not '|' in line:
                        print('Syntax error in line ' + str(linenum))
                        print('Line doesn\'t contain \'|\'')
                        sys.exit(9)
                    # Reparse line with different separator
                    parse = line.strip().split('|')
                    if len(parse) != 2:
                        print('Syntax error in line' + str(linenum))
                        sys.exit(9)
                elif len(beginstack) != 0:
                    print('Syntax error in line ' + str(linenum))
                    print('Blank line inside scene')
                    sys.exit(8)
                
            linenum += 1
            
        if numScenes != correctNumScenes: 
            print('Number of scenes doesn\'t match number specified in SCENE statement')
            print('Expected: ' + str(correctNumScenes))
            print('Found: ' + str(numScenes))
            sys.exit(7)
        print('No syntax errors')
        # Run scene
        if(len(sys.argv) == 3 and argv[2] = '-r'):
            

if __name__ == "__main__":
    main()