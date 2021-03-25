# check command line arguments
if [ -z $1 ]; then echo "runsettings file path is missing"; exit 1; fi
if [ -z $2 ]; then echo "authentication JWT bearer token validator issuer is missing"; exit 1; fi
if [ -z $3 ]; then echo "authentication JWT bearer token validator authority is missing"; exit 1; fi
if [ -z $4 ]; then echo "authentication JWT bearer token validator audience is missing"; exit 1; fi
if [ -z $5 ]; then echo "firebase application key is missing"; exit 1; fi
if [ -z $6 ]; then echo "firebase authentication username is missing"; exit 1; fi
if [ -z $7 ]; then echo "firebase authentication password is missing"; exit 1; fi
if [ -z $8 ]; then echo "keeptrack API URL is missing"; exit 1; fi

# variables
outputfilepath=$1
tokenvalidationissuer=$2
tokenvalidationauthority=$3
tokenvalidationaudience=$4
firebaseapplicationkey=$5
firebaseauthenticationusername=$6
firebaseauthenticationpassword=$7
keeptrackapiurl=$8

# create the file
cat > $outputfilepath <<EOL
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <RunConfiguration>
    <EnvironmentVariables>
      <Authentication__JwtBearer__TokenValidation__Issuer>${tokenvalidationissuer}</Authentication__JwtBearer__TokenValidation__Issuer>
      <Authentication__JwtBearer__Authority>${tokenvalidationauthority}</Authentication__JwtBearer__Authority>
      <Authentication__JwtBearer__TokenValidation__Audience>${tokenvalidationaudience}</Authentication__JwtBearer__TokenValidation__Audience>
      <Firebase__Application__Key>${firebaseapplicationkey}</Firebase__Application__Key>
      <Firebase__Username>${firebaseauthenticationusername}</Firebase__Username>
      <Firebase__Password>${firebaseauthenticationpassword}</Firebase__Password>
      <Keeptrack__Production__Url>${keeptrackapiurl}</Keeptrack__Production__Url>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
EOL
