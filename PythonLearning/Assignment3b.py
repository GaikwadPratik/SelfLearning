#    There is a review file called review_data.txt, which is pipe-separated.
#    The assignment is to find the top N words (function should be able to take
#    in N as a parameter) and another parameter "score or scores" [corresponds
#    to the last column] and return the top N words in reviews that have the
#    given score
#    The third column is a sentence.  The fourth column is the rating of the
#    review, which can be 0,1,2,3 or 4.  You can ignore the first two columns.
import nltk
from nltk import word_tokenize
import string
import re
import sys
import mmap
import os
from nltk.corpus import stopwords
from collections import Counter
from tqdm import tqdm

class WordProcessorUsingNltk:
    
    #Space character
    _gap = ' '
    #new line character
    _newLine = "\n"
    #tab character
    _tab = "\t"

    # list to hold stop words which will be bypassed in the counting
    _listStopWords = {}

    # dictionary containing text by rating
    _fileDictionary = {}

    # dictionary containing word count
    wordDictionary = {}

    # puctuation list
    _strPuctuation = string.punctuation

    def __init__(self, fileName):

        self._strPuctuation = "{0},{1},{2}".format(string.punctuation, self._gap, self._newLine, self._tab)

        #if list of stop word is empty fill it
        if(self._listStopWords is None or len(self._listStopWords) == 0):
            self._listStopWords = set(stopwords.words('english'))

        # if file dictionary is empty read the file and fill it
        if(self._fileDictionary is None or len(self._fileDictionary.keys()) == 0):
            total = os.path.getsize(fileName)
            file = open(fileName,'r')
            for line in tqdm(file, total=total, desc=os.path.basename(fileName), unit="B", unit_scale=True,miniters=1, ncols=80, ascii=True):
                columns = line.split('|')
                key = columns[3].replace('\n','')
                if(key in self._fileDictionary):
                    self._fileDictionary[key] = "{0} {1}".format(self._fileDictionary[key],columns[2].strip())
                else:
                    self._fileDictionary[key] = columns[2].strip()

            file.close()

        #compile regular expression
        self._regEx = re.compile("[" + self._strPuctuation + "]+")

    def DoWordCount(self):
        """
        Function to iterate over the file dictionary. 
        This will call CountNumber function over the value of the dictionary
        """        
        for key, value in tqdm(self._fileDictionary.items(),unit="B", unit_scale=True,miniters=1, ncols=80, ascii=True):
             #using regular expression to break the sentence
            lstTemp = self._regEx.split(value)
            #removing the empty strings from the list
            lstTemp = list(filter(None,lstTemp))
            lstTemp = [wordInLine for wordInLine in lstTemp if wordInLine not in self._listStopWords]
            self.wordDictionary[key] = Counter(lstTemp)

    def GetTopWords(self, rating, count):
        lst = []
        if rating is str:
            rating = [rating]
        for rat in rating:
            lst.extend([val for val in sorted(self.wordDictionary[rat], key=self.wordDictionary[rat].get, reverse=True)][:count])
        return lst


# Here, we are calling function topnwords with different combination of values
if(__name__ == '__main__'):
    #topnwords(5, ['1'])
    #topnwords(5, ['1', '4'])
    #topnwords(5, ['1', '2'])
    #topnwords(5, ['2', '3'])
    #topnwords(5, ['3', '4'])

    nltkWordProcessor = WordProcessorUsingNltk('review_data.txt')
    nltkWordProcessor.DoWordCount()
    print(nltkWordProcessor.GetTopWords(['1','2'],10))
    print(nltkWordProcessor.GetTopWords('1',10))