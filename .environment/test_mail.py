import smtplib

sender = 'from@example.com'
receivers = ['aruponse@gmail.com']
#receivers = ['aruponse@example.com']
message = """From: From Person <from@example.com>
To: To Another Person <aruponse@gmail.com>
Subject: SMTP email example


This is another test message.
"""

try:
    smtpObj = smtplib.SMTP('localhost:1025')
    smtpObj.sendmail(sender, receivers, message)         
    print("Successfully sent email")
except BaseException:
    pass