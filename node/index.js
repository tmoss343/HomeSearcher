var fs = require('fs');
var readline = require('readline');
var googleApis = require('googleapis');
var google = new googleApis.GoogleApis();
var googleAuth = require('google-auth-library');
var json2csv = require('json2csv');
var Client = require('node-rest-client').Client;
var date = new Date();
var client = new Client();

var SCOPES = ['https://www.googleapis.com/auth/drive'];
var TOKEN_DIR = '/.credentials/';
var TOKEN_PATH = TOKEN_DIR + 'drive-nodejs-quickstart.json';

googleApis.auth.getApplicationDefault(function(err, authClient) {
    if (err) {
      return err;
    }});
// Load client secrets from a local file.
fs.readFile('client_secret.json', function processClientSecrets(err, content) {
  if (err) {
    console.log('Error loading client secret file: ' + err);
    return;
  }
  //console.log(content.toString('ascii'));
  // Authorize a client with the loaded credentials, then call the
  // Drive API.
  authorize(JSON.parse(content), createZillowDataSpreadSheet);
});
fs.readFile('client_secret.json', function processClientSecrets(err, content) {
  if (err) {
    console.log('Error loading client secret file: ' + err);
    return;
  }
  //console.log(content.toString('ascii'));
  // Authorize a client with the loaded credentials, then call the
  // Drive API.
  authorize(JSON.parse(content), createLandBankDataSpreadSheet);
});

/**
 * creates spreadsheet
 *
 * @param {google.auth.OAuth2} auth
 */
function createLandBankDataSpreadSheet(auth) {

  var drive = google.drive('v3');
  var fields = ['acquisition_date', 'address', 'city', 'city_council_district', 'inventory_type', 'location_1_address', 'location_1_city', 'location_1_state', 'postal_code', 'market_value', 'market_value_year', 'neighborhood', 'off_stree_parking', 'property_class', 'property_condition', 'propert_status', 'school_district', 'square_footage', 'zoned_as'];
  // direct way
  client.get("http://localhost:5000/houses/landbankhouses", function (json, response) {
      // parsed response body as js object
      // console.log(data);
      // raw response
      // console.log(response);
      var result = json2csv({ data: json, fields: fields });
      fs.writeFile("data.csv", result, function(err) {
        if(err) {
            return console.log(err);
        }
        console.log("The data was saved!");
      });
      
      var media = {
        mimeType: 'text/csv',
        body: fs.createReadStream('data.csv')
      };
      
      var fileMetadata = {
        'name': 'LandBankDataDump' + date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear(),
        'mimeType': 'application/vnd.google-apps.spreadsheet'
      };
      drive.files.create({
        auth: auth,
        resource: fileMetadata,
        media: media,
        fields: 'id'
      }, function(err, file) {
        if(err) {
          // Handle error
          console.log(err);
        } else {
          console.log('File Id:' , file.id);
        }
      });
  });
}

/**
 * creates spreadsheet
 *
 * @param {google.auth.OAuth2} auth
 */
function createZillowDataSpreadSheet(auth) {

  var drive = google.drive('v3');
  var fields = ['id', 'lastsoldprice', 'lastsolddate', 'finishedSqFt', 'bathrooms', 'bedrooms', 'zestimate', 'lastUpdated', 'address', 'url'];
  // direct way
  client.get("http://localhost:5000/houses/propertysearchresults", function (json, response) {
      // parsed response body as js object
      // console.log(data);
      // raw response
      // console.log(response);
      var result = json2csv({ data: json, fields: fields });
      fs.writeFile("data.csv", result, function(err) {
        if(err) {
            return console.log(err);
        }
        console.log("The data was saved!");
      });
      
      var media = {
        mimeType: 'text/csv',
        body: fs.createReadStream('data.csv')
      };
      
      var fileMetadata = {
        'name': 'HouseDataDump' + date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear(),
        'mimeType': 'application/vnd.google-apps.spreadsheet'
      };
      drive.files.create({
        auth: auth,
        resource: fileMetadata,
        media: media,
        fields: 'id'
      }, function(err, file) {
        if(err) {
          // Handle error
          console.log(err);
        } else {
          console.log('File Id:' , file.id);
        }
      });
  });
}

/**
 * Create an OAuth2 client with the given credentials, and then execute the
 * given callback function.
 *
 * @param {Object} credentials The authorization client credentials.
 * @param {function} callback The callback to call with the authorized client.
 */
function authorize(credentials, callback) {
  var clientSecret = credentials.installed.client_secret;
  var clientId = credentials.installed.client_id;
  var redirectUrl = credentials.installed.redirect_uris[0];
  var auth = new googleAuth();
  var oauth2Client = new auth.OAuth2(clientId, clientSecret, redirectUrl);
  
  console.log(TOKEN_PATH);
  // Check if we have previously stored a token.
  fs.readFile(TOKEN_PATH, function(err, token) {
    if (err) {
      getNewToken(oauth2Client, callback);
    } else {
      token.toString('ascii');
      oauth2Client.credentials = JSON.parse(token);
      callback(oauth2Client);
    }
  });
}

/**
 * Get and store new token after prompting for user authorization, and then
 * execute the given callback with the authorized OAuth2 client.
 *
 * @param {google.auth.OAuth2} oauth2Client The OAuth2 client to get token for.
 * @param {getEventsCallback} callback The callback to call with the authorized
 *     client.
 */
function getNewToken(oauth2Client, callback) {
  var authUrl = oauth2Client.generateAuthUrl({
    access_type: 'offline',
    scope: SCOPES
  });
  console.log('Authorize this app by visiting this url: ', authUrl);
  var rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
  });
  rl.question('Enter the code from that page here: ', function(code) {
    rl.close();
    oauth2Client.getToken(code, function(err, token) {
      if (err) {
        console.log('Error while trying to retrieve access token', err);
        return;
      }
      oauth2Client.credentials = token;
      storeToken(token);
      callback(oauth2Client);
    });
  });
}

/**
 * Store token to disk be used in later program executions.
 *
 * @param {Object} token The token to store to disk.
 */
function storeToken(token) {
  try {
    fs.mkdirSync(TOKEN_DIR);
  } catch (err) {
    if (err.code != 'EEXIST') {
      throw err;
    }
  }
  fs.writeFile(TOKEN_PATH, JSON.stringify(token));
  console.log('Token stored to ' + TOKEN_PATH);
}

/**
 * Lists the names and IDs of up to 10 files.
 *
 * @param {google.auth.OAuth2} auth An authorized OAuth2 client.
 */
function listFiles(auth) {
  var google = new googleApis.GoogleApis();
  var service = google.drive('v3');
  service.files.list({
    auth: auth,
    pageSize: 10,
    fields: "nextPageToken, files(id, name)"
  }, function(err, response) {
    if (err) {
      console.log('The API returned an error: ' + err);
      return;
    }
    var files = response.files;
    if (files.length == 0) {
      console.log('No files found.');
    } else {
      console.log('Files:');
      for (var i = 0; i < files.length; i++) {
        var file = files[i];
        console.log('%s (%s)', file.name, file.id);
      }
    }
  });
}