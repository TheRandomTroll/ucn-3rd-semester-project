import spacy
import sys

title1 = sys.argv[1]
title2 = sys.argv[2]
description1 = sys.argv[3]
description2 = sys.argv[4]
# nlp = spacy.load("en_core_web_lg")
nlp = spacy.load("en_core_web_md")

doc1 = nlp(title1)
doc2 = nlp(title2)

print(doc1.similarity(doc2));

doc1 = nlp(description1)
doc2 = nlp(description2)

print(doc1.similarity(doc2));