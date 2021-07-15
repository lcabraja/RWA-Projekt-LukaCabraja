file = open("selectem.sql")
for f in file.readlines():
    if len(f.split("'")) > 1:
        print(f.split("'")[1])      