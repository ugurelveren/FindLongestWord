Write a program that reads a file containing a sorted list of words (one word per line, no spaces, all lower case), then identifies 
1. The longest word in the file that can be constructed by concatenating copies of shorter words also found in the file. 
2. The program should then go on to report the 2nd longest word found 
3. Total count of how many of the words in the list can be constructed of other words in the list. 
For example, if the file contained: 
cat 
cats 
catsdogcats 
catxdogcatsrat 
dog 
dogcatsdog 
hippopotamuses 
rat 
ratcatdogcat 

The solution would be 'ratcatdogcat' - at 12 letters, it is the longest word made up of other words in the list.
The program should then go on to report how many of the words in the list can be constructed of other words in the list.
