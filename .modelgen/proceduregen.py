file = open("tables procedures")

tables = {}
nexto = ""
for line in file.readlines():
    if nexto == "":
        operations, table = line.strip().split(" ")
        entry = {}
        entry["op"] = operations
        entry["title"] = table
        entry["lines"] = {}
        tables[table] = entry
        nexto = str(table)
        continue
    if line == "\n":
        nexto = ""
        continue
    line = line.strip()
    value = "int" if "ID" in line else "string"
    tables[nexto]["lines"][line] = value

def generateUpdateString(table):
    return ", ".join([f'@{i} = @{i}' if not i.startswith("ID") else "" for i in tables[table]["lines"]])

def generateVariables(table):
    return ",\n\t".join([f'@{i} {tables[table]["lines"][i]}' for i in tables[table]["lines"]])

def getColumnID(table):
    for l in tables[table]["lines"]:
        if l.startswith("ID"):
            return l

def generateRowHeaders(table):
    return ", ".join([f'@{i}' for i in tables[table]["lines"]])
        
def generateIdVariable(table):
    return f'@{getColumnID(table)} int'

create = """\
IF OBJECT_ID('proc_create_{}') IS NOT NULL
BEGIN 
    DROP PROCEDURE proc_create_{} 
END
GO
CREATE PROCEDURE proc_create_{}
    {}
AS
BEGIN
INSERT INTO {}
    VALUES ({})
 
SELECT SCOPE_IDENTITY() AS {}
END
GO"""

read = """\
IF OBJECT_ID('proc_select_{}') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_{}
END
GO
CREATE PROCEDURE proc_select_{}
    @{}
AS
BEGIN
    SELECT * FROM {} WHERE {} = @{}
END
GO
"""

read_multiple = """\
IF OBJECT_ID('proc_select_multiple_{}') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_select_multiple_{}
END
GO
CREATE PROCEDURE proc_select_multiple_{}
AS
BEGIN
    SELECT * FROM {}
END
GO
"""

update = """\
IF OBJECT_ID('proc_update_{}') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_update_{}
END
GO
CREATE PROCEDURE proc_update_{}
    {}
AS
BEGIN
    UPDATE {} SET {} WHERE {} = @{} 
END
GO
"""

delete = """\
IF OBJECT_ID('proc_delete_{}') IS NOT NULL
BEGIN
    DROP PROCEDURE proc_delete_{}
END
GO
CREATE PROCEDURE proc_delete_{}
    @{}
AS 
BEGIN 
    DELETE FROM {} WHERE {} = @{}
END
GO
"""
def crud_create(table):
    print(create.format(
        table,
        table,
        table,
        generateVariables(table),
        table,
        generateRowHeaders(table),
        getColumnID(table)
    ))
def crud_read(table):
    print(read.format(
        table, 
        table, 
        table,
        generateIdVariable(table),
        table,
        getColumnID(table),
        getColumnID(table)
    ))
def crud_read_multiple(table):
    print(read_multiple.format(
        table, 
        table, 
        table,
        table,
    ))
def crud_update(table):
    print(update.format(
        table,
        table,
        table,
        generateVariables(table),
        table,
        generateUpdateString(table),
        getColumnID(table),
        getColumnID(table)
    ))
def crud_delete(table):
    print(delete.format(
        table,
        table,
        table,
        generateIdVariable(table),
        table,
        getColumnID(table),
        getColumnID(table)
    ))

for t in tables:
    print(f'//----------------------------------------------{t}----------------------------------------------')
    print(f'public class {t} {{')
    for c in tables[t]["lines"]:
        print(f'\tpublic {tables[t]["lines"][c]} {c} {{ get; set; }}')
    print("}\n\n")
    continue
    print(generateUpdateString(t))
    print(generateRowHeaders(t))
    print(generateVariables(t))
    if "U" in tables[t]["op"]:
        crud_update(t)
    crud_read_multiple(t)
    if "C" in tables[t]["op"]:
        crud_create(t)
    if "R" in t["op"]:
        crud_read(t)
        crud_update(t)
    if "D" in t["op"]:
        crud_delete(t)
