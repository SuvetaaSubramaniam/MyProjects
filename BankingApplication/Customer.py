from Transaction import *
import os
class Customer:

    def __init__(self,firstname,lastname,email,amountMain,amountSaving):
        self.firstname=firstname
        self.lastname=lastname
        self.email=email
        self.amountMain=int(amountMain)
        self.amountSaving=int(amountSaving)
        self.accountNumber=self.firstname[0].lower()+self.lastname[0].lower()+"-"+str(len(self.firstname+self.lastname))+"-"+str(ord(self.firstname[0].upper())-64)+"-"+str(ord(self.lastname[0].upper())-64)
        self.accountPin=int(str(ord(self.firstname[0].upper())-64)+str(ord(self.lastname[0].upper())-64))
        if not os.path.exists("{}{}Main.txt".format(firstname,lastname)):
            myfile = open("{}{}Main.txt".format(firstname,lastname), mode="w")
            self.transactionMain=myfile
        if not os.path.exists("{}{}Saving.txt".format(firstname,lastname)):
            myfile = open("{}{}Saving.txt".format(firstname, lastname), mode="w")
            self.transactionSaving = myfile

    def toString(self):
        return self.firstname+" "+self.lastname+" "+self.email+" "+str(self.amountMain)+" "+str(self.amountSaving)

    #fonction pour ahouter une transaction chez un client
    def AddTransactionMain(self,date,action,money,balance):
        myfile=self.transactionMain
        t=Transaction(date,action,money,balance)
        myfile.write(t.toString()+ "\n")
        myfile.close()




