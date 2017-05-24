import nltk
import sys

class ArticleProcessor:

    #Dictionary that holds the counter value
    tagCounter = {}

    def pos_counts(self, fname):
        """
        Function to read the article file and do natural language processing
        """
        fname = open(fname,'r')

        tags_set = []
        # Read each line create the tags from tokens and append that to the list.
        # Once the file read operation is done close file
        # after performing nltk.pos_tag operation,tags are always stored as
        # ('word',tag) inside tags_set.
        for line in fname:
            line = line.strip()
            tokens = nltk.word_tokenize(line)
            line_tag = nltk.pos_tag(tokens)
            tags_set.extend(line_tag)
        fname.close()

        # loop over each touple and process tagValue
        for index, (tagKey,tagValue) in enumerate(tags_set):
                self.CountNumber(tagValue)

    def CountNumber(self, value):
        """
        Function to increase tags counter in the dictionary
        """
        #initiate the counter to 
        counter = 0
        #if the tag is already present in the dictionary then get the counter
        if(value in self.tagCounter):
            counter = self.tagCounter[value]
        #always increment the counter by 1 and save in the dictionary
        self.tagCounter[value] = counter + 1
        

if(__name__ == "__main__"):
    #Assumption that code and 'article.txt' are in same directory
    fileName = 'article.txt'
    # if 'article.txt' is in different directory or different file needs to be processed 
    # then pass it as the argument for the command in stdin
    if(sys.argv is not None and len(sys.argv) == 2):
        fileName = sys.argv[1]
    articleProcessor = ArticleProcessor()
    articleProcessor.pos_counts(fileName)
    print(articleProcessor.tagCounter)
