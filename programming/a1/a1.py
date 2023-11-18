def main():
    numWords = int(input().split()[0])
    words = []
    for _ in range(numWords):
        words.append(input())
    print(countUniqueAnagrams(words))

def countUniqueAnagrams(words):
    # add anagrams to sets - one to store all anagrams and one for non-unique ones
    fullSet = set()
    nonUniqueSet = set()
    for word in words:
        # sorts the letters in a word
        anagram = "".join(sorted(word))
        if anagram in fullSet:
            nonUniqueSet.add(anagram)
        fullSet.add(anagram)

    return len(fullSet) - len(nonUniqueSet)

main()