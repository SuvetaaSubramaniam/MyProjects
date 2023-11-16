import datetime
import os.path
import os
import sys

from Customer import *
from datetime import *
import tkinter as tk
import customtkinter as ctk
from PIL import ImageTk

# Set the appearance mode and color theme for the graphical user interface
ctk.set_appearance_mode("dark")
ctk.set_default_color_theme("dark-blue")


# Read customer information from the file and store it in a list
listAllCustomer=[]

file=open("Customer.txt","r")
file.seek(0)
for i in file:
    if len(i.strip())!=0:
        liste=i.split()
        listAllCustomer.append(liste)
print(listAllCustomer)

#HELPING FUNCTION: Function to convert a customer object to a list representation
def convertCustomerToList(a):
    mycustomer = []
    mycustomer.append(a.firstname)
    mycustomer.append(a.lastname)
    mycustomer.append(a.email)
    mycustomer.append(a.amountMain)
    mycustomer.append(a.amountSaving)
    mycustomer.append(a.accountNumber)
    mycustomer.append(a.accountPin)
    return mycustomer

#HELPING FUNCTION: Function to convert a list representation of customer information to a customer object
def convertListToCustomer(l):
    c=Customer(l[0],l[1],l[2],l[3],l[4])
    return c

#HELPING FUNCTION:     # Function to delete a customer from the Customer.txt file and the listAllCustomer list
def deleteCustomerList(firstname,lastname):
    if os.stat("Customer.txt").st_size==0:
        return False
    else:
        with open("Customer.txt", mode="r+") as myfile:
            lignes = myfile.readlines()

            # Creating two variables to determine if the customer has been correctly deleted
            # length1 is the length of the file before the deleting process
            # length2 is the length after the deleting process
            length1 = len(lignes)
            myfile.seek(0)
            length2 = 0

            for ligne in lignes:
                tab = ligne.split()
                # If the customer name and surname don't match, we can add the customer to the file
                if (tab[0] != firstname or tab[1] != lastname):
                    myfile.write(ligne)
                    length2 += 1
            myfile.truncate()
            #After removing customer from file, we delete him from the list
            for i in listAllCustomer:
                if i[0] == firstname and i[1] == lastname:
                    listAllCustomer.remove(i)
                    print(listAllCustomer)
            # If the length of the file remains the same, the customer doesn't exist in the text file
            if length1==length2:
                return False
            else:
                return True

#HELPING FUNCTION: Function to add a new customer to the Customer.txt file and the listAllCustomer list
def AddCustomerFile(firstname,lastname,email,amountMain,amountSaving):
    newCustomer = Customer(firstname, lastname, email, amountMain, amountSaving)

    # Append the converted customer information to the listAllCustomer list
    listAllCustomer.append(convertCustomerToList(newCustomer))
    print(listAllCustomer)
    mylist = list(convertCustomerToList(newCustomer))
    info = ""
    for i in mylist:
        info += str(i) + " "

    # If the "Customer.txt" file does not exist, create it
    if not os.path.exists("Customer.txt"):
        mylist = open("Customer.txt", mode="w")
    mylist = open("Customer.txt", mode="a+")
    mylist.write(info + "\n")
    mylist.close()


#MAIN INTERFACE
def CustomerEmployee():
    base.withdraw()
    CustomerOrEmployee = ctk.CTk()
    CustomerOrEmployee.geometry("500x400+500+200")
    CustomerOrEmployee.title("Customer or Employee")
    frame=ctk.CTkFrame(master=CustomerOrEmployee)
    frame.pack(pady=80,padx=60,fill="both",expand=True)
    label=ctk.CTkLabel(master=frame,text="Connexion ",font=("Arial Rounded MT Bold",18))
    label.pack(pady=20,padx=10)

    def customer():
        CustomerOrEmployee.withdraw()
        customerAccess=ctk.CTk()
        customerAccess.geometry("700x600+400+80")
        customerAccess.title("Customer Access")
        frame1 = ctk.CTkFrame(master=customerAccess)
        frame1.pack(pady=80, padx=60, fill="both", expand=True)

        def loginCustomer():
            # Get input values from the entry fields
            firstname=entry4.get()
            lastname=entry1.get()
            account=entry2.get()
            pin=entry3.get()
            print(firstname,lastname,account,pin)

            # Initialize a flag to check if customer information is found
            check=False

            for i in listAllCustomer:
                print(i[0],i[1],i[5],i[6])
                # Check if the entered customer information matches the current customer in the loop
                if i[0]==firstname and i[1]==lastname and i[5]==account and i[6]==pin:
                    check=True
                    customerPlateform()
                    break
            if check==False:
                label = ctk.CTkLabel(master=customerAccess, text='Customer information not find.Try again', font=("Calibri Light", 13))
                label.place(relx=0.5, rely=0.85, anchor="center")

        def customerPlateform():
            customerAccess.withdraw()
            cPlateforme = ctk.CTk()
            cPlateforme.geometry("500x400+500+200")
            cPlateforme.title("Customer Access")


            def backCustomerAccess():
                cPlateforme.withdraw()
                customerAccess.deiconify()

            def transactionHistory():
                cPlateforme.withdraw()
                transaction=ctk.CTk()
                transaction.geometry("1000x1200+200+40")
                cPlateforme.title("Transaction History")
                frame = ctk.CTkFrame(master=transaction)
                frame.pack(pady=80, padx=80, fill="both", expand=True)
                label = ctk.CTkLabel(master=frame, text="Transaction History ", font=("Arial Rounded MT Bold", 18))
                label.place(relx=0.5,rely=0.1,anchor="center")
                label1 = ctk.CTkLabel(master=frame, text="Account detail: ", font=("Futura", 14))
                label1.place(relx=0, rely=0.25, anchor="nw")
                accounDetail=""
                historyMain=""
                historySaving=""
                firstname=""
                for i in listAllCustomer:
                    if i[0]==entry4.get() and i[1]==entry1.get() and i[5]==entry2.get():
                        c=convertListToCustomer(i)
                        firstname=c.firstname
                        accounDetail="NAME: "+c.firstname+c.lastname+ "  ACCOUNT NUMBER:: "+str(c.accountNumber)+"  BALANCE MAIN: "+str(c.amountMain)+"  BALANCE SAVING: "+str(c.amountSaving)
                        break
                # Display account details
                label2 = ctk.CTkLabel(master=frame, text=accounDetail, font=("Futura", 13))
                label2.place(relx=0, rely=0.30, anchor="nw")

                # Main account transaction details
                label3 = ctk.CTkLabel(master=frame, text="TRANSACTION DETAIL IN MAIN ACCOUNT:\n", font=("Futura", 14))
                label3.place(relx=0, rely=0.4, anchor="nw")
                myMain=open("{}{}Main.txt".format(firstname,entry1.get()),"r")
                for i in myMain:
                    historyMain+=i
                n = 0.45
                label4 = ctk.CTkLabel(master=frame, text=historyMain,font=("Futura", 13))
                label4.place(relx=0, rely=n, anchor="nw")

                # Saving account transaction details
                label5 = ctk.CTkLabel(master=frame, text="TRANSACTION DETAIL IN SAVING ACCOUNT", font=("Futura", 14))
                label5.place(relx=0, rely=n+0.15, anchor="nw")
                myMain=open("{}{}Saving.txt".format(firstname,entry1.get()),"r")
                for i in myMain:
                    historySaving+=i
                label6 = ctk.CTkLabel(master=frame, text=historySaving,font=("Futura", 13))
                label6.place(relx=0, rely=n+0.2, anchor="nw")

                def backcPlateforme():
                    transaction.withdraw()
                    cPlateforme.deiconify()

                buttonWith = ctk.CTkButton(master=transaction, text="BACK", command=backcPlateforme)
                buttonWith.place(relx=0.5, rely=0.80, anchor="center")

            def addMoney():
                cPlateforme.withdraw()
                addM = ctk.CTk()
                addM.geometry("600x500+500+200")
                addM.title("Deposit Money (client)")
                frameCreateT = ctk.CTkFrame(master=addM)
                frameCreateT.pack(pady=80, padx=80, fill="both", expand=True)
                label = ctk.CTkLabel(master=addM, text="Transaction Detail", font=("Arial Rounded MT BOLD", 18))
                label.place(relx=0.5, rely=0.15, anchor="center")
                e1 = ctk.CTkEntry(master=addM, placeholder_text="Date (dd-mm-YYY)")
                e1.place(relx=0.5, rely=0.28, anchor="center")
                e2 = ctk.CTkEntry(master=addM, placeholder_text="Amount")
                e2.place(relx=0.5, rely=0.36, anchor="center")
                e3 = ctk.CTkEntry(master=addM, placeholder_text="Main or Saving")
                e3.place(relx=0.5, rely=0.44, anchor="center")

                def backcPlateforme():
                    addM.withdraw()
                    cPlateforme.deiconify()

                def add():
                    firstname=entry4.get()
                    lastname=entry1.get()
                    mydate = datetime.strptime(e1.get(), "%d-%m-%Y")
                    amount = int(e2.get())
                    type = e3.get()
                    balance = 0
                    check = False
                    for i in listAllCustomer:
                        if firstname == i[0] and lastname == i[1]:
                            check = True
                            file = open("{}{}{}.txt".format(firstname, lastname, type), "a+")
                            c = convertListToCustomer(i)
                            if type == "Main":
                                balance = int(i[3]) + amount
                                c.amountMain = balance
                            elif type == "Saving":
                                balance = int(i[4]) + amount
                                c.amountSaving = balance
                            else:
                                check = False
                                break
                            deleteCustomerList(firstname, lastname)
                            AddCustomerFile(c.firstname, c.lastname, c.email, c.amountMain, c.amountSaving)
                            t = Transaction(mydate, type, amount, balance)
                            file.write(t.toString() + " TYPE: deposit  -CUSTOMER-\n")
                            file.close()
                            label = ctk.CTkLabel(master=addM, text="Transaction has been successfully completed",bg_color="#81bfdf", text_color="black")
                            label.place(relx=0.5, rely=0.92, anchor="center")
                    if check == False:
                        label = ctk.CTkLabel(master=addM, text="The transaction has failed", bg_color="#81bfdf", text_color="black")
                        label.place(relx=0.5, rely=0.92, anchor="center")

                buttonDeposit = ctk.CTkButton(master=addM, text="DEPOSIT", command=add)
                buttonDeposit.place(relx=0.5, rely=0.7, anchor="center")
                buttonWith = ctk.CTkButton(master=addM, text="BACK", command=backcPlateforme)
                buttonWith.place(relx=0.5, rely=0.77, anchor="center")


            def removeMoney():
                cPlateforme.withdraw()
                removeM = ctk.CTk()
                removeM.geometry("600x500+500+200")
                removeM.title("Withdraw Money (client)")
                frameCreateT = ctk.CTkFrame(master=removeM)
                frameCreateT.pack(pady=80, padx=80, fill="both", expand=True)
                label = ctk.CTkLabel(master=removeM, text="Transaction Detail", font=("Arial Rounded MT BOLD", 18))
                label.place(relx=0.5, rely=0.15, anchor="center")
                e1 = ctk.CTkEntry(master=removeM, placeholder_text="Date (dd-mm-YYY)")
                e1.place(relx=0.5, rely=0.28, anchor="center")
                e2 = ctk.CTkEntry(master=removeM, placeholder_text="Amount")
                e2.place(relx=0.5, rely=0.36, anchor="center")
                e3 = ctk.CTkEntry(master=removeM, placeholder_text="Main or Saving")
                e3.place(relx=0.5, rely=0.44, anchor="center")

                def backcPlateformR():
                    removeM.withdraw()
                    cPlateforme.deiconify()

                def remove():
                    firstname = entry4.get()
                    lastname = entry1.get()
                    mydate = datetime.strptime(e1.get(), "%d-%m-%Y")
                    amount = int(e2.get())
                    type = e3.get()
                    balance = 0
                    check = False
                    for i in listAllCustomer:
                        if firstname == i[0] and lastname == i[1]:
                            check = True
                            file = open("{}{}{}.txt".format(firstname, lastname, type), "a+")
                            c = convertListToCustomer(i)
                            if type == "Main":
                                balance = int(i[3]) - amount
                                c.amountMain = balance
                            elif type == "Saving":
                                balance = int(i[4]) - amount
                                c.amountSaving = balance
                            else:
                                check = False
                                break
                            deleteCustomerList(firstname, lastname)
                            AddCustomerFile(c.firstname, c.lastname, c.email, c.amountMain, c.amountSaving)
                            t = Transaction(mydate, type, amount, balance)
                            file.write(t.toString() + " TYPE: withdraw  -CUSTOMER-\n")
                            file.close()
                            label = ctk.CTkLabel(master=removeM, text="Transaction has been successfully completed",bg_color="#81bfdf", text_color="black")
                            label.place(relx=0.5, rely=0.92, anchor="center")
                    if check == False:
                        label = ctk.CTkLabel(master=removeM, text="The transaction has failed", bg_color="#81bfdf",text_color="black")
                        label.place(relx=0.5, rely=0.92, anchor="center")

                buttonDeposit = ctk.CTkButton(master=removeM, text="WITHDRAW", command=remove)
                buttonDeposit.place(relx=0.5, rely=0.7, anchor="center")
                buttonWith = ctk.CTkButton(master=removeM, text="BACK", command=backcPlateformR)
                buttonWith.place(relx=0.5, rely=0.77, anchor="center")



            labelPlateforme = ctk.CTkLabel(master=cPlateforme, text="Customer Access",font=("Arial Rounded MT Bold", 18))
            labelPlateforme.place(relx=0.5, rely=0.3, anchor="center")
            buttonCreateC = ctk.CTkButton(master=cPlateforme, text="TRANSACTION HISTORY", command=transactionHistory)
            buttonCreateC.place(relx=0.5, rely=0.42, anchor="center")
            buttonDeleteC = ctk.CTkButton(master=cPlateforme, text="DEPOSIT MONEY", command=addMoney)
            buttonDeleteC.place(relx=0.5, rely=0.5, anchor="center")
            buttonCreateT = ctk.CTkButton(master=cPlateforme, text="WITHDRAW MONEY", command=removeMoney)
            buttonCreateT.place(relx=0.5, rely=0.58, anchor="center")
            buttonBack = ctk.CTkButton(master=cPlateforme, text="BACK", command=backCustomerAccess)
            buttonBack.place(relx=0.5, rely=0.77, anchor="center")

        def backCustomerOrEmployee():
            customerAccess.withdraw()
            CustomerOrEmployee.deiconify()

        label = ctk.CTkLabel(master=frame1, text="Login System", font=("Arial Rounded MT Bold", 18))
        label.place(relx=0.5, rely=0.20, anchor="center")
        entry4 = ctk.CTkEntry(master=frame1, placeholder_text="FIRSTNAME")
        entry4.place(relx=0.5, rely=0.32, anchor="center")
        entry1= ctk.CTkEntry(master=frame1, placeholder_text="LASTNAME")
        entry1.place(relx=0.5, rely=0.40, anchor="center")
        entry2 = ctk.CTkEntry(master=frame1, placeholder_text="ACCOUNT NUMBER")
        entry2.place(relx=0.5, rely=0.48, anchor="center")
        entry3 = ctk.CTkEntry(master=frame1, placeholder_text="PIN CODE", show="*")
        entry3.place(relx=0.5, rely=0.56, anchor="center")
        button = ctk.CTkButton(master=frame1, text="LOG IN", command=loginCustomer)
        button.place(relx=0.5, rely=0.72, anchor="center")
        button1 = ctk.CTkButton(master=frame1, text="BACK", command=backCustomerOrEmployee)
        button1.place(relx=0.5, rely=0.79, anchor="center")


    def employee():
        CustomerOrEmployee.withdraw()
        employeeAccess=ctk.CTk()
        employeeAccess.geometry("700x600+400+80")
        employeeAccess.title("Employee Access")
        frame1=ctk.CTkFrame(master=employeeAccess)
        frame1.pack(pady=80,padx=60,fill="both",expand=True)

        #Fonction permettant à l'employee de se connecter
        def loginEmployee():
            password = entry3.get()
            if password == "A1234":
                connexionlabel = ctk.CTkLabel(master=frame1, text='Connexion Successful', font=("Calibri Light", 13))
                connexionlabel.pack()
                plateformEmployee()
            else:
                connexionlabel = ctk.CTkLabel(master=frame1, text='Wrong Password. Try again.',font=("Calibri Light", 13))
                connexionlabel.pack()

        def detailCustomer():
            myfile=open("Customer.txt","r")
            for line in myfile:
                print(line)


        #Menu déroulant accés employé
        def plateformEmployee():
            employeeAccess.withdraw()
            plateforme = ctk.CTk()
            plateforme.geometry("700x600+400+80")
            plateforme.title("Employee Access")

            def backEmployeeAccess():
                plateforme.withdraw()
                employeeAccess.deiconify()

            def createCustomer():
                plateforme.withdraw()
                create = ctk.CTk()     #fenetre pour creer un customer
                create.geometry("700x600+400+80")
                create.title("Create a Customer account")
                frameCreate = ctk.CTkFrame(master=create)
                frameCreate.pack(pady=80, padx=80, fill="both", expand=True)

                #Function to go back from Create customer to the employee plateform
                def backCreate():
                    create.withdraw()
                    plateforme.deiconify()

                def AddCustomerToFile():
                    firstname = entry1.get()
                    lastname = entry2.get()
                    email = entry3.get()
                    AddCustomerFile(firstname,lastname,email,0,0)
                    label = ctk.CTkLabel(master=create, text='Customer added successfully', font=("Calibri Light", 13))
                    label.place(relx=0.5, rely=0.6, anchor="center")


                label = ctk.CTkLabel(master=create, text="Fill up the bellow detail",font=("Arial Rounded MT Bold", 18))
                label.place(relx=0.5, rely=0.25, anchor="center")
                entry1 = ctk.CTkEntry(master=create, placeholder_text="First Name")
                entry1.place(relx=0.5, rely=0.38, anchor="center")
                entry2 = ctk.CTkEntry(master=create, placeholder_text="Last Name")
                entry2.place(relx=0.5, rely=0.45, anchor="center")
                entry3 = ctk.CTkEntry(master=create, placeholder_text="Email address")
                entry3.place(relx=0.5, rely=0.52, anchor="center")
                button = ctk.CTkButton(master=create, text="SAVE", command=AddCustomerToFile)
                button.place(relx=0.5, rely=0.68, anchor="center")
                button = ctk.CTkButton(master=create, text="BACK", command=backCreate)
                button.place(relx=0.5, rely=0.75, anchor="center")

            def createTransaction():
                plateforme.withdraw()
                createT = ctk.CTk()
                createT.geometry("600x500+500+200")
                createT.title("Create a transaction")
                frameCreateT = ctk.CTkFrame(master=createT)
                frameCreateT.pack(pady=80, padx=80, fill="both", expand=True)
                label = ctk.CTkLabel(master=createT, text="Transaction Detail", font=("Arial Rounded MT BOLD", 18))
                label.place(relx=0.5, rely=0.15, anchor="center")
                entry1 = ctk.CTkEntry(master=createT, placeholder_text="First Name")
                entry1.place(relx=0.5, rely=0.28, anchor="center")
                entry2 = ctk.CTkEntry(master=createT, placeholder_text="Last Name")
                entry2.place(relx=0.5, rely=0.36, anchor="center")
                entry3 = ctk.CTkEntry(master=createT, placeholder_text="Date (dd-mm-YYY)")
                entry3.place(relx=0.5, rely=0.44, anchor="center")
                entry4 = ctk.CTkEntry(master=createT, placeholder_text="Amount")
                entry4.place(relx=0.5, rely=0.52, anchor="center")
                entry5 = ctk.CTkEntry(master=createT, placeholder_text="Main or Saving")
                entry5.place(relx=0.5, rely=0.6, anchor="center")

                def backCreateT():
                    createT.withdraw()
                    plateforme.deiconify()

                def deposit():
                    firstname=entry1.get()
                    lastname=entry2.get()
                    mydate=datetime.strptime(entry3.get(),"%d-%m-%Y")
                    amount=int(entry4.get())
                    type=entry5.get()
                    balance=0
                    check=False
                    for i in listAllCustomer:
                        if firstname == i[0] and lastname == i[1]:
                            check = True
                            file = open("{}{}{}.txt".format(firstname, lastname, type), "a+")
                            c = convertListToCustomer(i)
                            if type == "Main":
                                balance = int(i[3]) + amount
                                c.amountMain = balance
                            elif type == "Saving":
                                balance = int(i[4]) + amount
                                c.amountSaving = balance
                            else:
                                check = False
                                break
                            deleteCustomerList(firstname, lastname)
                            AddCustomerFile(c.firstname, c.lastname, c.email, c.amountMain, c.amountSaving)
                            t = Transaction(mydate, type, amount, balance)
                            file.write(t.toString()+" TYPE: deposit  -EMPLOYEE-\n")
                            file.close()
                            label = ctk.CTkLabel(master=createT, text="Transaction has been successfully completed",                  bg_color="#81bfdf", text_color="black")
                            label.place(relx=0.5, rely=0.92, anchor="center")
                    if check==False:
                        label = ctk.CTkLabel(master=createT, text="The transaction has failed",
                                             bg_color="#81bfdf", text_color="black")
                        label.place(relx=0.5, rely=0.92, anchor="center")


                def withdrawal():
                    firstname = entry1.get()
                    lastname = entry2.get()
                    mydate = datetime.strptime(entry3.get(), "%d-%m-%Y")
                    amount = int(entry4.get())
                    type = entry5.get()
                    balance = 0
                    check = False
                    for i in listAllCustomer:
                        if firstname == i[0] and lastname == i[1]:
                            check = True
                            file = open("{}{}{}.txt".format(firstname, lastname, type), "a+")
                            #Determine if we jave to add into the saving or main account
                            c = convertListToCustomer(i)
                            if type == "Main":
                                actualBalance = i[3]
                                # Verification if enough balance in account to withdraw
                                if int(actualBalance) >= amount:
                                    balance = int(actualBalance) - amount
                                else:
                                    check = False
                                    break
                                c.amountMain = balance
                            elif type == "Saving":
                                actualBalance = i[4]
                                # Verification if enough balance in account to withdraw
                                if int(actualBalance) >= amount:
                                    balance = int(actualBalance) - amount
                                else:
                                    check = False
                                    break
                                c.amountSaving = balance
                            else:
                                check = False
                                break
                            deleteCustomerList(firstname, lastname)
                            AddCustomerFile(c.firstname, c.lastname, c.email, c.amountMain, c.amountSaving)
                            t = Transaction(mydate, type, amount, balance)
                            file.write(t.toString()+" TYPE: withdraw  -EMPLOYEE-\n")
                            file.close()
                            label = ctk.CTkLabel(master=createT, text="Transaction has been successfully completed",bg_color="#81bfdf", text_color="black")
                            label.place(relx=0.5, rely=0.92, anchor="center")
                            break

                    if check == False:
                        label = ctk.CTkLabel(master=createT, text="The transaction has failed",bg_color="#81bfdf", text_color="black")
                        label.place(relx=0.5, rely=0.92, anchor="center")


                buttonDeposit = ctk.CTkButton(master=createT, text="DEPOSIT", command=deposit)
                buttonDeposit.place(relx=0.5, rely=0.7, anchor="center")
                buttonWith = ctk.CTkButton(master=createT, text="WITHDRAWAL", command=withdrawal)
                buttonWith.place(relx=0.5, rely=0.77, anchor="center")
                buttonBack = ctk.CTkButton(master=createT, text="BACK", command=backCreateT)
                buttonBack.place(relx=0.5, rely=0.84, anchor="center")

            def deleteCustomer():
                plateforme.withdraw()
                delete=ctk.CTk()
                delete.geometry("700x600+400+80")
                delete.title("Delete a Customer account")
                frameDelete = ctk.CTkFrame(master=delete)
                frameDelete.pack(pady=80, padx=80, fill="both", expand=True)

                #Passage fichier à liste
                #def ConvertFileToList(myfile):


                def delCustomer():
                    firstName=entry1.get()
                    lastName=entry2.get()
                    for i in listAllCustomer:
                        if i[0] == firstName and i[1] == lastName:
                            if os.path.exists("{}{}Main.txt".format(firstName, lastName)):
                                os.remove("{}{}Main.txt".format(firstName, lastName))
                            if os.path.exists("{}{}Saving.txt".format(firstName, lastName)):
                                os.remove("{}{}Saving.txt".format(firstName, lastName))

                            break
                    if(deleteCustomerList(firstName,lastName)==False):
                        labelVide = ctk.CTkLabel(master=delete, text="Customer has not been deleted. Please try again.",bg_color="#81bfdf", text_color="black")
                        labelVide.place(relx=0.5,rely=0.8,anchor="center")
                    else:
                        label = ctk.CTkLabel(master=delete, text="Customer has been deleted",bg_color="#81bfdf", text_color="black")
                        label.place(relx=0.5, rely=0.8, anchor="center")




                def backDelete():
                    delete.withdraw()
                    plateforme.deiconify()

                label = ctk.CTkLabel(master=delete, text="Delete customer \nPlease enter the following detail of the concerned customer",
                                     font=("Arial Rounded MT Bold", 17))
                label.place(relx=0.5, rely=0.3, anchor="center")
                entry1 = ctk.CTkEntry(master=delete, placeholder_text="First Name")
                entry1.place(relx=0.5, rely=0.45, anchor="center")
                entry2 = ctk.CTkEntry(master=delete, placeholder_text="Last Name")
                entry2.place(relx=0.5, rely=0.52, anchor="center")
                buttonDelete = ctk.CTkButton(master=delete, text="DELETE", command=delCustomer)
                buttonDelete.place(relx=0.5, rely=0.65, anchor="center")
                buttonBack = ctk.CTkButton(master=delete, text="BACK", command=backDelete)
                buttonBack.place(relx=0.5, rely=0.7, anchor="center")

            framePlateforme = ctk.CTkFrame(master=plateforme)
            framePlateforme.pack(pady=80, padx=80, fill="both", expand=True)
            labelPlateforme = ctk.CTkLabel(master=plateforme, text="Employee Access", font=("Arial Rounded MT Bold", 18))
            labelPlateforme.place(relx=0.5,rely=0.3,anchor="center")
            buttonCreateC = ctk.CTkButton(master=plateforme, text="CREATE CUSTOMER", command=createCustomer)
            buttonCreateC.place(relx=0.5,rely=0.42,anchor="center")
            buttonDeleteC = ctk.CTkButton(master=plateforme, text="DELETE CUSTOMER", command=deleteCustomer)
            buttonDeleteC.place(relx=0.5,rely=0.5,anchor="center")
            buttonCreateT = ctk.CTkButton(master=plateforme, text="CREATE TRANSACTION", command=createTransaction)
            buttonCreateT.place(relx=0.5,rely=0.58,anchor="center")
            buttonDetail = ctk.CTkButton(master=plateforme, text="GET CUSTOMER DETAIL", command=detailCustomer)
            buttonDetail.place(relx=0.5,rely=0.66,anchor="center")
            buttonBack = ctk.CTkButton(master=plateforme, text="BACK", command=backEmployeeAccess)
            buttonBack.place(relx=0.5, rely=0.77, anchor="center")

        def backCustomerOrEmployee():
            employeeAccess.withdraw()
            CustomerOrEmployee.deiconify()

        label = ctk.CTkLabel(master=frame1, text="Login System",font=("Arial Rounded MT Bold",18))
        label.pack(pady=30, padx=10)
        entry3=ctk.CTkEntry(master=frame1,placeholder_text="Password",show="*")
        entry3.pack(pady=30,padx=10)
        button = ctk.CTkButton(master=frame1, text="LOG IN", command=loginEmployee)
        button.pack(pady=9,padx=10)
        button1 = ctk.CTkButton(master=frame1, text="BACK", command=backCustomerOrEmployee)
        button1.pack(pady=9,padx=10)



    button = ctk.CTkButton(master=frame, text="Customer", command=customer)
    button.pack(pady=12, padx=10)
    button1 = ctk.CTkButton(master=frame, text="Employee", command=employee)
    button1.pack(pady=12, padx=10)




#Entrance of the application

base=tk.Tk()
base.title("SS Bank")
#base.eval("tk::PlaceWindow . center")
base.geometry("500x400+500+200")
frame0=tk.Frame(base,width=500,height=400,bg="black")
frame0.grid(row=0,column=0)
frame0.pack_propagate(False)
#Logo widget
logo=ImageTk.PhotoImage(file="LogoFinale1.jpeg")
logo_label=tk.Label(frame0,image=logo,bg="black")
logo_label.image=logo
logo_label.pack()
label=tk.Label(frame0,text="Welcome to SS BANK",bg="black",fg="white",font=("Arial black",22))
label.pack(padx=0,pady=0)
#Button widget
button=ctk.CTkButton(frame0,text="LOG IN",command=CustomerEmployee)
button.pack(pady=50)
base.mainloop()







