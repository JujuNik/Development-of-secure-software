from fileinput import filename
import zipfile
import json 
import os
import wmi
import time
from tkinter import *
from tkinter import ttk
from tkinter.filedialog import askopenfilename
import xml.etree.ElementTree as ET
import xml.dom.minidom
from re import M
import shutil

def taskThree():
    #filePath = 'myFile.json'
    filePath = input('Введите название файла: ') + '.json'
    students = {}
    students['student'] = []

    def writeToFile(filePath):
        with open(filePath, 'w') as f:
            json.dump(students, f)
            print('Данные записаны.')

    def readFromFile(filePath):
        with open(filePath) as json_file:
            data = json.load(json_file)
            for p in data['student']:
                #stud = Student(p['name'], p['age'])
                students['student'].append({
                    'name': p['name'],
                    'age': p['age']
                })
            print('Полученные данные из файла:')
            print (json.dumps(data, indent=2))

    def addData():
        print('Добавьте данные в файл\n')
        age = input('Введите возраст студента: ')
        name = input('Введите имя студента: ')

        students['student'].append({
                    'name': name,
                    'age': age
                })

    def removeFile(filePath):
        deleteFile = input('Удалить файл? Введите 1 - если да, 0 - если нет\n')
        if(deleteFile == '1'):
            os.remove(filePath)
            print('Файл ', filePath, ' удален.')
        print('Работа с JOSN файлом завершена')


    if(os.path.exists(filePath)):
        print('Файл сущетвует.')
        readFromFile(filePath)
    else:
        print('Файла не сущетвует. Он будет создан.')
    addData()
    writeToFile(filePath)
    readFromFile(filePath)
    removeFile(filePath)

def taskTwo():
    fileName = input('Ввидеите название файла: ') + '.txt'
    if(os.path.exists(fileName)):
        str = input('Введите строку для записи в файл: ')
        with open(fileName, 'a') as myfile:
            myfile.write(str)
            print('Данные записаны.')
    else:
        print('\nФайла не существует. Он будет создан.\n')
        str = input('Введите строку для записи в файл: ')
        with open(fileName, 'w') as myfile:
            myfile.write(str)
            print('Данные записаны.')

    readFile = input("Прочитать данные из файла? Введите 1 - если да, 0 - если нет\n")
    if (readFile == "1"):
        with open(fileName, 'r') as myfile:
            print("Полученные данные из файла: ")
            for line in myfile:
                print(line)

    deleteFile = input("Удалить файл? Введите 1 - если да, 0 - если нет\n")
    if (deleteFile == "1"):
        os.remove(fileName)
        print("Файл ", fileName, " удален.")

def taskFive(): 
    def createFile(zipName):
        myZip = zipfile.ZipFile(zipName, 'w')  
        return myZip

    def openFile():
        Tk().withdraw() 
        fileName = askopenfilename()
        print('Имя файла:')
        print(fileName)
        return fileName

    def writeFile(fileName, myZip, zipName):
        myZip.write(fileName, os.path.basename(fileName))
        myZip.close()

        print('Содержимое архива:')
        myZip.printdir()

    def readFile(fileName, zipName):
        direct = input('Введите название папки для извлечения файлов: ')
        if not os.path.isdir(direct):
            os.mkdir(direct)
        myZip = zipfile.ZipFile(zipName, 'r') 
        myZip.extractall(direct)
        myZip.close()

        for file in os.listdir(direct):
            print('\nДанные о файле:')
            print('Имя файла: ', file)
            print('Размер файла: ', os.stat(os.path.dirname(os.path.abspath(file))).st_size)

        deleteFileAndZip(fileName, zipName, direct)

    def deleteFileAndZip(fileName, zipName, direct):
        deleteZip = input('Удалить архив? Введите 1 - если да, 0 - если нет\n')
        if(deleteZip == '1'):     
            os.remove(zipName) 
            print('Архив удален.')
        deleteDirect = input('\nУдалить папку? Введите 1 - если да, 0 - если нет\n')
        if(deleteDirect == '1'):   
            shutil.rmtree(direct)
            print('Папка удалена.')
        print('Работа с архивом завершена.')


    zipName = input('Введите название архива: ') + '.zip'
    myZip=createFile(zipName)
    fileName=openFile()
    writeFile(fileName, myZip, zipName)
    readFile(fileName, zipName)


def taskOne():
    info = wmi.WMI() #подключение к локальной машине 
    for r in info.Win32_LogicalDisk(): #находит все жетские диски 
        print(f"Название диска: {r.Caption} \nСвободное пространство: {r.FreeSpace} \nОбъем диска: {r.Size} \nТип: {r.DriveType} \nМетка: {r.VolumeName}")

def taskFour():
    class XMLFile:
        pathFile = ""
        def readFile(self):
            print("Чтение файла:\n")
            dom = xml.dom.minidom.parse(self.pathFile)
            pretty_xml_as_string = dom.toprettyxml()
            print(pretty_xml_as_string)

        
        def writeFile(self):
            if (os.path.isfile(self.pathFile)):
                q = input("Добавить запись в файл? Введите 1 - если да, 0 - если нет: ")    
                if (q == '1'):
                    parser = ET.XMLParser(encoding="utf-8")
                    tree = ET.parse(self.pathFile, parser=parser)

                    root = tree.getroot()
                    student = ET.SubElement(root, 'student')
                    nameText = input('Введите имя студента: ')
                    ageText = input('Введите возраст студента: ')
                    name = ET.SubElement(student, 'name')
                    age = ET.SubElement(student, 'age')
                    name.text = nameText
                    age.text = ageText
                    tree.write(self.pathFile, encoding="utf-8")
            else:
                print("Файла не существует. Он будет создан.\nДобавьте запись в файл\n")
                data = ET.Element('students')
                student = ET.SubElement(data, 'student')
                nameText = input('Введите имя студента: ')
                ageText = input('Введите возраст студента: ')
                name = ET.SubElement(student, 'name')
                age = ET.SubElement(student, 'age')
                name.text = nameText
                age.text = ageText
                mydata = ET.tostring(data, encoding="utf-8", method="xml")
                with open(self.pathFile, 'w', encoding='utf-8') as myfile:
                    myfile.write(mydata.decode(encoding = "utf-8"))
        
        def deleteFile(self):
            os.remove(self.pathFile)
            print("Файл удален")


        def start (self):
            self.pathFile = input("Введите название файла: ") + '.xml'
            self.writeFile()
            self.readFile()
            q = input("Удалить файл? Введите 1 - если да, 0 - если нет\n ")    
            if (q == '1'):
                self.deleteFile()
            print("Работа с XML заверешна")

    xmlFile = XMLFile()
    xmlFile.start()

def main():
    window = Tk()  
    window.title("Практика 1")  
    window.geometry('300x200') 
    btnTaskOne = Button(window, text="Задание 1", padx=10, pady=10, command=taskOne)  
    btnTaskOne.place(relx=.5, rely=.1, anchor="c", height=30, width=130)
    
    btnTaskTwo = Button(window, text="Задание 2", padx=10, pady=10, command=taskTwo)  
    btnTaskTwo.place(relx=.5, rely=.3, anchor="c", height=30, width=130)
    
    btnTaskThree = Button(window, text="Задание 3", padx=10, pady=10, command=taskThree)  
    btnTaskThree.place(relx=.5, rely=.5, anchor="c", height=30, width=130)

    btnTaskFour = Button(window, text="Задание 4", padx=10, pady=10, command=taskFour)  
    btnTaskFour.place(relx=.5, rely=.7, anchor="c", height=30, width=130)

    btnTaskFive = Button(window, text="Задание 5", padx=10, pady=10, command=taskFive)  
    btnTaskFive.place(relx=.5, rely=.9, anchor="c", height=30, width=130)

    window.mainloop() 


main()