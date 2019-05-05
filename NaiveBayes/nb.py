import numpy as np
import string
f = open("train.tsv")

def tokenize(text):
    text = list(c for c in text if c in (string.ascii_letters + " "))
    curr = "" 
    res = [] 
    for c in text:
        if c == " " and curr != "":
            res.append(curr)
            curr = ""
        if c != " ":
            curr += c
    if curr != "":
        res.append(curr)
    return list(w.lower() for w in res)

def extract(line):
    cat = line.split("\t")[0]
    cat = int(cat)
    text = line.split("\t")[1]
    return (tokenize(text), cat)

x = list(extract(line) for line in f)
words = set(w for w in sum([text for text,cat in x], []))
words = list(words)
cat_count = [0, 0]
cat_freq = [{}, {}]
for w in words:
    cat_freq[0][w] = 0
    cat_freq[1][w] = 0

for text, cat in x:
    cat_count[cat] += 1
    for w in text:
        cat_freq[cat][w] += 1

text = input(">> ")
text = tokenize(text)
res = [] 
for c in range(2):
    cc = np.log(cat_count[c])
    l = np.log(cat_count[c]) + sum(np.log((cat_freq[c].get(w, 0)+1) / (cat_freq[0].get(w, 0) + cat_freq[1].get(w, 0))) for w in text if w in words)
    res.append(l)

if res[0] > res[1]:
    print("Negative!")
else:
    print("Positive!")
