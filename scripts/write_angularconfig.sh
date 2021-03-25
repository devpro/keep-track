# check command line arguments
if [ -z $1 ]; then echo "angular config file path is missing"; exit 1; fi
if [ -z $2 ]; then echo "firebase API key is missing"; exit 1; fi
if [ -z $3 ]; then echo "firebase project ID is missing"; exit 1; fi
if [ -z $4 ]; then echo "firebase messaging sender ID is missing"; exit 1; fi
if [ -z $5 ]; then echo "firebase application ID is missing"; exit 1; fi
if [ -z $6 ]; then echo "firebase measurement ID is missing"; exit 1; fi
if [ -z $7 ]; then echo "keeptrack API URL is missing"; exit 1; fi

# variables
outputfilepath=$1
firebaseapikey=$2
firebaseprojectid=$3
firebasemessagingsenderid=$4
firebaseappid=$5
firebasemeasurementid=$6
keeptrackapiurl=$7

# create the file
cat > $outputfilepath <<EOL
export const environment = {
  production: true,
  keepTrackApiUrl: '${keeptrackapiurl}',
  firebase: {
    apiKey: '${firebaseapikey}',
    authDomain: '${firebaseprojectid}.firebaseapp.com',
    databaseURL: 'https://${firebaseprojectid}.firebaseio.com',
    projectId: '${firebaseprojectid}',
    storageBucket: '${firebaseprojectid}.appspot.com',
    messagingSenderId: '${firebasemessagingsenderid}',
    appId: '${firebaseappid}',
    measurementId: '${firebasemeasurementid}'
  }
};
EOL
