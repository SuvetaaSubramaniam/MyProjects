from datetime import *
class Transaction:
    def __init__(self,date,action,money,balance):
        self.date=date
        self.action=action
        self.money=money
        self.balance=balance

    #Function to display information of the transaction
    def toString(self):
        mystring="DATE: " + str(self.date) + " TRANSACTION AMOUNT: " + str(self.money) + "  BALANCE:" + str(self.balance)
        return mystring