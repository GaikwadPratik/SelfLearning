import string,re

def SplitSentence(line):
    """
    Function to break setence by various punctuation marks
    """
    #Space character
    _gap = ' '
    #new line character
    _newLine = "\n"
    #tab character
    _tab = "\t"
    #Extracting the string puctuation in the string for processing
    listPunctuation = string.punctuation
    #appending few more puctuation marks
    listPunctuation = "{0},{1},{2}".format(listPunctuation, _gap, _newLine, _tab)
    #using regular expression to break the sentence
    separatedList = re.split("["+listPunctuation+"]+",line)
    #removing the empty strings from the list
    separatedList = list(filter(None,separatedList))
    return separatedList

if (__name__ == "__main__"):
    x = "this! is,a , sentence...\n\n"
    assert SplitSentence(x) == ['this', 'is', 'a', 'sentence']
    x = 'red+blue=purple!'
    assert SplitSentence(x) == ['red', 'blue', 'purple']
