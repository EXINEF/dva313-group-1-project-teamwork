# dva313-group-1-project-teamwork
This is the official Repository of DVA313 - Software Engineering 2: Project Teamwork - 2021 Group 1 Branch DEV

## HOW TO RUN SERVER LOCALLY
1) Have python 3.9 or higher installed, if you don't have install it.
2) Open the terminal inside the folder "dva313-group-1-project-teamwork"
3) Install the requirements.txt
```bash
pip install -r requirements.txt
```

4) Go in the folder /project
```bash
cd project
```

5) Start the local server
```bash
python manage.py runserver
```

6) Check the website<br>http://127.0.0.1:8000/

## CERDENTIALS

### TEST DATABASE Credentials
<br>Hostname:   eu-cdbr-west-02.cleardb.net
<br>Username:   b787ff70333075
<br>Password:   0d107855
<br>Database:   heroku_5b190513145da9f

### SUPERADMIN CREDENTIALS TO LOG IN
http://127.0.0.1:8000/admin
<br>Username:   a
<br>Password:   a

### COMPANY ADMIN CREDENTIALS TO LOG IN
http://127.0.0.1:8000/admin
<br>Username:   volvoadmin
<br>Password:   volvo2021

### FLEET MANAGER CREDENTIALS TO LOG IN
http://127.0.0.1:8000/admin
<br>Username:   fm1
<br>Password:   volvo2021

### CREATING CLASS DIAGRAM
```bash
python manage.py graph_models -a -o classUML.png
```



