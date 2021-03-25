#!/bin/bash

# check command line arguments
if [ -z $1 ]; then echo "wwwroot folder path is missing"; exit 1; fi
if [ -z $2 ]; then echo "firebase API key is missing"; exit 1; fi
if [ -z $3 ]; then echo "firebase project ID is missing"; exit 1; fi
if [ -z $4 ]; then echo "firebase messaging sender ID is missing"; exit 1; fi
if [ -z $5 ]; then echo "firebase application ID is missing"; exit 1; fi
if [ -z $6 ]; then echo "firebase measurement ID is missing"; exit 1; fi
if [ -z $7 ]; then echo "keeptrack API URL is missing"; exit 1; fi

# variables
wwwrootfolderpath=$1
firebaseapikey=$2
firebaseprojectid=$3
firebasemessagingsenderid=$4
firebaseappid=$5
firebasemeasurementid=$6
keeptrackapiurl=$7

# create the file
rm -f $wwwrootfolderpath/appsettings.json
cat > $wwwrootfolderpath/appsettings.json <<EOL
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Keeptrack": {
    "Api": {
      "Url": "${keeptrackapiurl}"
    }
  }
}
EOL
cat > $wwwrootfolderpath/js/src/firebase.config.js <<EOL
export var firebaseConfig = {
  apiKey: '${firebaseapikey}',
  authDomain: '${firebaseprojectid}.firebaseapp.com',
  databaseURL: 'https://${firebaseprojectid}.firebaseio.com',
  projectId: '${firebaseprojectid}',
  storageBucket: '${firebaseprojectid}.appspot.com',
  messagingSenderId: '${firebasemessagingsenderid}',
  appId: '${firebaseappid}',
  measurementId: '${firebasemeasurementid}'
};
EOL
