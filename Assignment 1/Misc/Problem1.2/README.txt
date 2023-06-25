In the NaiveBayesSolution you will find the skeleton code.

Several methods (or parts thereof) have been removed from
the complete solution to the problem. Thus, initially, even
though the code compiles (so that you can, for example, read
in the data), it does not really *do* anything. You need to 
add code first. To find out what to add, you can search for 
"To do" using Edit - Find and Replace - Find in Files, in 
the C# IDE.

To run the code (once it has been completed, by you), start
the program, load the training documents, then the test
documents, then the list of stop words. All files can be
found in the Data/ folder. 
Next, move to the Classificaton tab and run through the six steps in order:
Clean, Tokenize, Find stop words, Find prior probabilities,
Find conditional probabilities, and, finally, Classify all.

The end result will be a printout (that can be saved) with
the classification of all (training and test) documents,
where the training documents have been used for generating
the classifier, which is then applied (unchanged) to the
test documents.